using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uaction_SetGoal : UtilityAction {
    [SerializeField]
    UtilityGoal m_thisGoal;
    [SerializeField]
    UtilityGoal m_goalSet;
    public override void Perform() {
        base.Perform();
        m_thisGoal.SetInsistance(0);
        m_goalSet.SetInsistance(100);
    }
}
