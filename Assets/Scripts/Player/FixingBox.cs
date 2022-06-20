using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Vehicle"))
            Debug.Log("Mực Mực đẹp trai");
    }
}
