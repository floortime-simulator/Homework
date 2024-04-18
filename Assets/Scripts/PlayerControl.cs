using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    Vector3 direction;

    [SerializeField]
    float multiplier = 3f;

    [SerializeField]
    float jumpforce = 300f;

    bool onGround = false;

    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;  //{0, 0, 0}
        rb = gameObject.GetComponent<Rigidbody>();
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += velocity * Time.deltaTime;
        if (rb.velocity.magnitude < 7)
        {
            rb.AddForce(direction * multiplier);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        float movementX = input.x;
        float movementZ = input.y;

        // rb.velocity = new Vector3(movementX, 0, movementZ) * multiplier;

        direction = new Vector3(movementX, 0, movementZ);
    }

    void OnJump()
    {
        if (jumpCount > 1) { 
            return;
        }
        Debug.Log("Jumping");
        Debug.Log("Jump Count is " +jumpCount);
        Vector3 jumpVector = new Vector3(0, jumpforce, 0);
        rb.AddForce(jumpVector);
        jumpCount++;
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
}
