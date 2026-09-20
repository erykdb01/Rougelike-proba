using UnityEngine;

public enum EnemyState { Idle, Chasing }

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 6f;
    public float speed = 3f;

    private EnemyState state = EnemyState.Idle;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (state == EnemyState.Idle)
        {
            if (distanceToPlayer <= detectionRange)
            {
                state = EnemyState.Chasing;
            }
        }
        else if (state == EnemyState.Chasing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }
}