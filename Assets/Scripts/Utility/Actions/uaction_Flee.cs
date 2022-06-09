using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Complete {
    public class uaction_Flee : UtilityAction {
        [SerializeField]
        private UtilityAgent m_utilityAgent;

        [SerializeField]
        private UtilityGoal m_thisGoal;

        [SerializeField]
        private TankHealth m_tankHealth;
        [SerializeField]
        private NavMeshAgent m_agent;

        Vector3 m_SafePoint;

        private void Start() {
            m_SafePoint = transform.position;
        }
        public override void Perform() {
            base.Perform();
            if (m_tankHealth.m_Slider.value < 100f) {
                m_tankHealth.m_Slider.value += 4 * Time.deltaTime;
            }

            if (m_utilityAgent.GetTargetTank() != null) {
                //temporarily point the object to look away from the player
                transform.rotation = Quaternion.LookRotation(transform.position - m_utilityAgent.GetTargetTank().transform.position);

                //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
                // for this if you want variable results) and store it in a new Vector3 called runTo
                m_SafePoint = transform.position + transform.forward * 10f;
            }
            m_agent.speed = 6f;
            m_agent.SetDestination(m_SafePoint);
        }
    }
}