using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _partHielo;
    [SerializeField]
    ParticleSystem _partAgua;
    [SerializeField]
    ParticleSystem _partNubes;
    [SerializeField]
    Animator _anim;
    ParticleSystem _sistema;
    
    public void AnimarTransicion(int modo)
    {
        //Haced lo que queráis para que el script sepa qué sistema tiene que activar.
        //Sugerencia: usad el int para pasar temperatura o algo que se os ocurra.
        //Hecho eso sólo tenéis que activar el objeto ParticleSystem que corresponda al nuevo estado.
        //Dicho ParticleSystem se desactivará solo al terminar la transición.
        //Y asignadlo a la variable _sistema.
        _anim.gameObject.SetActive(true);
        _sistema.Play();

    }
}
