using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    int maxHp;
    int currHp;
    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
    }

    public void GetHurt(int dmg)
    {
        currHp -= dmg;
        
        if (currHp < 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
