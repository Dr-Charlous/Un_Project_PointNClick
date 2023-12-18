using System;
using System.Collections;
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
    [Header("")]
    public float Xpos = 1, Ypos = -1;
    private float XMpos = 1, YMpos = -1;
    public bool IsActive = false;
    [Header("")]
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

        if (Xpos != -XMpos)
            XMpos = -Xpos;
        if (Ypos != -YMpos)
            YMpos = -Ypos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(IconeClicker.transform.position, new Vector2(Xpos * 2, Ypos * 2));
    }

    public IEnumerator RythmMiniGame()
    {
        Vector2 InitPos = IconeClicker.gameObject.transform.position;
        IsActive = true;
        for (int i = 0; i < ListIcones.Icones.Length; i++)
        {
            IconeClicker.color = ListIcones.colorInit;
            IconeClicker.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(XMpos, Xpos) + InitPos.x, UnityEngine.Random.Range(YMpos, Ypos) + InitPos.y);
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
                {
                    IconeClicker.gameObject.transform.position = InitPos;
                    yield return new WaitForSeconds(ListIcones.Icones[i].TimerBetweenIcones);
                }
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
        IconeClicker.gameObject.transform.position = InitPos;
    }
}
