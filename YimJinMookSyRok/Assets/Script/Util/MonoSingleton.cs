using UnityEngine;

namespace Script.Util
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _Instance;

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = FindObjectOfType<T>();
                    if (_Instance == null)
                    {
                        var _obj = new GameObject($"{nameof(T)} Singleton");
                        _Instance = _obj.AddComponent<T>();
                    }
                }

                return _Instance;
            }
        }
        
    }
}
