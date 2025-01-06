using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowedUnit : MonoBehaviour
{
    [SerializeField]
    GameObject follower;
    [field : SerializeField]
    public string unitName { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        GameObject nf = Instantiate(follower);
        nf.GetComponent<UnitFollower>().FollowUnit(this);
    }
    public void SetUp(string unitName, GameObject follower)
    {
        if (string.IsNullOrEmpty(this.unitName))
        {
            this.unitName = unitName;
        }
        this.follower = follower;
    }
}
