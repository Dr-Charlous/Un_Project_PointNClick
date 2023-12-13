using DG.Tweening;
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

    public IEnumerator ScreenShake(float intensity, float duration)
    {
        Vector3 origin = Camera.main.transform.position;

        Debug.Log("ScreenShake");

        for(float i = 0;i < duration * 0.01f;i+=Time.deltaTime)
        {
            Vector2 transfert = new Vector2(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));

            Camera.main.transform.DOKill();
            Camera.main.transform.DOMove(origin + (Vector3)transfert, 10 * Time.deltaTime);

            yield return new WaitForSeconds(duration * Time.deltaTime);
        }

        Camera.main.transform.DOKill();
        Camera.main.transform.DOMove(origin, 10 * Time.deltaTime);
    }
}
