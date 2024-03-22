using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SwapStatsPlayer _statsPlayer;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform camtransform;
    [SerializeField] private MainMenuManager mainmenu;
    [SerializeField] private Animator animator; 
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector3 externalMoveDirection = Vector3.zero;
    float rotationX = 0;
    
    public bool canMove = false;

    public bool returntomenu = false;

    private float amountOfDoubleJumpsDone = 0;
    
    public Vector3 ExternalMoveDirection { get { return externalMoveDirection; } set { externalMoveDirection = value; } }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = camtransform.forward;
        Vector3 right = camtransform.right;

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? _statsPlayer.RunSpeed.Value : _statsPlayer.WalkSpeed.Value) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? _statsPlayer.RunSpeed.Value : _statsPlayer.WalkSpeed.Value) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonUp("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = _statsPlayer.JumpSpeed.Value;
            animator.SetTrigger("Jump");
        }
        else if(Input.GetButtonUp("Jump") && canMove && amountOfDoubleJumpsDone < _statsPlayer.DoubleJumpAmount.Value)
        {
            moveDirection.y = _statsPlayer.DoubleJumpHeight.Value;
            amountOfDoubleJumpsDone += 1;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= _statsPlayer.GravitySpeed.Value * Time.deltaTime;
            animator.SetBool("Jumping", true);
        }
        else
        {
            amountOfDoubleJumpsDone = 0;
            
            animator.SetBool("Jumping", false);
        }

        // Player
        if (canMove)
        {    // Move the controller
            Vector3 externalMove = externalMoveDirection * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime + -1 * externalMove);
            if(Mathf.Abs(curSpeedX) > 0 || Mathf.Abs(curSpeedY) > 0)
                transform.forward = new Vector3(camtransform.forward.x, 0, camtransform.forward.z);

        }

        //reset button
        if (canMove && Input.GetKeyUp(KeyCode.R))
        {
            DieOrWin();
        }

        if (transform.position.y <= -10)
        {
            DieOrWin();
        }
        
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Idle", false);


            var runMultiply = (isRunning ? 2 : 1);

         

            var oldXVel = animator.GetFloat("X Velocity");
            var oldZVel = animator.GetFloat("Z Velocity");

            animator.SetFloat("X Velocity", Mathf.Lerp(Input.GetAxis("Horizontal") * runMultiply, oldXVel, 8 * Time.deltaTime));
            animator.SetFloat("Z Velocity", Mathf.Lerp(Input.GetAxis("Vertical") * runMultiply, oldZVel, 8 * Time.deltaTime));
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Idle", true);
        }
        
    }

    public void DieOrWin()
    {
        if (!returntomenu)
        {
            _statsPlayer.roundDone();
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            StartSwapStuff();
        }
        else
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            _statsPlayer.resetvalues();
            mainmenu.exitGame();
        }
    }

    public void backtogame()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void StartSwapStuff()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _statsPlayer.enableSwapMode();
    }

    public void resetplayer()
    {
        
    }
}