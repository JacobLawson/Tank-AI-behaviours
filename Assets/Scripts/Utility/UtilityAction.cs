using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAction : MonoBehaviour
{
    //base class for all utility actions that make up the functionality of a goal

    protected UtilityGoal m_goal;
    public virtual void ResetAction() {

    }

    public virtual void Perform() {

    }

    public void SetGoalParent(UtilityGoal a_goal) { m_goal = a_goal; }

    public void SetGoalInsistance(float a_insistance, UtilityGoal a_goal) {
        a_goal.SetInsistance(a_insistance);
    }

}
