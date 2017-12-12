using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrow : MonoBehaviour {

    [Range(0, 1)] public float alpha;
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
        //transform.LookAt(targetPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position, Vector3.up), alpha);
    }
}
