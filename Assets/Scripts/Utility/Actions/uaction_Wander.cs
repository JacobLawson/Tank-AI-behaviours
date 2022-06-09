using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class uaction_Wander : UtilityAction {
    [SerializeField]
    private NavMeshAgent m_agent;

    private Vector3 finalPosition = Vector3.zero;
    private Vector3 m_randomDirection = Vector3.zero;
    private float m_actionTime;

    [SerializeField]
    private float m_walkRadius = 1f;
    [SerializeField]
    private float m_actionTimeLimit;

    private void Start() {
        m_actionTime = 0;
    }

    public override void ResetAction() {
        base.ResetAction();
        if (m_actionTime == 0) {
            
            m_randomDirection = Random.insideUnitSphere * m_walkRadius;
            m_randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(m_randomDirection, out hit, m_walkRadius, 1);
            finalPosition = hit.position;
        }
    }

    public override void Perform() {
        base.Perform();
        if (Vector3.Distance(transform.position, finalPosition) <= 2f) {
            m_actionTime = 0;
            ResetAction();
        }
        else if (m_actionTime >= m_actionTimeLimit) {
            m_actionTime = 0;
            ResetAction();
        }
        m_actionTime += 1f * Time.deltaTime;
        m_agent.SetDestination(finalPosition);
        m_goal.NextAction();
    }
}
