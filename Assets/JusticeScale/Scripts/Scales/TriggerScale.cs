using System.Collections.Generic;
using UnityEngine;

namespace JusticeScale.Scripts.Scales
{
    [RequireComponent(typeof(MeshCollider))]
    public class TriggerScale : Scale
    {
        [Tooltip("The total weight of the objects currently on this scale trigger.")]
        public override float TotalWeight => weight;

        // Container for objects detected by the scale
        private GameObject _objectContainer;
        
        // HashSet to track objects on the scale, ensuring each is only counted once 
        // (useful if the GameObject has multiple triggers that could detect the same object).
        private HashSet<Transform> _detectedObjects = new HashSet<Transform>();

        private void Start()
        {
            // Initialize the object container at the start
            _objectContainer = new GameObject("Objects Container");
            _objectContainer.transform.parent = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null && IsInDetectableLayer(other.gameObject) && _detectedObjects.Add(rb.transform))
            {
                AddObjectToContainer(rb.transform);
                weight += rb.mass;
                // Round the total weight to 2 decimal places
                weight = Mathf.Round(weight * 100f) / 100f;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null && _detectedObjects.Remove(rb.transform)) 
            {
                weight -= rb.mass;
                // Ensure total weight doesn't drop below zero
                weight = Mathf.Max(0, weight);
                weight = Mathf.Round(weight * 100f) / 100f;

                RemoveObjectFromContainer(rb.transform);
            }
        }

        private void AddObjectToContainer(Transform objectTransform)
        {
            if (_objectContainer == null)
            {
                // Create a new container for the objects if it doesn't exist
                _objectContainer = new GameObject("Objects Container");
                _objectContainer.transform.parent = transform;
            }

            // Set the parent of the object to the container
            objectTransform.SetParent(_objectContainer.transform, true);
        }

        private void RemoveObjectFromContainer(Transform objectTransform)
        {
            // Unparent the object from the container
            if (_objectContainer != null) objectTransform.parent = null;

            // Destroy the container if it has no child objects left
            if (_objectContainer.transform.childCount == 0) Destroy(_objectContainer);
        }
        
        private bool IsInDetectableLayer(GameObject obj)
        {
            // Check if the object is in the correct layer 
            return ((layerMask.value & (1 << obj.layer)) != 0);
        }
    }
}