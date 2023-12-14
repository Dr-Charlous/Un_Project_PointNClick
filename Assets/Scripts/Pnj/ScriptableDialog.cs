using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/SpawnManagerScriptableCharacter", order = 1)]
public class Characters : ScriptableObject
{
    public string Name;
    public Sprite SpriteCharacter;
    public AudioClip[] Sounds;
}

[Serializable]
public class DialogBox
{
    public Characters Character;
    public string DialogLine;
}

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/SpawnManagerScriptableDialog", order = 1)]
public class ScriptableDialog : ScriptableObject
{
    public DialogBox[] Dialogs;

    public AudioClip Speak(Characters chara)
    {
        int num = UnityEngine.Random.Range(0, chara.Sounds.Length);
        return chara.Sounds[num];
    }
}