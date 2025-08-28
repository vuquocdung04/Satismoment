using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelBundleManager : MonoBehaviour
{
    private AsyncOperationHandle<GameObject> currentHandle;
    private AsyncOperationHandle<GameObject> preloadHandle;
    private const string basePath = "Assets/00_BaseGame/02_Prefabs_Sprite/Prefabs/";

    /// <summary>
    /// HÀM PRELOAD: Load asset vào bộ nhớ (gọi ở Scene Loading)
    /// </summary>
    public void PreloadLevelAsset(int levelNumber, System.Action<bool> onComplete = null)
    {
        try
        {
            string levelAddress = GetLevelAddress(levelNumber);
            
            if (string.IsNullOrEmpty(levelAddress))
            {
                Debug.LogError($"Invalid level number for preload: {levelNumber}");
                onComplete?.Invoke(false);
                return;
            }

            Debug.Log($"Preloading level asset: {levelAddress}");
            
            // Chỉ load asset vào bộ nhớ, không tạo GameObject
            preloadHandle = Addressables.LoadAssetAsync<GameObject>(levelAddress);
            
            preloadHandle.Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"✅ Level {levelNumber} asset preloaded successfully!");
                    onComplete?.Invoke(true);
                }
                else
                {
                    Debug.LogError($"❌ Failed to preload level {levelNumber} asset");
                    onComplete?.Invoke(false);
                }
            };
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error preloading level {levelNumber}: {ex.Message}");
            onComplete?.Invoke(false);
        }
    }

    /// <summary>
    /// HÀM SINH: Tạo GameObject từ asset đã preload (gọi ở GamePlayController)
    /// </summary>
    public void InstantiateLevelFromPreloaded(int levelNumber, System.Action<GameObject> onComplete = null)
    {
        try
        {
            string levelAddress = GetLevelAddress(levelNumber);
            
            if (string.IsNullOrEmpty(levelAddress))
            {
                Debug.LogError($"Invalid level number for instantiate: {levelNumber}");
                onComplete?.Invoke(null);
                return;
            }

            Debug.LogWarning($"Instantiating level from preloaded asset: {levelAddress}");
            
            // Tạo GameObject từ asset đã preload (sẽ rất nhanh)
            currentHandle = Addressables.InstantiateAsync(levelAddress);
            
            currentHandle.Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"✅ Successfully instantiated level: {levelAddress}");
                    onComplete?.Invoke(handle.Result);
                }
                else
                {
                    Debug.LogError($"❌ Failed to instantiate level: {levelAddress}");
                    onComplete?.Invoke(null);
                }
            };
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error instantiating level {levelNumber}: {ex.Message}");
            onComplete?.Invoke(null);
        }
    }

    /// <summary>
    /// Giải phóng level hiện tại
    /// </summary>
    public void UnloadCurrentLevel()
    {
        if (currentHandle.IsValid())
        {
            Addressables.ReleaseInstance(currentHandle);
            Debug.Log("Current level unloaded");
        }
    }

    /// <summary>
    /// Giải phóng asset đã preload
    /// </summary>
    public void UnloadPreloadedAsset()
    {
        if (preloadHandle.IsValid())
        {
            Addressables.Release(preloadHandle);
            Debug.Log("Preloaded asset unloaded");
        }
    }

    /// <summary>
    /// Tạo địa chỉ Addressable cho level
    /// </summary>
    private string GetLevelAddress(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > 185)
        {
            Debug.LogError($"Level number {levelNumber} is out of range (1-185)");
            return null;
        }
        if (levelNumber <= 170)
        {
            int zoneIndex = (levelNumber - 1) / 10 + 1; // Zone 1-17
            return $"{basePath}Zone{zoneIndex}/Level_{levelNumber}.prefab";
        }
        // Level 171-185: Zone 18
        return $"{basePath}Zone18/Level_{levelNumber}.prefab";
    }
}