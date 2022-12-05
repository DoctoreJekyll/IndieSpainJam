using UnityEngine;

public class WaterDropCollision : MonoBehaviour
{
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Activable"))
        {
            if (other.GetComponent<IAffectedByWaterDrop>() != null)
            {
                IAffectedByWaterDrop iAffectedByWaterDrop = other.GetComponent<IAffectedByWaterDrop>();
                iAffectedByWaterDrop.DoStuffsWhenWaterTouch();
            }

        }
    }
}
