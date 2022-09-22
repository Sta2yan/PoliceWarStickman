using System;
using UnityEngine;

public class AttackerAnim : MonoBehaviour
{
    private const string IsAttack = "isAttack";
    private const string IsRun = "isRun";
    //private const string IsComplete = "isComplete";

    [SerializeField] private Animator _animator;
    [SerializeField] private Attacker _attacker;

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
