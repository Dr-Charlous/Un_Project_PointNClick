using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Door", menuName = "ScriptableObjects/ScriptableDoor", order = 1)]
public class Doors : ScriptableObject
{
    public bool isOpen = false;
    public CollectableUIScriptableObject Door;
    public SceneAsset SceneDestination;
}

[CreateAssetMenu(fileName = "ObjGive", menuName = "ScriptableObjects/ScriptableObjGive", order = 1)]
public class ObjGive : ScriptableObject
{
    public bool isUsed = false;
    public CollectableUIScriptableObject Door;
}

public class ObjectActionWithCollectable : MonoBehaviour
{
    public Doors doors;
    public ObjGive objGive;
    public GameObject ObjHide;
    [Header("Don't Touch")]
    public SceneAsset SceneDestination;
    public CollectableUIScriptableObject Door;

    public bool isOpen = false;
    public bool isUsed = false;
    public Inventory inventory;

    private void Start()
    {
        if (doors != null)
        {
            isOpen = doors.isOpen;
            Door = doors.Door;
            SceneDestination = doors.SceneDestination;
        }
        if (objGive != null)
        {
            isUsed = objGive.isUsed;
            Door = objGive.Door;
        }

        if (isOpen == true || isUsed == true)
        {
            if (ObjHide != null && ObjHide.GetComponent<Collectable>().ScrpitableObjectRefObjects.IsUsed == false)
            {
                Instantiate(ObjHide, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }

    public bool Check(CollectableUIScriptableObject key, CollectableUIScriptableObject door)
    {
        if (key != null && door != null && key == door)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActionDoor(CollectableUIScriptableObject key, int index)
    {
        if (key == Door && isOpen == false)
        {
            isOpen = true;
            doors.isOpen = isOpen;

            inventory.ObjectsInInventory[index] = null;
            inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image, false);

            GoToScene(SceneDestination);
        }
    }

    public void ActionObj(CollectableUIScriptableObject key, ObjectActionWithCollectable door, int index)
    {
        if (key == Door && isUsed == false)
        {
            isUsed = true;
            objGive.isUsed = isUsed;

            inventory.ObjectsInInventory[index] = null;
            inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image, false);

            Instantiate(ObjHide, transform.position, Quaternion.identity);
        }
    }

    public void GoToScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}