using System.Collections.Generic;
using System.Linq;
using Script.Util;
using Sirenix.Utilities;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Data
{
    public class Inventory
    {
        public List<int> hashList = new List<int>();
        public List<ItemData> inventory = new List<ItemData>();

        public void GetItem(ItemData data)
        {
            var _hash = data.name.GetHashCode();
            hashList.Add(_hash);
            inventory.Add(data);
        }

        public void LoadData(int[] data)
        {
            var _list = data.ToList();
            inventory = _ItemPool.InitInventory(_list);
            hashList = _list;
        }

        public void SaveData(PlayData data)
        {
            data.itemList.AddRange(hashList);
            hashList.Clear();
        }
    }
}
