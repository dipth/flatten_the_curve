using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWithinTPRange : StateMachineBehaviour
{
    NPC npc;
    NPCEmoteSystem emoteSystem;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<NPC>();
        emoteSystem = animator.GetComponent<NPCEmoteSystem>();
        npc.SetMoveSpeed(0);
        emoteSystem.SetEmotion(NPCEmotions.Happy);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //npc.CheckForToiletPaper();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
