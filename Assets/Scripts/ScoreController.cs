using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    Dictionary<Color, float> colorDict = new Dictionary<Color, float>() {
        { Color.green, 0.5f},
        { Color.red, 1f},
        { Color.yellow, 1.5f}
    };

    public int GetScore()
    {
        return score;
    }

    public int Rounding(float number)
    {
        int results = (int)number;
        if (number - results >= 0.5f)
        {
            results += 1;
        }
        return results;
    }

    public void RecordDisk(GameObject disk)
    {
        DiskData diskData = disk.GetComponent<DiskData>();
        score += Rounding(colorDict[diskData.color] + (1.1f - diskData.size) * 4 + (diskData.speed - 14f) * 0.3f);
    }
}
