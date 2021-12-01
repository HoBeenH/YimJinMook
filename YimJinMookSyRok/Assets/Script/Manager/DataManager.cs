using System;
using System.IO;
using System.Threading.Tasks;
using Script.Data;
using Script.Util;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Manager
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public Action<PlayData> save;
        private const string DOT_JSON = "Save.json";
        private PlayData m_Data;

        private void Awake()
        {
            LoadData();
            SaveData();
            Debug.LogWarning(SavePath());
        }

        private void SaveData()
        {
            save?.Invoke(m_Data);
            var _path = SavePath();
            var _data = JsonUtility.ToJson(m_Data,true);
            File.WriteAllText(_path, _data);
        }

        private void LoadData()
        {
            var _path = SavePath();
            if (File.Exists(_path))
            {
                var _data = File.ReadAllText(_path);
                m_Data = JsonUtility.FromJson<PlayData>(_data);
                // _PlayerController.Inventory.LoadData(m_Data.itemList);
            }
            else
            {
                Debug.LogWarning("Nope");
                m_Data = CreateNewData();
            }
        }

        private PlayData CreateNewData()
        {
            return new PlayData()
            {
                clearLevel = new[]
                {
                    false
                },
                itemList = new []
                {
                    20,40,60   
                },
            };
        }

        private static string SavePath() => Path.Combine(Application.persistentDataPath, DOT_JSON);
    }
}