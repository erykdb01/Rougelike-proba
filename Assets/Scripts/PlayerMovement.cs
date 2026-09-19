using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Odczytujemy input z klawiatury (A/D lub strzałki = horizontal, W/S = vertical)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Budujemy kierunek ruchu na podstawie inputu
        Vector3 kierunek = new Vector3(horizontal, 0f, vertical);

        // normalized, żeby ruch po skosie nie był szybszy niż na wprost
        kierunek = kierunek.normalized;

        // Przesuwamy gracza
        transform.position += kierunek * speed * Time.deltaTime;
    }
}