using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RemoteLoadTest : MonoBehaviour
{
    [Header("Settings")]
    public string remoteBasePath = "https://vuquocdung04.github.io/satismoment-data/Android/";

    private GameObject currentLevelInstance;
    private AsyncOperationHandle<GameObject> currentHandle;
    private bool isInitialized = false;

    public void Init()
    {
        InitializeRemoteAddressables();
    }
    
    private void InitializeRemoteAddressables()
    {
        Debug.Log("Initializing remote Addressables...");
        
        var initHandle = Addressables.InitializeAsync();
        initHandle.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                isInitialized = true;
                Debug.Log("✅ Remote Addressables initialized successfully!");
            }
            else
            {
                Debug.LogError("❌ Failed to initialize Addressables: " + handle.OperationException?.Message);
            }
        };
    }

    public void LoadLevelGameObject(int levelNumber)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Addressables not initialized yet!");
            return;
        }

        // Giải phóng level cũ
        UnloadCurrentLevel();

        // Tính thông tin level - DÙNG ĐƯỜNG DẪN ĐẦY ĐỦ
        string address = GetFullAssetPath(levelNumber);
        
        Debug.Log($"=== LOAD LEVEL INFO ===");
        Debug.Log($"Level: {levelNumber}");
        Debug.Log($"Full Address: {address}");
        Debug.Log($"=== STARTING LOAD ===");

        // Load và instantiate level
        currentHandle = Addressables.InstantiateAsync(address);
        currentHandle.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentLevelInstance = handle.Result;
                Debug.Log($"✅ SUCCESS: Level {levelNumber} loaded as GameObject!");
                Debug.Log($"Instance name: {currentLevelInstance.name}");
            }
            else
            {
                Debug.LogError($"❌ FAILED: Could not load level {levelNumber}");
                Debug.LogError($"Error: {handle.OperationException?.Message}");
            }
        };
    }

    private string GetFullAssetPath(int levelNumber)
    {
        if (levelNumber <= 170)
        {
            int zoneIndex = (levelNumber - 1) / 10 + 1;
            return $"Assets/00_BaseGame/02_Prefabs_Sprite/Prefabs/Zone{zoneIndex}/Level_{levelNumber}.prefab";
        }
        return $"Assets/00_BaseGame/02_Prefabs_Sprite/Prefabs/Zone18/Level_{levelNumber}.prefab";
    }

    public void UnloadCurrentLevel()
    {
        if (currentLevelInstance != null && currentHandle.IsValid())
        {
            Addressables.ReleaseInstance(currentHandle);
            currentLevelInstance = null;
            Debug.Log("Level unloaded");
        }
    }
}