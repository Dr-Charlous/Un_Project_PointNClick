using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectActionWithCollectable : MonoBehaviour
{
    public CollectableUIScriptableObject Door;
    public SceneAsset SceneActiveObject;

    public void Action(CollectableUIScriptableObject key, string scene)
    {
        if (key == Door && scene == SceneActiveObject.name)
        {
            Debug.Log("Ouais");
        }
        else
        {
            Debug.Log("Non");
        }
    }
}
