using UnityEngine;
using System;

public class appleSpawn : MonoBehaviour
{
    private float sizeX, sizeZ;
	private int probability = 1; // ... de ser uma 'bad apple'

	private Color badAppleColor;
	private Color appleColor;
	public static bool eatsBadApple;
	public static bool nextAppleIsBad;
	private Transform appleColorComponent;

    void Start()
    {
        sizeX = 20;
        sizeZ = 20;
		badAppleColor = new Color32 (231, 142, 29, 255);
		Transform[] teste;
		teste = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < teste.Length; i++) {
			if (teste [i].tag == "appleColor") {
				appleColorComponent = teste [i];
			}
		}
		appleColor = appleColorComponent.GetComponent<Renderer> ().material.color;

		nextAppleIsBad = false;
		eatsBadApple = false;
        changePos();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "head")
        {
			if (!nextAppleIsBad) 
			{
                if (!eatsBadApple) // comeu maca normal -> sortear a proxima
                {
                    if (UnityEngine.Random.Range(0, 10) <= probability)
                    {
                        nextAppleIsBad = true;
						appleColorComponent.GetComponent<Renderer> ().material.color = badAppleColor;

					}
				}
				else { // comeu maca normal depois de ter comido uma estragada -> movimentos voltam ao normal
					eatsBadApple = false;
				}
			} 
			else // next=true, eatsBad=false : comeu maca estragada -> troca valores das flags e a cor da proxima maca
			{
				nextAppleIsBad = false;
				eatsBadApple = true; // vai inverter os movimentos
				appleColorComponent.GetComponent<Renderer> ().material.color = appleColor; 
				print ("comeu estragada: next = false, eatsBad = true");
			}

			changePos ();
        }
        if (collision.gameObject.tag == "body")
        {
            changePos();
        }
    }

    private void changePos()
    {
        Vector3 newPosition = new Vector3(
			(float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2) - 1f)),
            -0.2f,
			(float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2) - 1f))
       );
        transform.position = newPosition;
        while ( transform.GetComponent<Collider>().gameObject.tag == "head" || transform.GetComponent<Collider>().gameObject.tag == "body")
        {
            this.changePos();
        }
    }
}
