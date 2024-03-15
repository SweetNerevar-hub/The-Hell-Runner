using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject[] sectors;
    [SerializeField] private List<GameObject> avaliableSectors;
    [SerializeField] private GameObject latestSector;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float m_scrollSpeed;

    [SerializeField] private Transform player;

    Vector2 m_midScreen;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        avaliableSectors = new List<GameObject>(sectors.Length);
    }

    private void Start()
    {
        CheckForEmptyList();

        m_midScreen = mainCamera.WorldToScreenPoint(mainCamera.transform.position);
    }

    private void Update()
    {
        m_scrollSpeed = ScaleScrollSpeed();

        //print(mainCamera.WorldToScreenPoint(mainCamera.transform.position));
        //print($"Player Pos: {mainCamera.WorldToScreenPoint(player.position)}");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * m_scrollSpeed * Time.deltaTime);
    }

    public void SpawnSector()
    {
        int sector = GetRandomSectorFromList();
        GameObject lastSector = latestSector;

        latestSector = Instantiate(avaliableSectors[sector], transform);
        latestSector.transform.position = lastSector.GetComponent<Sector>().end.position;

        avaliableSectors.RemoveAt(sector);
        CheckForEmptyList();
    }

    private void CheckForEmptyList()
    {
        if (avaliableSectors.Count == 0)
        {
            foreach (GameObject sector in sectors)
            {
                avaliableSectors.Add(sector);
            }
        }
    }

    private float ScaleScrollSpeed()
    {
        Vector2 playerPosOnScreen = mainCamera.WorldToScreenPoint(player.position);

        if (playerPosOnScreen.x > m_midScreen.x)
        {
            return 3f;
        }

        return 0.5f;
    }

    private int GetRandomSectorFromList()
    {
        return Random.Range(0, avaliableSectors.Count);
    }
}
