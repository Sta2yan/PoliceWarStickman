using UnityEngine;

public class AttackParticlesActivator : MonoBehaviour
{
    private const float AdditivePosition = 1f;

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
        Instantiate(_particleSystem, new Vector3(transform.position.x , transform.position.y + AdditivePosition, transform.position.z), Quaternion.identity);
    }
}
