using UnityEngine;

public class EnemyEnergyAnim : MonoBehaviour
{
    private const float AdditionallyPosition = 1.5f;

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
        Instantiate(_templateEnergy, new Vector3(transform.position.x, transform.position.y + AdditionallyPosition, transform.position.z), Quaternion.identity);
    }
}
