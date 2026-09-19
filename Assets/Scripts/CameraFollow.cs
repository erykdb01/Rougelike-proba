using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // kogo śledzimy (przeciągniemy Playera tutaj)
    public Vector3 offset = new Vector3(0f, 10f, -10f); // stały dystans od gracza

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}