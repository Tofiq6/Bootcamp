using JusticeScale.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace JusticeScale.Demo.Scripts
{
    public class SliderWeight : MonoBehaviour
    {
        [SerializeField] private ScaleController scaleController;
        [SerializeField] private Text weightText;
    
    
        public void SetWeightValue(float value)
        {
            scaleController.maxWeightDifference = value;
            weightText.text = $"Max weight difference between scales set to {(int)value} kg";
        }
    }
}