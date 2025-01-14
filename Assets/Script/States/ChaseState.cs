using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    [SerializeField] private string playerDetectorTag = "";

    private Transform target;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float stoppingDistance;
    private float chasingDistance;

    [SerializeField] private float groundDetectionDistance = 0.15f;
    [SerializeField] private LayerMask whatIsJumpable;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log(gameObject.name + " entered chasing state");
    }

    public override void OnUpdateState()
    {
        if (target != null)
        {
            Move();
            LookAtTarget();

            if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
            {
                controller.ChangeState(controller.AttackState);
            }
            if (chasingDistance != 0)
            {
                if (Vector3.Distance(transform.position, target.position) > chasingDistance)
                {
                    Debug.Log(Vector3.Distance(transform.position, target.position).ToString() + " > " + chasingDistance.ToString());
                    controller.ChangeState(controller.PatrolState);
                }
            }

        }
    }

    private void Move()
    {
        if (this.TryGetComponent(out Bat bat))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            float directionX;
            if (target.position.x > transform.position.x) directionX = -1;
            else directionX = 1;

            if(Physics2D.Raycast(transform.position, Vector3.down, groundDetectionDistance, whatIsJumpable))
            {
                transform.Translate(Vector3.right * directionX * chaseSpeed * Time.deltaTime);
            }
            
        }
    }

    private void LookAtTarget()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerDetectorTag))
        {
            target = collision.transform;
            chasingDistance = Vector3.Distance(transform.position, target.position) + 1;
            
        }
    }
    public override void OnExitState()
    {

    }

    private void OnDrawGizmos()
    {
        Physics2D.Raycast(transform.position, Vector3.down, groundDetectionDistance, whatIsJumpable);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDetectionDistance);
    }
}
