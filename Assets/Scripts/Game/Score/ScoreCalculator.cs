using Game.MatchTiles;
using UnityEngine;

namespace Game.Score
{
    public class ScoreCalculator
    {
        private GameProgress _gameProgress;

        public ScoreCalculator(GameProgress gameProgress) => 
            _gameProgress = gameProgress;

        public void CalculateScoreToAdd(MatchDirection matchDirection)
        {
            switch (matchDirection)
            {
                case MatchDirection.Horizontal:
                case MatchDirection.Vertical:
                    _gameProgress.AddScore(20);
                    Debug.Log("+20 score");
                    break;
                case MatchDirection.LongHorizontal:
                case MatchDirection.LongVertical:
                    _gameProgress.AddScore(50);
                    Debug.Log("+50 score");
                    break;
                case MatchDirection.Multiply:
                    _gameProgress.AddScore(200);
                    Debug.Log("+200 score");
                    break;
            }
        }
    }
}