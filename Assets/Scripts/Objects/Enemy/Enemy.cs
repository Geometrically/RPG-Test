using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamagableObject
{
    public virtual void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 14)
            GetComponent<SpriteRenderer>().enabled = false;
        else
            GetComponent<SpriteRenderer>().enabled = true;
    }
}
