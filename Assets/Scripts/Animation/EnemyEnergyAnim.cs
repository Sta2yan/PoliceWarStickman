using UnityEngine;

public class EnemyEnergyAnim : MonoBehaviour
{
    [SerializeField] private GameObject _templateEnergy;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _health.Die -= OnDie;
    }

    private void OnDie()
    {
        Instantiate(_templateEnergy, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
    }
}
