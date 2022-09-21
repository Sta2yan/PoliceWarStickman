using UnityEngine;

public class PoliceAttacker : Attacker
{
    protected override void FindTargetForAttack(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStickman stickman))
            if (stickman.TryGetComponent(out Health healthEnemyStickman))
                Attack(healthEnemyStickman);
    }

    protected override void FindTargetForStopAttack(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStickman stickman))
            if (stickman.TryGetComponent(out Health health))
                if (health == Target)
                    LeaveZone();
    }
}
