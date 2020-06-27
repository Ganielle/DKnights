using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mMenuController : MonoBehaviour
{
    public Animator anim;
    public void touchTitle()
    {
        anim.SetBool("isDisableTitleScreen", true);
    }
}
