using UnityEngine;

public class EnemyLife : MonoBehaviour, IActivable
{

    //Esto seria para matar enemigos de un toque
    [SerializeField] private int life;

    private void Start()
    {
        life = 1;
    }

    private void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void LoseLife(int amount)
    {
        life -= amount;
    }

    public void DoActivate()
    {
        LoseLife(1);
    }
}
