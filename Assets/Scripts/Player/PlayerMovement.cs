using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed;
	public float runSpeed;
	public Rigidbody rb;
	public bool moving = true;
	public GameObject cameraObject;

	private CamControl cam;
	private ArmController arm;

    private void Awake()
    {
		cam = GetComponentInChildren<CamControl>();
		arm = GetComponent<ArmController>();
	}

    private void Start()
	{
		InitilisePlayer(); //set up player
	}

	private void Update()
	{
		if (moving)
		{
			Move();
			cam.UpdateCamera();
		}
		else
		{
			arm.UpdateArm();
		}

		if (Input.GetKeyDown("g"))
        {
			moving = !moving;

		}
	}

	void InitilisePlayer() // set up player
	{
		if (rb == null) // fetch rb if not set
		{
			rb = GetComponent<Rigidbody>();
		}
		rb.isKinematic = false; //makes sure it can move
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY; // freezes constraints so that it will stand up
	}

	void Move()
	{

		float x = Input.GetAxisRaw("Horizontal") * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed); //take players input to move left or right at a certain speed
		float y = Input.GetAxisRaw("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed); //take players input to move forwards or backwards at a certain speed

		Vector3 movePos = transform.right * x + transform.forward * y; // set the new pos to move to
		Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z); // sets the new position using velocity and preserves the y velocity
		rb.velocity = newMovePos; // move to position
	}
}
