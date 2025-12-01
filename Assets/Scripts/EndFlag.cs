using UnityEngine;

public class EndFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log ("Hit");
        if (collision.CompareTag("Player") ) Debug.Log ("Player Touches");
        {
            SceneController.Instance.NextLevel();
        }
    }
}
