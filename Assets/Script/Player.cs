using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private float inputH;
    [Header("Movement")]
    [SerializeField] private float groundDetectionDistance = 0.15f;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatIsJumpable;
    [SerializeField] private Transform feet;
    [SerializeField] private string movablePlatformTag = "Movable Platform";
    private bool isOnGround;

    [Header("Combat system")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private string fireballTag;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        LaunchAttack();
    }

    private void LaunchAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }
    //Executed from animation event
    private void Attack()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D collision in collisions) 
        {
            if (!collision.gameObject.CompareTag(fireballTag))
            {
                collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);
            }
            else
            {
                //Devolver el fireball
                collision.gameObject.GetComponent<Fireball>().GetParried();
            }
        }
    }

    private bool Grounded()
    {
        return Physics2D.Raycast(feet.position, Vector3.down, groundDetectionDistance, whatIsJumpable);

    }
    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && Grounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private void Movement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * horizontalSpeed, rb.velocity.y);
        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if(inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(movablePlatformTag))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(movablePlatformTag))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
