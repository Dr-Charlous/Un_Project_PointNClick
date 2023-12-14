using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public GameObject Onglet;
    public Color ColorNotUsing;
    public Image[] Image;

    private bool _isUiActive = false;
    private bool _isUiActiveCollect = false;

    private void Start()
    {
        Onglet.SetActive(false);
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_isUiActiveCollect == false)
        {
            InventoryAffTouch(mousePos);
        }

        Onglet.SetActive(_isUiActive);
    }

    private void InventoryAffTouch(Vector3 mousePos)
    {
        if ((mousePos.x >= -1 + transform.position.x && mousePos.x <= 1 + transform.position.x) && (mousePos.y <= 1 + transform.position.y && mousePos.y >= -1 + transform.position.y) && _isUiActive == false)
        {
            _isUiActive = true;
        }
        else if ((mousePos.x >= -1 + transform.position.x && mousePos.x <= 1 + transform.position.x) && (mousePos.y <= 1 + transform.position.y && mousePos.y >= -7 + transform.position.y) && _isUiActive)
        {
            _isUiActive = true;
        }
        else
        {
            _isUiActive = false;
        }
    }

    public void AffObjectUi(CollectableUIScriptableObject[] obj, Image[] im)
    {
        StartCoroutine(AffInventoryCollect(2f));

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
                im[i].color = ColorNotUsing;
            }
        }
    }

    IEnumerator AffInventoryCollect(float time)
    {
        _isUiActive = true;
        _isUiActiveCollect = true;
        yield return new WaitForSeconds(time);
        _isUiActiveCollect = false;
    }

    public IEnumerator ScreenShake(float intensity, float duration)
    {
        Vector3 origin = Camera.main.transform.position;

        Debug.Log("ScreenShake");

        for (float i = 0; i < duration * 0.01f; i += Time.deltaTime)
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
