using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public GameObject Onglet;
    public Image[] Image;

    public void AffObjectUi(GameObject[] obj, Image[] im)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
            {
                im[i].sprite = obj[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void UiAff()
    {
        Onglet.SetActive(!Onglet.activeSelf);
    }
}
