using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNode : MonoBehaviour {
   //base class for all nodes in behaviour tree agent
    public enum State {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    protected State m_state;

    //data needed to be given to the node for it to work in agent
    [SerializeField]
    protected string m_name;

    //inverting decorator
    [SerializeField]
    protected bool m_inverted = false;

    //parent object
    protected GameObject m_parent;
    //agent
    protected BehaviourTreeAgent m_BTA;

    private void Start() {
        m_BTA = null;
        m_parent = transform.parent.gameObject;
        ResetNode();
    }

    public virtual void ResetNode() {
        m_BTA = GetComponent<BehaviourTreeAgent>();
        m_state = State.RUNNING;
    }

    public virtual void Running() {
    }

    public virtual void Success() {
        GetComponent<BehaviourTreeAgent>().SetNodeResult(!m_inverted);
        GetComponent<BehaviourTreeAgent>().PopFromStack();
        ResetNode();
    }

    public virtual void Failure() {
        GetComponent<BehaviourTreeAgent>().SetNodeResult(m_inverted);
        GetComponent<BehaviourTreeAgent>().PopFromStack();
        ResetNode();
    }

    public void Perform() {
        switch (m_state)
        {
            case State.RUNNING:
                {
                    Running();
                }
                break;
            case State.SUCCESS:
                {
                    Success();
                }
                break;
            case State.FAILURE:
                {
                    Failure();
                }
                break;
        }
    }

    public string GetName() { return m_name; }
}
