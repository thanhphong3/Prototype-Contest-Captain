using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Image BloodBar;
    [SerializeField] Button BtnReplay;
    [SerializeField] Button BtnNext;
    [SerializeField] GameObject txtWin;
    [SerializeField] GameObject txtLose;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateBarBlood(float valueEnd)
    {
        DOTween.Kill("blood");
        BloodBar.DOFillAmount(valueEnd, .1f).SetId("blood");
    }
    public void ShowReplayBtn()
    {
        BtnReplay.gameObject.SetActive(true);
    }
    public void ShowNextBtn()
    {
        BtnNext.gameObject.SetActive(true);
    }
    public void ShowTextWin()
    {
        txtWin.SetActive(true);
    }
    public void ShowTextLose()
    {
        txtLose.SetActive(true);
    }


}
