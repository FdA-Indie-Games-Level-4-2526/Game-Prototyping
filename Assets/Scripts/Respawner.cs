using System.Collections;
using UnityEngine;
 
public class Respawner : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // assign in inspector
    private Rigidbody2D rb;
    private Vector2 startPos;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
 
    private void Start()
    {
        startPos = transform.position;
    }
 
    public void Die()
    {
        Debug.Log("Death");
        StartCoroutine(Respawn(1f));
    }
 
    private IEnumerator Respawn(float duration)
    {
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