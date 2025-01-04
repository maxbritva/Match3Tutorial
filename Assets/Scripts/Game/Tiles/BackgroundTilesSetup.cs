using System;
using System.Threading;
using Animations;
using Cysharp.Threading.Tasks;
using ResourcesLoading;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Game.Tiles
{
    public class BackgroundTilesSetup : IDisposable
    {
        private readonly GameResourcesLoader _resourcesLoader;
        private IAnimation _animation;
        private CancellationTokenSource _cts;
        private IObjectResolver _objectResolver;
        
        public BackgroundTilesSetup(GameResourcesLoader resourcesLoader, 
            IObjectResolver objectResolver, IAnimation animation)
        {
            _animation = animation;
            _resourcesLoader = resourcesLoader;
            _objectResolver = objectResolver;
        }
        
        public void Dispose()
        {
            _cts?.Dispose();
            _objectResolver?.Dispose();
        }

        public async UniTask SetupBackground(Transform parent, 
            bool[,] blanks, int width, int height)
        {
            _cts = new CancellationTokenSource();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(blanks[x,y]) continue;
                    var backgroundTile = CreateBackgroundTile(
                        new Vector3(x,y, 0.1f), parent);
                    if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0)
                        backgroundTile.GetComponent<SpriteRenderer>().sprite =
                            _resourcesLoader.DarkTile;
                    else
                        backgroundTile.GetComponent<SpriteRenderer>().sprite =
                            _resourcesLoader.LightTile;
                    var duration = Random.Range(0.8f, 1.5f);
                    _animation.Reveal(backgroundTile, duration);
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }

        private GameObject CreateBackgroundTile(Vector3 position, Transform parent) =>
            _objectResolver.Instantiate(_resourcesLoader.BackgroundTilePrefab,
                position, quaternion.identity, parent);
        

    }
}