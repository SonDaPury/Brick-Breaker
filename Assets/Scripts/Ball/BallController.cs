using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot,
        endGame,
    }

    public ballState currentBallState;
    public Rigidbody2D rbBall;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public Vector3 ballLaunchPosition;
    public Vector2 tempVelocity;
    private float ballVelocityX;
    private float ballVelocityY;
    public float constantSpeed;
    public GameObject arrowPrefab;
    public LineRenderer lineRenderer;
    public Transform ballTransform;
    public LayerMask collisionLayerMask;
    public GameObject arrowHeadPrefab;
    private GameObject arrowHeadInstance;
    public float circleOffset = 0.2f;
    public GameManager gameManager;
    public GameObject splitBallPowerupPrefab;

    public float minAngle = -75f;
    public float maxAngle = 75f;

    private void Awake()
    {
        lineRenderer = arrowPrefab.GetComponent<LineRenderer>();
        gameManager = FindAnyObjectByType<GameManager>();
        arrowHeadInstance = Instantiate(arrowHeadPrefab);
        arrowHeadInstance.SetActive(false);
    }

    private void Start()
    {
        currentBallState = ballState.aim;
        gameManager.ballsInScene.Add(gameObject);
    }

    private void Update()
    {
        switch (currentBallState)
        {
            case ballState.aim:
                if (Input.GetMouseButtonDown(0))
                {
                    MouseClicked();
                }
                if (Input.GetMouseButton(0))
                {
                    MouseDragged();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    ReleaseMouse();
                }
                break;

            case ballState.fire:
                break;

            case ballState.wait:
                if (gameManager.ballsInScene.Count == 1)
                {
                    currentBallState = ballState.endShot;
                }
                break;

            case ballState.endShot:
                foreach (var brick in gameManager.bricksInScene)
                {
                    brick.GetComponent<BrickMovementController>().currentState =
                        BrickMovementController.brickState.move;
                }

                gameManager.PlaceBricks();
                currentBallState = ballState.aim;
                break;

            case ballState.endGame:
                break;

            default:
                break;
        }
    }

    public void MouseClicked()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.enabled = true;
        arrowHeadInstance.SetActive(true);
    }

    public void MouseDragged()
    {
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mouseStartPosition - tempMousePosition).normalized;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        direction = new Vector2(
            Mathf.Sin(angle * Mathf.Deg2Rad),
            Mathf.Cos(angle * Mathf.Deg2Rad)
        ).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            ballTransform.position,
            direction,
            Mathf.Infinity,
            collisionLayerMask
        );

        Vector3 lineEndPosition;

        lineRenderer.SetPosition(0, ballTransform.position);

        if (hit.collider != null)
        {
            lineEndPosition = hit.point;
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineEndPosition = ballTransform.position + (Vector3)(direction * 10);
            lineRenderer.SetPosition(1, ballTransform.position + (Vector3)(direction * 10));
        }

        Vector3 adjustedPosition = lineEndPosition - (Vector3)(direction * circleOffset);
        arrowHeadInstance.transform.position = adjustedPosition;

        arrowHeadInstance.transform.rotation = Quaternion.Euler(
            0f,
            0f,
            -Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)
        );
    }

    public void ReleaseMouse()
    {
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
        ballVelocityY = mouseStartPosition.y - mouseEndPosition.y;

        tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;

        float angle = Mathf.Atan2(tempVelocity.x, tempVelocity.y) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        tempVelocity = new Vector2(
            Mathf.Sin(angle * Mathf.Deg2Rad),
            Mathf.Cos(angle * Mathf.Deg2Rad)
        ).normalized;

        rbBall.velocity = constantSpeed * tempVelocity;

        if (rbBall.velocity == Vector2.zero)
        {
            return;
        }

        ballLaunchPosition = transform.position;
        currentBallState = ballState.fire;

        arrowHeadInstance.SetActive(false);
        lineRenderer.enabled = false;
    }
}
