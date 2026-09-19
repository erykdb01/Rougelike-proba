using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 14f;  // jak daleko od gracza
    public float height = 10f;    // jak wysoko nad graczem
    public float angle = 45f;     // musi być takie samo jak cameraAngle w PlayerMovement!

    void LateUpdate()
    {
        // Obracamy kierunek "do tyłu" o zadany kąt - to odtwarza diagonalny widok izo
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
        offset.y = height;

        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}