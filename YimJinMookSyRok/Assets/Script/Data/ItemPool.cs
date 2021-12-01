using System.Collections.Generic;
using Script.Util;

namespace Script.Data
{
    public enum ItemName
    {
        Test,
        End
    }
    public class ItemPool : MonoSingleton<ItemPool>
    {
        public List<ItemData> itemList = new List<ItemData>();
        private readonly Dictionary<ItemName, ItemData> m_List = new Dictionary<ItemName, ItemData>();

        private void Awake()
        {
            itemList.ForEach(x => m_List.Add(x.name, x));
        }

        public List<ItemData> InitInventory(List<ItemName> list)
        {
            var _itemList = new List<ItemData>();
            list.ForEach(x => _itemList.Add(m_List[x]));
            return _itemList;
        }

        public ItemData Find(ItemName hash) => m_List[hash];
    }

}