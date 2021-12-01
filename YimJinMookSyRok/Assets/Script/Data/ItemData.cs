using System;
using Script.Player;
using UnityEngine;

namespace Script.Data
{
    public abstract class ItemData : MonoBehaviour
    {
        public string name;
        public GameObject item;
        public Action<PlayerController> action;

        public virtual void GetItem()
        {
            
        }
    }
    
    
}