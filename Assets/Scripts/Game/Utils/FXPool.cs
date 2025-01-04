using System.Collections.Generic;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Utils
{
    public class FXPool
    {
        private readonly List<GameObject> _FXPool = new List<GameObject>();
        private GameObject _prefabFX;
        private IObjectResolver _objectResolver;
        private readonly GameResourcesLoader _resourcesLoader;

        public FXPool(IObjectResolver objectResolver, GameResourcesLoader resourcesLoader)
        {
            _objectResolver = objectResolver;
            _resourcesLoader = resourcesLoader;
        }

        public GameObject GetFXFromPool(Vector3 position, Transform transform)
        {
            for (int i = 0; i < _FXPool.Count; i++)
            {
                if(_FXPool[i].activeInHierarchy) continue;
                _FXPool[i].gameObject.transform.position = position;
                _FXPool[i].SetActive(true);
                return _FXPool[i];
            }
            var FX = CreateFX(position,transform);
            FX.SetActive(true);
            return FX;
        }

        private GameObject CreateFX(Vector3 position, Transform transform)
        {
            var FX = _objectResolver.Instantiate(_resourcesLoader.FXPrefab,
                position + Vector3.forward, Quaternion.identity, transform);
            _FXPool.Add(FX);
            return FX;
        }
        }
}