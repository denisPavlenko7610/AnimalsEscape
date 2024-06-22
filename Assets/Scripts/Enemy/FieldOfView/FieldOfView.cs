using System;
using System.Collections.Generic;
using System.Threading;
using AnimalsEscape.Utils;
using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;

namespace AnimalsEscape
{
    public class FieldOfView : MonoBehaviour
    {
        [field: SerializeField, Range(1, 5)] public float ViewRadius { get; set; }
        [field: SerializeField, Range(0, 360)] public float ViewAngle { get; set; }
        [SerializeField] LayerMask _targetMask;
        [SerializeField] LayerMask _obstacleMask;
        [SerializeField] float _timeToRepeat = .2f;
        [SerializeField] MeshFilter _viewMeshFilter;
        [SerializeField] float _meshResolution = 10f;
        [SerializeField] float _edgeResolveInterations = 4f;
        [SerializeField] float _edgeDistanceTreshold = 0.5f;

        public event Action OnScannerAndTriggerReactHandler;

        Mesh _viewMesh;
        bool _isStop;
        CancellationTokenSource _cancelToken;
        Transform _meshTransform;

        void Start()
        {
            InitViewMesh();
            StartFinding();
        }

        void LateUpdate()
        {
            DrawFieldOfView();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.AnimalTag))
                return;

            if (other.gameObject.TryGetComponent(out AnimalStatus animalStatus) && animalStatus._animalState == AnimalState.alive)
            {
                if (other.gameObject.TryGetComponent(out AnimalHealth animalHealth))
                {
                    CheckAnimal(other.transform);
                    animalHealth.DecreaseHealth();
                }
            }

        }

        void InitViewMesh()
        {
            _viewMesh = new Mesh
            {
                name = "View Mesh"
            };
            _viewMeshFilter.mesh = _viewMesh;
            _meshTransform = _viewMeshFilter.transform;
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

        void FindVisibleTargets()
        {
            var targetsInViewRadius = Physics.OverlapSphere(_meshTransform.position, ViewRadius, _targetMask);
            foreach (Collider targetCollider in targetsInViewRadius)
            {
                Transform target = targetCollider.transform;
                Vector3 directionToTarget = (target.position - _meshTransform.position).normalized;

                if (Vector3.Angle(_meshTransform.forward, directionToTarget) < ViewAngle / 2)
                {
                    var distanceToTarget = Vector3.Distance(_meshTransform.position, target.position);
                    if (!Physics.Raycast(_meshTransform.position, _meshTransform.forward, distanceToTarget,
                            _obstacleMask))
                    {
                        //print("target: " + target.name);
                        if (target.gameObject.TryGetComponent(out AnimalStatus animalStatus) && animalStatus._animalState == AnimalState.alive)
                        {
                            if (target.gameObject.TryGetComponent(out AnimalHealth animalHealth))
                            {
                                CheckAnimal(target.transform);
                                animalHealth.DecreaseHealth();
                            }
                        }
                    }
                }
            }
        }

        void DrawFieldOfView()
        {
            int stepCount = Mathf.RoundToInt(ViewAngle * _meshResolution);
            float stepAngleSize = ViewAngle / stepCount;
            List<Vector3> viewPoints = new();
            ViewCastInfo oldViewCast = new ViewCastInfo();
            for (int i = 0; i <= stepCount; i++)
            {
                float angle = _meshTransform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCastInfo = ViewCast(angle);

                if (i > 0)
                {
                    bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.Distance - newViewCastInfo.Distance) >
                                                        _edgeDistanceTreshold;
                    if (oldViewCast.Hit != newViewCastInfo.Hit ||
                        (oldViewCast.Hit && newViewCastInfo.Hit && edgeDistanceThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(oldViewCast, newViewCastInfo);
                        if (edge.PointA != Vector3.zero)
                            viewPoints.Add(edge.PointA);

                        if (edge.PointB != Vector3.zero)
                            viewPoints.Add(edge.PointB);
                    }
                }

                viewPoints.Add(newViewCastInfo.Point);
                oldViewCast = newViewCastInfo;
            }

            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = _meshTransform.InverseTransformPoint(viewPoints[i]);
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
            float minAngle = minViewCast.Angle;
            float maxAngle = maxViewCast.Angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;
            for (int i = 0; i < _edgeResolveInterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(angle);

                bool edgeDistanceThresholdExceeded =
                    Mathf.Abs(minViewCast.Distance - maxViewCast.Distance) > _edgeDistanceTreshold;
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

        ViewCastInfo ViewCast(float globalAngle)
        {
            var direction = DirectionFromAngle(globalAngle, true);
            if (Physics.Raycast(_meshTransform.position, direction, out var hit, ViewRadius, _obstacleMask))
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);

            return new ViewCastInfo(false, _meshTransform.position + direction * ViewRadius, ViewRadius, globalAngle);
        }

        void CheckAnimal(Transform target)
        {
            if (target.CompareTag(Constants.AnimalTag))
                OnScannerAndTriggerReactHandler?.Invoke();
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool anglesIsGlobal)
        {
            if (!anglesIsGlobal)
                angleInDegrees += transform.eulerAngles.y;

            var vector = new Vector3(Mathf
                .Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf
                .Cos(angleInDegrees * Mathf.Deg2Rad));

            return vector;
        }

        struct ViewCastInfo
        {
            public bool Hit { get; }
            public Vector3 Point { get; }
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

        void OnDestroy()
        {
            if (_cancelToken != null)
                _cancelToken.Cancel();
        }
    }
}