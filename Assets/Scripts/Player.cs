using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// variables taken from CharacterController.Move example script
	// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
	public float speed;
	public float jumpSpeed;
	public float gravity;
	private Vector3 moveDirection = Vector3.zero;

	public int Lives = 3; // number of lives the player hs

	public GameObject cameraObject;

	Vector3 start_position; // start position of the player

	Vector3 startScale;		//original scale of player
	Vector3 crouchScale;	//scale of player when crouched
	public int crouchCounter = 0;	//lets you crouch jump

	void Start()
	{
		// record the start position of the player
		start_position = transform.position;

		//define the transform
		startScale = gameObject.transform.lossyScale;
		crouchScale = new Vector3 (startScale.x, startScale.y / 2, startScale.z);
	}

	public void Reset()
	{
		// reset the player position to the start position
		transform.position = start_position;
	}

	void Update () 
	{
		CharacterController controller = GetComponent<CharacterController>();
		//run
		if (Input.GetKey ("z")) {
			speed = 12.0f;
			if (Mathf.Abs(Input.GetAxis ("Horizontal")) > 0.5f) 	//you can jump higher when running
			{
				jumpSpeed = 24.0f;
			}
			else 
			{
				jumpSpeed = 20.0f;
			}
		} 
		else 
		{
			speed = 6.0f;
			jumpSpeed = 20.0f;
		}
			
		if (controller.isGrounded) 
		{
			//crouch
			if (Input.GetKey ("down")) 
			{
				gameObject.transform.localScale = crouchScale;
				crouchCounter++;
				if (crouchCounter > 60)		//crouch for a bit to charge jump
				{
					jumpSpeed = 30.0f;
				}
			} 
			else 
			{
				gameObject.transform.localScale = startScale;
				crouchCounter = 0;
			}

			//jump
			moveDirection.y = 0;					//on the gound there is no vertical movement
			if (Input.GetKeyDown("x"))
			{
				moveDirection.y = jumpSpeed;					//after jumpin the vertical movement = jumpSpeed
				gameObject.transform.localScale = startScale; 	//uncrouch in air
				crouchCounter = 0; 								//reset this when jump
			}
		}

		if (gameObject.transform.lossyScale.y >= 0.9) 
		{	//if he isnt crouching essentially
			moveDirection.x = Input.GetAxis ("Horizontal");					//take horizontal movement from the x axis
			moveDirection.x *= speed;										//multiply by the speed
		} 
		else 
		{
			moveDirection.x = 0;			//stop
		}

		moveDirection.y -= gravity * Time.deltaTime;		//move down due to gravity

		controller.Move (moveDirection * Time.deltaTime);	//apply the movement

		cameraObject.transform.position = new Vector3 (gameObject.transform.position.x, 3.5f, -13.5f);
	}
}