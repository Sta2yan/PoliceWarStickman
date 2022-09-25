using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnTapScreen : MonoBehaviour
{
    [SerializeField] private EnergyViewCoins _coins;
    [SerializeField] private LevelController _controller;
    [SerializeField] private float _delayClearList;

    private GameObject _template;
    private int _cost;
    private List<GameObject> _spawnedGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        _controller.Ended += OnEnded;
    }

    private void Update()
    {
        if (_template != null)
            Set();
    }

    private void OnDisable()
    {
        _controller.Ended -= OnEnded;
    }

    public void SetTemplateToSpawn(GameObject template, int cost)
    {
        if (template == null)
            throw new NullReferenceException(nameof(template));

        _template = template;
        _cost = cost;
    }

    private void Set()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Infinity));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlaceToSpawnObject place))
                {
                    if (_coins.CanBuy(_cost))
                    {
                        GameObject gameObject = Instantiate(_template, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                        _spawnedGameObjects.Add(gameObject);

                        if (gameObject.TryGetComponent(out Fence fence))
                            fence.SetRotationY(place.RotationY);

                        if (gameObject.TryGetComponent(out PoliceShooterAnim policeStickmanAnim))
                        {
                            policeStickmanAnim.Init(_controller as IOnlyActionStartEnded);
                            policeStickmanAnim.Init(_controller as IOnlyActionLose);
                        }

                        if (gameObject.TryGetComponent(out AttackerAnim attackerAnim))
                        {
                            attackerAnim.Init(_controller as IOnlyActionStartEnded);
                            attackerAnim.Init(_controller as IOnlyActionLose);
                        }
                    }
                }
            }
        }
    }

    private void OnEnded()
    {
        Invoke(nameof(ClearList), _delayClearList);
    }

    private void ClearList()
    {
        for (int i = 0; i < _spawnedGameObjects.Count; i++)
            if (_spawnedGameObjects[i] != null)
                Destroy(_spawnedGameObjects[i]);
    }
}
