using UnityEditor;
using UnityEngine;

namespace AnimalsEscape
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        float _angle = 360f;

        void OnSceneGUI()
        {
            var fow = (FieldOfView)target;
            Handles.color = Color.white;
            Vector3 fowPosition = fow.transform.position;
            Handles.DrawWireArc(fowPosition, Vector3.up, Vector3.forward, _angle, fow.ViewRadius);
            Vector3 viewAngleA = fow.DirectionFromAngle(-fow.ViewAngle / 2, false);
            Vector3 viewAngleB = fow.DirectionFromAngle(fow.ViewAngle / 2, false);

            Handles.DrawLine(fowPosition, fowPosition + viewAngleA * fow.ViewRadius);
            Handles.DrawLine(fowPosition, fowPosition + viewAngleB * fow.ViewRadius);
        }
    }
}