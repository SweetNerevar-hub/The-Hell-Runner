using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    [SerializeField] private float m_rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * m_rotateSpeed * Time.deltaTime);
    }
}
