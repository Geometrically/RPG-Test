using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyingEnemy : ShootingEnemy
{
    public float speed;

    public Vector3 guardPos1;
    public Vector3 guardPos2;

    private float lastInterval = 0;

    public override void Update()
    {
        base.Update();

        double currentInterval = (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f;
        transform.position = Vector3.Lerp(guardPos1, guardPos2, (float)currentInterval);

        if (((currentInterval - lastInterval) > 0) && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (((currentInterval - lastInterval) < 0) && facingRight)
        {
            facingRight = false;
            transform.Rotate(0f, 180f, 0f);
        }

        lastInterval = (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f;
    }
}

