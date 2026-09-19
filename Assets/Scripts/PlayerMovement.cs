using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float cameraAngle = 45f; // musi pasować do Rotation Y kamery

    void Update()
    {
        // GetAxisRaw = brak wygładzania, natychmiastowa reakcja
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 kierunek = new Vector3(horizontal, 0f, vertical);
        kierunek = kierunek.normalized;

        // Obracamy kierunek ruchu o kąt kamery,
        // żeby W = "w górę ekranu" z perspektywy izo
        Quaternion obrot = Quaternion.Euler(0f, cameraAngle, 0f);
        Vector3 kierunekIzo = obrot * kierunek;

        transform.position += kierunekIzo * speed * Time.deltaTime;
    }
}