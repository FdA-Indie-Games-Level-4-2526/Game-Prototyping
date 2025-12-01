using UnityEngine;

public class Death : MonoBehaviour
{
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponentInParent<Respawner>();
        if (player != null)
        {
            player.Die();
        }
    } */

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.GetComponent<Respawner>();
        if (player != null)
        {
            player.Die();
        }
        else
        {
            Debug.LogError("player is null!" /*hello*/ );
        }
    }
}
