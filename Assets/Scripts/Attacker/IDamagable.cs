using UnityEngine.Events;

public interface IDamagable
{
    public event UnityAction AttackStarted;
    public event UnityAction AttackEnded;

    public void Attack(Health health);
}
