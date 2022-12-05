using System.Collections;
using UnityEngine;

public class ObjectWaterAffected : MonoBehaviour, IAffectedByWaterDrop
{

    [SerializeField] private bool isAffectedByParticle;
    private bool isUsed;

    [Header("Config")]
    [SerializeField] private Vector3 actualPosition;
    [SerializeField] private Vector3 addedPosition;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float timeToFinishGrowUp;
    private Vector3 targetPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        actualPosition = transform.position;
        targetPosition = actualPosition + addedPosition;
    }

    // Update is called once per frame
    void Update()//Estoy testeando como hacer el growup, de momento voy a dejarlo con una corrutina basica hasta que se decida como funciona
    {
        if (isAffectedByParticle)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            if (transform.position.y >= targetPosition.y)
            {
                isAffectedByParticle = false;
            }
        }
        
    }

    public void DoStuffsWhenWaterTouch()
    {
        if (!isUsed)
        {
            StartCoroutine(ObjectTransformAnim());
        }
        

    }
    
    IEnumerator ObjectTransformAnim()
    {
        isUsed = true;
        float timing = 0f;
        while (timing < timeToFinishGrowUp)
        {
            transform.position = Vector3.Lerp(actualPosition, targetPosition, curve.Evaluate(timing / timeToFinishGrowUp));
            timing += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void DoStuffsWhenWaterDontTouch()
    {
        isAffectedByParticle = false;
    }
}
