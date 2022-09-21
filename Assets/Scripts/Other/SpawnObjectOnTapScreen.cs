using System;
using UnityEngine;

public class SpawnObjectOnTapScreen : MonoBehaviour
{
    [SerializeField] private EnergyViewCoins _coins;
    [SerializeField] private LevelComplete _levelComplete;

    private GameObject _template;
    private int _cost;

    private void Update()
    {
        if (_template != null && _levelComplete.IsComplete == false)
            Set();
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

                        if (gameObject.TryGetComponent(out Fence fence))
                            fence.SetRotationY(place.RotationY);

                        if (gameObject.TryGetComponent(out PoliceShooterAnim anim))
                            anim.Init(_levelComplete);
                        else if (gameObject.TryGetComponent(out AttackerAnim anim_2))
                            anim_2.Init(_levelComplete);
                    }
                }
            }
        }
    }
}
