using UnityEngine;

public class PoliceShooterAnim : MonoBehaviour
{
    private const string Shoot = "Shoot";
    //private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Shooter _shooter;

    private void OnEnable()
    {
        _shooter.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _shooter.Attacked -= OnAttack;
    }

    private void OnAttack()
    {
        _animator.SetTrigger(Shoot);
    }
}
