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
        private List<ItemData> inventory = new List<ItemData>();

        public void GetItem(ItemData data)
        {
            inventory.Add(data);
        }

        public void LoadData(IEnumerable<ItemName> data)
        {
            var _list = data.ToList();
            inventory = _ItemPool.InitInventory(_list);
        }

        public void SaveData(PlayData data)
        {
            var _arr = inventory.Select(i => i.name).ToArray();
            data.items = _arr;
        }
    }
}
