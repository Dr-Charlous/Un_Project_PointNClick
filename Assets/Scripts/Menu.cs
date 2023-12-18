using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float Time = 1;
    public string nameScene;
    public Animator anim;

    private bool play = false;
    private bool quit = false;

    private void Start()
    {
        anim.gameObject.SetActive(true);
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.tag == "Play" && play == false)
            {
                StartCoroutine(TransitionPlay(Time, nameScene));
                play = true;
            }

            if (hit.collider.gameObject.tag == "Quit" && quit == false)
            {
                StartCoroutine(TransitionQuit(Time));
                quit = true;
            }
        }
    }

    IEnumerator TransitionPlay(float time, string nameScene)
    {
        anim.SetTrigger("Closing");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nameScene);
    }

    IEnumerator TransitionQuit(float time)
    {
        anim.SetTrigger("Closing");
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
