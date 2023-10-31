using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if we hit the object tagged Target
        if(collision.gameObject.CompareTag("Target"))
        {
            //Change the colour of the target
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            //Destroy the target after 1 second
            Destroy(collision.gameObject, 1);
            //Destroy this object
            Destroy(this.gameObject);
        }
    }
}
