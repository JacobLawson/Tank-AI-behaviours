using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourComposite_WeightedSelector : BehaviourNode {
    //weighted selector -- unused in Tank AI
    [SerializeField]
    private List<string> m_selections;
    [SerializeField]
    private List<float> m_weights;

    private int m_selectionIndex;

    public override void ResetNode() {
        base.ResetNode();
        m_selectionIndex = 0;
    }

    public override void Running() {
        m_selectionIndex = GetWeightedIndex();
        if (m_selections.Count == 0) {
            m_state = State.FAILURE;
        }
        if (m_selectionIndex < m_selections.Count) {
            GetComponent<BehaviourTreeAgent>().AddToStack(m_selections[m_selectionIndex]);
        }
        else {
            m_state = State.SUCCESS;
        }
    }

    private int GetWeightedIndex() {
        //select by weight you give in weights inspector
        if (m_weights.Count == 0) {
            return -1;
        }

        float weightSum = 0f;
        for (int i = 0; i < m_weights.Count; ++i) {
            weightSum += m_weights[i];
        }

        float w = 0;
        float t = 0;
        for (int i = 0; i < m_weights.Count; i++) {
            w = m_weights[i];
            if (float.IsPositiveInfinity(w)) return i;
            else if (w >= 0f && !float.IsNaN(w)) t += m_weights[i];
        }

        float r = Random.value;
        float s = 0f;

        for (int i = 0; i < m_weights.Count; i++) {
            w = m_weights[i];
            if (float.IsNaN(w) || w <= 0f) continue;

            s += w / weightSum;
            if (s >= r) return i;
        }

        return -1;
    }
}
