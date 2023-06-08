using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[System.Serializable]
public static class Data
{
    static string saveData = "savedata";
    public static PlayerData gameData = new PlayerData();
    public static void Save()
    {
        string data = JsonConvert.SerializeObject(gameData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });//ma hoa data thanh json//cho phepluu anh 2 chieu
        PlayerPrefs.SetString(saveData, data);  // tap PlayerPrefs cho json
    }
    public static void Load()
    {
        if (!PlayerPrefs.HasKey(saveData))//neu chua tung tao PlayerPrefs cho jso
        {
            Save();//CreatePlayerPrefs
        }
        else
        {
            var data = PlayerPrefs.GetString(saveData);//if co roi thi lay ra
            Debug.Log(data);
            var readData = JsonConvert.DeserializeObject<PlayerData>(data);//
            gameData = readData;//lay ra game data

        }
    }
    public static bool[,] GetMatrix(string key)
    {
        Load();
        if (gameData.allSaves.ContainsKey(key))
        {
            return gameData.allSaves[key];
        }
        else return null;
    }
    public static void AddData(string key, bool[,] value)
    {
        if (!gameData.allSaves.ContainsKey(key))
        {
            gameData.allSaves.Add(key, value);
        }
        else
        { Debug.Log("not find gamedata"); }

        Save();
    }

    public static void ClickTrue(string key, int i, int j)//save data everytime click true
    {
        if (gameData.allSaves[key][i, j] == false)
        {
            gameData.allSaves[key][i, j] = true;

        }
        /*  if (gameData.textureBlackWhite.ContainsKey(key))
          {
              textureBlackWhite.SetPixel(i, j, texture.GetPixel(i, j));
          }*/
        Save();
    }

}

public class PlayerData
{
    public Dictionary<string, bool[,]> allSaves = new Dictionary<string, bool[,]>();


}