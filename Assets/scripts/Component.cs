using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : MonoBehaviour {

    private Rigidbody2D rb2D;
     
    // Use this for initialization
	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
	{

		float speed = GameManager.instance.speed / 100;
        Vector2 start, end;

        start = transform.position;
        end = start + new Vector2(-speed/Time.deltaTime, 0);

        rb2D.MovePosition(end);
    }
}
