using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image filler;
    [SerializeField] private float cooldownTime;
    private float lastUseTime;
    private bool canUse = true;

    void Start()
    {
        Use();
    }
    // Update is called once per frame
    void Update()
    {
        filler.fillAmount = (Time.time - lastUseTime)/cooldownTime;
        if(Time.time - lastUseTime > cooldownTime)
        {
            canUse = true;
        }
    }
    public void Use()
    {
        canUse = false;
        lastUseTime = Time.time;
    }
    public bool CanUse()
    {
        return canUse;
    }
}
