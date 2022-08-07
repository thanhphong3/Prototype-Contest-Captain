using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDefiner : MonoBehaviour
{
    public static ObjectDefiner Instance;
    public Player player;
    public DestroyButton fixButton;
    public SkillButton skillButton;
    public RageSkillButton rageSkillButton;
    public EffectPool effectPool;
    public CamerasController camController;
    public GameObject mainMenuText;
    public GameObject ingameCanvas;
    [SerializeField] Text countingTxt;

    private void Awake() {
        Instance = this;
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        
    }
    // public void LinkWithFixButton(GameObject vehicle)
    // {
    //     vehicle.GameObject<>().LinkWithButton();
    // }
    public void DelinkFixButton()
    {
        fixButton.Hide();
    }
    public bool GetFixButtonActive()
    {
        return fixButton.gameObject.active;
    }
    public void SkillPress()
    {
        if(skillButton.CanUse())
        {
            skillButton.Use();
            player.SpeedUpSkill();
        }
    }
    public void RageSkillPress()
    {
        if(rageSkillButton.CanUse())
        {
            rageSkillButton.Use();
            player.RageSkill();
        }
    }
    public void EnterGame()
    {
        camController.ChangeIngameCamera();
        StartCoroutine(Counting());
    }
    private IEnumerator Counting()
    {
        mainMenuText.SetActive(false);
        for(int i = 3; i > 0; i--)
        {
            countingTxt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countingTxt.gameObject.SetActive(false);
        ingameCanvas.SetActive(true);
    }
}
