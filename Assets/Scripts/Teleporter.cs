using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("collidededed");
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            print("not a player?");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("collided");
    }
}
