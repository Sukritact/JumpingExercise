using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_jump : MonoBehaviour
{

	//==================================================
	// Properties
	//==================================================
	public float jumpUpVelocity;   // Velocity on y-axis
	private float angle = 80;

	[SerializeField] private jump_node current_node;
	private bool forward;
	//==================================================
	// Standard Unity Methods
	//==================================================
	void Start()
	{
		forward = !(current_node.IsDeadEnd(true));
	}

	void Update()
	{
		if(Mathf.Approximately(GetComponent<Rigidbody>().velocity.y,  0.0f)){
			 stateAction();
		 }
	}
	//==================================================
	// Custom Methods
	//==================================================
	void stateAction() {

		if (current_node.IsDeadEnd(forward)) forward = !forward;

		jump_node new_node = current_node.GetRandomNode(forward);
		Vector3 target = new_node.transform.position;
		target.y += new_node.transform.lossyScale.y/2;
		jumpTo(target);
		current_node = new_node;
	}

	void jumpTo(Vector3 point){
		Vector3 direction = point - transform.position;
		float height = direction.y;
		direction.y = 0;
		float dist = direction.magnitude;
		float a = angle * Mathf.Deg2Rad;
		direction.y = dist * Mathf.Tan(a);
		dist += height / Mathf.Tan(a);
		float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));

		GetComponent<Rigidbody>().velocity = velocity * direction.normalized;
	}

	// void jumpTo(Vector3 point){
	// 	Vector3 direction = point - transform.position;
	// 	float time = timeToLand(point);
	// 	float vX = direction.x / time;
	// 	float vZ = direction.z / time;

	// 	GetComponent<Rigidbody>().velocity = new Vector3(vX, jumpUpVelocity, vZ);
	// }

	// float timeToLand(Vector3 point){
	// 	float g = Physics.gravity.y;
	// 	float sqrt = Mathf.Sqrt(Mathf.Abs(jumpUpVelocity * jumpUpVelocity - 2 * g * (transform.position - point).y ));
	// 	return Mathf.Min(-jumpUpVelocity + sqrt, -jumpUpVelocity - sqrt ) / g;
	// }

}
