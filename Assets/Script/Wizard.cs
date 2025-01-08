using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void LaunchFireball()
    {
        Instantiate(fireball, spawnPoint.position, transform.rotation);
    }
}
