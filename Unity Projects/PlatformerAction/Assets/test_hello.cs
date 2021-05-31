using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_hello : MonoBehaviour
{
    public Animator animator;

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
}
