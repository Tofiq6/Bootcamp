using System;
using JusticeScale.Scripts.Scales;
using UnityEngine;
using UnityEngine.UI;

namespace JusticeScale.Scripts.UI
{
    public class ScaleUI : MonoBehaviour
    {
        [SerializeField] private Scale scale; // Reference to the Scale component
        [SerializeField] private Text balanceText;

        [SerializeField] private bool isInPounds;

        private void Awake()
        {
            // Automatically get the TriggerScale component attached to the parent GameObject
            if (scale == null) scale = GetComponentInParent<TriggerScale>();

            // Automatically get the Text component attached to the child GameObject
            if (balanceText == null) balanceText = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            // Update the balance text based on the selected unit
            balanceText.text = isInPounds 
                ? $"{ConvertKgToPound(scale.TotalWeight)} pounds" 
                : $"{scale.TotalWeight} kg";
        }

        private float ConvertKgToPound(float massKg)
        {
            var poundConverse = massKg * 2.20f;
            return (float)Math.Round(poundConverse, 2); // Round to 2 decimal places
        }
    }
}