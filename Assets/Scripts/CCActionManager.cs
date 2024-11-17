using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    private RoundController roundController;
    // Use this for initialization
    protected new void Start()
    {
        roundController = (RoundController)SSDirector.GetInstance().roundController;
        roundController.ccActionManager = this;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    #region IActionManager implementation
    public void PlayDisk(GameObject disk)
    {
        Vector3 position;
        Vector3 target;
        DiskData diskData = disk.GetComponent<DiskData>();

        // classification 0: The disk appears over;
        // classification 1: The disk appears below;
        // classification 2: The disk appears on the left;
        // classification 3: The disk appears on the right;
        int classification = Random.Range(0, 4);
        switch (classification)
        {
            case 0:
                {
                    position = new Vector3(Random.Range(-throwAreaWidth, throwAreaWidth), throwAreaHeight * 3);
                    target = new Vector3(Random.Range(-throwAreaWidth, throwAreaWidth), -throwAreaHeight * 3);
                    disk.transform.position = position;
                    CCMoveToAction ccMoveToAction = CCMoveToAction.GetSSAction(target, diskData.speed * 0.75f);
                    this.RunAction(disk, ccMoveToAction, this);
                    break;
                }
            case 1:
                {
                    position = new Vector3(Random.Range(-throwAreaWidth, throwAreaWidth), -throwAreaHeight * 3);
                    target = new Vector3(Random.Range(-throwAreaWidth, throwAreaWidth), throwAreaHeight * 3);
                    disk.transform.position = position;
                    CCMoveToAction ccMoveToAction = CCMoveToAction.GetSSAction(target, diskData.speed * 0.75f);
                    this.RunAction(disk, ccMoveToAction, this);
                    break;
                }
            case 2:
                {
                    position = new Vector3(-throwAreaWidth * 1.5f, Random.Range(-throwAreaHeight, throwAreaHeight));
                    target = new Vector3(throwAreaWidth * 1.5f, Random.Range(-throwAreaHeight, throwAreaHeight));
                    disk.transform.position = position;
                    CCMoveToAction ccMoveToAction = CCMoveToAction.GetSSAction(target, diskData.speed * 0.75f);
                    this.RunAction(disk, ccMoveToAction, this);
                    break;
                }
            case 3:
                {
                    position = new Vector3(throwAreaWidth * 1.5f, Random.Range(-throwAreaHeight, throwAreaHeight));
                    target = new Vector3(-throwAreaWidth * 1.5f, Random.Range(-throwAreaHeight, throwAreaHeight));
                    disk.transform.position = position;
                    CCMoveToAction ccMoveToAction = CCMoveToAction.GetSSAction(target, diskData.speed * 0.75f);
                    this.RunAction(disk, ccMoveToAction, this);
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
