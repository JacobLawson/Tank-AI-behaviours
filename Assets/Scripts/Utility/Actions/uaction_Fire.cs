using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class uaction_Fire : UtilityAction {
        [SerializeField]
        private UtilityAgent m_utilityAgent;
        [SerializeField]
        private TankShooting shooting;

        private float m_additiveForce = 0f;

        public override void Perform() {
            base.Perform();
            if (m_utilityAgent != null && m_utilityAgent.GetTargetTank() != null) {
                transform.LookAt(m_utilityAgent.GetTargetTank().transform);
            }
            if (m_goal.m_parent != null) {
                if (shooting.GetShellInstance() == null && m_utilityAgent.GetTargetTank() != null) {
                    Vector3 dirFromAtoB = (m_utilityAgent.GetTargetTank().transform.position - transform.position).normalized;
                    float dotProd = Vector3.Dot(dirFromAtoB, transform.transform.forward);
                    if (dotProd >= 1) {
                        transform.LookAt(m_utilityAgent.GetTargetTank().transform);
                        m_additiveForce = Vector3.Distance(transform.position, m_utilityAgent.GetTargetTank().transform.position);
                        if (m_additiveForce <= shooting.GetMaxForce()) {
                            shooting.SetForce(m_additiveForce);
                            shooting.Fire();
                            m_goal.NextAction();
                        }

                    }
                    else {
                        transform.LookAt(m_utilityAgent.GetTargetTank().transform);
                        m_goal.NextAction();
                    }
                }
                else {
                    m_goal.NextAction();
                }
            }
            else {
                m_goal.NextAction();
            }
        }
    }
}