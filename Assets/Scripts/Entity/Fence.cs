using UnityEngine;

public class Fence : MonoBehaviour
{
    public void SetRotationY(float rotationY)
    {
        transform.Rotate(new Vector3(transform.rotation.x, rotationY, transform.rotation.z));
    }
}