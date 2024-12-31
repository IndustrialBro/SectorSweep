using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    protected float cone, rps;
    protected float shotInterval = 0;
    [SerializeField]
    protected int dmg;
    [SerializeField]
    protected Transform barrel;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Já osobnì radiány nerad ale ok ig
        cone *= 3.14f / 180;
    }

    // Update is called once per frame
    protected void Update()
    {
        Debug.DrawRay(barrel.position, GetTrajectory() * 30);
        shotInterval -= Time.deltaTime;
    }

    public void Fire(string tarTag)
    {
        if (shotInterval > 0)
            return;

        if(Physics.Raycast(barrel.position, GetTrajectory(), out RaycastHit hit, 30))
        {
            if (hit.collider.tag == tarTag)
            {
                hit.collider.GetComponent<StateMachine>()?.OnHit(dmg, gameObject);
            }
        }
        
        shotInterval = 1f / rps;
    }

    protected Vector3 GetTrajectory()
    {
        float tg = Mathf.Tan(Random.Range(0, cone));
        Vector3 offDir = Random.insideUnitCircle.normalized;
        offDir = barrel.TransformDirection(offDir);

        return (offDir * tg) + barrel.forward;
    }
}
