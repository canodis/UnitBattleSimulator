using System;
using System.Collections;
using UnityEngine;

public class HealthSystem
{
    private float _health;
    private float _maxHealth;
    private Transform _healthBarTransform;

    public HealthSystem(float health, Transform healthBarTransform)
    {
        _health = health;
        _maxHealth = health;
        _healthBarTransform = healthBarTransform;
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        if (_health < 0)
        {
            _health = 0;
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float targetScale = GetHealthNormalized();

        _healthBarTransform.localScale = new Vector3(targetScale, 1f, 1f);
    }

    public float GetHealth()
    {
        return _health;
    }

    private float GetHealthNormalized()
    {
        return _health / _maxHealth;
    }
}