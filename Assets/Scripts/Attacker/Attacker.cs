using UnityEngine;
using UnityEngine.Events;

public abstract class Attacker : MonoBehaviour, IDamagable
{
    [SerializeField] private float _timeToAttack;
    [SerializeField] private int _damage;

    private Health _target;

    public bool IsAttacking { get; protected set; }
    public IReadOnlyNull Target => _target;

    public event UnityAction AttackStarted;
    public event UnityAction Attacked;
    public event UnityAction AttackEnded;

    private void Update()
    {
        if (_target != null)
            transform.LookAt(_target.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsAttacking == false)
            FindTargetForAttack(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsAttacking == true)
            FindTargetForStopAttack(other);
    }

    public void Attack(Health health)
    {
        _target = health;
        IsAttacking = true;
        AttackStarted?.Invoke();

        StartAttack();
    }

    protected abstract void FindTargetForAttack(Collider other);

    protected abstract void FindTargetForStopAttack(Collider other);

    protected void LeaveZone()
    {
        IsAttacking = false;
        AttackEnded?.Invoke();
    }

    private void StartAttack()
    {
        if (CanContinueAttack() == false)
            return;

        _target.TakeDamage(_damage);
        Attacked?.Invoke();

        if (CanContinueAttack() == false)
            return;

        Invoke(nameof(StartAttack), _timeToAttack);
    }

    private bool CanContinueAttack()
    {
        if (_target.IsAlive == false || IsAttacking == false)
        {
            IsAttacking = false;
            AttackEnded?.Invoke();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnValidate()
    {
        if (_timeToAttack < 0)
            _timeToAttack = 0;
    }
}