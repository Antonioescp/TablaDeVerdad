using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoringLayerChange : StateMachineBehaviour
{
    public bool enableOnEnter;
    public bool disableOnExit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enableOnEnter)
            animator.GetComponent<Cell>().CellSprite.sortingOrder = 1;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(disableOnExit)
            animator.GetComponent<Cell>().CellSprite.sortingOrder = 0;
    }
}
