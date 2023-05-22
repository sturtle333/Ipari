using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropState : State
{
    float timePassed;
    float dropTime;

    public DropState(Player _player, StateMachine _stateMachine) : base(_player, _stateMachine)
    {
        player = _player;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        timePassed = 0f;

        player.animator.SetTrigger("drop");
        // TODO : 물건 내려놓는 시간 애니메이터와 상의 후 정하기
        // !!! dropTime 반드시 수정해야함 !!! 
        dropTime = 1.467f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (timePassed > dropTime)
        {
            //player.animator.SetTrigger("move");
            stateMachine.ChangeState(player.idle);
        }
        timePassed += Time.deltaTime;
    }
}