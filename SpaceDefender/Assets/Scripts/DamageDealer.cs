using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] 
    private int _damage = 100;

    public int Damage { get => _damage; }

    public int Hit(int health)
    {
        Destroy(gameObject);
        var healthAfterHit = health - _damage;

        return healthAfterHit;
    }
}
