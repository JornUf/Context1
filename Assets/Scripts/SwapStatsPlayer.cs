using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapStatsPlayer : SwapStats
{
    public float startWalkSpeed = 7.5f;
    public float startRunSpeed = 8;
    public float startJumpSpeed = 11.5f;
    public float startGravitySpeed = 20f;
    
    [HideInInspector]
    public FloatRef WalkSpeed;
    [HideInInspector]
    public FloatRef RunSpeed;
    [HideInInspector]
    public FloatRef JumpSpeed;
    [HideInInspector]
    public FloatRef GravitySpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        WalkSpeed = ScriptableObject.CreateInstance<FloatRef>();
        WalkSpeed.Name = "WalkSpeed";
        WalkSpeed.Value = startWalkSpeed;
        floatList.Add(WalkSpeed);
        
        RunSpeed = ScriptableObject.CreateInstance<FloatRef>();
        RunSpeed.Name = "RunSpeed";
        RunSpeed.Value = startRunSpeed;
        floatList.Add(RunSpeed);
        
        JumpSpeed = ScriptableObject.CreateInstance<FloatRef>();
        JumpSpeed.Name = "JumpSpeed";
        JumpSpeed.Value = startJumpSpeed;
        floatList.Add(JumpSpeed);
        
        GravitySpeed = ScriptableObject.CreateInstance<FloatRef>();
        GravitySpeed.Name = "FallSpeed";
        GravitySpeed.Value = startGravitySpeed;
        floatList.Add(GravitySpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
