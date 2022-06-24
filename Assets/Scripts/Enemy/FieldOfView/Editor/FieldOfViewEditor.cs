using UnityEditor;
using UnityEngine;

namespace AnimalsEscape
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private float _angle = 360f;

        private void OnSceneGUI()
        {
            var fow = (FieldOfView)target;
            Handles.color = Color.white;
            var fowPosition = fow.transform.position;
            Handles.DrawWireArc(fowPosition, Vector3.up, Vector3.forward, _angle, fow.ViewRadius);
            var viewAngleA = fow.DirectionFromAngle(-fow.ViewAngle / 2, false);
            var viewAngleB = fow.DirectionFromAngle(fow.ViewAngle / 2, false);

            Handles.DrawLine(fowPosition, fowPosition + viewAngleA * fow.ViewRadius);
            Handles.DrawLine(fowPosition, fowPosition + viewAngleB * fow.ViewRadius);
            Handles.color = Color.red;
            foreach (var visibleTarget in fow.VisibleTargets)
            {
                Handles.DrawLine(fowPosition, visibleTarget.position);
            }
        }
    }
}