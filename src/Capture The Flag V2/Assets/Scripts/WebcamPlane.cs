using UnityEngine;
using System.Collections;

public class WebcamPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Quaternion rotation = Quaternion.Euler (0, -90, 0);
		Matrix4x4 rotationMatrix = Matrix4x4.TRS (Vector3.zero, rotation, new Vector3 (1, 1, 1));
		this.gameObject.GetComponent<Renderer> ().material.SetMatrix ("_Rotation", rotationMatrix);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
