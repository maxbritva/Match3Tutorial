using Menu.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.UI
{
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lebel;
        [SerializeField] private Button _button;
        public int Number { get; private set; }
        private StartGame _startGame;
        private SetupLevelSequence _setupLevel;

        private void OnEnable() => _button.onClick.AddListener(StartLevelButtonClick);

        private void OnDisable() => _button.onClick.RemoveListener(StartLevelButtonClick);

        public void SetNumber(int value) => Number = 
            Mathf.Clamp(value, 1,10);

        public void SetLabel() => _lebel.text = Number.ToString();

        public void SetButtonInteractable(bool value) => _button.interactable = value;

        private void StartLevelButtonClick() =>
            _startGame.Start(_setupLevel.CurrentLevelSequence.LevelSequence[Number - 1]);

        [Inject] private void Construct(SetupLevelSequence setupLevel, StartGame startGame)
        {
            _setupLevel = setupLevel;
            _startGame = startGame;
        }
    }
}