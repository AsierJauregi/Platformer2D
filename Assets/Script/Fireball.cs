using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float shotImpulse;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float attackDamage;
    private float timer = 0;
    private bool isExploding = false;
    private bool isParried;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public float AttackDamage { set => attackDamage = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shotImpulse, ForceMode2D.Impulse);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToDestroy && !isExploding)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player) && !isParried)
        {
            Explode();
            collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy) && isParried)
        {
            Explode();
            collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);
        }
    }

    private void Explode()
    {
        isExploding = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.SetTrigger("explode");
    }
    private void EndExplosion()
    {
        Destroy(this.gameObject);
    }

    public void GetParried()
    {
        if (!isExploding)
        {
            isParried = true;
            spriteRenderer.color = Color.green;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector3(1, 0.60f, 0) * shotImpulse, ForceMode2D.Impulse);
        }
    }
}
