using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogBox
{
    public Sprite SpriteCharacter;
    public string DialogLine;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableDialog", order = 1)]
public class ScriptableDialog : ScriptableObject
{
    public DialogBox[] Dialogs;
}
