using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour {

    private Rigidbody2D rb2D;
	public List<LetterController> letterPlaceholders;
     
	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{

		float speed = GameManager.instance.speed / 100;
        Vector2 start, end;

        start = transform.position;
        end = start + new Vector2(-speed/Time.deltaTime, 0);

        rb2D.MovePosition(end);
    }
}
