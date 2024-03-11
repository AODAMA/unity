using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 1;
    
    public BoxCollider2D bc;

    public LayerMask obstaclesLayer;

    public LayerMask voidLayer;

    public float distanceDetection = 0.5f;

    public bool isFacingRight = true;

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * transform.right.normalized.x, rb.velocity.y);

        if(HasCollisionWithObstacle()) {
            Flip();
        }
        if(SeeVoid()) {
            Flip();
        }
    }

    private void Flip() {
            transform.Rotate(0, 180, 0);
    }

    public bool SeeVoid() {
        Vector2 centerPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
            bc.bounds.min.y
        );
        Collider2D hit = Physics2D.OverlapCircle(
            centerPosition,
            0.1f,
            obstaclesLayer
        );

        return hit.transform == null;
    }

    public bool HasCollisionWithObstacle()
    {
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0, 0),
            obstaclesLayer
        );
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (bc != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                bc.bounds.center,
                bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0, 0)
            );

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(
                new Vector2
                (
                    bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x/2)), 
                    bc.bounds.min.y
                ), 
                0.1f
            );
        }
    }
}
