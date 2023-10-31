using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject triggeredObject;

    private void OnTriggerEnter(Collider other)
    {
        //Change the colour of the triggered object
        triggeredObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerStay(Collider other)
    {
        //Increase the size of the triggered object by 0.01f
        triggeredObject.transform.localScale += Vector3.one * 0.01f;
        triggeredObject.transform.localPosition += Vector3.up * 0.01f;
    }

    private void OnTriggerExit(Collider other)
    {
        //reset the size of the triggered object
        triggeredObject.transform.localScale = Vector3.one;
        triggeredObject.transform.localPosition = Vector3.one;
        //revert the colour of the triggered object
        triggeredObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
