using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BehaviourTreeAgent : MonoBehaviour {
    //behaviour tree agent (this is what runs the AI)
    [SerializeField]
    TextMeshProUGUI m_debugMessage;

    [SerializeField]
    private string m_rootNode;

    public Blackboard m_blackboard;

    [SerializeField]
    List<string> m_allNodes_Names;
    [SerializeField]
    List<BehaviourNode> m_AllNodes_Components;

    Dictionary<string, BehaviourNode> m_behaviourNodeDictionary;

    private Stack<string> m_behaviourStack;
    private string m_currentNode;

    private bool m_previousNodeEndState;

    //AI variables
    private GameObject m_targetTank;

    [SerializeField]
    private Rigidbody rb;

    private void Start() {
        m_targetTank = null;
        m_behaviourStack = new Stack<string>();

        m_behaviourNodeDictionary = new Dictionary<string, BehaviourNode>();
        for (int i = 0; i < m_allNodes_Names.Count; i++) {
            m_behaviourNodeDictionary.Add(m_allNodes_Names[i], m_AllNodes_Components[i]);
        }
    }


    private void Update() {
        rb.velocity = Vector3.zero;
        if (m_behaviourStack.Count > 0) {
            FetchCurrentNode();
        }
        else {
            AddToStack(m_rootNode);
        }
    }

    public void AddToStack(string a_node) {
        m_previousNodeEndState = true;
        m_behaviourStack.Push(a_node);
    }

    public void PopFromStack() {
        if (m_behaviourStack.Count > 0) {
            m_behaviourStack.Pop();
        }
    }

    public void FetchCurrentNode() {
        if (m_behaviourStack.Count > 0) {
            m_currentNode = m_behaviourNodeDictionary[m_behaviourStack.Peek()].GetName();
            m_behaviourNodeDictionary[m_currentNode].Perform();
        }
        else {
            m_previousNodeEndState = true;
        }
    }

    public GameObject GetBrain() { return gameObject; }

    public void SetNodeResult(bool a_failed) { m_previousNodeEndState = a_failed; }
    public bool GetPreviousNodeResult() { return m_previousNodeEndState; }

    public void SetTargetTank(GameObject a_tank) { m_targetTank = a_tank; }
    public GameObject GetTargetTank() { return m_targetTank; }

    public void SetStateDebugMessage(string a_message) { m_debugMessage.text = a_message; }
}
