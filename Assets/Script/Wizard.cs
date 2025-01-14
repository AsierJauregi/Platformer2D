using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy, ICanAttack
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private float attackDamage;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    //This method is called from the animator
    private void LaunchFireball()
    {
        Instantiate(fireball, spawnPoint.position, transform.rotation);
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
