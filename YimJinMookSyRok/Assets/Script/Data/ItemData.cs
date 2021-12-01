using System;
using Script.Player;
using UnityEngine;

namespace Script.Data
{
    public abstract class ItemData : MonoBehaviour
    {
        public ItemName name;
        public GameObject item;
        public Action<PlayerController> action;

        protected virtual void GetItem(PlayerController data)
        {
            
        }
    }
    
    
}