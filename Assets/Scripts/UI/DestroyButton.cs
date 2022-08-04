using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    [SerializeField] Image filler;
    [SerializeField] GameObject container; 

    public bool wasClicked;

    private int maxValue;
    private int currentValue;
    private Player player;
    // [SerializedField] Camera cam;
    private Camera cam;
    private CombatVehicle target;
    // Start is called before the first frame update
    void Start()
    {
        cam = CamerasController.Instance.mainCam;
        player = ObjectDefiner.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            // Vector3 buttonPos = new Vector3(target.transform.position.x, target.transform.position.y + yDeviatedDis, target.transform.position.x) ;
            transform.position = cam.WorldToScreenPoint(target.transform.position);
        }
    }
    public void Hide()
    {
        target = null;
        container.SetActive(false);
    }
    public void Link(CombatVehicle vehicle)
    {
        container.SetActive(true);
        target = vehicle;
        RefreshFiller();
    }
    private void ResetUI()
    {
        filler.fillAmount = 1;
    }
    private void RefreshFiller()
    {
        target.GetHP(out maxValue, out currentValue);
        filler.fillAmount = (float)currentValue / (float)maxValue;
    }
    public void ButtonClick()
    {
        wasClicked = true;
        int dmg = player.GetDmg();
        target.TakeDmg(dmg);
        if(target!=null)
        {
            RefreshFiller();
            ObjectDefiner.Instance.effectPool.GetEffectFromPool(2, target.gameObject.transform.position);
        }
        else
        {
            ResetUI();
        }
    }
    public void ButtonClickOut()
    {
        wasClicked = false;
    }
    public bool GetWasClicked()
    {
        return wasClicked;
    }
}
