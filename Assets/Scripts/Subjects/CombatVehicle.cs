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
    [SerializeField] int maxHP = 3;
    [SerializeField] int HP = 3;
    [SerializeField] int myRagePoint = 2;

    [Header("Object Elements")]
    [SerializeField] Image rechargeCounter;
    [SerializeField] GameObject fixingPanel;
    [SerializeField] Image fixCounter;
    [SerializeField] Animator anim;
    [SerializeField] GameObject UI;

    enum State
    {
        Disable,
        // Spawning,
        Counting,
        Stoping,
        Fixing,
        Removed
    }
    private RageSkillButton rageSkillButton;
    private float remainTime;
    private float remainFixingTime;
    private State state = State.Disable;
    private Vector3 opponentBasePos;

    void Start()
    {
        rageSkillButton = ObjectDefiner.Instance.rageSkillButton;
    }
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
                // case State.Fixing:
                //     remainFixingTime -= Time.deltaTime;
                //     if (remainFixingTime <= 0)
                //     {
                //         Fixed();
                //     }
                //     break;
        }
        // UpdateUI();
    }
    private void UpdateUI()
    {
        UI.transform.LookAt(new Vector3(0f, 0f, CamerasController.Instance.mainCam.transform.position.z));
        fixCounter.fillAmount = remainFixingTime / fixingTime;
        rechargeCounter.fillAmount = remainTime / rechargeTime;
    }
    private void FixingUISetActive(bool _isActive)
    {
        if (_isActive)
            ObjectDefiner.Instance.fixButton.Link(this);
        else
            ObjectDefiner.Instance.fixButton.Hide();
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
        float distance = Vector3.Distance(transform.position, PosEnd);
        float speedCurrent = distance / speed;
        transform.DOMove(PosEnd, speedCurrent).SetEase(Ease.Linear).OnComplete(StartCounting);
    }
    private void Fixed()
    {
        anim.Play("Fixed");
        Removed();
    }
    private void Fire()
    {
        anim.Play("Fire");
        SetState(State.Stoping);
        Invoke("StartCounting", rechargeDelayTime);
        MinigameManager.Instance.SubBlood(dmg);
    }
    private void BackToPool()
    {
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
    public void TakeDmg(int _dmg)
    {
        HP -= _dmg;
        if (HP <= 0)
        {
            Fixed();
            ObjectDefiner.Instance.fixButton.Hide();
            rageSkillButton.RagePointAdd(myRagePoint);
        }
    }
    public void GetHP(out int _maxHP, out int _HP)
    {
        _maxHP = maxHP;
        _HP = HP;
    }
}
