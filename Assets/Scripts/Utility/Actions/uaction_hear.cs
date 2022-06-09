using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uaction_hear : UtilityAction {
    [SerializeField]
    TankHear hearingSphere;

    [SerializeField]
    private UtilityGoal m_thisGoal;

    public override void Perform() {
        if (hearingSphere.sounds.Count > 0) {
            transform.LookAt(hearingSphere.sounds[0]);
            hearingSphere.sounds.Clear();
        }
        m_thisGoal.NextAction();
    }
}