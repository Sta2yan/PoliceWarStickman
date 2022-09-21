using UnityEngine;

public class AttackParticlesActivator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _particleSystem;

    private void OnEnable()
    {
        _health.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _health.Hitted -= OnHitted;
    }

    private void OnHitted()
    {
        Instantiate(_particleSystem, new Vector3(transform.position.x , transform.position.y + 1, transform.position.z), Quaternion.identity);
    }
}
