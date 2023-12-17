using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Icone", menuName = "ScriptableObjects/ScriptableIcone", order = 1)]
public class Icones : ScriptableObject
{
    public float TimerIcone;
    public float TimerBetweenIcones;
}

[CreateAssetMenu(fileName = "ListIcones", menuName = "ScriptableObjects/ScriptableListIcones", order = 1)]
public class ListIcones : ScriptableObject
{
    public Icones[] Icones;
    public Color colorInit;
    public Color colorConfirm;
}

public class RythmGpe : MonoBehaviour
{
    public int Score = 0;
    public bool IsActive = false;
    public ListIcones ListIcones;
    public Image IconeClicker;

    private bool isTouch = false;

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        if (IsActive && Input.GetMouseButtonDown(0) && isTouch == false)
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Image>() == IconeClicker)
            {
                isTouch = true;
                IconeClicker.color = ListIcones.colorConfirm;
                Score++;
            }
        }
        else if (IsActive == false && Score != 0)
        {
            Score = 0;
        }

        if (hit.collider != null && Input.GetMouseButtonDown(0) && IsActive == false)
        {
            if (hit.collider.gameObject.GetComponent<ObjectActionWithCollectable>())
            {
                StartCoroutine(RythmMiniGame());
            }
        }
    }

    public IEnumerator RythmMiniGame()
    {
        IsActive = true;
        for (int i = 0; i < ListIcones.Icones.Length; i++)
        {
            IconeClicker.color = ListIcones.colorInit;
            IconeClicker.gameObject.SetActive(true);
            yield return new WaitForSeconds(ListIcones.Icones[i].TimerIcone);
            IconeClicker.gameObject.SetActive(false);
            if (isTouch == false && IconeClicker.color == ListIcones.colorInit)
            {
                break;
            }
            else
            {
                isTouch = false;

                if (Score != ListIcones.Icones.Length - 1)
                    yield return new WaitForSeconds(ListIcones.Icones[i].TimerBetweenIcones);
            }
        }

        if (Score != ListIcones.Icones.Length)
        {
            Debug.Log("You lose :(");
        }
        else
        {
            Debug.Log("You win yeah :D");
        }

        IsActive = false;
    }
}
