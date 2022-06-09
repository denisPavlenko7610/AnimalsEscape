using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsEscape
{
    public class Waypoints : MonoBehaviour
    {
        [field:SerializeField] public List<Transform> Points { get; private set; } = new();

        private void OnValidate()
        {
            if (Points.Count == 0)
            {
                FindWaypoints();
            }
        }

        private void FindWaypoints()
        {
            Points = transform.GetComponentsInChildren<Transform>().Skip(1).ToList();
        }

        [ContextMenu("ReInit")]
        private void ReInit()
        {
            FindWaypoints();
        }
    }
}
