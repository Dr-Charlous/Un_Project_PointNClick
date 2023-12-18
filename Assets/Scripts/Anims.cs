using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anims : MonoBehaviour
{
    public void ActivateAnimSolo(Animator animator, string nameAnim)
    {
        animator.SetBool(nameAnim, true);
    }

    public void DesactivateAnimSolo(Animator animator, string nameAnim)
    {
        animator.SetBool(nameAnim, false);
    }


}
