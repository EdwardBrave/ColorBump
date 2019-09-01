using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RegularWallGenerator : MonoBehaviour, IWallGenerator
{
    public List<GameObject> obstacles;

    Random _rand = new Random();
    public List<GameObject> Generate(Vector3 size, float difficulty, List<Color32> colors)
    {
        List<GameObject> items = new List<GameObject>();
        int friendsCount = 0;
        for (int i = 0; i < size.x; i++)
        {
            var item = Instantiate(obstacles[_rand.Next(0, obstacles.Count)], new Vector3(i, 0, 0), new Quaternion());
            if ((_rand.Next(0, 100) / 100F < (size.x * (1F - difficulty) - friendsCount) / (size.x - i)))
            {
                friendsCount++;
                item.GetComponent<MaterialPainter>().givenColor = colors[0];
            }
            else
                item.GetComponent<MaterialPainter>().givenColor = colors[_rand.Next(1, colors.Count)];
            items.Add(item);
        }

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 1; j < size.z; j++)
            {
                var item = Instantiate(obstacles[_rand.Next(0, obstacles.Count)], new Vector3(i, 0, j), new Quaternion());
                item.GetComponent<MaterialPainter>().givenColor = (_rand.Next(0, 100) / 100F >= difficulty) ? colors[0] : colors[_rand.Next(1, colors.Count)];
                items.Add(item);
            }
        }
        return items;
    }
}
