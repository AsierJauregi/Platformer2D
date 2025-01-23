using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, ICanInteract
{
    [SerializeField] private GameObject canvasObj;
    private Canvas canvas;
    private bool hovering;
    [SerializeField] private GameObject eventSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = canvasObj.GetComponent<Canvas>();
        canvas.enabled = false;
    }
    public void OnHoverEnter()
    {
        Debug.Log("On hover entered " + gameObject.name);
        canvas.enabled = true;
        hovering = true;
    }
    public void OnHoverExit()
    {
        Debug.Log("On hover exited " + gameObject.name);
        canvas.enabled = false;
        hovering = false;
    }

    public void Interact()
    {
        if (hovering)
        {
            Debug.Log("Interacted with " + gameObject.name);
            eventSystem.GetComponent<SceneLoader>().LoadScene();
        }
    }

    
}
