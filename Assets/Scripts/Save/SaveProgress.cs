using Data;
using UnityEngine;

namespace save
{
    public class SaveProgress
    {
        private GameData _gameData;
        private const string CurrentLevel = "Level";
        private const string Sound = "Sound";

        public SaveProgress(GameData gameData) => _gameData = gameData;

        public void SaveData()
        {
            PlayerPrefs.SetInt(CurrentLevel, _gameData.CurrentLevelIndex);
            PlayerPrefs.SetInt(Sound, _gameData.IsEnabledSound ? 1 : 0);
        }

        public void LoadData()
        {
            _gameData.SetCurrentLevelIndex(PlayerPrefs.GetInt(CurrentLevel) >= 1
                ? PlayerPrefs.GetInt(CurrentLevel)
                : 1);
            _gameData.SetEnabledSound(PlayerPrefs.GetInt(Sound) != 0);
        }
    }
}