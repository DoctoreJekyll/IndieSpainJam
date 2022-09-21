using UnityEngine.Events;
using UnityEngine;

public class LeverEvents : MonoBehaviour, IActivable
{

    [SerializeField] private UnityEvent myEvent;
    
    public void DoActivate()
    {
        myEvent.Invoke();
    }
}
