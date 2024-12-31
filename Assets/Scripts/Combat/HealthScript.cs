using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    protected int maxHp;
    protected int currHp;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currHp = maxHp;
    }

    public void GetHurt(int dmg)
    {
        currHp -= dmg;
        
        if (currHp < 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
