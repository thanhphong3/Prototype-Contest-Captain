using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    [SerializeField] private Transform[] pools;
    [SerializeField] private GameObject[] effects;
    const int FIRE_EFFECT_INDEX = 0;
    const int FIXED_EFFECT_INDEX = 1;

    public void GetEffectFromPool(int _index, Vector3 _position)
    {
        if(pools[_index].childCount == 0)
        {
            CreateEffect(_index, _position);
            return;
        }
        else
        {
            for(int i = 0; i < pools[_index].childCount ; i++)
            {
                if(!pools[_index].GetChild(i).gameObject.active)
                {
                    pools[_index].GetChild(i).gameObject.SetActive(true);
                    pools[_index].GetChild(i).gameObject.transform.position = _position;
                    return;
                }
            }
            CreateEffect(_index, _position);
            return;
        }
    }
    private void CreateEffect(int _index, Vector3 _position)
    {
        Instantiate(effects[_index], _position, Quaternion.identity, pools[_index]);
    }
}
