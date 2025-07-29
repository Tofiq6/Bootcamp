using UnityEngine;

namespace JusticeScale.Demo.Scripts
{
    public class DestroyObjects : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        } 
    }
}
