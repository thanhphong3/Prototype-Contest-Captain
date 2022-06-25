using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cvm;
    private float shakeTimer;
    CinemachineBasicMultiChannelPerlin cbcp;
    // Start is called before the first frame update
    void Start()
    {
        cbcp = cvm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void OnDestroy()
    {
    }

    // Update is called once per frame
    public void Shake(float amount, float time)
    {
        // cbcp.m_AmplitudeGain = amount;
        cbcp.m_FrequencyGain = amount;
        shakeTimer = time;
    }

    void Update() {
        if(shakeTimer >= 0f)
        {
            shakeTimer -= Time.deltaTime;    
            if(shakeTimer <= 0f)    
                cbcp.m_FrequencyGain = 0;
        }
    }
}
