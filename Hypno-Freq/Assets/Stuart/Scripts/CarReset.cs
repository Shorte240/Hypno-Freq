using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarReset : MonoBehaviour {

    MoveBetweenPoints moveBetweenPoints;
    float timer, timeToReset;
    int min, max;
    Vector3 start;

	// Use this for initialization
	void Start () {
        moveBetweenPoints = gameObject.GetComponent<MoveBetweenPoints>();
        timer = 0;
        timeToReset = Random.Range(min, max);
        min = 3;
        max = 7;
        start = moveBetweenPoints.agent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > timeToReset)
        {
            timer = 0;
            timeToReset = Random.Range(min, max);
            moveBetweenPoints.agent.transform.position = start;
        }

        if (Input.GetKeyDown("space"))
        {
            timer = 0;
            timeToReset = Random.Range(min, max);
            moveBetweenPoints.agent.transform.position = start;
        }
    }
}
