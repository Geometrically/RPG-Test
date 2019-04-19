using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour
{
    public float health = 100f;

    public bool isInvulnerable;

    public bool facingRight = true;

    public virtual void DealDamage(float damageDealt)
    {
        health -= damageDealt;

        if (health <= 0 && !isInvulnerable)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool CanFire(Quaternion rotation)
    {
        if ((rotation.eulerAngles.z < 45 || rotation.eulerAngles.z > 315) && facingRight)
        {
            return true;
        }
        else if (rotation.eulerAngles.z > 135 && rotation.eulerAngles.z < 225 && !facingRight)
        {
            return true;
        }
        return false;
    }
}
