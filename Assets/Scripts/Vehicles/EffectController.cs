using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] Transform shotPoint;

    [Header("Game-feel side")]
    [SerializeField] float shakeAmount;
    [SerializeField] float shakeTime;
    public void SpawnFireEffect()
    {
        ObjectDefiner.Instance.effectPool.GetEffectFromPool(0, shotPoint.position);
        MinigameManager.Instance.CameraShake(shakeAmount, shakeTime);
    }
    public void SpawnFixedEffect()
    {
        ObjectDefiner.Instance.effectPool.GetEffectFromPool(1, shotPoint.position);
        MinigameManager.Instance.CameraShake(shakeAmount, shakeTime);
    }
}
