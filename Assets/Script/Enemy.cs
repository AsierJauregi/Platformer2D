using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private string playerHitboxTag = "";
    [SerializeField] protected float attackDamage;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsHittable;
    protected Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerHitboxTag))
        {
            Debug.Log("Player hit by " + gameObject.name);
            collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);

        }
    }
    //Called through animation events 
    virtual public void LaunchAttack()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsHittable);
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                Debug.Log(gameObject.name + "'s attack hit");
                collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);
            }
        }
    }
}
