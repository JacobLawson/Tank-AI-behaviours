using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class UtilityGoalHealth : UtilityGoal{
        [SerializeField]
        private TankHealth m_health;

        [SerializeField]
        private float m_healthThreshhold;

        private void Update() {
            //health determins insistance
            SetInsistance(m_healthThreshhold + (m_health.m_StartingHealth - m_health.m_Slider.value));
        }
    }
}