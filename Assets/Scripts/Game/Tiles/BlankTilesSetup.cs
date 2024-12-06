using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Game.Tiles
{
    public class BlankTilesSetup
    {
        public bool [,] Blanks { get; private set; }

        public void SetupBlanks(LevelConfig levelConfig)
        {
            Blanks = new bool[levelConfig.Width,levelConfig.Height];
            for (int i = 0; i < levelConfig.BlankTilesLayout.Count; i++)
                Blanks[levelConfig.BlankTilesLayout[i].XPosition, 
                    levelConfig.BlankTilesLayout[i].YPosition] = true;
        }

    }
}