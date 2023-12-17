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

    public bool Action(CollectableUIScriptableObject key, int index, bool action)
    {
        if (key == Door && SceneManager.GetActiveScene().name == SceneActiveObject.name)
        {
            if (action)
            {
                inventory.ObjectsInInventory[index] = null;
                inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image);
                //GoToScene(SceneDestination);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void GoToScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
