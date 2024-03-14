using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class blink : MonoBehaviour
{
    private SkinnedMeshRenderer smr;

    private float timer = 0;
    [SerializeField] private float blinkDelay;
    [SerializeField] private float blinkTime;
    [SerializeField] private float blinkSpeed;

    private void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        smr.SetBlendShapeWeight(0, 0);
        InvokeRepeating(nameof(addTime), 1f, 0.2f);
    }


    void FixedUpdate()
    {
        

        if (timer >= blinkDelay)
        {
            timer = 0;
            smr.SetBlendShapeWeight(0, 100);
            Invoke(nameof(openEyes), blinkTime);
        }
       
    }

    void openEyes()
    {
        smr.SetBlendShapeWeight(0, 0);
    }

    void addTime()
    {
        timer += blinkSpeed;
    }
}
