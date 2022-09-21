using System;
using UnityEngine;

public class AttackerAnim : MonoBehaviour
{
    private const string IsAttack = "isAttack";
    private const string IsRun = "isRun";
    private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;
    
    private LevelComplete _levelComplete;

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

    private void OnComplete(float delay)
    {
        _animator.SetTrigger(IsComplete);
        LevelCompleted?.Invoke();
    }
}
