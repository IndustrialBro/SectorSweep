using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    [SerializeField]
    Transform eye;
    [SerializeField]
    string visTag;
    [SerializeField]
    float visDist, detAngle;

    List<GameObject> du = new List<GameObject>();
    LayerMask mask;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Detectable");
    }

    public bool CheckForUnits(out GameObject[] detectedObjects)
    {
        du.Clear();
        bool b = false;

        Collider[] colls = Physics.OverlapSphere(transform.position, visDist, mask);
        if(colls.Length > 0)
        {
            foreach(Collider coll in colls)
            {
                if(coll.tag == visTag)
                {
                    Detectable d = coll.GetComponent<Detectable>();
                    foreach(Transform point in d.points)
                    {
                        if(CheckAngle(point) && !Physics.Linecast(eye.position, point.position, ~mask))
                        {
                            b = true;
                            du.Add(d.gameObject);
                            break;
                        }
                    }
                }
            }
        }
        
        detectedObjects = du.ToArray();
        return b;
    }

    bool CheckAngle(Transform ut)
    {
        Vector3 dir = ut.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);

        if (angle < detAngle)
            return true;
        return false;
    }
}
