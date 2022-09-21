using UnityEngine;

public class FenceAnim : MonoBehaviour
{
    private const string Hitted = "Hitted";

    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;

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
        _animator.SetTrigger(Hitted);
    }
}
