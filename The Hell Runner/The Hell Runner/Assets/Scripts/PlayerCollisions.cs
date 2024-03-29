using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tile")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tile")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_rb.velocity.y < 0 && collision.CompareTag("Hazard"))
        {
            print("Hit Hazard");
            // kill player
        }

        else if (collision.CompareTag("Soul"))
        {
            print("Increase Soul Count");
            collision.GetComponent<Soul>().PlayCollectedAnim();
        }
    }
}
