using UnityEngine;

public class PoliceDestroyByLose : MonoBehaviour
{
    public void Active()
    {
        if (gameObject.TryGetComponent(out Health health))
            health.TakeDamage(int.MaxValue);
    }
}
