using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovementController : MonoBehaviour
{
    public enum brickState
    {
        stop,
        move,
    }

    public brickState currentState;
    private bool hasMoved;

    private void OnEnable()
    {
        hasMoved = false;
        currentState = brickState.stop;
    }

    private void Start()
    {
        hasMoved = false;
        currentState = brickState.stop;
    }

    private void Update()
    {
        if (currentState == brickState.stop)
        {
            hasMoved = false;
        }

        if (currentState == brickState.move)
        {
            if (hasMoved == false)
            {
                AudioManager.Instance.PlaySoundEffect(2);
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                currentState = brickState.stop;
                hasMoved = true;
            }
        }
    }
}
