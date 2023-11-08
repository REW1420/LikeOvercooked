using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Collider collider;
    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        Debug.Log("Trigger On : " + collider.isTrigger);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (collider.isTrigger == true)
        {
            Debug.Log("is trigger");
        }
    }
}
