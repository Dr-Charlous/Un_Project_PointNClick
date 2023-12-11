using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActionWithCollectable : MonoBehaviour
{
    public CollectableUIScriptableObject Door;
    public SceneAsset SceneActiveObject;
    public Inventory inventory;

    public void Action(CollectableUIScriptableObject key, int index, string scene)
    {
        if (key == Door && scene == SceneActiveObject.name)
        {
            Debug.Log("Ouais");

            inventory.ObjectsInInventory[index] = null;
            inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image);
        }
        else
        {
            Debug.Log("Non");
        }
    }
}
