using System;
using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Menu.Levels
{
    [CreateAssetMenu(fileName = "LevelSequenceConfig", menuName = "Configs/LevelSequenceConfig")]
    public class LevelSequenceConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelSequence = new List<LevelConfig>();
        public List<LevelConfig> LevelSequence => _levelSequence;

        private void OnValidate()
        {
            if(_levelSequence.Count !=5)
                throw new ArgumentOutOfRangeException(
                    "Levels sequence must contain 5 elements");
        }
    }
}