using Script.FSM;
using Script.Player;
using UnityEngine;
using static Script.Util.Facade;

public class Player_Dodge : State<PlayerController>
{
    private static readonly int s_Dodge = Animator.StringToHash("Dodge");

    public override void OnStateEnter()
    {
        owner.EState = State.Dodge;
        machine.anim.SetBool(s_Dodge,true);
    }

    public override void OnStateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            machine.ChangeState(typeof(Player_Movement));
        }
    }

    public override void OnStateExit()
    {
        owner.EState = State.None;
        machine.anim.SetBool(s_Dodge,false);
    }
}
