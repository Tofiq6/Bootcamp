using UnityEngine;

namespace JusticeScale.Demo.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class ClickAndClone : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToClone;
        [SerializeField] private float customMass;
        

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnMouseDown()
        {
            if (prefabToClone == null) return;

            var clone = Instantiate(prefabToClone, transform.position, transform.rotation);

            var dragScript = clone.GetComponent<DragAndDrop>();
            clone.GetComponent<Rigidbody>().mass = customMass;
            clone.transform.localScale = transform.lossyScale;
            clone.layer = gameObject.layer;
            
            if (dragScript != null)
            {
                dragScript.isDragging = true; 
                dragScript.StartDrag();
            }
        }
    }
}