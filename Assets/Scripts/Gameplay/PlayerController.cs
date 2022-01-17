using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hSpeed = 5f;
    public float singleJumpForce = 10f;

    public Rigidbody2D playerPhysics;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;

    private Camera camera;

    bool hasJump = false;

    private Vector2 screenSize;
    public float maxHorizontalMovement;

    public bool isDead = false;

    private void Start()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        CalculateScreenBoundings();

    }

    private void CalculateScreenBoundings()
    {
        camera = Camera.main;
        screenSize = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        maxHorizontalMovement = (screenSize.x / 2f);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            MovePlayerHorizontally();
            JumpPlayer();
            GravityScale();
        }
    }

    private void ClampPlayerPosition()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, -(maxHorizontalMovement + 2.5f), maxHorizontalMovement + 2.5f);
        transform.position = position;
    }

    private void MovePlayerHorizontally()
    {
        float hMoveValue = Input.GetAxisRaw("Horizontal");
        if (hMoveValue > 0f)
        {
            FlipSpriteImage(false);
            transform.Translate(Vector2.right * hSpeed * Time.fixedDeltaTime);
            playerAnimator.SetBool("isRunning", true);
        }
        else if (hMoveValue < 0f)
        {
            FlipSpriteImage(true);
            transform.Translate(Vector2.left * hSpeed * Time.fixedDeltaTime);
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
        ClampPlayerPosition();
    }

    private void JumpPlayer()
    {
        if (Input.GetMouseButtonDown(0) && !hasJump)
        {
            AddJumpForce(singleJumpForce);
            hasJump = true;
        }
    }

    private void AddJumpForce(float force)
    {
        playerPhysics.AddForce(Vector2.up * force * Time.fixedDeltaTime);
        playerAnimator.SetBool("hasJump", true);

    }

    private void FlipSpriteImage(bool faceBackward)
    {
        playerSprite.flipX = (faceBackward) ? true : false;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Hit with ground");
            hasJump = false;
            playerAnimator.SetBool("hasJump", false);
        }
    }

    public void Die()
    {
        isDead = true;
        playerAnimator.SetTrigger("dead");
        StartCoroutine(FallPlayerBeneathGround());
    }

    private IEnumerator FallPlayerBeneathGround()
    {
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<PolygonCollider2D>().enabled = false;
        playerSprite.sortingLayerName = "Foreground";
        playerSprite.sortingOrder = 10;
    }

    private void GravityScale()
    {
        transform.Translate(Vector2.down * Time.fixedDeltaTime);
    }

}
