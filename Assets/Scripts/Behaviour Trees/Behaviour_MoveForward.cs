using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_MoveForward : BehaviourNode {
    //test node I used while developing
    public override void Running() {
        m_parent.transform.position = new Vector3(transform.position.x + 10f * Time.deltaTime, transform.position.y, transform.position.z);
        m_state = State.SUCCESS;
    }
}
