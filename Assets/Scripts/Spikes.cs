using UnityEngine;

public class Spikes : MonoBehaviour
{
   [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("collision");
        }*/

        Debug.Log("OnTriggerEnter with object: " + collision.gameObject.name);

        var player = collision.transform.GetComponentInParent<Respawner>();
        if (player != null)
        {
            Debug.Log("Player.Die called!");
            player.Die();
        }
    }    
}
