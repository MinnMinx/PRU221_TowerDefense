using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestSmokeParticle : MonoBehaviour
{
    private ParticleSystem ps;
    //private ParticleSystem.Particle[] buffer = new ParticleSystem.Particle[5];
    private Vector3 lastFramePos, lastSavedPos;
    private float goneDistance;
    private const float DISTANT_PER_PARTICLE = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        lastFramePos = transform.position;
        lastSavedPos = transform.position;
        goneDistance = 0;
    }

    private void Update()
    {
        goneDistance += Vector3.Distance(lastFramePos, transform.position);
        if (goneDistance >= DISTANT_PER_PARTICLE)
        {
            ps.Emit(new ParticleSystem.EmitParams
            {
                position = lastSavedPos,
                velocity = (lastFramePos - transform.position) / Time.deltaTime * 0.05f,
                rotation3D = new Vector3(0, lastSavedPos.x <= transform.position.x ? 0 : 180, 0),
                startSize = 0.6f,
            }, Mathf.CeilToInt(goneDistance / DISTANT_PER_PARTICLE));
            lastSavedPos = transform.position;
            goneDistance = 0;
        }
        lastFramePos = transform.position;
    }

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    if (ps == null)
    //        ps = GetComponent<ParticleSystem>();
    //    if (ps == null || ps.particleCount <= 0)
    //        return;
    //    int count = 0;
    //    while (count < ps.particleCount)
    //    {
    //        int add = ps.GetParticles(buffer, buffer.Length, count);
    //        for(int i = 0; i < buffer.Length; i++)
    //        {
    //            var particle = buffer[i];
    //            if (particle.startLifetime - particle.remainingLifetime <= Time.deltaTime)
    //            {
    //                if (particle.totalVelocity.x >= 0)
    //                {
    //                    var rot = particle.rotation3D;
    //                    rot.y = 180;
    //                    particle.rotation3D = rot;
    //                    buffer[i] = particle;
    //                }
    //            }
    //        }
    //        ps.SetParticles(buffer, add, count);
    //        count += add;
    //    }
    //}
}
