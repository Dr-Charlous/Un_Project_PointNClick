using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public RythmGpe rythm;

    [Header("")]
    public float DistanceUiAff = 1;
    public GameObject Onglet;
    public Color ColorNotUsing;
    public Image[] Image;

    private bool _isUiActive = false;
    private bool _isUiActiveCollect = false;

    private void Start()
    {
        _isUiActive = false;
        _isUiActiveCollect = false;
        Onglet.SetActive(false);
    }

    private void Update()
    {
        if (rythm.IsActive == false)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_isUiActiveCollect == false)
            {
                InventoryAffTouch(mousePos);
            }

            Onglet.SetActive(_isUiActive);
        }
        else if (_isUiActive != false)
        {
            _isUiActive = false;
            Onglet.SetActive(_isUiActive);
        }
    }

    private void InventoryAffTouch(Vector3 mousePos)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)mousePosition, Vector2.zero);

        float distance = Vector3.Distance(hit.point, transform.position);

        if (distance <= DistanceUiAff && _isUiActive == false)
        {
            _isUiActive = true;
        }
        else if (hit.collider != null && hit.collider.GetComponent<Inventory>() != null && _isUiActive)
        {
            _isUiActive = true;
        }
        else
        {
            _isUiActive = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, new Vector3(DistanceUiAff, DistanceUiAff, 0));
    }

    public void AffObjectUi(CollectableUIScriptableObject[] obj, Image[] im, bool begin)
    {
        if (begin == false)
        {
            StartCoroutine(AffInventoryCollect(2f));
        }

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
