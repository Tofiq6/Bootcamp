using UnityEngine;

public class ZoneMusicTrigger : MonoBehaviour
{
    public enum ZoneType { Cave, Castle }
    public ZoneType zoneType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoneType == ZoneType.Cave)
                AudioManager.Instance.EnterZone(AudioManager.Instance.caveClip);
            else if (zoneType == ZoneType.Castle)
                AudioManager.Instance.EnterZone(AudioManager.Instance.castleClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.ExitZone();
        }
    }
}
