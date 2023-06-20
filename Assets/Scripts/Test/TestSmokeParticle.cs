using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestSmokeParticle : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] buffer = new ParticleSystem.Particle[5];
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();
        if (ps == null || ps.particleCount <= 0)
            return;
        int count = 0;
        while (count < ps.particleCount)
        {
            int add = ps.GetParticles(buffer, buffer.Length, count);
            for(int i = 0; i < buffer.Length; i++)
            {
                var particle = buffer[i];
                if (particle.startLifetime - particle.remainingLifetime <= Time.deltaTime)
                {
                    if (particle.totalVelocity.x >= 0)
                    {
                        var rot = particle.rotation3D;
                        rot.y = 180;
                        particle.rotation3D = rot;
                        buffer[i] = particle;
                    }
                }
            }
            ps.SetParticles(buffer, add, count);
            count += add;
        }
    }
}
