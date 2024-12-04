using Game.Board;
using Game.Tiles;
using Game.Utils;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameResourcesLoader _loader;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_loader);
            builder.Register<Grid>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
        }
    }
}