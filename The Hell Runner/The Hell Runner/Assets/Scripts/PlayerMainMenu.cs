using UnityEngine;

public class PlayerMainMenu : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;

    [SerializeField] private Hat hat;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_animator.SetBool("IsRunning", true);
        hat.SetCosmetic();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = Vector2.right * 6;
    }
}
