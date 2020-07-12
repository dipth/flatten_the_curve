using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFleeing : StateMachineBehaviour
{
    NPC npc;
    NPCEmoteSystem emoteSystem;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<NPC>();
        emoteSystem = animator.GetComponent<NPCEmoteSystem>();
        
        npc.SetMoveSpeed(npc.sprintingSpeed);
        npc.SetMoveDirectionOppositeToPlayer();

        emoteSystem.SetEmotion(NPCEmotions.Confused);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.HandleAnimation();
        npc.HandleAvoidance();
        npc.CheckForDoor();
        npc.HandleGettingStuck();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
