using System;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _panelRectTransform;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;
        private IAnimation _animation;
        private AudioManager _audioManager;
        private EndGame _endGame;
        private CancellationTokenSource _cts;
        private bool _isWinCondition;

        private readonly string _win = "You have won!";
        private readonly string _loose = "You have loose!";

        private void OnEnable() => _closeButton.onClick.AddListener(ExitGame);

        private void OnDisable() => _closeButton.onClick.RemoveListener(ExitGame);

        public async void ShowEndGamePanel(bool isWinCondition)
        {
            _isWinCondition = isWinCondition;
            _title.text = isWinCondition ? _win : _loose;
            await StartAnimation();
            _closeButton.interactable = true;
        }

        private async UniTask StartAnimation()
        {
            _cts = new CancellationTokenSource();
            _audioManager.PlayWhoosh();
            _panel.SetActive(true);
            _animation.MoveUI(_panelRectTransform, new Vector3(0f,-150f,0f), 0.5f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
            _audioManager.StopMusic();
            if(_isWinCondition)
                _audioManager.PlayWin();
            else
                _audioManager.PlayLoose();
        }
        private void ExitGame() => _endGame.End(_isWinCondition);

        [Inject]
        private void Construct(AudioManager audioManager, EndGame endGame, IAnimation animation)
        {
            _animation = animation;
            _audioManager = audioManager;
            _endGame = endGame;
        }
    }
}