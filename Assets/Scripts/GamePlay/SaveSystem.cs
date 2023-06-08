/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameManager.fun";//t?o path
        FileStream stream = new FileStream(path, FileMode.Create);        //l?u+gán path

        Data data = new Data(gameManager); //l?y Data

        formatter.Serialize(stream, data); //mã hoa data vao file
        stream.Close();
    }

    public static Data loadData()
    {
        string path = Application.persistentDataPath + "/gameManager.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;//mã hóa ng??c l?i
            stream.Close();

            return data;

        }
        else
        {

            Debug.Log("null file");
            return null;
        }
    }
}*/
