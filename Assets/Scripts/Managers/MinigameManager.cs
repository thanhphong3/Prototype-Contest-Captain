using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;
    private float numberBloodCurrent = 1;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        MapManager.Instance.Init();
    }


    public void CameraShake(float _amount, float _time)
    {
        CamerasController.Instance.Shake(_amount, _time);
    }
    public void SubBlood(float damage)
    {
        if (numberBloodCurrent == 0)
            return;
        numberBloodCurrent -= damage;
        UIManager.Instance.UpdateBarBlood(numberBloodCurrent);

        if (numberBloodCurrent <= 0)
        {
            StartEventsLoser();
        }
    }

    private void StartEventsLoser()
    {
        UIManager.Instance.ShowReplayBtn();
    }


    public void ResetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

}
