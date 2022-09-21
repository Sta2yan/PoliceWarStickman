using UnityEngine;

public class EnemyAttacker : Attacker
{
    protected override void FindTargetForAttack(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Fence fence))
        {
            if (fence.TryGetComponent(out Health healthFence))
                Attack(healthFence);
        }
        else if (other.gameObject.TryGetComponent(out PoliceStickman stickman))
        {
            if (stickman.TryGetComponent(out Health healthPoliceStickman))
                Attack(healthPoliceStickman);
        }
        else if (other.gameObject.TryGetComponent(out Shooter shooter))
        {
            if (shooter.TryGetComponent(out Health shooterHealth))
                Attack(shooterHealth);
        }
    }

    protected override void FindTargetForStopAttack(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PoliceStickman stickman))
            if (stickman.TryGetComponent(out Health health))
                if (health == Target)
                    LeaveZone();
    }
}
