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

            if (hit.collider != null && hit.collider.GetComponent<Collectable>() != null)
            {
                int EmptyPlace = -1;
                GameObject obj = hit.collider.gameObject;

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

    public void Drag(int index)
    {
        if (ObjectsInInventory[index] != null) 
        {
            if (!ObjectsInInventory[index].activeInHierarchy)
            {
                ObjectsInInventory[index].SetActive(true);
            }

            var pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            ObjectsInInventory[index].transform.position = pos;
        }
    }

    public void Drop(int index)
    {
        if (ObjectsInInventory[index] != null)
        {
            ObjectsInInventory[index].SetActive(false);
        }
    }
}
