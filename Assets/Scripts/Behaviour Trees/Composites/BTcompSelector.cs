using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTcompSelector : BehaviourNode {
    [SerializeField]
    private List<string> m_selections;

    private int m_selectionIndex;

    public override void ResetNode() {
        base.ResetNode();
        m_selectionIndex = 0;
    }

    public override void Running() {
        if (m_selections.Count == 0) {
            m_state = State.FAILURE;
        }
        if (m_selectionIndex < m_selections.Count) {
            GetComponent<BehaviourTreeAgent>().AddToStack(m_selections[m_selectionIndex]);
        }
        else {
            m_state = State.SUCCESS;
        }
        m_selectionIndex++;
    }
}
