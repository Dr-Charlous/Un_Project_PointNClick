using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Anims : MonoBehaviour
{
    #region singleton
    public static Anims Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public void ActivateAnimSolo(Animator animator, string nameAnim)
    {
        animator.SetBool(nameAnim, true);
    }

    public void DesactivateAnimSolo(Animator animator, string nameAnim)
    {
        animator.SetBool(nameAnim, false);
    }
}
