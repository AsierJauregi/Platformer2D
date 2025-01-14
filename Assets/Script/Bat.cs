using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy, ICanAttack
{

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
        anim.SetTrigger("stopAttacking");

    }

}
