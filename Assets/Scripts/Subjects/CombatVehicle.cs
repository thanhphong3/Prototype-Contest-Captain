using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CombatVehicle : MonoBehaviour
{
    [Header("Object Parameters")]
    [SerializeField] float rechargeTime;
    [SerializeField] float speed;
    [SerializeField] float dmg;
    [SerializeField] float fixingTime;
    [SerializeField] bool isMovableObject;
    [SerializeField] float disableTime = 1f;
    [SerializeField] float rechargeDelayTime = 1f;

    [Header("Object Elements")]
    [SerializeField] Image rechargeCounter;
    [SerializeField] GameObject fixingPanel;
    [SerializeField] Image fixCounter;
    [SerializeField] Animator anim;

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

    void Update()
    {
        switch (state)
        {
            case State.Counting:
                remainTime -= Time.deltaTime;
                if (remainTime <= 0)
                {
                    Fire();
                }
                // if (isMovableObject)
                // {
                //     Move();
                // }
                break;
            case State.Fixing:
                remainFixingTime -= Time.deltaTime;
                if (remainFixingTime <= 0)
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
        if (state == State.Counting)
        {
            FixingUISetActive(true);
            SetState(State.Fixing);
        }
    }
    public void StopFixing()
    {
        if (state == State.Fixing)
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
    public void SetOpponentBasePos(Vector3 _position)
    {
        opponentBasePos = _position;
    }
    public void Removed()
    {
        SetState(State.Removed);
        Invoke("BackToPool", disableTime);
    }
    private void SetState(State _state)
    {
        state = _state;
    }
    public void Move(Vector3 PosEnd)
    {
        // Vector3 moveDir = (opponentBasePos - transform.position).normalized;
        // transform.Translate(moveDir * Time.deltaTime * speed);
        float distance = Vector3.Distance(transform.position, PosEnd);
        float speedCurrent = distance / speed;
        transform.DOMove(PosEnd, speedCurrent).SetEase(Ease.Linear).OnComplete(StartCounting);
    }
    private void Fixed()
    {
        Removed();
    }
    private void Fire()
    {
        anim.Play("Fire");
        SetState(State.Stoping);
        Invoke("StartCounting", rechargeDelayTime);
    }
    private void BackToPool()
    {
        MinigameManager.Instance.CameraShake(shakeAmount, shakeTime);
        // gameObject.SetActive(false);
        Destroy(gameObject);
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
