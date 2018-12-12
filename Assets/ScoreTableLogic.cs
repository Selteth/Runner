using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreTableLogic : MonoBehaviour {
    private static string DATA_FILE = "/scoreTable.dat";
    private static bool isSaved = false;
    void Awake()
    {
        if (ScoreTable == null)
            Load();
        if (ScoreTable == null)
            ScoreTable = new List<int>();
    }

    private static List<int> ScoreTable = null;//new List<int>();
    public void Add(int res)
    {
        ScoreTable.Add(res);
        isSaved = false;
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<int>GetResults()
    {
        ScoreTable.Sort();
        ScoreTable.Reverse();
        return ScoreTable.AsReadOnly();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + DATA_FILE, FileMode.OpenOrCreate);
        ScoreTable.Sort();
        ScoreTable.Reverse();
        bf.Serialize(fs, ScoreTable);
        fs.Close();
        isSaved = true;
        Debug.Log("DATA saved");
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath+ DATA_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + DATA_FILE, FileMode.Open);
            //Debug.Log("PERSISTENT DATA DIRECTORY IS: "+Application.persistentDataPath);
            ScoreTable = (List<int>)bf.Deserialize(fs);
            fs.Close();
        }
    }
    void OnDestroy()
    {
        if (!isSaved)
            Save();
    }
    void OnApplicationQuit()
    {
        if (!isSaved)
            Save();
    }
}
