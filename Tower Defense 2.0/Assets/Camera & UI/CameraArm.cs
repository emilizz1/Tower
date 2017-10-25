using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour {

    [SerializeField] float scrollSpeed = 1f;

    Camera mCamera;

	// Use this for initialization
	void Start () {
        mCamera = FindObjectOfType<Camera>();
	}

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        
        transform.Translate(new Vector3(xAxisValue * scrollSpeed, 0.0f, zAxisValue * scrollSpeed));
        if(scrollWheel > 0f)
        {
            mCamera.transform.Translate(new Vector3(0f, -1f, 0f));
        }
        else if (scrollWheel < 0f)
        {
            mCamera.transform.Translate(new Vector3(0f, 1f, 0f));
        }
    }
}
