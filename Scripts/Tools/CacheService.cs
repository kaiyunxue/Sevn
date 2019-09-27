using UnityEngine;
using System.Collections;

public class CacheService : MonoBehaviour
{
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
        return CacheTool.Set(key, value);
    }

    public static string Get(string key)
    {
        return CacheTool.Get(key);
    }
}
