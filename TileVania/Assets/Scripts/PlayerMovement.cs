using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpSpeed = 100f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 0f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Animator myAnimator;
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityReset;
    bool isAlive = true;


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityReset = playerRigidBody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            playerRigidBody.gravityScale = gravityReset;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, moveInput.y * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        playerRigidBody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            playerRigidBody.velocity = deathKick;
            StartCoroutine(FindObjectOfType<GameSession>().ProcessPlayerDeath());
        }
    }
}
