using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMouvements : MonoBehaviour
{

    public Rigidbody2D rb;

    //Store Player's direction
    public float moveDirectionX = 0;

    public float moveSpeed = 5;

    public float jumpForce = 7;

    public int nbMaxJumpsAllowed = 2;

    public int CurrentNumberOfJumps = 0;
    
    public Transform groundCheck;

    public float groundCheckRadius = 0.2f;

    public bool isOnGround;

    public LayerMask listGroundLayers;

    public bool isFacingRight = true;

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        if(
            Input.GetButtonDown("Jump") && CurrentNumberOfJumps < nbMaxJumpsAllowed
        ) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            CurrentNumberOfJumps += 1;
        }

        if(!Input.GetButton("Jump") && isOnGround) {
            CurrentNumberOfJumps = 0;
        }
        Flip();
    }

    public void FixedUpdate() {
        rb.velocity = new Vector2(moveDirectionX * moveSpeed, rb.velocity.y);
        isOnGround = IsGrounded();
    }

    public bool IsGrounded() {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            listGroundLayers
        );
    }

    public void Flip() {
        if(
            (moveDirectionX > 0 && !isFacingRight) || (moveDirectionX < 0 && isFacingRight)
        ) {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
