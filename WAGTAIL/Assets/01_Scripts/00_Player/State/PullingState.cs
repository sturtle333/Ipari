using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PullingState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    bool isGrounded;
    float playerSpeed;
    bool isPull;

    GameObject RopeHead;

    Vector3 cVelocity;
    

    public PullingState(Player _player, StateMachine _stateMachine) : base(_player, _stateMachine)
    {
        player = _player;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        // input 관련
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        isPull = player.isPull;
        RopeHead = player.currentInteractable;

        playerSpeed = player.playerSpeed;
        isGrounded = player.controller.isGrounded;
        gravityValue = player.gravityValue;
    }

    public override void HandleInput()
    {

        base.HandleInput();
        isPull = player.isPull;
        input = moveAction.ReadValue<Vector2>();
        // 3차원 공간에서는 y축(점프 제외)으로 이동하지 않기 때문에
        // 2차원에서의 이동 좌표에서 y축을 z축으로 사용함.
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * player.cameraTransform.right.normalized + 
            velocity.z * player.cameraTransform.forward.normalized;
        velocity.y = 0;
        //isPull = player.isPull;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log("State == Pulling");
        if(!isPull)
        {
            stateMachine.ChangeState(player.drop);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        isGrounded = player.controller.isGrounded;

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, player.velocityDampTime);
        player.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
        player.transform.LookAt(RopeHead.GetComponent<Node>().GetParent().TailRope.transform.position);


        //if (velocity.sqrMagnitude > 0)
        //{
        //    //player.transform.rotation =
        //    //    Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation());


        //    //player.transform.rotation =
        //    //    Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(velocity), player.rotationDampTime);
        //}
    }

    public override void Exit()
    {
        base.Exit();

        //player.controller.height = player.normalColliderHeight;
        //player.controller.center = player.normalColliderCenter;
        //player.controller.radius = player.normalColliderRadius;
        //gravityVelocity.y = 0f;
        //player.playerVelocity = new Vector3(input.x, 0, input.y);

    }
}
