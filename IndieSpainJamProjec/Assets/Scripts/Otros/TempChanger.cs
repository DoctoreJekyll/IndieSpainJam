using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChanger : MonoBehaviour
{
    public int temperatura;
    public int intensidad;
    TempManager mgr;
    // Start is called before the first frame update
    void Start()
    {
        mgr = Camera.main.GetComponent<TempManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
            //Debug.Log(potencia);
            mgr.cambiaTemperatura(temperatura, (intensidad + (potencia * 0.5f)));
        }
    }
}
