using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public Ui Ui;
    public CollectableUIScriptableObject[] ObjectsInInventory;
    public GameObject ObjectDragUi;

    private void Update()
    {
        MouseDown();
    }

    void MouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<Collectable>() != null)
            {
                GameObject obj = hit.collider.gameObject;

                for (int i = 0; i < ObjectsInInventory.Length; i++)
                {
                    //if (ObjectsInInventory[i] == obj.GetComponent<Collectable>().ScrpitableObjectRefObjects)
                    //{
                    //    Debug.Log($"{obj.name} is already in Inventory !");
                    //    obj = null;
                    //    break;
                    //}

                    if (ObjectsInInventory[i] == null)
                    {
                        ObjectsInInventory[i] = obj.GetComponent<Collectable>().ScrpitableObjectRefObjects;
                        obj.SetActive(false);

                        Debug.Log($"{obj.name} go in Inventory !");
                        Ui.AffObjectUi(ObjectsInInventory, Ui.Image);
                        break;
                    }
                }

                Debug.Log("Not enough place in Inventory !");
            }
        }
    }

    public void Drag(int index)
    {
        if (ObjectsInInventory[index] != null)
        {
            var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            if (!ObjectDragUi.activeInHierarchy)
            {
                ObjectDragUi.SetActive(true);
                ObjectDragUi.GetComponent<Image>().sprite = ObjectsInInventory[index].sprite;
                ObjectDragUi.GetComponent<Image>().color = ObjectsInInventory[index].color;
            }

            ObjectDragUi.transform.position = pos;
        }
    }

    public void Drop(int index)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<ObjectActionWithCollectable>() != null && ObjectsInInventory[index] != null)
        {
            hit.collider.GetComponent<ObjectActionWithCollectable>().Action(ObjectsInInventory[index], SceneManager.GetActiveScene().name);
        }

        ObjectDragUi.SetActive(false);
    }
}
