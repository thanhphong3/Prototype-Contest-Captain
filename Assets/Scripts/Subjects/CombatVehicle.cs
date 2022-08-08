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
    [SerializeField] float disableTime = 0.5f;
    [SerializeField] float rechargeDelayTime = 1f;
    [SerializeField] int maxHP = 3;
    [SerializeField] int HP = 3;
    [SerializeField] int myRagePoint = 2;
    [SerializeField] Material blueMat;

    [Header("Object Elements")]
    [SerializeField] Image rechargeCounter;
    [SerializeField] GameObject fixingPanel;
    [SerializeField] Image fixCounter;
    [SerializeField] Animator anim;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Chassis;
    [SerializeField] GameObject[] Tracks;
    [SerializeField] GameObject Turret;
    [SerializeField] GameObject FocusUI;
    private bool isNeedToFocus = false;

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
    private void FocusMe()
    {
        isNeedToFocus = false;
        FocusUI.SetActive(true);
    }
    public void SetFocus(float fakeFixTime)
    {
        isNeedToFocus = true;
        rechargeTime = fakeFixTime;
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
                UpdateUI();
                break;
        }
    }
    private void UpdateUI()
    {
        // UI.transform.LookAt(new Vector3(0f, 0f, CamerasController.Instance.mainCam.transform.position.z));
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
        if(_state == State.Counting)
        {
            if(isNeedToFocus)
                FocusMe();
            ShowUI();
        }
        else
            HideUI();
    }
    private void ShowUI()
    {
        UI.SetActive(true);
    }
    private void HideUI()
    {
        UI.SetActive(false);
    }
    public void Move(Vector3 PosEnd)
    {
        float distance = Vector3.Distance(transform.position, PosEnd);
        float speedCurrent = distance / speed;
        transform.DOMove(PosEnd, speedCurrent).SetEase(Ease.Linear).OnComplete(StartCounting);
    }
    private void Fixed()
    {
        // anim.Play("Fixed");
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
    public void SetAsBlueTeam()
    {
        Debug.LogError("Blue team spawn");
        Chassis.GetComponent<Renderer>().material = blueMat;
        Tracks[0].GetComponent<Renderer>().material = blueMat;
        Tracks[1].GetComponent<Renderer>().material = blueMat;
        Turret.GetComponent<Renderer>().material = blueMat;
    }
}
