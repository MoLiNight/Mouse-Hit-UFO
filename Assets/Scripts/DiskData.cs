using System.Collections.Generic;
using UnityEngine;

public class DiskData : MonoBehaviour
{
    public float size;
    public Color color;
    public float speed;

    public List<Color> colors = new List<Color>() { Color.green, Color.red, Color.yellow };

    public void RandomDiskData(int round)
    {
        this.size = Random.Range(1f - round * 0.03f, 1f);
        this.color = colors[Random.Range(0, 3)];
        this.speed = Random.Range(15f, 15f + round * 0.5f);
    }

}
