using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    public static CamerasController Instance;
    [SerializeField] private CinemachineVirtualCamera cvm;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin cbcp;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cbcp = cvm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void OnDestroy()
    {

    }


    public void Shake(float amount, float time)
    {
        // cbcp.m_AmplitudeGain = amount;
        cbcp.m_FrequencyGain = amount;
        shakeTimer = time;
    }

    void Update()
    {
        if (shakeTimer >= 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
                cbcp.m_FrequencyGain = 0;
        }
    }
}
