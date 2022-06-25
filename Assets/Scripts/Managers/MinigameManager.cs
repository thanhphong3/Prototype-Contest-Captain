using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [Header("Game Controllers")]
    [SerializeField] CamerasController cameraController;
    [SerializeField] VehiclePool vehiclePool;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPos = GetPointHand();
            SpawnVehicleInPool(1, spawnPos);
        }
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
    public void SpawnVehicleInPool(int _teamIndex, Vector3 _position)
    {
        vehiclePool.SpawnVehicleInPool(_teamIndex, _position);
    }
    public void CameraShake(float _amount, float _time)
    {
        cameraController.Shake(_amount, _time);
    }

}
