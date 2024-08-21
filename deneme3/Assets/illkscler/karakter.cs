using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakter : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5f; // Normal hareket hızı
    public float sprintSpeed = 10f; // Koşma hızı
    public float jumpForce = 8f; // Zıplama kuvveti
    public float gravity = 20f; // Yer çekimi kuvveti
    public Transform groundCheck; // Zemin kontrol noktası
    public float groundDistance = 0.4f; // Zemin mesafesi
    public LayerMask groundMask; // Zemin için katman maskesi

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        // Karakterin zeminde olup olmadığını kontrol et
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zeminle temas halinde olduğunda küçük bir negatif hız uygula
        }

        // Hareket ve hız ayarı
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Zıplama
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        // Yer çekimi
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}