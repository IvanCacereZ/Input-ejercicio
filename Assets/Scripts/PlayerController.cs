using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Sub Behaviours")]
    public MovementPlayer playerMovementBehaviour;
    public AnimatorController playerAnimator;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float movementSmoothingSpeed = 1f;
    private Vector2 rawInputMovement;
    private Vector2 smoothInputMovement;
    private Vector2 mousePosition;

    //Current Control Scheme
    private string currentControlScheme;
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerAnimator.PlayAttackAnimation();
        }
    }
    public void MouseLocate(InputAction.CallbackContext value)
    {
        Vector2 inputMouse = value.ReadValue<Vector2>();
        mousePosition = new Vector2(inputMouse.x, inputMouse.y);
    }
    private void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdateRayo();
    }

    public void OnControlsChanged()
    {
        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;
            RemoveAllBindingOverrides();
        }
    }
    void CalculateMovementInputSmoothing()
    {

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);

    }
    void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(smoothInputMovement);
    }
    void UpdateRayo()
    {
        playerMovementBehaviour.UpdateRayCast(mousePosition);
    }
    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }
}
