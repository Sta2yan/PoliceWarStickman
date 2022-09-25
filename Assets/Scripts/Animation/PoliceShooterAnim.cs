using System;
using UnityEngine;

public class PoliceShooterAnim : MonoBehaviour
{
    private const string Shoot = "Shoot";
    private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Shooter _shooter;

    private IOnlyActionStartEnded _levelEnded;
    private IOnlyActionLose _levelLose;

    private void OnEnable()
    {
        _shooter.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _shooter.Attacked -= OnAttack;

        if (_levelEnded != null)
            _levelEnded.StartEnded -= OnStartEnded;

        if (_levelLose != null)
            _levelLose.Lose -= OnLose;
    }

    public void Init(IOnlyActionStartEnded levelEnded)
    {
        if (levelEnded == null)
            throw new NullReferenceException();

        _levelEnded = levelEnded;
        _levelEnded.StartEnded += OnStartEnded;
    }

    public void Init(IOnlyActionLose actionLose)
    {
        if (actionLose == null)
            return;

        _levelLose = actionLose;
        _levelLose.Lose += OnLose;
    }

    private void OnStartEnded()
    {
        _animator.SetTrigger(IsComplete);
    }

    private void OnLose()
    {
        if (gameObject.TryGetComponent(out PoliceDestroyByLose destroy))
            destroy.Active();
        else
            OnStartEnded();
    }

    private void OnAttack()
    {
        _animator.SetTrigger(Shoot);
    }
}
