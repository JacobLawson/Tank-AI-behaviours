using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

namespace Complete {
    public class StateMachineAgent : MonoBehaviour {
        //components needed for state machine to work
        [SerializeField]
        TextMeshProUGUI m_debugMessage;

        [SerializeField]
        private GameObject m_parent;
        public Blackboard m_blackboard;

        private GameObject m_targetTank;
        [SerializeField]
        private TankHealth m_health;
        [SerializeField]
        private TankShooting shooting;

        [SerializeField]
        private float m_healthThreshhold = 50;
        [SerializeField]
        private float m_walkRadius = 20;

        [SerializeField]
        private FieldOfView m_vision;
        [SerializeField]
        private TankHear m_hearingSphere;
        [SerializeField]
        private NavMeshAgent m_agent;


        private float m_actionTime;
        private float m_lastHealth;

        enum STATES {
            WANDER,
            SEARCH,
            ATTACK,
            FLEE,
            DODGE
        }
        STATES m_state;

        // Start is called before the first frame update
        void Start() {
            m_actionTime = 0;
            m_state = STATES.WANDER;
            m_lastHealth = m_health.m_Slider.value;
        }

        private void OnDisable() {
            m_actionTime = 0;
            m_state = STATES.WANDER;
            m_lastHealth = m_health.m_Slider.value;
        }

        // Update is called once per frame
        void Update() {
            if (m_blackboard != null) {
                //State functionality
                switch (m_state) {
                    case STATES.WANDER:
                        {
                            //debug out
                            m_debugMessage.text = "Wander";

                            //wander action
                            if (m_actionTime == 0) {
                                Vector3 m_randomDirection = Random.insideUnitSphere * m_walkRadius;
                                m_randomDirection += transform.position;
                                NavMeshHit hit;
                                NavMesh.SamplePosition(m_randomDirection, out hit, m_walkRadius, 1);
                                Vector3 finalPosition = hit.position;
                                m_agent.SetDestination(finalPosition);
                            }
                            m_actionTime += 1f * Time.deltaTime;

                            if (Vector3.Distance(transform.position, m_agent.destination) <= 2f) {
                                m_actionTime = 0;
                            }
                            else if (m_actionTime >= 30.0f) {
                                m_actionTime = 0;
                            }

                            //switch state
                            if (LowHealth()) {
                                m_actionTime = 0f;
                                m_state = STATES.FLEE;
                            }
                            else if (BeenHit()) {
                                m_actionTime = 0f;
                                m_state = STATES.DODGE;
                            }
                            else if (KnownTargetLocation() || HearingRadius() || SpotEnemy()) {
                                m_actionTime = 0f;
                                m_state = STATES.SEARCH;
                            }
                        }
                        break;
                    case STATES.SEARCH:
                        {
                            //debug out
                            m_debugMessage.text = "Searching";

                            //search action
                            if (KnownTargetLocation()) {
                                if (m_targetTank.activeInHierarchy || m_targetTank != null) {
                                    m_agent.SetDestination(m_targetTank.transform.position);
                                }
                                else {
                                    m_blackboard.ResetPositions();
                                    m_targetTank = null;
                                    m_state = STATES.WANDER;
                                }

                                if (Vector3.Distance(transform.position, m_agent.destination) <= 6f) {
                                    m_targetTank = null;
                                    m_state = STATES.WANDER;
                                }
                                if (LowHealth()) {
                                    m_state = STATES.FLEE;
                                }
                                else if (BeenHit()) {
                                    m_actionTime = 0f;
                                    m_state = STATES.DODGE;
                                }
                                else if (SpotEnemy()) {
                                    m_state = STATES.ATTACK;
                                }
                            }
                            else {
                                m_targetTank = null;
                                m_state = STATES.WANDER;
                            }

                        }
                        break;
                    case STATES.ATTACK:
                        {
                            //debug out
                            m_debugMessage.text = "Attack";

                            //attack action
                            float m_additiveForce = 0f;

                            if (LowHealth()) {
                                m_state = STATES.FLEE;
                            }
                            else if (!SpotEnemy()) {
                                m_state = STATES.SEARCH;
                            }
                            else {
                                transform.LookAt(m_targetTank.transform);
                                if (shooting.GetShellInstance() == null && m_targetTank != null) {
                                    Vector3 dirFromAtoB = (m_targetTank.transform.position - transform.position).normalized;
                                    float dotProd = Vector3.Dot(dirFromAtoB, transform.transform.forward);
                                    if (dotProd >= 1) {
                                        transform.LookAt(m_targetTank.transform);
                                        m_additiveForce = Vector3.Distance(transform.position, m_targetTank.transform.position);
                                        if (m_additiveForce <= shooting.GetMaxForce()) {
                                            shooting.SetForce(m_additiveForce);
                                            shooting.Fire();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case STATES.FLEE:
                        {
                            //debug out
                            m_debugMessage.text = "Flee";

                            //flee action
                            if (m_health.m_Slider.value < 100f) {
                                if (m_targetTank != null && m_targetTank.activeInHierarchy) {
                                    transform.rotation = Quaternion.LookRotation(transform.position - m_targetTank.transform.position);

                                    //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
                                    // for this if you want variable results) and store it in a new Vector3 called runTo
                                    Vector3 safePoint = transform.position + transform.forward * 10f;
                                    m_agent.speed = 6f;
                                    m_agent.SetDestination(safePoint);
                                }
                                m_health.m_Slider.value += 5 * Time.deltaTime;
                            } 
                            else {
                                m_state = STATES.SEARCH;
                            }
                        }
                        break;
                    case STATES.DODGE:
                        {
                            //debug out
                            m_debugMessage.text = "Dodge /Escape Line of fire";
                            
                            if (m_actionTime < 1) {
                                if (m_targetTank != null && m_targetTank.activeInHierarchy) {
                                    //transform.rotation = Quaternion.LookRotation(transform.position - m_targetTank.transform.position);

                                    //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
                                    // for this if you want variable results) and store it in a new Vector3 called runTo
                                    Vector3 safePoint = transform.position + transform.right * 10f;
                                    m_agent.speed = 6f;
                                    m_agent.SetDestination(safePoint);
                                }
                                m_actionTime += 1f * Time.deltaTime;
                            }
                            else {
                                if (LowHealth()) {
                                    m_actionTime = 0f;
                                    m_state = STATES.FLEE;
                                }
                                if (Vector3.Distance(transform.position, m_agent.destination) <= 2f) {
                                    m_actionTime = 0f;
                                    m_state = STATES.WANDER;
                                }
                            }
                        }
                        break;
                }
                m_lastHealth = m_health.m_Slider.value;
            }
        }

        private bool SpotEnemy() {
            //has spot Player?
            if (m_vision.GetVisibleTargets().Count > 0 && m_blackboard != null)
            {
                if (m_vision.GetVisbleObjects().Count > 0) {
                    List<GameObject> list = m_vision.GetVisbleObjects();
                    foreach (GameObject seenObject in m_vision.GetVisbleObjects()) {
                        if (!m_blackboard.CheckTeam(seenObject) && !m_blackboard.CheckEnemyteam(seenObject)) {
                            m_blackboard.AddToEnemyTeam(seenObject);
                            m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                            m_targetTank = seenObject;
                            return true;
                        }
                        else if (m_blackboard.CheckEnemyteam(seenObject)) {
                            m_blackboard.GiveLastPos(seenObject, seenObject.transform.position);
                            m_targetTank = seenObject;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool HearingRadius() {
            //has heard noise?
            if (m_hearingSphere.sounds.Count > 0) {
                m_hearingSphere.sounds.Clear();
                return true;
            }
            return false;
        }

        private bool LowHealth() {
            //low health?
            if (m_health.m_Slider.value <= m_healthThreshhold) {
                return true;
            }
            return false;
        }

        private bool BeenHit() {
            //been hit by surprise?
            if (m_lastHealth != m_health.m_Slider.value) {
                return true;
            }
            return false;
        }

        private bool KnownTargetLocation() {
            //Know enemy positions?
            GameObject target = m_blackboard.searchForNearestLastSeenPosition(transform);
            if (target != null && target.activeInHierarchy) {
                m_targetTank = target;
                return true;
            }
            return false;
        }

        public GameObject GetBrain() { return gameObject; }
    }
}
