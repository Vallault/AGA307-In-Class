using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject triggeredObject;

    private void OnTriggerEnter(Collider other)
    {
        //Change the color of the triggered object.
        triggeredObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {
        //Increase the size of the triggered object by 0.01f.
        triggeredObject.transform.localScale += Vector3.one * 0.01f;
    }

    private void OnTriggerExit(Collider other)
    {
        //Reset the size of the trigered object.
        triggeredObject.transform.localScale = Vector3.one;
        //Revert the color of the triggered object.
        triggeredObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
