using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : GameBehaviour
{
    public int health = 3;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Hit()
    {
        health--;
        if (health <= 0)
            anim.SetTrigger("TargetDie");
        else
            anim.SetTrigger("TargetHit");
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject, 2f);
    }
}
