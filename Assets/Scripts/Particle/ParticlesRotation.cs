using UnityEngine;

public class ParticlesRotation : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);    
    }
}
