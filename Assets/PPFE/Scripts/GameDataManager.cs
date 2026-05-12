using UnityEngine;
using System.IO;


public class GameDataManager
{
    public bool SaveGameData(GameData gameData, string filename)
    {
        string data = JsonUtility.ToJson(gameData);
        string path = Application.dataPath + "/" + filename;
        File.WriteAllText(path, data);
        return true;
    }

    public GameData LoadGameData(string filename)
    {
        GameData gameData = new GameData();
        string path = Application.dataPath + "/" + filename;

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            SaveGameData(gameData, filename);
        }
        return gameData;
    }
}
