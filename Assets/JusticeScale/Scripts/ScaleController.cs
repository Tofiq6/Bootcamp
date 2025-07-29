using JusticeScale.Scripts.Scales;
using UnityEngine;

namespace JusticeScale.Scripts
{
    public class ScaleController : MonoBehaviour
    {
        [Header("Scales")]
        [SerializeField]
        [Tooltip("Reference to the left scale. Should be assigned to the left side of the balance.")]
        private Scale leftScale;

        [SerializeField] 
        [Tooltip("Reference to the right scale. Should be assigned to the right side of the balance.")]
        private Scale rightScale;

        [Tooltip("Normalized value representing the current balance. 0 means fully tilted to the left, 1 means fully tilted to the right, and 0.5 is perfectly balanced.")]
        public float BalanceNormalized { get; private set; } = 0.5f;

        [Tooltip("The difference in weight between the left and right scales. A positive value means the left scale is heavier, while a negative value means the right scale is heavier.")]
        public float WeightDifference { get; private set; }

        [Header("Configuration")]
        [Min(0.01f)]
        [Tooltip("The maximum weight difference between the two sides for the scale to reach its limit. A higher value makes the balance less sensitive.")]
        public float maxWeightDifference = 10f;

        [SerializeField]
        [Tooltip("The smoothing time for updating the balance. This helps to avoid abrupt changes that could cause erratic behavior or glitches in the balance's operation.")]
        private float balanceSmoothTime = 0.05f;

        [SerializeField] [Tooltip("Internal smoothed result for the balance. Used for gradual balance adjustments.")]
        private float weightResultSmoothed;

        private void Update()
        {
            if (!leftScale || !rightScale)
            {
                Debug.LogWarning("The scale references are not assigned.");
                return;
            }
            
            UpdateBalance();
        }

        private void UpdateBalance()
        {
            var targetWeightDifference = leftScale.TotalWeight - rightScale.TotalWeight;
            WeightDifference = targetWeightDifference;
            weightResultSmoothed = Mathf.Lerp(weightResultSmoothed, targetWeightDifference, balanceSmoothTime);

            BalanceNormalized = Mathf.InverseLerp(-maxWeightDifference, maxWeightDifference, weightResultSmoothed);
        }
    }
}