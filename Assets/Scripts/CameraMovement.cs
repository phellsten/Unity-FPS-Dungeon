﻿// Paul Hellsten - 758077
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Camera cam;

    private Vector2 mouseLook;
    private Vector2 deltaMousePos;
    public float sens = 2.0f;

    public float normalHeight = 0f;
    public float crouchHeight = -0.5f;
    public float crouchSpeed = 5f;

    private float maxY = 90f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Game is paused, don't move camera.
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        // Change new direction based on mouse axis.
        deltaMousePos.x = Mathf.Lerp(deltaMousePos.x, Input.GetAxisRaw("Mouse X") * sens, 1f);
        deltaMousePos.y = Mathf.Lerp(deltaMousePos.y, Input.GetAxisRaw("Mouse Y") * sens, 1f);

        mouseLook += deltaMousePos;

        // Clamp camera between -90 and 90 degrees.
        mouseLook.y = Mathf.Clamp(mouseLook.y, -maxY, maxY);

        // Apply rotation
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

        // Holding shift to run faster
        if (Input.GetKey(KeyBindings.CrouchKey) && player.GetComponent<CharController>().grounded)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                new Vector3(0f, crouchHeight, 0f), crouchSpeed * Time.deltaTime);
        }
        if (!Input.GetKey(KeyBindings.CrouchKey) && player.GetComponent<CharController>().grounded)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                new Vector3(0f, normalHeight, 0f), crouchSpeed * Time.deltaTime);
        }
    }
}