using Cysharp.Threading.Tasks;

namespace SceneLoading
{
    public interface IAsyncSceneLoading
    {
        UniTask LoadAsync(string sceneName);
        
        UniTask UnloadAsync(string sceneName);
        
        void LoadingIsDone(bool value);
    }
}