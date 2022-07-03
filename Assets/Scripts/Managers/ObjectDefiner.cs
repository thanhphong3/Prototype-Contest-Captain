using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDefiner : MonoBehaviour
{
    public static ObjectDefiner Instance;
    public Player player;
    public DestroyButton fixButton;

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
}
