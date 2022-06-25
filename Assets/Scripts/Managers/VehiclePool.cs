using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePool : MonoBehaviour
{
    [SerializeField] private Transform[] pool;
    const int RED_TEAM_INDEX = 0;
    const int BLUE_TEAM_INDEX = 1;

    // Start is called before the first frame update
    public void SpawnVehicleInPool(int _teamIndex, Vector3 _position)
    {
        for(int i = 0; i < pool[_teamIndex].childCount; i++)
        {
            if(!pool[_teamIndex].GetChild(i).gameObject.active)
            {
                pool[_teamIndex].GetChild(i).gameObject.GetComponent<CombatVehicle>().Spawn(_position);
                return;
            }
        }
    }
}
