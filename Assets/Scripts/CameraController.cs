using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform targetPos;
    public Transform targetLookAt;
    [Range(0, 1)] public float movementAlpha;
    [Range(0, 1)] public float rotationAlpha;
    private Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        m_transform.position = Vector3.Lerp(m_transform.position, targetPos.position, movementAlpha);
        m_transform.rotation = Quaternion.Lerp(m_transform.rotation, targetPos.rotation, rotationAlpha);
    }
}
