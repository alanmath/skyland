using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public int playerNumber;
    public float jumpForce = 5f;
    public float speed = 2f;
    private Rigidbody rb;
    private bool isGrounded1;
    private bool isGrounded2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
void Update()
{
    // Movimentação
    float moveHorizontal = 0f;
    float moveVertical = 0f;

    // Detecta os inputs baseado no número do jogador
    if (playerNumber == 1) // Primeiro jogador usa WASD
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }

        if (Input.GetKeyDown(KeyCode.G) && isGrounded1) // Tecla G para pular
        {
            Jump();
        }
    }
    else if (playerNumber == 2) // Segundo jogador usa setas
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1;
        }

        if (Input.GetKeyDown(KeyCode.L) && isGrounded2) // Tecla L para pular
        {
            Jump();
        }
    }

    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
    rb.AddForce(movement);
}

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (playerNumber == 1)
        {
            isGrounded1 = false;
        }
        else if (playerNumber == 2)
        {
            isGrounded2 = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (playerNumber == 1)
            {
                isGrounded1 = true;
            }
            else if (playerNumber == 2)
            {
                isGrounded2 = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            GameManager.Instance.IncrementCount();
        }
    }

}
