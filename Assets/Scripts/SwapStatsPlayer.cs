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
    public float startDoubler = 1;
    public float startSquare = 1;
    public float startHalf = 1;
    public float startRNGMin = 0;
    public float startRNGMax = 10;
    
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

    private FloatRef Doubler;
    private FloatRef Square;
    private FloatRef Half;
    private FloatRef Random;
    
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
        
        Doubler = ScriptableObject.CreateInstance<FloatRef>();
        Doubler.Name = "Double";
        Doubler.Value = startDoubler;
        floatList.Add(Doubler);

        Square = ScriptableObject.CreateInstance<FloatRef>();
        Square.Name = "Square";
        Square.Value = startSquare;
        floatList.Add(Square);

        Half = ScriptableObject.CreateInstance<FloatRef>();
        Half.Name = "Half";
        Half.Value = startHalf;
        floatList.Add(Half);

        Random = ScriptableObject.CreateInstance<FloatRef>();
        Random.Name = "Random";
        Random.Value = UnityEngine.Random.Range(startRNGMin, startRNGMax);
        floatList.Add(Random);
    }

    public void resetvalues()
    {
        WalkSpeed.Value = startWalkSpeed;
        RunSpeed.Value = startRunSpeed;
        JumpSpeed.Value = startJumpSpeed;
        GravitySpeed.Value = startGravitySpeed;
        DoubleJumpAmount.Value = startAmountofDoubleJumps;
        DoubleJumpHeight.Value = startDoubleJumpHeight;
        Doubler.Value = startDoubler;
        Square.Value = startSquare;
        Half.Value = startHalf;
        Random.Value = UnityEngine.Random.Range(startRNGMin, startRNGMax);
    }

    public void roundDone()
    {
        Doubler.Value *= 2;
        Square.Value *= Square.Value;
        Half.Value /= 2;
        Random.Value = UnityEngine.Random.Range(startRNGMin, startRNGMax);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
