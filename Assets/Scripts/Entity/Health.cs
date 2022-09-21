using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IReadOnlyNull
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public bool IsAlive => _currentHealth > 0;

    public event UnityAction Die;
    public event UnityAction Hitted;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;
        Hitted?.Invoke();

        if (IsAlive == false)
            Dead();
    }

    private void Dead()
    {
        Die?.Invoke();
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (_maxHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(_maxHealth));
    }
}
