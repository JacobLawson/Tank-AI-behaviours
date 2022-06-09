using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FieldOfView))]
public class FieldOfVision : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.GetViewRadius());
        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.GetViewAngle() / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.GetViewAngle() / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.GetViewRadius());
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.GetViewRadius());

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fov.GetVisibleTargets())
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
