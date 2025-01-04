using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Game.Tiles;
using Levels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourcesLoading
{
    public class GameResourcesLoader
    {
        public GameObject TilePrefab { get; private set; }
        public GameObject BackgroundTilePrefab { get; private set; }
        public GameObject FXPrefab { get; private set; }
        public TileConfig BlankConfig { get; private set; }
        public Sprite DarkTile { get; private set; }
        public Sprite LightTile { get; private set; }

        public List<TileConfig> CurrentTileSet { get; private set; }

        private GameData _gameData;
        private CancellationTokenSource _cts;
        public GameResourcesLoader(GameData gameData) => _gameData = gameData;

        public async UniTask Load()
        {
            CurrentTileSet = new List<TileConfig>();
            if (_gameData.CurrentLevel.TileSets == TileSets.Kingdom) 
                await LoadSet("Kingdom");
            if (_gameData.CurrentLevel.TileSets == TileSets.Gem) 
                await LoadSet("Gem");
            await LoadTilePrefabs();
            await LoadBlankTile();
            await LoadBackgroundSprites();
        }

        private async UniTask LoadSet(string key)
        {
            _cts = new CancellationTokenSource();
            var set = Addressables.LoadAssetAsync<TileSetConfig>(key);
            await set.ToUniTask();
            if (set.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentTileSet = set.Result.Set;
                Addressables.Release(set);
            }
            _cts.Cancel();
        }

        private async UniTask LoadTilePrefabs()
        {
            _cts = new CancellationTokenSource();
            var tile = 
                Addressables.LoadAssetAsync<GameObject>("TilePrefab");
            var backgroundTile = 
                Addressables.LoadAssetAsync<GameObject>("BackgroundTile");
            var FX = 
                Addressables.LoadAssetAsync<GameObject>("FXPrefab");
            await tile.ToUniTask();
            await backgroundTile.ToUniTask();
            await FX.ToUniTask();
            if (tile.Status == AsyncOperationStatus.Succeeded && backgroundTile.Status ==
                AsyncOperationStatus.Succeeded && FX.Status == AsyncOperationStatus.Succeeded)
            {
                TilePrefab = tile.Result;
                BackgroundTilePrefab = backgroundTile.Result;
                FXPrefab = FX.Result;
                 Addressables.Release(tile);
                 Addressables.Release(backgroundTile);
                 Addressables.Release(FX);
            }
            _cts.Cancel();
        }
        
        private async UniTask LoadBlankTile()
        {
            _cts = new CancellationTokenSource();
            var blank = Addressables.LoadAssetAsync<TileConfig>("BlankTile");
            await blank.ToUniTask();
            if (blank.Status == AsyncOperationStatus.Succeeded)
            {
                BlankConfig = blank.Result;
                Addressables.Release(BlankConfig);
            }
            _cts.Cancel();
        }
        
        private async UniTask LoadBackgroundSprites()
        {
            _cts = new CancellationTokenSource();
            var darkSprite = Addressables.LoadAssetAsync<Sprite>("DarkBG");
            var lightSprite = Addressables.LoadAssetAsync<Sprite>("LightBG");
            await darkSprite.ToUniTask();
            await lightSprite.ToUniTask();
            if (lightSprite.Status == AsyncOperationStatus.Succeeded && 
                darkSprite.Status == AsyncOperationStatus.Succeeded )
            {
                DarkTile = darkSprite.Result;
                LightTile = lightSprite.Result;
                Addressables.Release(darkSprite);
                Addressables.Release(lightSprite);
            }
            _cts.Cancel();
        }
    }
}