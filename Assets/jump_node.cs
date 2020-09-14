using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump_node : MonoBehaviour
{

	//==================================================
	// Properties
	//==================================================

	public List<jump_node> child_nodes = new List<jump_node>();
	protected List<jump_node> parent_nodes = new List<jump_node>();

	//==================================================
	// Standard Unity Methods
	//==================================================
	void Start()
	{
		// Add self to childrens' parent node list
		foreach(jump_node child_node in child_nodes){
			child_node.parent_nodes.Add(this);
		}
	}
	void Update()
	{
	}
	//==================================================
	// Unity Editor Methods
	//==================================================
	void OnDrawGizmos(){
		float R = (transform.position.x % 5.0f)/5.0f;
		float G = (transform.position.y % 5.0f)/5.0f;
		float B = (transform.position.z % 5.0f)/5.0f;
		Gizmos.color = new Color(R, G, B, 1.0f);

		Debug.Log(Gizmos.color);

		Gizmos.DrawSphere(transform.position, 0.5f);
		foreach(jump_node child_node in child_nodes){
			Gizmos.DrawLine(transform.position, child_node.transform.position);
		}
	}
	//==================================================
	// Custom Methods
	//==================================================
	public jump_node GetRandomNode(bool forward = true){
		List<jump_node> nodes = forward ? child_nodes : parent_nodes;
		int num_nodes  = nodes.Count;

		if (num_nodes < 1) return null;
		return nodes[Random.Range(0, num_nodes)];
	}

	public bool IsDeadEnd(bool forward = true){
		List<jump_node> nodes = forward ? child_nodes : parent_nodes;
		int num_nodes  = nodes.Count;

		return (num_nodes < 1);
	}
}
