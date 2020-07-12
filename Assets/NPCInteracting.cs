using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteracting : StateMachineBehaviour
{
    NPC npc;
    NPCEmoteSystem emoteSystem;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<NPC>();
        emoteSystem = animator.GetComponent<NPCEmoteSystem>();
        
        npc.SetMoveSpeed(0);
        emoteSystem.SetEmotion(NPCEmotions.Interacting);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.CheckForToiletPaper();

        int randomInt = Random.Range(0, 10000);
        if (randomInt == 42)
        {
            emoteSystem.SetEmotion(NPCEmotions.Interacting);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
