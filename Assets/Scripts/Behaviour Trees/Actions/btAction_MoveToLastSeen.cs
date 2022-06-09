using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class btAction_MoveToLastSeen : BehaviourNode {
    private NavMeshAgent m_agent;
    private GameObject m_Target;

    private Vector3 finalPosition = Vector3.zero;
    private Vector3 m_lastPos;
    private float m_actionTime = 0;

    [SerializeField]
    private float m_walkRadius = 1f;
    [SerializeField]
    private float m_actionTimeLimit;

    public override void ResetNode() {
        base.ResetNode();
        //m_actionTime = 0;

        m_agent = m_parent.GetComponent<NavMeshAgent>();
        
        if (m_actionTime == 0) {
            m_Target = null;
            if (m_BTA.m_blackboard != null) {
                m_Target = m_BTA.m_blackboard.searchForNearestLastSeenPosition(transform);
            }
            if (m_Target != null) {
                finalPosition = m_Target.transform.position;
            }
            else {
                finalPosition = transform.position;
            }
        }
    }
    public override void Running(){
        if (Vector3.Distance(transform.position, finalPosition) <= 6f) {
            m_actionTime = 0;
            ResetNode();
            m_state = State.SUCCESS;
            m_lastPos = transform.position;
        }
        else if (m_actionTime >= m_actionTimeLimit || Vector3.Distance(transform.position, m_lastPos) < 0.01f) {
            m_actionTime = 0;
            ResetNode();
            m_state = State.SUCCESS;
        }
        m_actionTime += 1f * Time.deltaTime;
        m_lastPos = transform.position;
        m_agent.SetDestination(finalPosition);
        m_state = State.SUCCESS;
    }
}