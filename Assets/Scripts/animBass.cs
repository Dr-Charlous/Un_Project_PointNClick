using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animBass : MonoBehaviour
{
    public float Time = 1f;
    public Animator anim;

    void Start()
    {
        StartCoroutine(Bass(Time));
    }

    IEnumerator Bass(float time)
    {
        Anims.Instance.ActivateAnimSolo(anim, "Bass_Boum");
        yield return new WaitForSeconds(0.1f);
        Anims.Instance.DesactivateAnimSolo(anim, "Bass_Boum");
        yield return new WaitForSeconds(time);
        StartCoroutine(Bass(time));
    }
}
