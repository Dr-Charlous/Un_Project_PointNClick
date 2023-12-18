using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pnj : MonoBehaviour
{
    public RythmGpe rythm;
    [Header("")]
    public Image ImageCharacter;
    public TextMeshProUGUI TextComponent;
    [Header("")]
    public ScriptableDialog[] Dialog;
    public BoxCollider2D Collider2D;
    [Header("")]
    public GameObject DilaogAff;
    public AudioSource AudioSource;

    public int _currentDialog = 0;
    private int _currentText = 0;
    private bool _isDialogActive = false;

    private void Start()
    {
        DilaogAff.SetActive(false);
    }

    private void Update()
    {
        if (rythm.IsActive == false)
        {
            MouseDown();
        }
    }

    void MouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

            if (_isDialogActive)
            {
                WriteDialog(_currentDialog);
            }
            else if (hit.collider != null && hit.collider.GetComponent<Pnj>() != null)
            {
                _isDialogActive = true;
                WriteDialog(_currentDialog);
            }
        }
    }

    public void WriteDialog(int index)
    {
        if (_currentText < Dialog[index].Dialogs.Length)
        {
            if (Dialog[index].Dialogs[_currentText].Character != null)
            {
                ImageCharacter.sprite = Dialog[index].Dialogs[_currentText].Character.SpriteCharacter;

                if (Dialog[index].Dialogs[_currentText].Character.Sounds.Length != 0)
                {
                    AudioSource.clip = Dialog[index].Speak(Dialog[index].Dialogs[_currentText].Character);
                    AudioSource.Play();
                }
            }
            if (Dialog[index].Dialogs[_currentText].DialogLine != null)
            {
                TextComponent.text = Dialog[index].Dialogs[_currentText].DialogLine;
            }

            _currentText++;
        }
        else
        {
            _currentText = 0;
            if (_currentDialog < Dialog.Length - 1)
                _currentDialog++;
            ImageCharacter.sprite = null;
            TextComponent.text = "";
            _isDialogActive = false;
        }

        DilaogAff.SetActive(_isDialogActive);
    }
}
