using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AnimalsEscape
{
    public class FieldOfView : MonoBehaviour
    {
        [field: SerializeField, Range(1, 5)] public float ViewRadius { get; set; }
        [field: SerializeField, Range(0, 360)] public float ViewAngle { get; set; }
        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask ObstacleMask;
        [SerializeField] float _timeToRepeat = .2f;
        [SerializeField] MeshFilter _viewMeshFilter;
        [SerializeField] float _meshResolution = 10f;
        [SerializeField] float _edgeResolveInterations = 4f;
        [SerializeField] float _edgeDistanceTreshold = 0.5f;

        [HideInInspector] public List<Transform> VisibleTargets = new();
        
        Mesh _viewMesh;
        bool _isStop;
        CancellationTokenSource _cancelToken;

        void Start()
        {
            InitViewMesh();
            StartFinding();
        }

        private void LateUpdate()
        {
            DrawFieldOfView();
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool anglesIsGlobal)
        {
            if (!anglesIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            var vector = new Vector3(Mathf
                .Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf
                .Cos(angleInDegrees * Mathf.Deg2Rad));

            return vector;
        }

        void InitViewMesh()
        {
            _viewMesh = new Mesh();
            _viewMesh.name = "View Mesh";
            _viewMeshFilter.mesh = _viewMesh;
        }

        void DrawFieldOfView()
        {
            var stepCount = Mathf.RoundToInt(ViewAngle * _meshResolution);
            var stepAngleSize = ViewAngle / stepCount;
            List<Vector3> viewPoints = new();
            var oldViewCast = new ViewCastInfo();
            for (int i = 0; i <= stepCount; i++)
            {
                var angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
                var newViewCastInfo = ViewCast(angle);

                if (i > 0)
                {
                    bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.Distance - newViewCastInfo.Distance) > _edgeDistanceTreshold;
                    if (oldViewCast.Hit != newViewCastInfo.Hit || (oldViewCast.Hit && newViewCastInfo.Hit && edgeDistanceThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(oldViewCast, newViewCastInfo);
                        if (edge.PointA != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointA);
                        }
                        if (edge.PointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointB);
                        }
                    }
                }
                viewPoints.Add(newViewCastInfo.Point);
                oldViewCast = newViewCastInfo;
            }

            var vertexCount = viewPoints.Count + 1;
            var vertices = new Vector3[vertexCount];
            var triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }
            
            _viewMesh.Clear();
            _viewMesh.vertices = vertices;
            _viewMesh.triangles = triangles;
            _viewMesh.RecalculateNormals();
        }

        EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            var minAngle = minViewCast.Angle;
            var maxAngle = maxViewCast.Angle;
            var minPoint = Vector3.zero;
            var maxPoint = Vector3.zero;
            for (int i = 0; i < _edgeResolveInterations; i++)
            {
                var angle = (minAngle + maxAngle) / 2;
                var newViewCast = ViewCast(angle);

                bool edgeDistanceThresholdExceeded = Mathf.Abs(minViewCast.Distance - maxViewCast.Distance) > _edgeDistanceTreshold;
                if (newViewCast.Hit == minViewCast.Hit && !edgeDistanceThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.Point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.Point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }

        async UniTaskVoid StartFinding()
        {
            _cancelToken = new CancellationTokenSource();

            while (!_isStop)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeToRepeat), cancellationToken: _cancelToken.Token);
                //print(1);
                FindVisibleTargets();
            }
        }

        ViewCastInfo ViewCast(float globalAngle)
        {
            var direction = DirectionFromAngle(globalAngle, true);
            if (Physics.Raycast(transform.position, direction, out var hit, ViewRadius, ObstacleMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }

            return new ViewCastInfo(false, transform.position + direction * ViewRadius, ViewRadius, globalAngle);
        }

        void FindVisibleTargets()
        {
            VisibleTargets.Clear();
            Collider[] colliders = { };
            Physics.OverlapSphereNonAlloc(transform.position, ViewRadius, colliders, targetMask);
            foreach (var targetCollider in colliders)
            {
                var target = targetCollider.transform;
                var directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < ViewAngle / 2)
                {
                    var distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                    {
                        VisibleTargets.Add(target);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _cancelToken.Cancel();
        }

        struct ViewCastInfo
        {
            public bool Hit { get; }
            public Vector3 Point { get;}
            public float Distance { get; }
            public float Angle { get; }

            public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
            {
                Hit = hit;
                Point = point;
                Distance = distance;
                Angle = angle;
            }
        }
        
        struct EdgeInfo
        {
            public Vector3 PointA { get; }
            public Vector3 PointB { get; }

            public EdgeInfo(Vector3 pointA, Vector3 pointB)
            {
                PointA = pointA;
                PointB = pointB;
            }
        }
    }
}