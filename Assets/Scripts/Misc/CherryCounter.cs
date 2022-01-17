using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCounter : MonoBehaviour
{

    bool isCollided = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollided)
        {
            isCollided = true;
            GameManager.instance.previousCherryCount = GameManager.instance.currentCherryCount;
            GameManager.instance.currentCherryCount++;
            GameManager.instance.cherryCollectedText.text = GameManager.instance.currentCherryCount + " / " + GameManager.instance.totalNumberOfCherries;
            GetComponent<Animator>().SetTrigger("cherryCollected");
            Destroy(gameObject, 0.5f);
        }
    }

}
