using System.Collections;
using UnityEngine;

public class MoveSpikes : MonoBehaviour
{
    
    private Vector3 startPosition;
    private Vector3 targetPosition;
    
    [Header("Positions")]
    [SerializeField] private Vector3 restPosition;
    
    [Header("Times and curves")]
    [SerializeField] private float timeToFinishTheAnimation;
    [SerializeField] private float ratioInside;
    [SerializeField] private float ratioOutside;
    [SerializeField] private AnimationCurve curve;
    
    [Header("Can hide")]
    [SerializeField] private bool spikesCanMove;

    private void Start()
    {
        Vector3 position = transform.position;
        
        startPosition = position;
        targetPosition = position - restPosition;

        StartCoroutine(RepeatCorroutineByBoolean());
    }

    IEnumerator RepeatCorroutineByBoolean()//Esto más adelante igual es mejor controlarlo de otra forma
    {
        while (spikesCanMove)
        {
            yield return new WaitForSeconds(ratioInside);
            StartCoroutine(ObjectTransformAnim());
            yield return new WaitForSeconds(ratioOutside);
            StartCoroutine(ObjectTransformAnimOut());
            
        }
    }

    //Lerp entre una posicion y otra, esto nos sirve tanto para pinchos como para plataformas móviles o elevadores que no dejen de moverse
    IEnumerator ObjectTransformAnim()
    {
        float timing = 0f;
        while (timing < timeToFinishTheAnimation)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, curve.Evaluate(timing / timeToFinishTheAnimation));
            timing += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        //SwitchBetweenPositions();
    }
    
    IEnumerator ObjectTransformAnimOut()
    {
        float timing = 0f;
        while (timing < timeToFinishTheAnimation)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, curve.Evaluate(timing / timeToFinishTheAnimation));
            timing += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        //SwitchBetweenPositions();
    }


    private void SwitchBetweenPositions()
    {
        targetPosition = startPosition;
        startPosition = transform.position;
    }
    
}
