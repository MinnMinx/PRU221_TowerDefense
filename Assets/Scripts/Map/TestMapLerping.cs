using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestMapLerping : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Progress;
    public AILerp lerper;

    // Start is called before the first frame update
    void Start()
    {
        lerper.Teleport(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lerper.reachedDestination)
        {
            lerper.MovementUpdate(Time.deltaTime, out Vector3 pos, out Quaternion rot);
            transform.position = pos;
            lerper.FinalizeMovement(pos, rot);
        }
    }
}
