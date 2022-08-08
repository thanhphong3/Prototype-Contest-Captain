using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;
    private float numberBloodCurrent = 1;
    private float countingTime = 3f;
    private bool isFirstTimePlay = false;
    public enum GAME_STATE
    {
        MainMenu,
        Counting,
        Ingame,
        Endgame
    }
    public GAME_STATE gameState = GAME_STATE.MainMenu;

    void Awake()
    {
        Instance = this;
        // PlayerPrefs.SetInt("Level", 1);
    }

    void Start()
    {
        // Init();
    }

    private void Init()
    {
        MapManager.Instance.Init();
    }
    private IEnumerator EndCounting()
    {
        yield return new WaitForSeconds(countingTime);
        gameState = GAME_STATE.Ingame;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameState == GAME_STATE.MainMenu)
        {
            // gameState = GAME_STATE.Ingame;
            ObjectDefiner.Instance.EnterGame();
            gameState = GAME_STATE.Counting;
            StartCoroutine(EndCounting());
            Init();
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

        if(isFirstTimePlay)
        {
            ShowFireInfo();
        }
    }
    public void SetFirstPlayTime()
    {
        isFirstTimePlay = true;
    }
    private void ShowFireInfo()
    {
        ObjectDefiner.Instance.ShowFireInfo();
    }

    private void StartEventsLoser()
    {
        Player.Instance.SetState(Player.STATE_PLAYER.Lose);
        UIManager.Instance.ShowTextLose();
        // UIManager.Instance.ShowReplayBtn();
        gameState = GAME_STATE.Endgame;
        StartCoroutine(c_nextLevel());
    }
    public void CheckWin()
    {
        bool isNullEnemy = MapManager.Instance.CheckNullEnemy();
        if (isNullEnemy && isFinalWave())
        {
            Player.Instance.SetState(Player.STATE_PLAYER.Win);
            UIManager.Instance.ShowTextWin();
            // UIManager.Instance.ShowReplayBtn();
            // UIManager.Instance.ShowNextBtn();
            int currentLevel = PlayerPrefs.GetInt("Level", 0);
            currentLevel += 1;
            if(currentLevel >= 2)
            {
                currentLevel = 0;
            }
            PlayerPrefs.SetInt("Level", currentLevel);
            gameState = GAME_STATE.Endgame;
            StartCoroutine(c_nextLevel());
        }
    }
    private bool isFinalWave()
    {
        return MapManager.Instance.IsLastPhase();
    }
    private IEnumerator c_nextLevel()
    {
        yield return new WaitForSeconds(3f);
        ResetGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

}
