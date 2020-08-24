using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageToBeDealt = 100;
    public int returnDamage()
    {
        return damageToBeDealt;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
