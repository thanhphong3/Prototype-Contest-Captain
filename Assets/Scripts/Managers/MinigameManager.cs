using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
    private Vector3 GetPointHand()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.point != null)
            {
                Vector3 result = raycastHit.point;
                result.y = 0;
                return result;
            }
        }
        return transform.position;
    }


    public void CameraShake(float _amount, float _time)
    {
        CamerasController.Instance.Shake(_amount, _time);
    }

}
