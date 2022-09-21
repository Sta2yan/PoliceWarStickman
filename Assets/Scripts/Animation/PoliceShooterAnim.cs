using System;
using UnityEngine;

public class PoliceShooterAnim : MonoBehaviour
{
    private const string Shoot = "Shoot";
    private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Shooter _shooter;

    private LevelComplete _levelComplete;

    private void OnEnable()
    {
        _shooter.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _shooter.Attacked -= OnAttack;

        if (_levelComplete != null)
            _levelComplete.Completed -= OnComplete;
    }

    public void Init(LevelComplete levelComplete)
    {
        if (levelComplete == null)
            throw new NullReferenceException(nameof(levelComplete));

        _levelComplete = levelComplete;
        _levelComplete.Completed += OnComplete;
    }

    private void OnAttack()
    {
        _animator.SetTrigger(Shoot);
    }

    private void OnComplete(float delay)
    {
        _animator.SetTrigger(IsComplete);
    }
}
