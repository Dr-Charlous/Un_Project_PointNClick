using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActionWithCollectable : MonoBehaviour
{
    public Inventory inventory;
    public CollectableUIScriptableObject Door;
    public SceneAsset SceneActiveObject;
    public SceneAsset SceneDestination;

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
        if (key == Door && SceneManager.GetActiveScene().name == SceneActiveObject.name)
        {
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
