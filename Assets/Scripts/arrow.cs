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
        apple = GameObject.FindWithTag("normalFood");
        lookApplePosition();
        //transform.Rotate(new Vector3(180, 180, 180));
    }
	
	// Update is called once per frame
	void Update ()
    {
        lookApplePosition();
    }

    private void lookApplePosition()
    {
        var targetPosition = apple.transform.position;
        //targetPosition = Quaternion.Euler(180, 180, 180) * targetPosition;
        transform.LookAt(targetPosition);
    }
}
