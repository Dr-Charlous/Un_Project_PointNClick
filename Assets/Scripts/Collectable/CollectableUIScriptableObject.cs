using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/ScriptableObject", order = 1)]
public class CollectableUIScriptableObject : ScriptableObject
{
    public bool IsUsed = false;
    public Sprite sprite;
    public Color color;
}
