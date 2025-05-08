using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;

/*
    save and load game data via a binary formatter for security and privacy
*/

public static class SaveManager
{
    public static void SaveScore(ScoreManager scoreManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/HighScore.sb";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(scoreManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/HighScore.sb";
    
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData =  formatter.Deserialize(stream) as GameData;
            stream.Close();
            
            return gameData;
        }
        else
        {
            return null;
        }
    }
}