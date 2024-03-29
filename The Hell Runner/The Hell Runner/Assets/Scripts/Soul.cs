using UnityEngine;

public class Soul : MonoBehaviour
{
    private Animator m_animator;
    private BoxCollider2D m_boxCollider;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_boxCollider = GetComponent<BoxCollider2D>();
    }

    public void PlayCollectedAnim()
    {
        m_animator.SetBool("IsCollected", true);
        m_boxCollider.enabled = false;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
