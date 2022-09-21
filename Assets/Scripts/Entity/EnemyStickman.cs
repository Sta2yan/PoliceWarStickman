using System;
using UnityEngine;

[RequireComponent(typeof(StickmanMover), typeof(EnemyAttacker))]
public class EnemyStickman : MonoBehaviour
{
    private StickmanMover _mover;
    private EnemyAttacker _attacker;

    private void Awake()
    {
        _mover = GetComponent<StickmanMover>();
        _attacker = GetComponent<EnemyAttacker>();
    }

    private void OnEnable()
    {
        _attacker.AttackStarted += _mover.Stop;
        _attacker.AttackEnded += _mover.Continue;
    }

    private void OnDisable()
    {
        _attacker.AttackStarted -= _mover.Stop;
        _attacker.AttackEnded -= _mover.Continue;
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new NullReferenceException(nameof(target));

        _mover.SetTarget(target);
    }
}
