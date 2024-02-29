using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject[] sectors;
    [SerializeField] private List<GameObject> avaliableSectors;
    [SerializeField] private GameObject latestSector;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float m_scrollSpeed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        avaliableSectors = new List<GameObject>(sectors.Length);
    }

    private void Start()
    {
        CheckForEmptyList();
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

    private int GetRandomSectorFromList()
    {
        return Random.Range(0, avaliableSectors.Count);
    }
}
