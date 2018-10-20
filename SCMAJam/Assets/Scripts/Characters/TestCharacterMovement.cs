using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class TestCharacterMovement : MonoBehaviour {

    public float speed = 3;
    public Rigidbody2D rigidbody2D;
	
	void Update () {

        GamePadState state = GamePad.GetState(PlayerIndex.One);

        Vector2 input = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);

        rigidbody2D.AddForce(input * speed);

	}
}
