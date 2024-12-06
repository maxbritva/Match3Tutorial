using System.Collections.Generic;
using UnityEngine;

namespace Game.Tiles
{
    public class BlankTilesSetup : MonoBehaviour
    {
        [SerializeField] private List<BlankTile> _blankTilesLayout;
        
        public bool [,] Blanks { get; private set; }

        public void SetupBlanks(int width, int height)
        {
            Blanks = new bool[width,height];
            for (int i = 0; i < _blankTilesLayout.Count; i++)
                Blanks[_blankTilesLayout[i].XPosition, _blankTilesLayout[i].YPosition] = true;
        }

    }
}