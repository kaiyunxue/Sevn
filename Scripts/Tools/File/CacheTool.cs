using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data<T>
{
    public List<T> items;
}

[System.Serializable]
public class DataItem
{
    public string key;
    public string value;
}

public class CacheTool : MonoBehaviour
{
    private static string CacheFileName = "cache.json";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool Set(string key, string value)
    {
        string jsonData = FileTool.LoadInfoFromFile(Application.persistentDataPath, CacheFileName);
        if (jsonData == null)
        {
            Data<DataItem> dt = new Data<DataItem>();
            DataItem dataItem = new DataItem();
            dt.items = new List<DataItem>();
            dataItem.key = key;
            dataItem.value = value;
            dt.items.Add(dataItem);
            jsonData = JsonUtility.ToJson(dt, false);
        }
        else
        {
            bool isSet = false;
            Data<DataItem> dt = JsonUtility.FromJson<Data<DataItem>>(jsonData);
            foreach(var item in dt.items)
            {
                if (item.key == key)
                {
                    item.value = value;
                    isSet = true;
                    break;
                }
            }
            if (!isSet)
            {
                DataItem dataItem = new DataItem();
                dataItem.key = key;
                dataItem.value = value;
                dt.items.Add(dataItem);
            }
            jsonData = JsonUtility.ToJson(dt, false);
        }
        return FileTool.SaveInfoToFile(Application.persistentDataPath, CacheFileName, jsonData);
    }

    public static string Get(string key)
    {
        string jsonData = FileTool.LoadInfoFromFile(Application.persistentDataPath, CacheFileName);
        if(jsonData == null)
        {
            return null;
        }
        Data<DataItem> dt = JsonUtility.FromJson<Data<DataItem>>(jsonData);
        foreach(var item in dt.items)
        {
            if (item.key == key)
            {
                return item.value;
            }
        }
        return null;
    }
}
