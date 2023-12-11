using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public GameObject Onglet;
    public Image[] Image;

    private void Start()
    {
        Onglet.SetActive(false);
    }

    public void AffObjectUi(CollectableUIScriptableObject[] obj, Image[] im)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
            {
                im[i].sprite = obj[i].sprite;
                im[i].color = obj[i].color;
                Onglet.SetActive(true);
            }
            else
            {
                im[i].sprite = null;
                im[i].color = Color.white;
            }
        }
    }

    public void UiAff()
    {
        Onglet.SetActive(!Onglet.activeSelf);
    }
}
