using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatVehicle : MonoBehaviour
{
    [Header("Object Parameters")]
    [SerializeField] float rechargeTime;
    [SerializeField] float speed;
    [SerializeField] float dmg;
    [SerializeField] float fixingTime;
    [SerializeField] bool isMovableObject;

    [Header("Object Elements")]
    [SerializeField] Image rechargeCounter;
    [SerializeField] GameObject fixingPanel;
    [SerializeField] Image fixCounter;

    [Header("Game-feel side")]
    [SerializeField] float shakeAmount;
    [SerializeField] float shakeTime;

    enum State
    {
        Disable,
        // Spawning,
        Counting,
        Stoping,
        Fixing,
        Removed
    }
    private float remainTime;
    private float remainFixingTime;
    private State state = State.Disable;
    private Vector3 opponentBasePos;
    void Start()
    {
        Spawn(transform.position); //Debug
    }
    void Update()
    {
        switch (state)
        {
            case State.Counting:
                remainTime -= Time.deltaTime;
                if(remainTime <= 0)
                {
                    Fire();
                }
                if(isMovableObject)
                {
                    Move();
                }
            break;
            case State.Fixing:
                remainFixingTime -= Time.deltaTime;
                if(remainFixingTime <= 0)
                {
                    Fixed();
                }
            break;
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        fixCounter.fillAmount = remainFixingTime / fixingTime;
        rechargeCounter.fillAmount = remainTime / rechargeTime;
    }
    private void FixingUISetActive(bool _isActive)
    {
        fixingPanel.SetActive(_isActive);
    }
    public void StartFixing()
    {
        if(state == State.Counting)
        {
            FixingUISetActive(true);
            SetState(State.Fixing);
        }
    }
    public void StopFixing()
    {
        if(state == State.Fixing)
        {
            FixingUISetActive(false);
            SetState(State.Counting);
        }
    }
    public void StartCounting()
    {
        remainTime = rechargeTime;
        remainFixingTime = fixingTime;
        SetState(State.Counting);
    }
    public void Spawn(Vector3 _position)
    {
        gameObject.SetActive(true);
        transform.position = _position;
        StartCounting();
    }
    public void SetOpponentBasePos(Vector3 _position)
    {
        opponentBasePos = _position;
    }
    public void Removed()
    {
        SetState(State.Removed);
        BackToPool();
    }
    private void SetState(State _state)
    {
        state = _state;
    }
    private void Move()
    {
        Vector3 moveDir = (opponentBasePos - transform.position).normalized; 
        transform.Translate(moveDir * Time.deltaTime * speed);
    }
    private void Fixed()
    {
        Removed();
    }
    private void Fire()
    {
        MinigameManager.Instance.CameraShake(shakeAmount, shakeTime);
        Removed();
    }
    private void BackToPool()
    {
        gameObject.SetActive(false);
    }
    public void StopCounting()
    {
        SetState(State.Stoping);
    }
    public void ContinueCounting()
    {
        SetState(State.Stoping);
    }
}
