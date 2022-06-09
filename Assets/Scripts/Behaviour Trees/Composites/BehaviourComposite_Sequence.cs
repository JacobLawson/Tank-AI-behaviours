using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourComposite_Sequence : BehaviourNode {
    //Sequence composite node
    [SerializeField]
    private List<string> m_sequence;

    private int m_index;

    public override void ResetNode() {
        base.ResetNode();
        m_index = 0;
    }

    public override void Running() {
        if (m_BTA != null) {
            m_BTA.SetStateDebugMessage(m_name);
        }
        if (GetComponent<BehaviourTreeAgent>().GetPreviousNodeResult() == true) {
            if (m_index < m_sequence.Count) {
                GetComponent<BehaviourTreeAgent>().AddToStack(m_sequence[m_index]);
            }
            else {
                m_state = State.SUCCESS;
            }
            m_index++;
        }
        else {
            m_state = State.FAILURE;
        }
    }
}
