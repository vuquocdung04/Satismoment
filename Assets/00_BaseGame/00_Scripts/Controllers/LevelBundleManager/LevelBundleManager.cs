using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelBundleManager : MonoBehaviour
{
    private GameObject currentLevelInstance;
    private AsyncOperationHandle<GameObject> currentHandle;
    private string basePath = "Assets/00_BaseGame/02_Prefabs_Sprite/Prefabs/";
    
    public void LoadCurrentLevel()
    {
        int selectedLevel = UseProfile.CurrentLevel;
        LoadLevelByNumber(selectedLevel);
    }

    /// <summary>
    /// Load level theo số level
    /// </summary>
    public void LoadLevelByNumber(int levelNumber)
    {
        try
        {
            // Giải phóng level cũ
            UnloadCurrentLevel();

            // Tính zone và tạo địa chỉ level
            string levelAddress = GetLevelAddress(levelNumber);
            
            if (string.IsNullOrEmpty(levelAddress))
            {
                Debug.LogError($"Invalid level number: {levelNumber}");
                return;
            }

            // Load level từ Addressables theo địa chỉ cụ thể
            LoadLevelByAddress(levelAddress);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading level {levelNumber}: {ex.Message}");
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

        // Level 1-170: Zone 1-17 (10 levels mỗi zone)
        if (levelNumber <= 170)
        {
            int zoneIndex = (levelNumber - 1) / 10 + 1; // Zone 1-17
            int levelInZone = ((levelNumber - 1) % 10) + 1; // Level 1-10
            
            return $"{basePath}Zone{zoneIndex}/Level_{levelInZone}.prefab";
        }
        
        // Level 171-185: Zone 18
        return $"{basePath}Zone18/Level_{levelNumber}.prefab";
    }

    /// <summary>
    /// Load level theo địa chỉ Addressable
    /// </summary>
    private void LoadLevelByAddress(string levelAddress)
    {
        Debug.LogWarning($"Trying to load level: {levelAddress}");
        
        currentHandle = Addressables.InstantiateAsync(levelAddress);
        
        currentHandle.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentLevelInstance = handle.Result;
                Debug.Log($"Successfully loaded level: {levelAddress}");
            }
            else
            {
                Debug.LogError($"Failed to load level: {levelAddress}");
                Debug.LogError($"Error: {handle.OperationException?.Message}");
                Debug.LogError($"Status: {handle.Status}");
            }
        };
    }
    /// <summary>
    /// Giải phóng level hiện tại
    /// </summary>
    public void UnloadCurrentLevel()
    {
        if (currentLevelInstance != null && currentHandle.IsValid())
        {
            Addressables.ReleaseInstance(currentHandle);
            currentLevelInstance = null;
            Debug.Log("Current level unloaded");
        }
    }

}