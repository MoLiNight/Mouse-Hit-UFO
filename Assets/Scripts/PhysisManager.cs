using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisManager : SSActionManager, ISSActionCallback, IActionManager
{
    private RoundController roundController;
    // Use this for initialization
    protected new void Start()
    {
        roundController = (RoundController)SSDirector.GetInstance().roundController;
        roundController.physisManager = this;
    }

    protected Vector3 GetRandomVectorOne(bool isleft)
    {
        Vector3 vector = new Vector3(0f, 0f, 0f);
        while (vector.y <= 0f)
        {
            if (isleft)
            {
                vector.x = UnityEngine.Random.Range(0f, 1f);
            }
            else
            {
                vector.x = UnityEngine.Random.Range(-1f, 0f);
            }
            vector.y = Mathf.Sqrt(1 - vector.x * vector.x) * 0.7f;
        }
        return vector;
    }

    #region IActionManager implementation
    public void PlayDisk(GameObject disk)
    {
        Vector3 position;
        DiskData diskData = disk.GetComponent<DiskData>();

        // classification 0: The disk appears on the left;
        // classification 1: The disk appears on the right;
        int classification = Random.Range(0, 2);
        switch(classification)
        {
            case 0:
                {
                    position = new Vector3(-throwAreaWidth, Random.Range(-throwAreaHeight, throwAreaHeight));
                    disk.transform.position = position;
                    PhysicalAction physicalAction = PhysicalAction.GetSSAction(GetRandomVectorOne(true) * diskData.speed);
                    this.RunAction(disk, physicalAction, this);
                    break;
                }
            case 1:
                {
                    position = new Vector3(throwAreaWidth, Random.Range(-throwAreaHeight, throwAreaHeight));
                    disk.transform.position = position;
                    PhysicalAction physicalAction = PhysicalAction.GetSSAction(GetRandomVectorOne(false) * diskData.speed);
                    this.RunAction(disk, physicalAction, this);
                    break;
                }
        }
    }
    #endregion

    #region ISSActionCallback implementation
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
    {
        DiskFactory diskFactory = DiskFactory.GetInstance();
        diskFactory.FreeDisk(source.gameobject);
    }
    #endregion

}