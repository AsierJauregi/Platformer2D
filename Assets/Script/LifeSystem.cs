using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{

    [SerializeField] private float lives;

    public void ReceiveDamage(float damageReceived)
    {
        lives -= damageReceived;
        if(lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
