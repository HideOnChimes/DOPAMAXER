using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public abstract class BaseSaveData<T> where T : BaseSaveData<T>, new()
{
    private static T _current;
    public static T current
    {
        get
        {
            return _current ??= new T();
        }

        set
        {
            _current = value;
        }
    }
}
