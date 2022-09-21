using UnityEngine;

public class PlaceToSpawnObject : MonoBehaviour 
{
    [SerializeField] private float _rotationY;

    public float RotationY => _rotationY;
}