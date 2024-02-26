using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform body;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private bool isMove;

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
            isMove = true;
        else
            isMove = false;

        if (isMove)
        {
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
            HandleRotation();
        }
    }
    private void HandleRotation()
    {
        if (movementDirection != Vector3.zero)
        {
            var rotationY = Quaternion.LookRotation(movementDirection, Vector3.up);
            body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, rotationY, rotateSpeed * Time.deltaTime);
        }
    }

    public bool GetIsMove()
    {
        return isMove;
    }
}
