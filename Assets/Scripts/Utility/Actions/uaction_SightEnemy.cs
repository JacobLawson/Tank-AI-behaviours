using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class uaction_SightEnemy : UtilityAction {
    [SerializeField]
    private FieldOfView m_vision;
    [SerializeField]
    private NavMeshAgent m_agent;
    [SerializeField]
    private UtilityAgent m_utilityAgent;

    [SerializeField]
    private UtilityGoal m_otherGoal;
    [SerializeField]
    private UtilityGoal m_thisGoal;

    [SerializeField]
    private bool EndSearch;

    private void Start() {
        ResetAction();
    }

    public override void ResetAction() {
        base.ResetAction();
    }

    public override void Perform() {
        base.Perform();
        bool found = false;
        if (m_vision.GetVisibleTargets().Count > 0 && m_utilityAgent.m_blackboard != null) {
            if (m_vision.GetVisbleObjects().Count > 0) {
                List<GameObject> list = m_vision.GetVisbleObjects();
                foreach (GameObject seenObject in m_vision.GetVisbleObjects()) {
                    if (!m_utilityAgent.m_blackboard.CheckTeam(seenObject) && !m_utilityAgent.m_blackboard.CheckEnemyteam(seenObject)) {
                        m_utilityAgent.m_blackboard.AddToEnemyTeam(seenObject);
                        m_utilityAgent.m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                        m_utilityAgent.SetTargetTank(seenObject);
                        found = true;
                    }
                    else if (m_utilityAgent.m_blackboard.CheckEnemyteam(seenObject)) {
                        m_utilityAgent.m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                        m_utilityAgent.SetTargetTank(seenObject);
                        found = true;
                    }
                }
            }

        }
        if (!found) {
            m_thisGoal.NextAction();
        }
        else {
            SetGoalInsistance(100f, m_otherGoal);
            SetGoalInsistance(0f, m_thisGoal);
        }
    }
}
