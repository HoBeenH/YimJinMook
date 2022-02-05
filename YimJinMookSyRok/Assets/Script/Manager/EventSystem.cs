using System;
using System.Collections;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class EventSystem : MonoSingleton<EventSystem>
{
    private Dictionary<(long, Type), Action<Object>> m_EventList = new Dictionary<(long, Type), Action<Object>>();

    #region private

    private static (long, Type) GetKey<T>(T id) where T : unmanaged, Enum => (Convert.ToInt64(id), id.GetType());

    private void AddEvent<T>(T id, Action<Object> action) where T : unmanaged, Enum
    {
        var _key = GetKey(id);
        if (m_EventList.ContainsKey(_key))
            m_EventList[_key] += action;
        else
            m_EventList.Add(_key, action);
    }

    private void RemoveEvent<T>(T id) where T : unmanaged, Enum
    {
        var _key = GetKey(id);
        if (m_EventList.ContainsKey(_key))
            m_EventList.Remove(GetKey(id));
    }

    private void RemoveEvent<T>(T id, Action<Object> action) where T : unmanaged, Enum
    {
        var _key = GetKey(id);
        if (m_EventList.ContainsKey(_key))
            m_EventList[_key] -= action;
    }

    private void RemoveEvent<T>() where T : unmanaged, Enum
    {
        foreach (var value in Enum.GetValues(typeof(T)))
        {
            RemoveEvent((T)value);
        }
    }

    private void PublishEvent<T>(T id, Object obj) where T : unmanaged, Enum
    {
        var _key = GetKey(id);
        if(m_EventList.TryGetValue(_key, out var callBack))
            callBack.Invoke(obj);
    }
    #endregion

    public static void BindEvent<T>(T id, Action<Object> action) where T : unmanaged, Enum => Instance.AddEvent(id,action);

    public static void CallEvent<T>(T id, Object obj = null) where T : unmanaged, Enum => Instance.PublishEvent(id,obj);

    public static void AddClickEvent<T>(T id, Button src, Object obj = null) where T : unmanaged, Enum
    {
        src.onClick.AddListener(() => Instance.PublishEvent(id,obj));
    }

    public static void BindAndCallEvent<T>(T id,Action<Object> action, Object obj = null) where T : unmanaged, Enum
    {
        Instance.AddEvent(id,action);
        Instance.PublishEvent(id,obj);
    }

    public static void UnbindEvent<T>(T id, Action<Object> action) where T : unmanaged, Enum
    {
        Instance.RemoveEvent(id,action);
    }

    public static void UnbindEvent<T>(T id) where T : unmanaged, Enum
    {
        Instance.RemoveEvent(id);
    }

    public static void UnbindEvent<T>() where T : unmanaged, Enum
    {
        Instance.RemoveEvent<T>();
    }

    public static void ClearEvent() => Instance.m_EventList.Clear();
}