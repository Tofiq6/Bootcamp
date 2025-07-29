using JusticeScale.Scripts;
using UnityEngine;

namespace JusticeScale.Demo.Scripts
{
    [RequireComponent(typeof(Rigidbody)),RequireComponent(typeof(Collider))]
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 _mousePosition, _mouseCameraPos, _initialPosition;
        private Transform _scaleTransform;
        
        private Rigidbody _objectRigidbody;
        private float _lerpPosition;
        
        [HideInInspector] public bool isDragging; 
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Awake()
        {
            _objectRigidbody = GetComponent<Rigidbody>();
            _scaleTransform = FindAnyObjectByType<ScaleController>().transform;
        }

        private Vector3 GetMousePos()
        {
            return Camera.main != null ? Camera.main.WorldToScreenPoint(transform.position) : Vector3.zero;
        }

        public void StartDrag()
        {
            if (_objectRigidbody == null) return;

            _objectRigidbody.useGravity = false;
#if UNITY_6000_0_OR_NEWER
            _objectRigidbody.linearVelocity = Vector3.zero;
#else            
            _objectRigidbody.velocity = Vector3.zero;
#endif
            
            _initialPosition = transform.position;
            _mousePosition = Input.mousePosition - GetMousePos();
            isDragging = true;
        }

        private void Update()
        {
            if (isDragging && _camera)
            {
                _objectRigidbody.angularVelocity = Vector3.zero;
                _mouseCameraPos = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
                
                _lerpPosition = Mathf.InverseLerp(_initialPosition.x, _scaleTransform.position.x, _mouseCameraPos.x);

                if (Mathf.Approximately(_lerpPosition, 1)) _initialPosition = _scaleTransform.transform.position;

                transform.position = new Vector3(
                    _mouseCameraPos.x,
                    _mouseCameraPos.y,
                    Mathf.Lerp(_initialPosition.z, _scaleTransform.position.z, _lerpPosition));

                if (Input.GetMouseButtonUp(0))
                    EndDrag();
            }
        }

        private void EndDrag()
        {
            if (!_objectRigidbody) return;
           
            isDragging = false;
            _objectRigidbody.useGravity = true;
        }

        private void OnMouseDown()
        {
            StartDrag();
        }
    }
}