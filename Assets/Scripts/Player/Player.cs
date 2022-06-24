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
        SetState(STATE_PLAYER.Run);
        myTween = transform.DOMove(target, GetTimeToTarget()).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (triggerWithVehicle)
                SetState(STATE_PLAYER.Fix);
            else
                SetState(STATE_PLAYER.Idle);
        });
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
