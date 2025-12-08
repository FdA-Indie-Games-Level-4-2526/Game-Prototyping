using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool _active;

    [Header("CoyoteTime")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    
    [Header("Wall Jumps")]
    [SerializeField] private float wallJumpX; 
    [SerializeField] private float wallJumpY;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Vector3 OriginalScale;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    [SerializeField] private float jumpforce;
    private Vector2 _respawnPoint;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        OriginalScale = transform.localScale;
        SetRespawnPoint((Vector2)transform.position);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // - Handles the player movement (jumping, falling, walking, etc)
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            body.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }

        // - Flip player when moveing Left-Right
        if (horizontalInput > 0.01f)
            transform.localScale = OriginalScale;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-OriginalScale.x, OriginalScale.y, OriginalScale.z);


        // - Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // - Adjustable Jump Height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if(isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime;

        }
    }
     

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0) return;

        if(onWall())
            WallJump();
        else
        {
            if(isGrounded())
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            else
            {
                if(jumpCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                    jumpCounter--;
                }
            }

            coyoteCounter = 0;
        }

        
    }

    public void SetRespawnPoint(Vector3 position)
    {
        _respawnPoint = (Vector2)position;
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private bool isGrounded()
    {
        // - Checks if Player is grounded
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool onWall()
    {
        // - Check if Player is on the wall

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public void Die()
    {
        // - This kills the Player

        _active = false;
        boxCollider.enabled = false;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        // - This respawns the player

        yield return new WaitForSeconds(1f);
        transform.position = (Vector3)_respawnPoint;
        _active = true;
        boxCollider.enabled = true;
    }

} 
