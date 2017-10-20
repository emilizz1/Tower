using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour {

    [SerializeField] float scrollSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(xAxisValue * scrollSpeed, 0.0f, zAxisValue * scrollSpeed));
    }
}
