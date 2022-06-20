using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private const float NEAR_0 = 0.01f;
    private Vector3 target;
    private CombatVehicle currentFixingVehicle;

    [SerializeField] float speed;

    void Start()
    {
        Init();
    }
    public void Init()
    {
        target = transform.position;
        Instance = GetComponent<Player>();
        // CustomEvents.OnClickOnMap += OnClickOnMap;
    }
    void OnDestroy() 
    {
        // CustomEvents.OnClickOnMap -= OnClickOnMap;
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(Vector3.Distance(target, transform.position) > NEAR_0)
            transform.Translate((target - transform.position).normalized * Time.deltaTime * speed);
    }
    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }
    // void OnClickOnMap(Vector3 _target)
    // {
    //     Vector3 fixYTarget = new Vector3(_target.x,transform.position.y,_target.z);
    //     SetTarget(fixYTarget);
    // }
    public CombatVehicle GetCurrentFixingVehicle()
    {
        return currentFixingVehicle;
    }
}
