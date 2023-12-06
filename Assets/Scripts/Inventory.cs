using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Ui Ui;
    public GameObject[] ObjectsInInventory;

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

            if (hit.collider != null)
            {
                int EmptyPlace = -1;
                GameObject obj = hit.collider.gameObject;
                Debug.Log(obj.name);

                for (int i = 0; i < ObjectsInInventory.Length; i++)
                {
                    if (ObjectsInInventory[i] == hit.collider.gameObject)
                    {
                        obj = null;
                    }

                    if (ObjectsInInventory[i] == null && EmptyPlace == -1)
                    {
                        EmptyPlace = i;
                    }
                }

                if (obj != null && EmptyPlace != -1)
                {
                    ObjectsInInventory[EmptyPlace] = obj;
                    obj.SetActive(false);

                    Debug.Log($"{obj.name} go in Inventory !");
                    Ui.AffObjectUi(ObjectsInInventory, Ui.Image);
                }
                else if (obj != null)
                {
                    Debug.Log("Not enough place in Inventory !");
                }
            }

        }
    }
}
