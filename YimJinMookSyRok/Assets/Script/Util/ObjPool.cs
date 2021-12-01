using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Util
{
    public enum EPrefab
    {
    }

    public class ObjPool : MonoBehaviour
    {

        #region Private Method
        
        [Serializable]
        private class Prefab
        {
            public EPrefab name;
            public GameObject obj;
            public int count;
            public Queue<GameObject> queue = new Queue<GameObject>();
            [HideInInspector] public Transform m_Parent;
        }

        [SerializeField] private List<Prefab> m_Prefabs = new List<Prefab>();

        private void Awake() => Creat();

        private void Creat()
        {
            m_Prefabs.ForEach(p =>
            {
                p.m_Parent = new GameObject($"{name} parent").transform;
                for (var i = 0; i < p.count; i++)
                {
                    p.queue.Enqueue(CreateObj(p));
                }
            });
        }

        private static GameObject CreateObj(Prefab prefab)
        {
            var _obj = Instantiate(prefab.obj, prefab.m_Parent, true);
            _obj.SetActive(false);
            return _obj;
        }

        private Prefab FindObj(EPrefab objName) => m_Prefabs.FirstOrDefault(p => p.name == objName);

        private IEnumerator Delay(EPrefab objName, GameObject obj, WaitForSeconds time)
        {
            yield return time;
            var _prefab = FindObj(objName);
            obj.transform.SetParent(_prefab.m_Parent);
            obj.SetActive(false);
            _prefab.queue.Enqueue(obj);
        }

        #endregion

        public GameObject GetObj(EPrefab objName, Vector3 pos)
        {
            var _prefab = FindObj(objName);
            if (_prefab != null)
            {
                var _obj =  (_prefab.queue.Count <= 0) switch
                {
                    true => CreateObj(_prefab),
                    false => _prefab.queue.Dequeue()
                };
                _obj.transform.SetParent(null);
                _obj.transform.position = pos;
                _obj.SetActive(true);
                return _obj;
            }
            Debug.LogWarning($"Can't Find {objName} Please Check Name");
            return null;
        }

        public void ReturnObj(EPrefab objName, GameObject obj)
        {
            var _prefab = FindObj(objName);
            obj.transform.SetParent(_prefab.m_Parent);
            obj.SetActive(false);
            _prefab.queue.Enqueue(obj);
        }

        public void ReturnObj(EPrefab objName, GameObject obj, WaitForSeconds time) =>
            StartCoroutine(Delay(objName, obj, time));
    }
}