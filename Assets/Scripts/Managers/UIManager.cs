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

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateBarBlood(float valueEnd)
    {
        DOTween.Kill("blood");
        BloodBar.DOFillAmount(valueEnd, .1f).SetId("blood");
    }



}
