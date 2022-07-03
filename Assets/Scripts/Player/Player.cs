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
    private Tween myTween;
    private STATE_PLAYER state = STATE_PLAYER.Idle;
    private bool triggerWithVehicle = false;

    [SerializeField] float speed;
    [SerializeField] GameObject model;

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
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }
    public void Move()
    {
        myTween.Kill();
        target = GetPointHand();
        Debug.Log("target: " + target);
        SetState(STATE_PLAYER.Run);
        RotateModel(target);
        myTween = transform.DOMove(target, GetTimeToTarget()).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (triggerWithVehicle)
                SetState(STATE_PLAYER.Fix);
            else
                SetState(STATE_PLAYER.Idle);
        });
    }
    private void RotateModel(Vector3 _target)
    {
        // float tan = (_target.x - transform.position.x)/(_target.z - transform.position.z);
        // float rotateDir = 0f;
        // if(_target.z < transform.position.z)
        // {
        //     rotateDir = 180f;
        // }
        // model.transform.rotation = Quaternion.Euler(0f, rotateDir + Mathf.Rad2Deg * Mathf.Atan(tan), 0f);
        model.transform.DOLookAt(_target, .1f, AxisConstraint.Y);
    }
    private float GetTimeToTarget()
    {
        return Vector3.Distance(target, transform.position) / speed;
    }
    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }

    private Vector3 GetPointHand()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
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
    public void SetState(STATE_PLAYER _state)
    {
        state = _state;
    }
    public STATE_PLAYER GetState()
    {
        return state;
    }
    private bool CheckNearVehicle()
    {
        return false;
    }
    public void SetTriggerWithVehicle(bool _isTrigger)
    {
        triggerWithVehicle = _isTrigger;
    }
}
