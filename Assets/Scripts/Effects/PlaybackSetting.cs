using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybackSetting : MonoBehaviour
{
    [SerializeField] float playbackTime = 40f;
    // Start is called before the first frame update
    void Start()
    {  
        // ParticleSystem Ps = GetComponent<ParticleSystem>();
        GetComponent<ParticleSystem>().Simulate(playbackTime, true, true);
        // GetComponent<ParticleSystem>().playbackTime = playbackTime;
        // Ps.Pause(false);
        // Ps.time = playbackTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
