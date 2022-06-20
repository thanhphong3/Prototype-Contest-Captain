using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public enum STATE_PLAYER
    {
        Idle,
        Run,
        Fix,
    }

    public static Player Instance;

    private Vector3 target;
    private CombatVehicle currentFixingVehicle;
    [SerializeField] float speed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }
    public void Init()
    {
        target = transform.position;
    }


    public void Move()
    {
        Vector3 posTarget = GetPointHand();
        transform.DOMove(posTarget, GetTimeToTarget()).OnComplete(() =>
        {

        });
    }
    private float GetTimeToTarget()
    {
        return speed;
    }
    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }

    private Vector3 GetPointHand()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.point != null)
            {
                return raycastHit.point;
            }
        }
        return Vector3.zero;
    }
}
