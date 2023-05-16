using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerRGB2D;
    [SerializeField] private float movementSpeed = 3f;
    private Vector2 MovementDirection;
    [SerializeField] private Camera mainCamera;
    Vector2 MousePosition;

    public void UpdateMovementData(Vector2 newMovementDirection)
    {
        MovementDirection = newMovementDirection;
    }
    public void UpdateRayCast(Vector2 rayCast)
    {
        MousePosition = rayCast;
    }
    private void FixedUpdate()
    {
        MoveThePlayer();
    }
    private void Update()
    {
        CreateRaycast();
    }
    void CreateRaycast()
    {
        Vector2 startPoint = transform.position;
        Vector2 direction = MousePosition - startPoint;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction);
        Debug.DrawRay(startPoint, direction, Color.red, 5f);
    }

    void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(MovementDirection) * movementSpeed * Time.deltaTime;
        PlayerRGB2D.MovePosition(transform.position + movement);
    }
    Vector2 CameraDirection(Vector2 movementDirection)
    {
        var cameraForward = mainCamera.transform.up;
        var cameraRight = mainCamera.transform.right;

        cameraForward.z = 0f;
        cameraRight.z = 0f;

        return cameraForward * movementDirection.y + cameraRight * movementDirection.x;
    }
}
