using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pnj : MonoBehaviour
{
    public Image ImageCharacter;
    public TextMeshProUGUI TextComponent;

    public ScriptableDialog Dialog;
    public BoxCollider2D Collider2D;

    public AudioSource AudioSource;

    private int _currentText = 0;

    private void Update()
    {
        MouseDown();
    }

    void MouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<Pnj>() != null)
            {
                WriteDialog();
            }
        }
    }

    public void WriteDialog()
    {
        if (_currentText < Dialog.Dialogs.Length)
        {
            if (Dialog.Dialogs[_currentText].Character != null)
            {
                ImageCharacter.sprite = Dialog.Dialogs[_currentText].Character.SpriteCharacter;

                if (Dialog.Dialogs[_currentText].Character.Sounds.Length != 0)
                {
                    AudioSource.clip = Dialog.Speak(Dialog.Dialogs[_currentText].Character);
                    AudioSource.Play();
                }
            }
            if (Dialog.Dialogs[_currentText].DialogLine != null)
            {
                TextComponent.text = Dialog.Dialogs[_currentText].DialogLine;
            }

            _currentText++;
        }
        else
        {
            _currentText = 0;
            ImageCharacter.sprite = null;
            TextComponent.text = "";
        }
    }
}
