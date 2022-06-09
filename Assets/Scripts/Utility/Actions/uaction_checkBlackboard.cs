using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class uaction_checkBlackboard : UtilityAction
{
    [SerializeField]
    private UtilityAgent m_utilityAgent;
    [SerializeField]
    private NavMeshAgent m_agent;
    [SerializeField]
    private GameObject m_target;

    [SerializeField]
    private UtilityGoal m_thisGoal;
    [SerializeField]
    private UtilityGoal m_otherGoal;

    private Vector3 finalPosition = Vector3.zero;
    private Vector3 m_lastPos;
    private float m_actionTime;

    // Start is called before the first frame update
    void Start() {
        m_actionTime = 0;
    }

    public override void ResetAction() {
        base.ResetAction();
        m_target = null;
        if (m_utilityAgent.m_blackboard != null) {
            m_target = m_utilityAgent.m_blackboard.searchForNearestLastSeenPosition(transform);
        }
    }

    public override void Perform() {
        base.Perform();
        ResetAction();
        if (m_target != null) {
            
            if (m_otherGoal != null) {
                SetGoalInsistance(100f, m_otherGoal);
                SetGoalInsistance(0f, m_thisGoal);
            }
            else {
                m_goal.NextAction();
            }
        }
        else {
            m_goal.NextAction();
        }
    }
}
