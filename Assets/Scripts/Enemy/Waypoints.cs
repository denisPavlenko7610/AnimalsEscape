using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsEscape
{
    public class Waypoints : MonoBehaviour
    {
        [field:SerializeField] public List<Transform> Points { get; private set; } = new();

        void FindWaypoints()
        {
            Points = transform.GetComponentsInChildren<Transform>().Skip(1).ToList();
        }

        [ContextMenu("ReInit")]
        void ReInit()
        {
            FindWaypoints();
        }
    }
}
