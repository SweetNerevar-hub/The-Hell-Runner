using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Material m_backgroundMat;
    [SerializeField] private float m_offsetSpeed;
    [SerializeField] private float m_offsetSpeedY;

    private void Update()
    {
        m_backgroundMat.mainTextureOffset += new Vector2(m_offsetSpeed * Time.deltaTime, m_offsetSpeedY * Time.deltaTime);
    }
}
