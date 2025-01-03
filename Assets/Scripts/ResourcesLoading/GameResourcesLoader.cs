using Game.Tiles;
using UnityEngine;

namespace ResourcesLoading
{
    public class GameResourcesLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject _blankPrefab;
        [SerializeField] private TileConfig _blankConfig;
        [SerializeField] private TileSetConfig _tileSetConfig;

        public GameObject TilePrefab => _tilePrefab;
        public TileSetConfig TileSetConfig => _tileSetConfig;

        public GameObject BlankPrefab => _blankPrefab;
        public TileConfig BlankConfig => _blankConfig;
    }
}