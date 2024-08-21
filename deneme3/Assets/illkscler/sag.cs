using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sag : MonoBehaviour
{
  

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Karakterin Transform'u
    public float lookSpeed = 2f; // Kamera döndürme hızı
    public float verticalRotationLimit = 80f; // Dikey döndürme sınırı

    private float verticalRotation = 0f;
    private bool isLooking = false;

    void Update()
    {
        if (Input.GetMouseButton(1)) // Sağ fare tuşuna basılı mı?
        {
            isLooking = true;

            // Fare hareketini al
            float horizontal = Input.GetAxis("Mouse X") * lookSpeed;
            float vertical = Input.GetAxis("Mouse Y") * lookSpeed;

            // Yatay döndürme (kamera ve karakter döndürülür)
            player.Rotate(0, horizontal, 0);

            // Dikey döndürme (sadece kamera döndürülür)
            verticalRotation -= vertical;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
            transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            // Kamera, karakterin arkasında ve yukarıda sabit bir mesafede olmalı
            transform.position = player.position - transform.forward * 5f + Vector3.up * 2f;
        }
        else
        {
            isLooking = false;
        }

        // Eğer sağ tıklama yapılmıyorsa, kamerayı karaktere odakla
        if (!isLooking)
        {
            transform.position = player.position - transform.forward * 5f + Vector3.up * 2f;
            transform.LookAt(player.position + Vector3.up * 2f);
        }
    }
}
}
