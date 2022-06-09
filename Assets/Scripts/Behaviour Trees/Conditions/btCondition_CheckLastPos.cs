using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class btCondition_CheckLastPos : BehaviourNode {
    private GameObject m_target;

    private void Start() {
        m_target = null;
        ResetNode();
    }

    public override void ResetNode() {
        base.ResetNode();
        if (m_BTA.m_blackboard != null) {
            m_target = m_BTA.m_blackboard.searchForNearestLastSeenPosition(transform);
        }
    }
    public override void Running() {
        if (m_target == null) {
            m_state = State.FAILURE;
        }
        else {
            m_state = State.SUCCESS;
        }
    }
}
