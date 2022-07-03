using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSkillButton : MonoBehaviour
{
    [SerializeField] private Image filler;
    [SerializeField] private int ragePointNeed;
    private int currentRagePoint;
    private bool canUse = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        filler.fillAmount = (float)currentRagePoint/ragePointNeed;
    }

    public void RagePointAdd(int _value)
    {
        currentRagePoint += _value;
        if(ragePointNeed <= currentRagePoint)
        {
            canUse = true;
        }
    }
    public void Use()
    {
        currentRagePoint = 0;
        canUse = false;
    }
    public bool CanUse()
    {
        return canUse;
    }
}
