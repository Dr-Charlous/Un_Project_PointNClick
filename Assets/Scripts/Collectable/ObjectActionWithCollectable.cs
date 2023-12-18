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
    public SceneAsset SceneActiveObject;
    public SceneAsset SceneDestination;
}

public class ObjectActionWithCollectable : MonoBehaviour
{
    public Doors doors;
    public SceneAsset SceneDestination;

    private bool isOpen = false;
    public Inventory inventory;
    private CollectableUIScriptableObject Door;
    private SceneAsset SceneActiveObject;

    private void Start()
    {
        isOpen = doors.isOpen;
        Door = doors.Door;
        SceneActiveObject = doors.SceneActiveObject;
        SceneDestination = doors.SceneDestination;
    }

    public bool Check(CollectableUIScriptableObject key)
    {
        if (key == Door && SceneManager.GetActiveScene().name == SceneActiveObject.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Action(CollectableUIScriptableObject key, int index)
    {
        if (key == Door && SceneManager.GetActiveScene().name == SceneActiveObject.name && isOpen == false)
        {
            isOpen = true;
            doors.isOpen = isOpen;
            inventory.ObjectsInInventory[index] = null;
            inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image, false);
            GoToScene(SceneDestination);
        }
    }

    public void GoToScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}


