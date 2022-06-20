using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    Debug.Log("Mực đã click vào vị trí: " + raycastHit.point); 
                    CustomEvents.OnClickOnMap?.Invoke(raycastHit.point);
                }
            }
        }
    }
}
