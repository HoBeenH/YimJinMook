using System;
using System.Collections.Generic;
using Script.Player;
using Script.Util;
using Sirenix.Utilities;
using UnityEngine;

namespace Script.Data
{
    public class ItemPool : MonoSingleton<ItemPool>
    {
        public List<ItemData> itemList = new List<ItemData>();
        private readonly Dictionary<int, ItemData> m_List = new Dictionary<int, ItemData>();

        private void Awake()
        {
            itemList.ForEach(x => m_List.Add(x.GetHashCode(), x));
        }

        public List<ItemData> InitInventory(List<int> list)
        {
            var _itemList = new List<ItemData>();
            list.ForEach(x => _itemList.Add(m_List[x]));
            return _itemList;
        }

        public ItemData Find(int hash) => m_List[hash];
    }

}