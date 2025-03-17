using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LegAnimator : MonoBehaviour
{
    [SerializeField]
    Transform leftFootIK, rightFootIK, leftRaySrc, rightRaySrc;
    Leg leftLeg, rightLeg, currLeg;

    [SerializeField]
    float maxDistance, yOffset, forwardOffset, midPointOffset, moveDuration;

    bool isMoving = false;
    float elapsedTime = 0;
    Vector3 targetPos, midPoint;
    void Awake()
    {
        leftFootIK.SetParent(null);
        rightFootIK.SetParent(null);
    }
    private void Start()
    {
        leftLeg = new Leg(leftFootIK, leftRaySrc);
        rightLeg = new Leg(rightFootIK, rightRaySrc);
        currLeg = leftLeg;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Physics.Raycast(currLeg.raySrc.position, -currLeg.raySrc.up, out RaycastHit hit) && Vector3.Distance(currLeg.ik.position, hit.point) > maxDistance)
        {
            targetPos = hit.point + (hit.point - currLeg.ik.position).normalized * forwardOffset;
            midPoint = new Vector3((currLeg.ik.position.x + targetPos.x) / 2, ((currLeg.ik.position.y + targetPos.y) / 2) + midPointOffset, (currLeg.ik.position.z +  targetPos.z) / 2);
            elapsedTime = 0;
            isMoving = true;
        }
        else if(isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / moveDuration, 0, 1);

            currLeg.ik.position = Vector3.Lerp(Vector3.Lerp(currLeg.ik.position, midPoint,t), Vector3.Lerp(midPoint, targetPos, t), t);
            if(currLeg.ik.position == targetPos)
            {
                SwitchLegs();
                isMoving = false;
            }
        }
    }
    void SwitchLegs()
    {
        if(currLeg == leftLeg)
            currLeg = rightLeg;
        else
            currLeg = leftLeg;
    }
}
public class Leg
{
    public Leg(Transform ik, Transform raySrc)
    {
        this.ik = ik;
        this.raySrc = raySrc;
    }
    public Transform ik {  get; private set; }
    public Transform raySrc { get; private set; }
}