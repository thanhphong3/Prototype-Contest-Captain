using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using NaughtyAttributes;
// #if UNITY_EDITOR
// using UnityEditor;
// #endif

namespace Kinder.Minigame.NBA_1
{
    [CreateAssetMenu(menuName = "Level_Config")]
    public class LevelConfig : ScriptableObject
    {
        public List<ItemPhase> ListPhase;
    }

    [Serializable]
    public class ItemPhase
    {
        public List<ItemTurn> ListItemTurn;
    }

    [Serializable]
    public class ItemTurn
    {
        public float timeAppear;
        public List<ItemEnemy> ListEnemy_A;
        public List<ItemEnemy> ListEnemy_B;

    }
    public enum EnemyType
    {
        Tank = 0,
        Plane = 1,
    }
    [Serializable]
    public class ItemEnemy
    {
        public EnemyType enemyType;
        public int indexAppear;
    }
}
