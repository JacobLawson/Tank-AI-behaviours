using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityGoalHearing : UtilityGoal
{
    [SerializeField]
    TankHear hearingSphere;
    void Update()
    {
        //distance to heard sound determins insistance
        if (hearingSphere.sounds.Count > 0)
        {
            SetInsistance(110 - ((Vector3.Distance(transform.position, hearingSphere.sounds[0]) + hearingSphere.sounds.Count)));
        }
        else
        {
            SetInsistance(0f);
        }
    }
}
