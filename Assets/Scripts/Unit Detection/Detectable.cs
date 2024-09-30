using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectable : MonoBehaviour
{
    [field: SerializeField]
    public List<Transform> points { get; private set; } = new List<Transform>();
}
