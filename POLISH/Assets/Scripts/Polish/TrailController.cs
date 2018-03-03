using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : PolishController {

    private TrailRenderer trail;

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
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    public override void OnEnabledUpdate()
    {
        base.OnEnabledUpdate();
        if (!wasEnabled)
        {
            wasEnabled = true;
            trail.enabled = true;
        }
    }

    public void NextLevel()
    {
        StartCoroutine(ResetTrailRenderer(trail));
    }
}
