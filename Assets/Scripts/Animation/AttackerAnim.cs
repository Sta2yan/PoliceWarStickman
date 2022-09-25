using System;
using UnityEngine;

public class AttackerAnim : MonoBehaviour
{
    private const string IsAttack = "isAttack";
    private const string IsRun = "isRun";
    private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

    private IOnlyActionStartEnded _levelEnded;
    private IOnlyActionLose _levelLose;

    public event Action LevelCompleted;

    private void OnEnable()
    {
        _attacker.Attacked += OnAttackStart;
        _attacker.AttackEnded += OnAttackEnd;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttackStart;
        _attacker.AttackEnded -= OnAttackEnd;

        if (_levelEnded != null)
            _levelEnded.StartEnded -= OnStartEnded;

        if (_levelLose != null)
            _levelLose.Lose -= OnLose;
    }

    public void Init(IOnlyActionStartEnded levelEnded)
    {
        if (levelEnded == null)
            return;

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
        LevelCompleted?.Invoke();
    }

    private void OnLose()
    {
        if (gameObject.TryGetComponent(out PoliceDestroyByLose destroy))
            destroy.Active();
        else
            OnStartEnded();
    }

    private void OnAttackStart()
    {
        _animator.SetTrigger(IsAttack);
        _animator.SetBool(IsRun, false);
    }

    private void OnAttackEnd()
    {
        _animator.SetTrigger(IsAttack);
        _animator.SetBool(IsRun, true);
    }
}
