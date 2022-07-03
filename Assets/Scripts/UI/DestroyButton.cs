using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    [SerializeField] Image filler;
    private int maxValue;
    private int currentValue;
    // [SerializedField] Camera cam;
    private Camera cam;
    private CombatVehicle target;
    // Start is called before the first frame update
    void Start()
    {
        cam = CamerasController.Instance.mainCam;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
            transform.position = cam.WorldToScreenPoint(target.transform.position);
    }
    public void Hide()
    {
        target = null;
        gameObject.SetActive(false);
    }
    public void Link(CombatVehicle vehicle)
    {
        gameObject.SetActive(true);
        target = vehicle;
        target.GetHP(out maxValue, out int currentValue);
        SetMaxFillerValue(maxValue);
    }
    private void ResetUI()
    {
        filler.fillAmount = 1;
    }
    private void SetMaxFillerValue(int _value)
    {
        maxValue = _value;
        ResetUI();
    }
    private void RefreshFiller()
    {
        filler.fillAmount = (float)currentValue / (float)maxValue;
    }
    public void ButtonClick()
    {
        int dmg = 1; //hardcode
        target.TakeDmg(dmg);
        if(target!=null)
        {
            target.GetHP(out maxValue, out currentValue);
            RefreshFiller();
        }
    }
}
