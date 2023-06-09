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
    //public static List<Btn_loadGame1> GetListDrawed()
    //{
    //    Load();
    //    return gameData.list_drawed;
    //}
    //public static List<Btn_loadGame1> GetListDrawing()
    //{
    //    Load();
    //    return gameData.list_drawing;
    //}
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
    //public static void AddDrawed(Btn_loadGame1 input)
    //{
    //    if (!gameData.list_drawed.Contains(input))
    //    {
    //        gameData.list_drawed.Add(input);
    //    }
    //    else
    //    { Debug.Log("Added"); }

    //    Save();
    //}
    //public static void AddDrawing(Btn_loadGame1 input)
    //{
    //    if (!gameData.list_drawing.Contains(input))
    //    {
    //        gameData.list_drawing.Add(input);
    //    }
    //    else
    //    { Debug.Log("Added"); }

    //    Save();
    //}

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
    /*  public List<Btn_loadGame1> list_drawed = new List<Btn_loadGame1>();
      public List<Btn_loadGame1> list_drawing = new List<Btn_loadGame1>();*/


}