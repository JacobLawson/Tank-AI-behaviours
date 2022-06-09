using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class uaction_SearchForTank : UtilityAction {
    [SerializeField]
    private UtilityAgent m_utilityAgent;
    [SerializeField]
    private NavMeshAgent m_agent;

    [SerializeField]
    private UtilityGoal m_thisGoal;
    [SerializeField]
    private UtilityGoal m_otherGoal;

    private GameObject m_target;
    private Vector3 m_finalPosition;
    public override void ResetAction() {
        base.ResetAction();

        m_target = null;
        if (m_utilityAgent.m_blackboard != null) {
            m_target = m_utilityAgent.m_blackboard.searchForNearestLastSeenPosition(transform);
        }
        if (m_target != null) {
            m_finalPosition = m_target.transform.position;
        }
        else {
            m_finalPosition = transform.position;
        }
    }

    public override void Perform() {
        base.Perform();
        ResetAction();
        if (Vector3.Distance(transform.position, m_finalPosition) <= 6f) {
            
            m_thisGoal.SetInsistance(0);
            m_otherGoal.SetInsistance(100);
        }
        m_agent.SetDestination(m_finalPosition);
        m_goal.NextAction();
    }
}
