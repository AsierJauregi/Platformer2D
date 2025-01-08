using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private string PlayerDetectorTag = "";
    [SerializeField] private string PlayerHitboxTag = "";
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float attackDamage;
    private Vector3 actualDestination;
    private int actualIndex;

    // Start is called before the first frame update
    void Start()
    {
        actualIndex = 0;
        actualDestination = waypoints[actualIndex].position;
        LookAtDestination();
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != actualDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, actualDestination, speedPatrol * Time.deltaTime);
                yield return null;
            }
            DefineNewDestination();
        }
        
        
    }
    private void DefineNewDestination()
    {
        actualIndex++;
        if (actualIndex >= waypoints.Length)
        {
            actualIndex = 0;
        }
        actualDestination = waypoints[actualIndex].position;
        LookAtDestination();
    }
    private void LookAtDestination()
    {
        if (actualDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerDetectorTag))
        {
            Debug.Log("Player Detected by " + gameObject.name);
        }
        else if (collision.gameObject.CompareTag(PlayerHitboxTag))
        {
            Debug.Log("Player hit by " + gameObject.name);
            collision.gameObject.GetComponent<LifeSystem>().ReceiveDamage(attackDamage);

        }
    }
}
