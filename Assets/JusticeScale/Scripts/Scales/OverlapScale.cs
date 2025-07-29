using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JusticeScale.Scripts.Scales
{
    public class OverlapScale : Scale
    {
        [Header("Capsule Config")]
        
        [Min(0)] [SerializeField]
        private float capsuleLength = 1.5f; // The vertical length of the capsule used to detect objects above the scale
        
        [Min(0)] [SerializeField]
        private float capsuleRadius = 0.5f; // The radius of the capsule, determining its width
        
        [Min(0)] [SerializeField]
        private float capsuleOffsetHeight = 0.5f; // The vertical offset from the scale’s position to the start of the capsule

        private Vector3 _startPoint, _endPoint; // The calculated start and end points of the capsule

        private readonly HashSet<Rigidbody> _detectedObjects = new(); // Set of detected rigidbodies to avoid duplicates
        private readonly List<Rigidbody> _objectsInContainer = new(); // List of objects currently inside the capsule area
        private GameObject _objectContainer; // A container object to parent detected objects
        
        private float _previousWeight; // Stores the previous total weight to optimize calculations
        
        // Returns the calculated total weight of objects detected inside the capsule
        public override float TotalWeight => CalculateWeightInCapsule(); 

        private float CalculateWeightInCapsule()
        {
            weight = 0f;
            _detectedObjects.Clear();

            // Adjust the capsule variables by the scale of the GameObject
            var scaledCapsuleLength = capsuleLength * transform.lossyScale.normalized.magnitude;
            var scaledCapsuleRadius = capsuleRadius * transform.lossyScale.normalized.magnitude;
            var scaledCapsuleOffsetHeight = capsuleOffsetHeight * transform.lossyScale.normalized.magnitude;
            
            // Define capsule start and end points based on configuration, adjusted for the scale
            _startPoint = transform.position + Vector3.up * scaledCapsuleOffsetHeight;
            _endPoint = _startPoint + Vector3.up * scaledCapsuleLength;

            // Perform the OverlapCapsule detection, gathering all colliders within the volume
            // ReSharper disable once Unity.PreferNonAllocApi
            Collider[] colliders = Physics.OverlapCapsule(_startPoint, _endPoint, scaledCapsuleRadius, layerMask);
            
            foreach (var collider in colliders)
            {
                var rb = collider.GetComponent<Rigidbody>();
                if (rb != null && !_detectedObjects.Contains(rb))
                {
                    _detectedObjects.Add(rb);
                    ManageObjectContainer(rb);

                    weight += rb.mass;

                    if (!_objectsInContainer.Contains(rb)) _objectsInContainer.Add(rb);
                }
            }

            RemoveDetectedObjectsNotInCapsule(_detectedObjects);

            // Exit early if the weight hasn't changed significantly
            if (Mathf.Abs(weight - _previousWeight) < 0.001f) return _previousWeight;

            _previousWeight = weight;
            return weight;
        }

        private void ManageObjectContainer(Rigidbody rb)
        {
            if (_objectContainer == null)
            {
                _objectContainer = new GameObject("Objects Container");
                _objectContainer.transform.parent = transform;
            }

            rb.transform.SetParent(_objectContainer.transform, true);
        }

        private void RemoveDetectedObjectsNotInCapsule(HashSet<Rigidbody> detectedObjects)
        {
            foreach (var obj in _objectsInContainer.ToList())
                if (!detectedObjects.Contains(obj))
                {
                    obj.transform.parent = null;
                    _objectsInContainer.Remove(obj);
                }

            if (detectedObjects.Count == 0) Destroy(_objectContainer);
        }
        
        private void OnDrawGizmosSelected()
        {
            if (transform == null) return;
            
            // Adjust the capsule variables by the scale of the GameObject
            float scaledCapsuleLength = capsuleLength * 0.1f * transform.lossyScale.magnitude;
            float scaledCapsuleRadius = capsuleRadius * 0.1f * transform.lossyScale.magnitude;
            float scaledCapsuleOffsetHeight = capsuleOffsetHeight * 0.1f * transform.lossyScale.magnitude;

            // Define the start and end points, adjusted for the object's scale
            _startPoint = transform.position + Vector3.up * scaledCapsuleOffsetHeight;
            _endPoint = _startPoint + Vector3.up * scaledCapsuleLength;

            // Draw the capsule in the editor (with scaling applied)
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_startPoint, scaledCapsuleRadius);
            Gizmos.DrawWireSphere(_endPoint, scaledCapsuleRadius);
            Gizmos.DrawLine(
                _startPoint + Vector3.up * scaledCapsuleRadius,
                _endPoint + Vector3.up * scaledCapsuleRadius
            );
            Gizmos.DrawLine(
                _startPoint - Vector3.up * scaledCapsuleRadius,
                _endPoint - Vector3.up * scaledCapsuleRadius
            );
        }
    }
}