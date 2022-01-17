using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed = 5f;
    public Camera camera;
    public SpriteRenderer enemySprite;

    public GameObject stone;
    public float timeRateToSpawnRock = 0.05f;
    private float lastTimeWhenRockSpawned;
    public Transform stoneSpawningPoint;

    private Vector2 screenSize;
    public float maxHorizontalMovement;

    private void Start()
    {
        CalculateScreenBoundings();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    private void CalculateScreenBoundings()
    {
        camera = Camera.main;
        screenSize = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        maxHorizontalMovement = (screenSize.x / 2f);
    }

    private void Update()
    {
        AttackPlayer();
    }

    private void FixedUpdate()
    {
        MoveEnemyToAndFro();
        FlipSpriteAtThEnd();
    }

    private void MoveEnemyToAndFro()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(
            Mathf.PingPong(Time.time * 3f, screenSize.x) - maxHorizontalMovement,
            transform.position.y), enemySpeed * Time.fixedDeltaTime);
       
    }

    public void FlipSpriteAtThEnd()
    {
        Debug.Log("X pos of enemy : " + Mathf.Floor(maxHorizontalMovement));
        if (Mathf.Approximately(Mathf.Round(transform.position.x), Mathf.Round(maxHorizontalMovement)))
            enemySprite.flipX = false;
        else if (Mathf.Approximately(Mathf.Round(transform.position.x), -Mathf.Round(maxHorizontalMovement)))
            enemySprite.flipX = true;
        
    }

    private void AttackPlayer()
    {
        if (Time.time > timeRateToSpawnRock + lastTimeWhenRockSpawned)
        {
            Instantiate(stone, stoneSpawningPoint.position, Quaternion.identity);
            lastTimeWhenRockSpawned = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }

}
