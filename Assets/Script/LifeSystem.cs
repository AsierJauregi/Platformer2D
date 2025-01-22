using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private string playerTag = "PlayerHitbox";
    [SerializeField] private float lives;
    [SerializeField] private float damageFeedbackTime = 0.2f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ReceiveDamage(float damageReceived)
    {
        lives -= damageReceived;
        StartCoroutine(AnimateHit());
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

    IEnumerator AnimateHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageFeedbackTime);
        spriteRenderer.color = Color.white;

    }
}
