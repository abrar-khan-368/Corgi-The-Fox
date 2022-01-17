using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            GetComponent<Animator>().SetTrigger("rockdestroyed");
            Destroy(gameObject, 0.5f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("rockdestroyed");
            collision.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject, 0.5f);
        }
    }
}
