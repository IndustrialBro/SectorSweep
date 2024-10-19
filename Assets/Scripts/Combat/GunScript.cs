using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    float cone;
    [SerializeField]
    int dmg;
    // Start is called before the first frame update
    void Start()
    {
        //Já osobnì radiány nerad ale ok ig
        cone *= 3.14f / 180;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, GetTrajectory() * 10);
    }

    public void Fire(string tarTag)
    {
        if(Physics.Raycast(transform.position, GetTrajectory(), out RaycastHit hit))
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
        offDir = transform.TransformDirection(offDir);

        return (offDir * tg) + transform.forward;
    }
}
