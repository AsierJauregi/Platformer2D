using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Enemy, ICanAttack
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
