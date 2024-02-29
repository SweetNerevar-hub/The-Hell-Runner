using UnityEngine;

public class Sector : MonoBehaviour
{
    private Map map;
    private Camera mainCamera;
    private Plane[] cameraPlanes;
    private BoxCollider2D sectorEnd;

    [SerializeField] private bool hasBeenInBounds;

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
}
