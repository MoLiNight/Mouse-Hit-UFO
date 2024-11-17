using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class DiskFactory : MonoBehaviour
{
    private static DiskFactory _instance;

    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

    public static DiskFactory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DiskFactory();
        }
        return _instance;
    }

    public GameObject CreateDisk(int round, DiskData diskData, int stage)
    {
        GameObject disk;
        if(stage == 0)
        {
            disk = GameObject.Instantiate(Resources.Load("Prefabs/Disk", typeof(GameObject))) as GameObject;
        }
        else
        {
            disk = GameObject.Instantiate(Resources.Load("Prefabs/NoGravityDisk", typeof(GameObject))) as GameObject;
        }

        if (diskData == null)
        {
            disk.GetComponent<DiskData>().RandomDiskData(round);
            diskData = disk.GetComponent<DiskData>();
        }
        used.Add(diskData);
        // Instantiate disk size
        disk.transform.localScale = new Vector3(2, 0.15f, 2) * diskData.size;
        // Instantiate disk color
        Renderer renderer = disk.GetComponent<Renderer>();
        renderer.material.color = diskData.color;

        return disk;
    }

    public void Reset()
    {
        used.Clear();
        free.Clear();
    }

    // stage = 0 -> Disk
    // stage = 1 -> NoGravityDisk
    public GameObject GetDisk(int round, int stage)
    {
        DiskData diskData = null;
        if (free.Count > 0)
        {
            diskData = free[Random.Range(0, free.Count)];
            free.Remove(diskData);
        }
        return CreateDisk(round, diskData, stage);
    }

    public void FreeDisk(GameObject disk)
    {
        DiskData diskData = disk.GetComponent<DiskData>();
        Destroy(disk);
        free.Add(diskData);
        used.Remove(diskData);
    }
}
