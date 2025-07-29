using UnityEngine;

namespace JusticeScale.Scripts.Scales
{
    public abstract class Scale : MonoBehaviour
    {
        public abstract float TotalWeight { get; }
        [SerializeField] protected LayerMask layerMask = -1; // LayerMask specifying which layers to include in the scale detection

        [SerializeField] [Tooltip("The current weight of the scale (not visible in the Inspector), for easy developer testing.")]
        protected float weight; // Current weight for inspector display; TotalWeight is the actual calculated value.

        private void Update()
        {
            weight = TotalWeight;
        }
    }
}