using Script.Util;
using UnityEngine;

public enum Task
{
    Jump,
    Attack,
    Dodge,
    None
}

public class Input_Manager : MonoSingleton<Input_Manager>
{
    private const string TAG_JUMP = "Jump";
    private const string TAG_ENEMY = "Enemy";
    [SerializeField] private LayerMask m_LayerMask;
    [HideInInspector] public Task task = Task.None;

    private void Update()
    {
        MouseAction();
    }

   

    private void MouseAction()
    {
        var _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var _hit = Physics2D.Raycast(_target, Vector2.zero, Mathf.Infinity, m_LayerMask);
        if (_hit.collider != null)
        {
            task = _hit.collider.tag switch
            {
                TAG_JUMP => Task.Jump,
                TAG_ENEMY => Task.Attack,
                _ => Task.None
            };
        }
        else
        {
            task = Task.None;
        }
    }
}