using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _charC;

    public Vector3 moveDir = Vector3.zero;

    public float jumpSpeed = 8.0f;
    public float maxSpeed = 6.0f;
    public float currSpeed;
    public float gravity = 20.0f;
    public float runSpeedMultiplier = 2.5f;
    public CharacterStatistics charStats;

    // Start is called before the first frame update
    void Start()
    {
        _charC = this.GetComponent<CharacterController>();
        currSpeed = maxSpeed;
        charStats = this.GetComponentInChildren<CharacterStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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
