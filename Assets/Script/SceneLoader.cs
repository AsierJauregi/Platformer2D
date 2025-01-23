using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private float animationSpeed;
    [SerializeField] private float introTime = 0.85f;
    [SerializeField] private float outroTime = 0.85f;
    private bool introHasEnded = false;
    private bool outroStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(IntroAnimation());

        if(outroStarted) StartCoroutine(OutroAnimation());
    }

    private IEnumerator IntroAnimation()
    {
        if (panel != null && !introHasEnded)
        {
            panel.transform.Translate(Vector3.left * animationSpeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(introTime);
        introHasEnded = true;
    }
    private IEnumerator OutroAnimation()
    {
        if (panel != null && outroStarted)
        {
            panel.transform.Translate(Vector3.right * animationSpeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(outroTime);

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadScene()
    {
        outroStarted = true;
    }

}
