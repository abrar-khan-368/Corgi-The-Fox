using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Eagle"))
        {
            GetComponent<Animator>().SetTrigger("rockdestroyed");
            collision.gameObject.GetComponent<EnemyAI>().isStunned = true;
            Destroy(gameObject, 0.15f);
        }
    }
}
