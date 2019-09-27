using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileTool : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool SaveInfoToFile(string path, string fileName, string info)
    {
        StreamWriter streamWriter;
        FileInfo t = new FileInfo(path + "//" + fileName);
        //重建文本
        streamWriter = t.CreateText();
        //以行的形式写入信息
        streamWriter.Write(info);
        //关闭流
        streamWriter.Close();
        //销毁流
        streamWriter.Dispose();
        return true;
    }

    public static string LoadInfoFromFile(string path, string fileName)
    {
        StreamReader streamReader;
        try
        {
            streamReader = File.OpenText(path + "//" + fileName);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        string info;
        info = streamReader.ReadToEnd();
        streamReader.Close();
        streamReader.Dispose();
        return info;
    }
}
