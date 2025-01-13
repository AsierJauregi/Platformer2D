using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private string playerHitboxTag = "";
    [SerializeField] private float attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerHitboxTag))
        {
            Debug.Log("Player hit by " + gameObject.name);
            collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);

        }
    }
}
