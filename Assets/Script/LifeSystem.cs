using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private string playerTag = "PlayerHitbox";
    [SerializeField] private float lives;

    public void ReceiveDamage(float damageReceived)
    {
        lives -= damageReceived;
        if(lives <= 0)
        {
            if (this.gameObject.CompareTag(playerTag))
            {
                this.gameObject.GetComponent<Player>().Die();
            }
            else 
            {
                Destroy(this.gameObject);
            }
        }
    }
}
