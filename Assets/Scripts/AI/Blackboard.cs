using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_myTeam;
    [SerializeField]
    private List<GameObject> m_enemyTeam;

    private Dictionary<GameObject, Vector3> m_enemyPositions;

    private void Start() {
        m_myTeam = new List<GameObject>();
        m_enemyTeam = new List<GameObject>();
        m_enemyPositions = new Dictionary<GameObject, Vector3>();
    }

    //add the last seen position of an enemy to the dictionary
    public void GiveLastPos(GameObject a_target, Vector3 a_pos) {
        m_enemyPositions[a_target] = a_pos;
    }

    public GameObject searchForNearestLastSeenPosition(Transform a_transform) {
        //get nearest known position an enemy was at
        float distance = 0;
        GameObject seek = null;
        foreach (GameObject i in m_enemyTeam) {
            if (m_enemyPositions[i] != Vector3.zero) {
                float temp = Vector3.Distance(a_transform.position, m_enemyPositions[i]);
                if (temp > distance) {
                    distance = temp;
                    seek = i;
                }
            }
        }
        return seek;
    }

    public bool CheckTeam(GameObject a_gameObject) {
        //check if an object is on my team
        for (int i = 0; i < m_myTeam.Count; i++)
        {
            if (a_gameObject == m_myTeam[i]) {
                return true;
            }
        }
        return false;
    }
    public bool CheckEnemyteam(GameObject a_gameObject) {
        //check if object is on enemy team
        for (int i = 0; i < m_enemyTeam.Count; i++) {
            if (a_gameObject == m_enemyTeam[i]) {
                return true;
            }
        }
        return false;
    }

    public void AddToMyTeam(GameObject a_gameObject) {
        m_myTeam.Add(a_gameObject);
    }

    public void AddToEnemyTeam(GameObject a_gameObject) {
        m_enemyTeam.Add(a_gameObject);
        m_enemyPositions.Add(a_gameObject, a_gameObject.transform.position);
    }

    public void ResetPositions() {
        foreach (GameObject enemy in m_enemyTeam) {
            m_enemyPositions[enemy] = Vector3.zero;
        }
    }
}
