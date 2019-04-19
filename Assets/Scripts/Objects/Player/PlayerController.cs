using UnityEngine;

public class PlayerController : DamagableObject
{
    private Animator animator;
    private Rigidbody2D rb;

    public int speed = 10;
    public int jumpPower = 50;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float horizontalMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
        PlayAnimations();
        HandleFiring();
    }

    private void MovePlayer()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpPower);
        }

        if (horizontalMovement < 0.0f && facingRight)
        {
            facingRight = false;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (horizontalMovement > 0.0f && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMovement * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void PlayAnimations()
    {
        animator.SetBool("isWalking", horizontalMovement != 0 && rb.velocity.y == 0 ? true : false);
        animator.SetBool("isJumping", rb.velocity.y == 0 ? false : true);
    }

    private void HandleFiring()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        Vector2 direction = (target - (Vector2)firePoint.position);
        direction.Normalize();

        var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if (Input.GetButtonUp("Fire1") & CanFire(rotation))
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().shooter = gameObject;
        }

        animator.SetBool("isShooting", Input.GetButtonUp("Fire1") && CanFire(rotation));
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
        if (GetComponent<PlayerState>().respawnCount > 0)
        {
            GetComponent<PlayerState>().Respawn();
        }
    }
}
