using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StickmanMover), typeof(EnemyAttacker), typeof(Health))]
public class EnemyStickman : MonoBehaviour, IEnergyCollectable
{
    [SerializeField] private int _energyCostBonus;

    private StickmanMover _mover;
    private EnemyAttacker _attacker;
    private Health _health;

    public int EnergyCostBonus => _energyCostBonus;

    public event UnityAction<IEnergyCollectable> EnergyCollected;

    private void Awake()
    {
        _mover = GetComponent<StickmanMover>();
        _attacker = GetComponent<EnemyAttacker>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _attacker.AttackStarted += _mover.Stop;
        _attacker.AttackEnded += _mover.Continue;
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _attacker.AttackStarted -= _mover.Stop;
        _attacker.AttackEnded -= _mover.Continue;
        _health.Die -= OnDie;
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new NullReferenceException(nameof(target));

        _mover.SetTarget(target);
    }

    private void OnDie()
    {
        EnergyCollected?.Invoke(this);
    }
}

public interface IEnergyCollectable
{
    public event UnityAction<IEnergyCollectable> EnergyCollected;
    public int EnergyCostBonus { get; }
}