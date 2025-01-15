using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA; // Primer punto del movimiento
    [SerializeField] private Transform pointB; // Segundo punto del movimiento
    [SerializeField] private float speed = 2f; // Velocidad del Tilemap

    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        positionA = pointA.position;
        positionB = pointB.position;
        nextPosition = positionB;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        { 
            if (nextPosition == positionB) 
            {
                nextPosition = positionA;    
            }
            else
            {
                nextPosition = positionB;
            }
        }
    }
}
