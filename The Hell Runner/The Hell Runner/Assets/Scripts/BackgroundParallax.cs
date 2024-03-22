using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private SpriteRenderer m_background;

    int foo = 0;

    private void Start()
    {
        m_background = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        foo++;
        m_background.material.mainTextureOffset = new Vector2(foo, 0);
    }
}
