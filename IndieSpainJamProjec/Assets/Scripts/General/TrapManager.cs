using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] private bool isPlayerOnTrap;
    [SerializeField] private GameObject checkObject;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask layerToCheckPlayer;

    private void Update()
    {
        PlayerDetect();
        CallToPlayerDeath();
    }

    private void PlayerDetect()
    {
        isPlayerOnTrap = Physics2D.OverlapBox(checkObject.transform.position, boxCheckSize, 0, layerToCheckPlayer);
    }

    private void CallToPlayerDeath()
    {
        if (isPlayerOnTrap)
        {
            PlayerDeath playerDeath = FindObjectOfType<PlayerDeath>().GetComponent<PlayerDeath>();
            playerDeath.OnDeath();
        }
    }

    [SerializeField] private bool seeGizmos;
    private void OnDrawGizmosSelected()
    {
        if (seeGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(checkObject.transform.position, boxCheckSize);
        }
    }
    
    
}
