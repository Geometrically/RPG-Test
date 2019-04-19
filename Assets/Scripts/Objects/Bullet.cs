using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 30f;

    public Vector2 direction;

    public GameObject impactEffect;
    public GameObject shooter;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != shooter && !collision.gameObject.GetComponent<Checkpoint>())
        {
            var objectHit = collision.GetComponent<DamagableObject>();
            if (objectHit != null)
            {
                objectHit.DealDamage(damage);
            }
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
