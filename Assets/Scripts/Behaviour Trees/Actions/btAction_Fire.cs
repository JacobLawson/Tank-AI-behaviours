using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class btAction_Fire : BehaviourNode {
        private float m_additiveForce = 0f;

        public override void Running() {
            if (m_BTA != null && m_BTA.GetTargetTank() != null) {
                transform.LookAt(m_BTA.GetTargetTank().transform);
            }
            else {
                m_state = State.FAILURE;
            }
            if (m_parent != null) {
                TankShooting tankShooting = m_parent.GetComponent<TankShooting>();
                if (tankShooting.GetShellInstance() == null && m_BTA.GetTargetTank() != null ) {
                    Vector3 dirFromAtoB = (m_BTA.GetTargetTank().transform.position - transform.position).normalized;
                    float dotProd = Vector3.Dot(dirFromAtoB, transform.transform.forward);
                    if (dotProd >= 1) {
                        transform.LookAt(m_BTA.GetTargetTank().transform);
                        m_additiveForce = Vector3.Distance(transform.position, m_BTA.GetTargetTank().transform.position);
                        if (m_additiveForce <= tankShooting.GetMaxForce()) {
                            tankShooting.SetForce(m_additiveForce);
                            tankShooting.Fire();
                            m_state = State.SUCCESS;
                        }
                        
                    }
                    else {
                        transform.LookAt(m_BTA.GetTargetTank().transform);
                        m_state = State.SUCCESS;
                    }

                }
                else {
                    m_state = State.FAILURE;
                }
            }
            else {
                m_state = State.FAILURE;
            }
        }
    }
}