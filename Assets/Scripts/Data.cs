using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

	const int INITIAL_SIZE = 3;

	static bool alive;
	static int size;
	static int points;


	public static void addSize(){
		size++;
	}

	public static void addPoints(int inc){
		points += inc;
	}

	public static void losePoints(int inc){
		points -= inc;
	}

	public static void died(){
		alive = false;
		print ("ELA MORREEEEEU!");
	}

	public static void rise(){
		alive = true;
		print ("ressuscitou !");
	}

	public static bool isAlive(){
		return alive;
	}


	// Use this for initialization
	void Start () {
		alive = true;
		size = INITIAL_SIZE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
