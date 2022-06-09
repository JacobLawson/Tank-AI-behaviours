using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityGoal : MonoBehaviour
{
    //needed componenets for goal to work
    //this is a base class
    [SerializeField]
    private string m_name;
    [SerializeField]
    private float m_insistance = 0;
    private int m_currentAction = 0;

    [SerializeField]
    List<UtilityAction> m_actions;

    public GameObject m_parent;

    public void Start() {
        for (int i = 0; i < m_actions.Count; i++) {
            m_actions[i].SetGoalParent(this);
        }
    }

    public float GetInsistance() { return m_insistance; }
    public string GetName() { return m_name; }

    public UtilityAction GetCurrentAction() { return m_actions[m_currentAction]; }
    public void NextAction() {
        //progress linearly through sequence of actions
        m_actions[m_currentAction].ResetAction();
        if (m_currentAction < m_actions.Count -1) {
           
            m_currentAction++;
        }
        else {
            m_currentAction = 0;
        }
    }

    public void PreviousAction()
    {
        m_actions[m_currentAction].ResetAction();
        if (m_currentAction > 0)
        {
            m_currentAction--;
        }
    }

    public void SetInsistance(float a_inistance) { m_insistance = a_inistance; }

    public void ResetGoal() {
        m_currentAction = 0;
    }
}
