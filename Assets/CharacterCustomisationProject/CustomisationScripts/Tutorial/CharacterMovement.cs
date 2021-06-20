using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _charC;

    public Vector3 moveDir = Vector3.zero;

    private float horizontal = 0;
    private float vertical = 0;
    public float jumpSpeed = 8.0f;
    public float maxSpeed = 6.0f;
    public float currSpeed;
    public float gravity = 20.0f;
    public float runSpeedMultiplier = 2.5f;
    public CharacterStatistics charStats;

    KeyCode moveForward;
    KeyCode moveLeft;
    KeyCode moveRight;
    KeyCode moveBackward;

    public HotkeyRebinder hotkey;
    // Start is called before the first frame update
    void Start()
    {
        hotkey = FindObjectOfType<HotkeyRebinder>();
        hotkey.LoadKeys();

        _charC = this.GetComponent<CharacterController>();
        currSpeed = maxSpeed;
        charStats = this.GetComponentInChildren<CharacterStatistics>();
        moveForward = hotkey.GetKey("ForwardKeybind");
        moveBackward = hotkey.GetKey("BackwardKeybind");
        moveLeft = hotkey.GetKey("LeftKeybind");
        moveRight = hotkey.GetKey("RightKeybind");

        Debug.Log("ForwardKey: " + moveForward.ToString());
        Debug.Log("BackwardKey: " + moveBackward.ToString());
        Debug.Log("LeftKey: " + moveLeft.ToString());
        Debug.Log("RightKey: " + moveRight.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(moveForward))
        {
            vertical = -1.0f;
        }
        if(Input.GetKeyUp(moveForward))
        {
            vertical = 0;
        }
        if(Input.GetKeyDown(moveLeft))
        {
            horizontal = 1.0f;
        }
        if(Input.GetKeyUp(moveLeft))
        {
            horizontal = 0;
        }
        if(Input.GetKeyDown(moveRight))
        {
            horizontal = -1.0f;
        }
        if(Input.GetKeyUp(moveRight))
        {
            horizontal = 0;
        }
        if(Input.GetKeyDown(moveBackward))
        {
            vertical = 1.0f;
        }
        if(Input.GetKeyUp(moveBackward))
        {
            vertical = 0;
        }
        
        PlayerPrefs.SetString("LoadFrom", "GameScene");
        

        if(_charC.isGrounded)
        {
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= currSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }

            if(Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                if (charStats.currentStamina > 0)
                {
                    currSpeed *= runSpeedMultiplier;
                    charStats.isPlayerRunning = true;
                }
                
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                if(currSpeed > maxSpeed) currSpeed /= runSpeedMultiplier;
                charStats.isPlayerRunning = false;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;

        _charC.Move(moveDir * Time.deltaTime);

        charStats.RefreshStat();
    }
}
