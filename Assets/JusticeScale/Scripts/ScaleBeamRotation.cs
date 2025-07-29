using UnityEngine;

namespace JusticeScale.Scripts
{
    [RequireComponent(typeof(ScaleController))]
    public class ScaleBeamRotation : MonoBehaviour
    {
        [SerializeField] [Tooltip("Reference to the ScaleController that manages the balance logic.")]
        private ScaleController scaleController;

        [SerializeField]
        [Tooltip("The Transform representing the balance beam that rotates based on the weight difference.")]
        private Transform balanceBeam;

        [SerializeField]
        [Range(0, 75)]
        [Tooltip(
            "Maximum rotation angle of the balance beam in degrees. The beam will rotate between -blendRotation and +blendRotation. " +
            "It's recommended to test different values in the editor to see how the beam rotates visually.")]
        private float blendRotation = 15;

        private void Awake()
        {
            // Automatically get the ScaleController component attached to the same GameObject
            if (scaleController == null) scaleController = GetComponent<ScaleController>();
        }

        private void FixedUpdate()
        {
            // Calculates the rotation based on the normalized balance value.
            // If BalanceNormalized is 0, the beam is fully tilted to the left (-blendRotation).
            // If BalanceNormalized is 1, the beam is fully tilted to the right (+blendRotation).
            // If BalanceNormalized is 0.5, the beam is perfectly balanced (rotation = 0).
            var rotation = Mathf.Lerp(-blendRotation, blendRotation, scaleController.BalanceNormalized);

            // Apply the calculated rotation to the balance beam's local rotation.
            balanceBeam.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}