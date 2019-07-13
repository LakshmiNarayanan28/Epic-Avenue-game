using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 moveVector;
	private float speed=5.0f;
	private float verticalVelocity= 0.0f;
	private float gravity= 12.0f;
	private float animationDuration = 3.0f;
	private float startTime;
	
	private bool isDead = false;
	
    // Start is called before the first frame update
    void Start()

    {
		controller=GetComponent<CharacterController> ();
		startTime=Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
		
		if(isDead)
			return;
		
		if(Time.time -startTime < animationDuration)
		{
			controller.Move(Vector3.forward * speed * Time.deltaTime);
			return;
		}
		moveVector=Vector3.zero;
		
		if(controller.isGrounded)
		{
			 verticalVelocity =-0.5f;
		}
		else
		{
			verticalVelocity -= gravity * Time.deltaTime;
		}
		//x -Left and Right
		moveVector.x =Input.GetAxisRaw("Vertical") * speed;
		//y-Up and Down
		
		moveVector.y=verticalVelocity;
		
		//z - Forward and Backward
		moveVector.z=speed;
		
        controller.Move(moveVector * Time.deltaTime);
    }
	public void SetSpeed(float modifier)
	{
		speed = 5.0f + modifier;
	}
	
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(hit.point.z > transform.position.z + (controller.radius/2)) //set as per our game
		{
			if (hit.gameObject.tag == "obstacles"){
			Death();
			}
		}
	}
	private void Death()
	{
		
		 isDead = true;
		  GetComponent<Score> ().OnDeath();
	}
}
