using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, ICanAttack
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void StopAttacking()
    {
        
    }
}
