using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public InputField inputField;
    public string currentPlayerName;
    public string highestScorePlayerName;
    public int highScore;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        if (inputField.text != "")
        {
            currentPlayerName = inputField.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            inputField.image.color = Color.red;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string highestScorePlayerName;
        public int highScore;
    }

    public void SavePlayerInfos()
    {
        SaveData data = new SaveData();
        data.highestScorePlayerName = highestScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerInfos()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScorePlayerName = data.highestScorePlayerName;
            highScore = data.highScore;
        }
    }
}
