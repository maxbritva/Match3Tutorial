using Menu;
using Menu.Levels;
using Menu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private LevelSequenceView _sequenceView;
        [SerializeField] private MenuView _menuView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.Register<SetupLevelSequence>(Lifetime.Singleton);
            builder.RegisterInstance(_sequenceView);
            builder.RegisterInstance(_menuView);
        }
    }
}