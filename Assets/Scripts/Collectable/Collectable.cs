using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableUIScriptableObject ScrpitableObjectRefObjects;

    private void Start()
    {
        if (ScrpitableObjectRefObjects.IsUsed)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
