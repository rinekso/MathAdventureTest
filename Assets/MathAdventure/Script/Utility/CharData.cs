using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathAdventure.Char {
    [CreateAssetMenu(fileName = "New Char Data", menuName = "MathAdventure/Char")]
    public class CharData : ScriptableObject
    {
        public string name;
        public Side side;
        public Sprite icon;
        public GameObject prefabs;
        public Properties properties;
    }
    public enum Side
    {
        HERO, ENEMY, NETRAL
    }
    [System.Serializable]
    public struct Properties{
        public float HP;
        public float attackDemage;
        public float attackInterval;
    }
}
