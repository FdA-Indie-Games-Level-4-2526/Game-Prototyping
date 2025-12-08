using System.Collections;
using UnityEngine;
 
public class Respawner : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    private Rigidbody2D rb;
    private Vector2 startPos;
 
    private void Awake()
    {
        // - Calls upon the Players sprite in order to make sure it's there

        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
 
    private void Start()
    {
        // - Manages the players spawn position

        startPos = transform.position;
    }
 
    public void Die()
    {
        // - The Player dies

        Debug.Log("Death");
        StartCoroutine(Respawn(1f));
    }
 
    private IEnumerator Respawn(float duration)
    {
        // - This despawns the sprite.

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
 
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
 
        yield return new WaitForSeconds(duration);
 
        transform.position = startPos;
 
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;
 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}