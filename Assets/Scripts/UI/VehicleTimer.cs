using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTimer : MonoBehaviour
{
    private GameObject mainCamera;
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(mainCamera.transform.position);
    }
}
