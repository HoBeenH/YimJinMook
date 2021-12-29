using Script.FSM;
using Script.Player;
using UnityEngine;
using static Script.Util.Facade;

public class Player_Dodge : State<PlayerController>
{
    public Player_Dodge() : base("Base Layer.Dodge.Player_Dodge") =>
        s_Dodge = Animator.StringToHash("Dodge");

    private static int s_Dodge;
    public override void OnStateEnter()
    {
        owner.E_State = State.Dodge;
        machine.anim.SetTrigger(s_Dodge);
        owner.Stat.Stamina -= 10f;
        owner.StartCoroutine(machine.WaitIdle(typeof(Player_Movement), animHash));
    }

    public override void OnStateExit()
    {
        owner.E_State = State.None;
    }
}