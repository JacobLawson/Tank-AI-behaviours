using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class btCondition_SpotPlayer : BehaviourNode {
    [SerializeField]
    FieldOfView m_vision;
    [SerializeField]
    NavMeshAgent m_agent;

    [SerializeField]
    private bool EndSearch;

    private void Start() {
        ResetNode();
    }

    public override void ResetNode() {
        base.ResetNode();
    }

    public override void Running() {
        
        bool found = false;
        if (m_vision.GetVisibleTargets().Count > 0 && m_BTA.m_blackboard != null) {
            if (m_vision.GetVisbleObjects().Count > 0) {
                List<GameObject> list = m_vision.GetVisbleObjects();
                foreach (GameObject seenObject in m_vision.GetVisbleObjects()) {
                    if (!m_BTA.m_blackboard.CheckTeam(seenObject) && !m_BTA.m_blackboard.CheckEnemyteam(seenObject)) {
                        m_BTA.m_blackboard.AddToEnemyTeam(seenObject);
                        m_BTA.m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                        m_BTA.SetTargetTank(seenObject);
                        found = true;
                        m_state = State.SUCCESS; 
                    }
                    else if (m_BTA.m_blackboard.CheckEnemyteam(seenObject)) {
                        m_BTA.m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                        m_BTA.SetTargetTank(seenObject);
                        found = true;
                        m_state = State.SUCCESS;
                    }
                }
            }
            
        }

        if (!found) {        
            if (EndSearch && m_agent.velocity.x < 0.1f && m_agent.velocity.z < 0.1f) {
                if (m_BTA.GetTargetTank() != null) {
                    m_BTA.m_blackboard.GiveLastPos(m_BTA.GetTargetTank(), Vector3.zero);
                    m_BTA.SetTargetTank(null);
                }
            }
            m_state = State.FAILURE;
        }
    }
}
