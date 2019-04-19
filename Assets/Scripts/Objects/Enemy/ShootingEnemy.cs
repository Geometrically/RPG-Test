using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    private Animator animator;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public int shootingChance = 50;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Random.Range(1, shootingChance) == 1)
        {
            Vector2 target = GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector2 direction = (target - (Vector2)firePoint.position);
            direction.Normalize();

            var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            if (CanFire(rotation) && GetComponent<SpriteRenderer>().enabled)
            {
                animator.SetTrigger("Attack");

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                bullet.GetComponent<Bullet>().direction = direction;
                bullet.GetComponent<Bullet>().shooter = gameObject;
            }
        }
    }

    public override void DealDamage(float damage)
    {
        base.DealDamage(damage);
        animator.SetTrigger("Hurt");
    }


    public override void Die()
    {
        animator.SetTrigger("Die");
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
