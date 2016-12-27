
using UnityEngine;
using System;
using System.Collections;

public class UUIDManager : SingletonMonoBehaviour<UUIDManager>
{
    Guid guid;
    [SerializeField]
    string _uuid = "";
    public string uuid
    {
        get
        {
            if (_uuid == "" && HasUUID())
            {
                Load();
            }
            return _uuid;
        }
        private set
        {
            _uuid = value;
        }
    }

    void Start()
    {
        if (!HasUUID())
        {
            Create();
            Save();
        }
        else if (uuid == "")
        {
            Load();
        }
    }

    public void Create()
    {
        guid = Guid.NewGuid();
        uuid = guid.ToString();
    }

    public void Save()
    {
        SaveData.SetString("uuid", uuid);
        SaveData.Save();
        Debug.Log("uuid Save");
    }

    public void Load()
    {
        uuid = SaveData.GetString("uuid");
        Debug.Log("uuid Load");
    }

    public void Delete()
    {
        SaveData.Remove("uuid");
    }

    public bool HasUUID()
    {
        if (SaveData.GetString("uuid").Length > 0)
            return true;
        return false;
    }

}