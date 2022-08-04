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
    private DestroyButton fixButton;
    private float speed;
    private int dmg;
    // private GameObject skillButton;

    [SerializeField] float normalSpeed;
    [SerializeField] float skillSpeed;
    [SerializeField] float speedUpDuration;
    [SerializeField] int normalDmg;
    [SerializeField] int rageDmg;
    [SerializeField] float rageDuration;
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
        fixButton = ObjectDefiner.Instance.fixButton;
        speed = normalSpeed;
        dmg = normalDmg;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Move");
            Move();
        }
    }
    public void Move()
    {
        if(fixButton.GetWasClicked())
        {
            fixButton.ButtonClickOut();
            return;
        }
        myTween.Kill();
        target = GetPointHand();
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
        if(MinigameManager.Instance.gameState == MinigameManager.GAME_STATE.Ingame)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.point != null)
                {
                    Vector3 result = raycastHit.point;
                    result.y += 0.1f;
                    ObjectDefiner.Instance.effectPool.GetEffectFromPool(3, result);
                    result.y = 0;
                    return result;
                }
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
    public void SpeedUpSkill()
    {
        speed = skillSpeed;
        Invoke("SpeedUpEnd", speedUpDuration);
    }
    private void SpeedUpEnd()
    {
        speed = normalSpeed;
    }
    public int GetDmg()
    {
        return dmg;
    }
    public void RageSkill()
    {
        dmg = rageDmg;
        Invoke("RageEnd", rageDuration);
    }
    private void RageEnd()
    {
        dmg = normalDmg;
    }
}
