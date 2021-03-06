﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalking : StateMachineBehaviour
{
    private NPC npc;
    public bool canInteract = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<NPC>();
        npc.SetMoveSpeed(npc.walkingSpeed);
        npc.SetNewRandomMoveDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.HandleAvoidance();
        npc.HandleAnimation();
        
        if (canInteract)
            npc.HandleBehaviour();
        
        npc.HandleGettingStuck();
        npc.CheckForToiletPaper();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
