using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingEnemy : Enemy
{
    public Vector3 contractedPos;
    public Vector3 extendedPos;

    public float contractedTime = 2F;
    public float pushingTime = 1F;
    public float extendedTime = 2F;

    public float damage = 60f;

    void Start()
    {
        gameObject.transform.localPosition = contractedPos;
        StartCoroutine(WaitCooldownTime());
        isInvulnerable = true;
    }

    IEnumerator WaitCooldownTime()
    {
        yield return new WaitForSeconds(contractedTime);
        StartCoroutine(MoveOverSeconds(extendedPos));
        StartCoroutine(WaitExtendedTime());
    }

    IEnumerator WaitExtendedTime()
    {
        yield return new WaitForSeconds(extendedTime);
        StartCoroutine(MoveOverSeconds(contractedPos));
        StartCoroutine(WaitCooldownTime());
    }

    IEnumerator MoveOverSeconds(Vector3 end)
    {
        float elapsedTime = 0;
        var startingPos = gameObject.transform.localPosition;
        while (elapsedTime < pushingTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / pushingTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.localPosition = end;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var objectHit = collision.gameObject.GetComponent<DamagableObject>();
        if(objectHit != null)
        {
            objectHit.DealDamage(damage);
        }
    }
}

