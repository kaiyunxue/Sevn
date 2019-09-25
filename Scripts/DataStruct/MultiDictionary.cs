using System.Collections.Generic;
using UnityEngine;

public class MultiDictionary<Key1, Key2, Value>
{
    /// <summary>
    /// 字典结构
    /// </summary>
    Dictionary<Key1, Dictionary<Key2, Value>> mDict1;

    /// <summary>
    /// 赋值
    /// </summary>
    public void Set(Key1 key1, Key2 key2, Value value)
    {
        if (mDict1.ContainsKey(key1))
        {
            var dict2 = mDict1[key1];
            if (dict2.ContainsKey(key2))
                dict2[key2] = value;
            else
                dict2.Add(key2, value);
        }
        else
        {
            var dict2 = new Dictionary<Key2, Value>();
            dict2.Add(key2, value);
            mDict1.Add(key1, dict2);
        }
    }

    /// <summary>
    /// 取值
    /// </summary>
    public Value Get(Key1 key1, Key2 key2, Value defaultValue = default(Value))
    {
        if (mDict1.ContainsKey(key1))
        {
            var dict2 = mDict1[key1];
            if (dict2.ContainsKey(key2))
                return dict2[key2];
        }
        return defaultValue;
    }

    public MultiDictionary() {
        mDict1 = new Dictionary<Key1, Dictionary<Key2, Value>>();
    }
}