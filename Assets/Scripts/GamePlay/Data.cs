using Newtonsoft.Json;
using System.Collections;
//using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

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
            var readData = JsonConvert.DeserializeObject<PlayerData>(data);//
            gameData = readData;//lay ra game data

        }
    }
    public static bool[,] GetMatrix(string key)
    {
        if (gameData.matrix.ContainsKey(key))
        {
            return gameData.matrix[key];
        }
        else return null;
    }

    public static void AddData(string key, bool[,] value)
    {

        if (!gameData.matrix.ContainsKey(key))
        {
            gameData.matrix.Add(key, value);
            Debug.Log("add keymatrix:" + key);
        }

        if (!gameData.isdrawed.ContainsKey(key))
        {
            gameData.isdrawed.Add(key, true);
            Debug.Log("add key:" + key);

        }
        else { Debug.Log(gameData.isdrawed[key]); }
        if (!gameData.isComplete.ContainsKey(key))
        {
            gameData.isComplete.Add(key, false);
            Debug.Log("add key:" + key);

        }
        else { Debug.Log(gameData.isComplete[key]); }
        Save();
    }
    public static void IncreaseNumberWebCamPicture()
    {
        gameData.WebCamPictureCount++;
        Save();
    }

    public static void ClickTrue(string key, int i, int j)//save data everytime click true
    {
        if (gameData.matrix[key][i, j] == false)
        {
            gameData.matrix[key][i, j] = true;

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
    public Dictionary<string, bool[,]> matrix = new Dictionary<string, bool[,]>();
    public Dictionary<string, bool> isdrawed = new Dictionary<string, bool>();
    public Dictionary<string, bool> isComplete = new Dictionary<string, bool>();
    public int WebCamPictureCount = 0;



}