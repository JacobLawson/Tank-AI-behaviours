using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHear : MonoBehaviour
{
    //list of sound hit positions
    public List<Vector3> sounds;
    private void Start() {
        sounds = new List<Vector3>();
    }

    private void OnEnable() {
        sounds = new List<Vector3>();
    }
    private void OnTriggerEnter(Collider other) {
        //if trigger is hit get the position at which the sound is from and store
        if (other.tag == "Player" || other.tag == "Sound") {
            sounds.Add(other.transform.position);
        }
    }
}
