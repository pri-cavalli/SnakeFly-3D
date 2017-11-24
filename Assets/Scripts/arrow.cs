using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrow : MonoBehaviour {

    private GameObject apple;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start ()
    {
        apple = GameObject.FindWithTag("apple");
        lookApplePosition();
    }
	
	// Update is called once per frame
	void Update ()
    {
        lookApplePosition();
    }

    private void lookApplePosition()
    {
        var targetPosition = apple.transform.position;
        transform.LookAt(targetPosition);
    }
}
