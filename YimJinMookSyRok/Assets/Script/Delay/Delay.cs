using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Delay : MonoBehaviour
{
    private class DelayState
    {
        public bool canUse = true;
        private readonly float m_MaxDelayTime;
        public float nowTime = 0f;
        public Type name;

        public DelayState(float time, Type name)
        {
            this.m_MaxDelayTime = time;
            this.name = name;
        }
    }

    private List<DelayState> m_List = new List<DelayState>();

    public void AddDelay(float maxTime, Type type)
    {
        m_List.Add(new DelayState(maxTime, type));
    }

    public Type UseState(Type name) =>
        m_List.Where(l => l.name == name).Select(l => l.canUse ? l.name : null).FirstOrDefault();
}