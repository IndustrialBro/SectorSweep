using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    float cone;
    float shotInterval;
    [SerializeField]
    int dmg, rps;
    [SerializeField]
    Transform barrel;
    // Start is called before the first frame update
    void Start()
    {
        //Já osobnì radiány nerad ale ok ig
        cone *= 3.14f / 180;
        shotInterval = 1 / rps;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(barrel.position, GetTrajectory() * 10);
        shotInterval -= Time.deltaTime;
    }

    public void Fire(string tarTag)
    {
        if (shotInterval > 0)
            return;

        if(Physics.Raycast(barrel.position, GetTrajectory(), out RaycastHit hit))
        {
            if (hit.collider.tag == tarTag)
            {
                hit.collider.GetComponent<HealthScript>().GetHurt(dmg);
            }
        }
    }

    Vector3 GetTrajectory()
    {
        float tg = Mathf.Tan(Random.Range(0, cone));
        Vector3 offDir = Random.insideUnitCircle.normalized;
        offDir = barrel.TransformDirection(offDir);

        return (offDir * tg) + barrel.forward;
    }
}
