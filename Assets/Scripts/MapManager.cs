using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MapManager : MonoBehaviour
{
    public List<Color32> colors;
    public List<MonoBehaviour> walGens;
    public List<GameObject> objects;

    public GameObject gameCamera;
    private float camForwardSpace, camFrontSpace;
    private float gcUpdateTimer, camFieldSpasing = 5F;
    public float gcUpdateSpeed = 10F;

    public int wallsCount = 3;
    public float difficulty = 0.6F;

    public GameObject floor;
    public GameObject player;
    public GameObject finishLine;

    private void Start()
    {
        float halfFieldOfView = gameCamera.GetComponent<Camera>().fieldOfView/2F;
        float higth = gameCamera.transform.position.y;
        float angleDif = gameCamera.transform.rotation.eulerAngles.x;
        camForwardSpace = higth * Mathf.Tan( (90 - angleDif + halfFieldOfView) * Mathf.PI /180);
        camFrontSpace = higth * Mathf.Tan( (halfFieldOfView + angleDif - 90) * Mathf.PI / 180);
        Debug.Log(camForwardSpace + " and " + camFrontSpace);
        Generate();
        Clean();
    }

    private void Generate()
    {
        Vector3 wallSize = Vector3.one * 10;
        floor.transform.localScale = new Vector3(1, 1, wallsCount * 3 + 3.5F); 
        floor.transform.position = new Vector3(0, 0, floor.transform.localScale.z * 5);
        finishLine.transform.position = new Vector3(0F, 0.01F, wallsCount * (30) + 10);
        player.GetComponent<MaterialPainter>().givenColor = colors[0];

        Random _rand = new Random();
        for (int i = 1; i <= wallsCount; i++)
        {
            var someObject = walGens[_rand.Next(0, walGens.Count)];
            if (someObject is IWallGenerator WallGen)
            {
                List<GameObject> items = WallGen.Generate(wallSize, difficulty, colors);
                foreach (var item in items)
                    item.transform.position += new Vector3( -4.5F, 1F, 25F * i);
                objects.AddRange(items);
            } 
        }
    }

    private void Update()
    {
        gcUpdateTimer += gcUpdateSpeed * Time.deltaTime;
        if (gcUpdateTimer >= gcUpdateSpeed)
        {
            Clean();
            gcUpdateTimer -= gcUpdateSpeed;
        }
    }

    public void Clean()
    {
        float forwardLine = gameCamera.transform.position.z + camForwardSpace + camFieldSpasing;
        float frontLine = gameCamera.transform.position.z - camFrontSpace - camFieldSpasing;
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i].transform.position.z > forwardLine)
                objects[i].SetActive(false);
            else if (objects[i].transform.position.z > frontLine)
                objects[i].SetActive(true);
            else
            {
                Destroy(objects[i]);
                objects.RemoveAt(i);
            }
        }
    }
}
