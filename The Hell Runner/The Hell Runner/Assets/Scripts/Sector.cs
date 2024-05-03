using UnityEngine;

public class Sector : MonoBehaviour
{
    private Map map;
    private Camera mainCamera;
    private Plane[] cameraPlanes;
    private BoxCollider2D sectorEnd;

    [SerializeField] private bool hasBeenInBounds;
    [SerializeField] private Transform m_soulSpawn;
    [SerializeField] private GameObject m_soul;

    public Transform end;

    private void Awake()
    {
        sectorEnd = end.GetComponent<BoxCollider2D>();
        map = GameObject.Find("Map").GetComponent<Map>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        if (m_soulSpawn)
        {
            CalculateSoulSpawn();
        }
    }

    private void FixedUpdate()
    {
        Bounds bounds = sectorEnd.bounds;
        if (GeometryUtility.TestPlanesAABB(cameraPlanes, bounds) && !hasBeenInBounds)
        {
            hasBeenInBounds = true;
            map.SpawnSector();
            return;
        }

        else if (!GeometryUtility.TestPlanesAABB(cameraPlanes, bounds) && hasBeenInBounds)
        {
            Destroy(gameObject);
        }
    }

    private void CalculateSoulSpawn()
    {
        int r = Random.Range(0, 10);

        if (r > -1)
        {
            Instantiate(m_soul, m_soulSpawn.position, Quaternion.identity, map.transform);
        }
    }
}
