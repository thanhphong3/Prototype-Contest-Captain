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
        if(state == State.Counting)
        {
            remainTime -= Time.deltaTime;
            if(remainTime <= 0)
            {
                Fire();
            }
        }
        if(state == State.Fixing)
        {
            remainFixingTime -= Time.deltaTime;
            if(remainFixingTime <= 0)
            {
                Fixed();
            }
        }
        if(isMovableObject && state == State.Counting)
        {
            Move();
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
    public void StopCounting()
    {
        SetState(State.Stoping);
    }
    public void Spawn(Vector3 _position)
    {
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
        Removed();
    }
    private void BackToPool()
    {
        Destroy(gameObject);
    }
}
