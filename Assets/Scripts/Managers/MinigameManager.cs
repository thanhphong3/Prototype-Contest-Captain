using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;
    private float numberBloodCurrent = 1;
    public enum GAME_STATE
    {
        MainMenu,
        Ingame,
        Endgame
    }
    public GAME_STATE gameState = GAME_STATE.MainMenu;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameState == GAME_STATE.MainMenu)
        {
            gameState = GAME_STATE.Ingame;
            ObjectDefiner.Instance.EnterGame();
        }
        if (gameState == GAME_STATE.Ingame)
        {
            CheckWin();
        }
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
        Player.Instance.SetState(Player.STATE_PLAYER.Lose);
        UIManager.Instance.ShowTextLose();
        UIManager.Instance.ShowReplayBtn();
        gameState = GAME_STATE.Endgame;
    }
    public void CheckWin()
    {
        bool isNullEnemy = MapManager.Instance.CheckNullEnemy();
        if (isNullEnemy)
        {
            Player.Instance.SetState(Player.STATE_PLAYER.Win);
            UIManager.Instance.ShowTextWin();
            UIManager.Instance.ShowReplayBtn();
            gameState = GAME_STATE.Endgame;
        }
    }


    public void ResetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

}
