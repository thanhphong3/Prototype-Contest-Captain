using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            other.gameObject.GetComponent<CombatVehicle>().StartFixing();
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            other.gameObject.GetComponent<CombatVehicle>().StopFixing();
        }
    }
}
