using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : MonoBehaviour {

    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 start, end;

        start = transform.position;
        end = start + new Vector2(-0.1f, 0);

        rb2D.MovePosition(end);
    }
}
