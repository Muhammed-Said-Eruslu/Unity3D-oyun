using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haraket : MonoBehaviour
{
   float inputX; // x girdisi almak icin
   float inputY; // y girdisi almak icin

   public Transform Model; //konumunu, dönüşünü ve ölçeğini temsil eden bir bileşeni (Transform) tutmak için kullanılır.

   Vector3 StickDirection; //  X ekseni üzerindeki değeri temsil eder (sağ-sol). y: Y ekseni üzerindeki değeri temsil eder (yukarı-aşağı). 
   //z: Z ekseni üzerindeki değeri temsil eder (ileri-geri). yani yonunu haraketini ve girişini temsil eder

   Animator Anim;
   Camera mainCam;
   public float damp; // gecikme
   [Range(1,20)] // range si 1 ila 20 arasında olabılır
   public float rotationSpeed; // rotasyon hızı
   void Start()
   {
            Anim = GetComponent<Animator>(); // scirpt ile anımator aynı yerde olucak aynı objede
            mainCam = Camera.main; // main kamera tagına sahıp kamera varsa bahsedilen kamera odur
   }

   private void LateUpdate() // kamera normalde fixed update de calıstıgı ıcın bu kullanılır optimize olması ıcın
   {
        inputX = Input.GetAxis("Horizontal"); // x değeri yatay
        inputY = Input.GetAxis("Vertical"); // dikey 
        
        StickDirection = new Vector3(inputX, 0, inputY);
        
        if (StickDirection != Vector3.zero)
        {
            InputMove();
            InputRotation();
        }
   }

   void InputMove()
   {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude, damp, Time.deltaTime * 10);
        // input y değeri -1 olsa bile speed -1 olamaz  bi şekilde 1 doğru gitsin 0 ile 1 arasınad ortaladım
        // caprazd gidebilir clapMagnitude bu işe yarar ortalamasını alır 
        // damp ani donuslerı daha smoot yapar birden geçmez 
   }

   void InputRotation()
   {
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection); // main kamera doğrultusunda haraketleri takıp eder 
        rotOfset.y = 0; // yukarı asagı haraket edemez
        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed); // karakter dairesel donus yapacagı ıcın slerp 
   }
}

