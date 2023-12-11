using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class DataManager : PersistentSingleton<DataManager>
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SyncDB();
#endif

    public PlayerData SavedData;

    [SerializeField] private InventorySO _inventory;
    [SerializeField] private YandexManager _yandexManager;

    private string _path => Application.persistentDataPath + Path.AltDirectorySeparatorChar + "CircusTilesSave.json";

    protected override void Awake()
    {
        base.Awake();

        _yandexManager.YandexInitialized += LoadData;
    }

    public void SaveData(PlayerData data)
    {
        SavedData = data;
        string json = JsonUtility.ToJson(data);
        string encodedJson = CustomEncoding.Base64Encode(json);

#if !UNITY_WEBGL || UNITY_EDITOR
        encodedJson = json;
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.SetCloudSaveData(json, ()=>Debug.Log("Saved to cloud"), (data) => Debug.Log("Couldn't save " + data));
            //SetLeaderboardNewValue();
        }
#endif

        File.WriteAllText(_path, encodedJson);
        Debug.Log("succesfull written data" + json);

#if UNITY_WEBGL && !UNITY_EDITOR
        SyncDB();
#endif
    }

    public void LoadData()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("if (PlayerAccount.IsAuthorized)" + PlayerAccount.IsAuthorized);
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.GetCloudSaveData(LoadFromCloud, LoadDataFromFile);
        else
        {
            LoadDataFromFile("");
        } 
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        LoadDataFromFile();
#endif

    }

    public void LoadFromCloud(string text)
    {
        string json;
        Debug.Log("loaded from cloud" + text);
        if (string.IsNullOrEmpty(text) == false && text != "{}")
        {
            if (CustomEncoding.IsBase64String(text))
                json = CustomEncoding.Base64Decode(text);
            else
                json = text;

            SavedData = JsonUtility.FromJson<PlayerData>(json);
            _inventory.LoadInventory(SavedData.TipsData);
            LoadNextScene();
        }
        else
        {
            LoadDataFromFile();
            return;
        }
    }

    public void LoadDataFromFile(string data = null)
    {
        string text = "";
        Debug.Log("Does save file exists: " + File.Exists(_path));
        if (File.Exists(_path))
        {
            text = File.ReadAllText(_path);
            string json;
            if (CustomEncoding.IsBase64String(text))
                json = CustomEncoding.Base64Decode(text);
            else
                json = text;
            Debug.Log(json);
            SavedData = JsonUtility.FromJson<PlayerData>(json);
            _inventory.LoadInventory(SavedData.TipsData);
            Debug.Log("Loaded from local storage" + json);
            LoadNextScene();

        }
        else
        {
            Debug.Log("Save doesn't exists ... creating new game");
            _inventory.InitializeWithStartingData();
            SavedData = new PlayerData(0,false, 0, _inventory.Items);
            SaveData(SavedData);
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveAsIfNextLevelCompleted()
    {
        PlayerData dataToSave = new PlayerData(SavedData.Level + 1, SavedData.AdsTurnedOff, SavedData.Money, SavedData.TipsData);
        SaveData(dataToSave);
    }
}
