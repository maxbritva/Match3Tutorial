using System;
using System.Collections.Generic;
using Menu.Levels;
using UnityEngine;
using VContainer;

namespace Menu.UI
{
    public class LevelSequenceView : MonoBehaviour
    {
        [SerializeField] private List<StartLevelButton> _levelButtons
            = new List<StartLevelButton>();

        private SetupLevelSequence _setupLevel;
        
        private void OnValidate()
        {
            if(_levelButtons.Count !=5)
                throw new ArgumentOutOfRangeException(
                    "Level buttons must contain 5 elements");
        }

        public void SetupButtonsView(int currentLevel)
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].SetNumber(_setupLevel.CurrentLevelSequence.LevelSequence[i].LevelNumber);
                _levelButtons[i].SetLabel();
                if(_levelButtons[i].Number > currentLevel)
                    _levelButtons[i].SetButtonInteractable(false);
            }
        }
        
        
        [Inject] private void Construct(SetupLevelSequence setupLevel)
        {
            _setupLevel = setupLevel;
        }
    }
}