using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : PolishController
{

    private ParticleSystem pc;

    private bool wasEnabled = false;

    //Keep it for later
    IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        float trailTime = tr.time;
        tr.time = 0;
        yield return null;
        tr.time = trailTime;
    }

    public void Start()
    {
        pc = GetComponent<ParticleSystem>();
        pc.Stop();
    }

    public override void OnEnabledUpdate()
    {
        base.OnEnabledUpdate();
        if (!wasEnabled)
        {
            wasEnabled = true;
            pc.Play();
        }
    }
}
