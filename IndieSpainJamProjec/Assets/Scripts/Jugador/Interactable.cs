using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float range;
    public PlayerJump playerJump;
    public Animator waterAnimator;
    
    
    public void InteractAction(InputAction.CallbackContext context)//Se llama en el input system
    {
        if (context.performed && playerJump.isOnFloor)
        {
            waterAnimator.Play("IDLE");
            waterAnimator.Play("INTERACTING");

            DetectStuffs();
        }
        
    }
    
    private void DetectStuffs()//Podemos llamar también esto al atacar y generará un bool si colisiona con un enemigo
    {
        Collider2D[] hitSomething = Physics2D.OverlapCircleAll(transform.position, range, interactableLayers);

        foreach (Collider2D enemy in hitSomething)
        {
            //Creamos la interfaz/objeto, si existe llamamos a su metodo/contrato establecido que en función de lo que sea hará una cosa u otra
            //Es decir, el doactivate del enemigo no hara lo mismo que el de una palanca pero ese no es el problema del player
            IActivable interactable = enemy.GetComponent<IActivable>();
            if (interactable == null)
            {
                return;
            }
            interactable.DoActivate();

        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
