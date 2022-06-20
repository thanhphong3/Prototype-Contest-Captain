using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatVehicle : MonoBehaviour
{
    [SerializeField] float rechargeTime;
    [SerializeField] float speed;
    [SerializeField] float dmg;

    enum State
    {
        Disable,
        Spawning,
        Counting,
        Fixing,
        Removed
    }
    private State state = State.Disable;
    private float remainTime;
    void Start()
    {
        
    }
    void Update()
    {
        if(state == State.Counting)
        {

        }
    }

    public void StartFixing()
    {
        if(state == State.Counting)
        {
            state = State.Fixing;
        }
    }
    public void StopFixing()
    {
        if(state == State.Fixing)
        {
            state = State.Counting;
        }
    }
}
