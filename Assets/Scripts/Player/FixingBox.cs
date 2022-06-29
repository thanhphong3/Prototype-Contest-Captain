using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingBox : MonoBehaviour
{
    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            other.GetComponentInParent<CombatVehicle>().StartFixing();
            Player.Instance.SetTriggerWithVehicle(true);
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            other.GetComponentInParent<CombatVehicle>().StopFixing();
            Player.Instance.SetTriggerWithVehicle(false);
        }
    }
}
