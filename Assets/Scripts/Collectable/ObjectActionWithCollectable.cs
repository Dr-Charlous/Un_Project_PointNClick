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

    public void Action(CollectableUIScriptableObject key, int index, string scene)
    {
        if (key == Door && scene == SceneActiveObject.name)
        {
            Debug.Log("Ouais");

            inventory.ObjectsInInventory[index] = null;
            inventory.Ui.AffObjectUi(inventory.ObjectsInInventory, inventory.Ui.Image);
            GoToScene(SceneDestination);
        }
        else
        {
            Debug.Log("Non");
        }
    }

    public void GoToScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
