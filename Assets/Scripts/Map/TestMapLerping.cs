using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class TestMapLerping : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Progress;
    public AILerp lerper;
    private bool checkflip = true;


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
            float distanceX = transform.position.x - pos.x;
            
            if ((distanceX < 0 && !checkflip) || (distanceX > 0 && checkflip))
            {
                // quay quái
                checkflip = !checkflip;
                Vector3 scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;

                // giữ nguyên thanh máu
                Vector3 scale1 = gameObject.GetComponentInChildren<Slider>().transform.localScale;
                scale1.x = -scale1.x;
                gameObject.GetComponentInChildren<Slider>().transform.localScale = scale1;
            }
            transform.position = pos;
            lerper.FinalizeMovement(pos, rot);
        }
    }
}
