using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _charC;

    public Vector3 moveDir = Vector3.zero;

    public float jumpSpeed = 8.0f;
    public float speed = 6.0f;
    public float gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _charC = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;

        _charC.Move(moveDir * Time.deltaTime);

    }
}
