using System.Collections.Generic;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TilePool
    {
     
        private List<Tile> _tilePool = new List<Tile>();
        private IObjectResolver _objectResolver;
        private GameResourcesLoader _resourcesLoader;
        
        public TilePool(IObjectResolver objectResolver, GameResourcesLoader resourcesLoader)
        {
            _objectResolver = objectResolver;
            _resourcesLoader = resourcesLoader;
        }

        public Tile GetTile(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilePool.Count; i++)
            {
                if(_tilePool[i].gameObject.activeInHierarchy) continue;
                _tilePool[i].SetTileConfig(GetRandomTileConfig());
                _tilePool[i].gameObject.transform.position = position;
                return _tilePool[i];
            }
            var tile = CreateTile(position, parent);
            return tile;
        }
        
        private Tile CreateTile(Vector3 position, Transform parent)
        {
            var tilePrefab = _objectResolver.Instantiate(_resourcesLoader.TilePrefab, 
                position, Quaternion.identity, parent);
            var tile = tilePrefab.GetComponent<Tile>();
            tile.SetTileConfig(GetRandomTileConfig());
            _tilePool.Add(tile);
            return tile;
        }

        private TileConfig GetRandomTileConfig() =>
            _resourcesLoader.TileSetConfig.Set[Random.Range(0, _resourcesLoader.TileSetConfig.Set.Count)];
    }
}