using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Complete
{
    public class AISettingsPanel : MonoBehaviour
    {
        [SerializeField]
        GameManager gameManager;
        [SerializeField]
        Button buttonOne;
        [SerializeField]
        Button buttonTwo;

        private bool m_initialised;
        private int m_brainsTeamOne;
        private int m_brainsTeamTwo;

        private void Start()
        {
            m_initialised = false;
            m_brainsTeamOne = 0;
            m_brainsTeamTwo = 1;          
        }

        private void Update()
        {
            if (!m_initialised)
            {
                if (gameManager.m_Tanks[0].m_Instance != null)
                {
                    SetTeamOneAI();
                    SetTeamTwoAI();
                    m_initialised = true;
                }
            }

            if (m_brainsTeamOne >= 3)
            {
                m_brainsTeamOne = 0;

                SetTeamOneAI();
            }

            if (m_brainsTeamTwo >= 3)
            {
                m_brainsTeamTwo = 0;
                SetTeamTwoAI();
            }
        }
        public void TeamOnePressed()
        {
            m_brainsTeamOne += 1;

            SetTeamOneAI();

        }

        public void TeamTwoPressed()
        {
            m_brainsTeamTwo += 1;

            SetTeamTwoAI();

        }

        void SetTeamOneAI()
        {
            for (int i = 0; i < gameManager.m_Tanks.Length; i++)
            {
                if (gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().m_PlayerNumber % 2 != 0)
                {
                    if (m_brainsTeamOne == 0)   //Behaviour Tree
                    {
                        buttonOne.GetComponentInChildren<Text>().text = "Behaviour Tree";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(true);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(false);
                    }
                    else if (m_brainsTeamOne == 1)  //Utility
                    {
                        buttonOne.GetComponentInChildren<Text>().text = "Goal/Utility";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(true);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(false);
                    }
                    else if (m_brainsTeamOne == 2)  //State Machine
                    {
                        buttonOne.GetComponentInChildren<Text>().text = "State Machine";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(true);
                    }
                }
            }
        }
        void SetTeamTwoAI()
        {
            for (int i = 0; i < gameManager.m_Tanks.Length; i++)
            {
                if (gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().m_PlayerNumber % 2 == 0)
                {
                    if (m_brainsTeamTwo == 0)   //Behaviour Tree
                    {
                        buttonTwo.GetComponentInChildren<Text>().text = "Behaviour Tree";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(true);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(false);
                    }
                    else if (m_brainsTeamTwo == 1)  //Utility
                    {
                        buttonTwo.GetComponentInChildren<Text>().text = "Goal/Utility";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(true);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(false);
                    }
                    else if (m_brainsTeamTwo == 2)  //State Machine
                    {
                        buttonTwo.GetComponentInChildren<Text>().text = "State Machine";
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetBehaviourTreeAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetUtilityAgent().GetBrain().SetActive(false);
                        gameManager.m_Tanks[i].m_Instance.GetComponent<TankMovement>().GetStateMachineAgent().GetBrain().SetActive(true);
                    }
                }
            }
        }
    }
}
