using UnityEngine;

public class DeadParticle : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private ParticleSystem _particle;

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
        Instantiate(_particle, transform.position, Quaternion.identity);
    }
}
