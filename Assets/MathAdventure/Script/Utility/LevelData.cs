using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathAdventure.Char;

namespace MathAdventure.Level {
    [CreateAssetMenu(fileName = "New Level Data", menuName = "MathAdventure/Level")]
    public class LevelData : ScriptableObject
    {
        public List<Level> levels;
    }
    [System.Serializable]
    public struct Level{
        public string title;
        public List<CharData> enemySlot;
        public Sprite background;
    }
}
