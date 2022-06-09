using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UtilityAgent : MonoBehaviour
{
    //components needed for utility/goal agent to work
    [SerializeField]
    TextMeshProUGUI debugMessage;

    [SerializeField]
    private GameObject m_parent;
    [SerializeField]
    private List<UtilityGoal> m_goals;
    public UtilityGoal m_currentGoal;
    public Blackboard m_blackboard;

    private GameObject m_targetTank;
    private bool resetBlackboard;

    private void Start() {
        //initialise
        m_currentGoal = m_goals[0];
        resetBlackboard = true;
        m_currentGoal.SetInsistance(100);
        for (int i = 1; i < m_goals.Count; i++)
        {
            m_goals[i].m_parent = m_parent;
            m_goals[i].SetInsistance(0f);
        }
        m_currentGoal = GetMostInsistantGoal();

    }

    public GameObject GetBrain() { return gameObject; }

    private void OnEnable() {
        //reset
        m_targetTank = null;
        m_goals[0].SetInsistance(100);
        resetBlackboard = true;
        for (int i = 1; i < m_goals.Count; i++)
        {
            m_goals[i].SetInsistance(0f);
        }
    }

    private void Update() {
        //reset blackboard at start of round
        if (m_blackboard != null) {
            if (resetBlackboard) {
                m_blackboard.ResetPositions();
                resetBlackboard = false;
            }
        }
        //perform actions from within goals
        if (m_currentGoal != null) {
            debugMessage.text = m_currentGoal.GetName(); //+ ": " + m_currentGoal.GetCurrentAction();
            m_currentGoal = GetMostInsistantGoal();
            m_currentGoal.GetCurrentAction().Perform();
        }
    }

    private UtilityGoal GetMostInsistantGoal() {
        //find out which goals needs are most insistant and then perform that goal
        UtilityGoal highestGoal = null;
        float insistance = 0;
        for (int i = 0; i < m_goals.Count; i++) {
            if (m_goals[i].GetInsistance() > insistance) {
                insistance = m_goals[i].GetInsistance();
                highestGoal = m_goals[i];
            }
        }
        if (highestGoal != null && m_currentGoal != null) {
            if (m_currentGoal != highestGoal) {
                m_currentGoal.ResetGoal();
                highestGoal.ResetGoal();
            }
        }
        return highestGoal;
    }

    public void SetTargetTank(GameObject a_Tank) { m_targetTank = a_Tank; }
    public GameObject GetTargetTank() { return m_targetTank; }
}
