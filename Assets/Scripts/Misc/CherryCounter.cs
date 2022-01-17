using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCounter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.previousCherryCount = GameManager.instance.currentCherryCount;
            GameManager.instance.currentCherryCount++;

            Destroy(gameObject);
        }
    }

}
