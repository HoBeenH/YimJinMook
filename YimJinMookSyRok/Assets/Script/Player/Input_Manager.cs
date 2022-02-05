using System;
using Script.Util;
using UnityEngine;

public enum Task
{
    Jump,
    Attack,
    Dodge,
    Defence,
    None
}

public class Input_Manager : MonoSingleton<Input_Manager>
{
    private const string TAG_JUMP = "Jump";
    private const string TAG_ENEMY = "Enemy";
    [SerializeField] private LayerMask m_LayerMask;

    private void Update()
    {
        MouseAction();
    }

    public Task MouseAction()
    {
        var _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var _hit = Physics2D.Raycast(_target, Vector2.zero, Mathf.Infinity, m_LayerMask);
        if (_hit.collider != null)
        {
            return _hit.collider.tag switch
            {
                TAG_JUMP => Task.Jump,
                TAG_ENEMY => Task.Attack,
            };
        }

        return Task.None;
    }
}