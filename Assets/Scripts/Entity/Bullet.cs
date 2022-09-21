using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const int Damage = 1000000;

    [SerializeField] private float _speed;

    private Transform _target;

    private void Update()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyStickman stickman))
            if (stickman.TryGetComponent(out Health health))
            {
                health.TakeDamage(Damage);
                Destroy(gameObject);
            }
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new NullReferenceException(nameof(target));

        _target = target;
    }

    private void OnValidate()
    {
        if (_speed < 0)
            throw new ArgumentOutOfRangeException(nameof(_speed));
    }
}
