using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy, ICanAttack
{
    [SerializeField] private GameObject fireballPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        fireballPrefab.GetComponent<Fireball>().AttackDamage = attackDamage;
    }

    //This method is called from the animator
    public override void LaunchAttack()
    {
        Instantiate(fireballPrefab, attackPoint.position, transform.rotation);
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
        anim.SetBool("stopAttacking", false);
    }

    public void StopAttacking()
    {
        Debug.Log("Stopping attack animation");
        anim.SetBool("stopAttacking", true);
    }

    
}
