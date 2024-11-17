using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PhysicalAction : SSAction
{
    public Vector3 force;

    public static PhysicalAction GetSSAction(Vector3 force)
    {
        PhysicalAction action = ScriptableObject.CreateInstance<PhysicalAction>();
        action.force = force;
        return action;
    }

    public override void Start()
    {
        Rigidbody rb = this.gameobject.GetComponent<Rigidbody>();
        rb.AddForce(force.x, force.y, force.z, ForceMode.Impulse);
    }

    public override void Update()
    {
        if (!this.gameobject.activeSelf || this.transform.position.y < -10)
        {
            //waiting for destroy
            this.destory = true;
            this.callback.SSActionEvent(this);
        }
    }
}
