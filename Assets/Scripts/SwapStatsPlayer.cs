using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapStatsPlayer : SwapStats
{
    public float startWalkSpeed = 7.5f;
    public float startRunSpeed = 8;
    public float startJumpSpeed = 11.5f;
    public float startGravitySpeed = 20f;
    public float startAmountofDoubleJumps = 1;
    public float startDoubleJumpHeight = 7.5f;
    
    [HideInInspector]
    public FloatRef WalkSpeed;
    [HideInInspector]
    public FloatRef RunSpeed;
    [HideInInspector]
    public FloatRef JumpSpeed;
    [HideInInspector]
    public FloatRef GravitySpeed;
    [HideInInspector] 
    public FloatRef DoubleJumpAmount;
    [HideInInspector] 
    public FloatRef DoubleJumpHeight;
    
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
        
        DoubleJumpAmount = ScriptableObject.CreateInstance<FloatRef>();
        DoubleJumpAmount.Name = "DoubleJumps";
        DoubleJumpAmount.Value = startAmountofDoubleJumps;
        floatList.Add(DoubleJumpAmount);
        
        DoubleJumpHeight = ScriptableObject.CreateInstance<FloatRef>();
        DoubleJumpHeight.Name = "DoubleJumpHeight";
        DoubleJumpHeight.Value = startDoubleJumpHeight;
        floatList.Add(DoubleJumpHeight);
    }

    public void resetvalues()
    {
        WalkSpeed.Value = startWalkSpeed;
        RunSpeed.Value = startRunSpeed;
        JumpSpeed.Value = startJumpSpeed;
        GravitySpeed.Value = startGravitySpeed;
        DoubleJumpAmount.Value = startAmountofDoubleJumps;
        DoubleJumpHeight.Value = startDoubleJumpHeight;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
