using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    float moveInput;
    bool jumpRequested;
    bool isGrounded;
    int score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null )
        {
            Debug.LogError("Missing animator");
        }
    }

    void Update()
    {
        CheckInput();
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < -5)
        {
            GameOver();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        TryJump();
    }

    private void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput !=  0)
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Iddle");
        } 
        else
        {
            animator.SetTrigger("Iddle");
            animator.ResetTrigger("Walk");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sprite direction
        if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x < 0)
        {
            sr.flipX=false;
        }
    }

    private void TryJump()
    {
        if (jumpRequested && isGrounded)
        {
            animator.SetTrigger("Iddle");
            animator.ResetTrigger("Walk");
            isGrounded = false;
            jumpRequested = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        //update scrore UI
    }

    // TODO: This should not be part of the player, instead better be part of a GameManager
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
        {
            isGrounded = true;
            jumpRequested = false;
        }
    }
}
