using UnityEngine;

public class PlatformOrbit : MonoBehaviour
{
    [SerializeField] private float m_orbitSpeed;
    [SerializeField] private Transform m_orbitPoint;

    private void Update()
    {
        transform.RotateAround(m_orbitPoint.position, Vector3.forward, m_orbitSpeed * Time.deltaTime);
    }
}
