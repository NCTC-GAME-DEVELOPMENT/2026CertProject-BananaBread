using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class GateForce : MonoBehaviour
{
    protected SwitchGate sg;
    public Vector3 myPosition;

    private void Start()
    {
        sg = GetComponentInParent<SwitchGate>();
    }
    public float MovePoint(char axis)
    {
        if (sg.Facing == Common.currentDirection.North && axis == 'y')
        {
            return (myPosition.z + 3f);
        }
        if (sg.Facing == Common.currentDirection.East && axis == 'x')
        {
            return (myPosition.x + 3f);
        }
        if (sg.Facing == Common.currentDirection.South && axis == 'y')
        {
            return (myPosition.z - 3f);
        }
        if (sg.Facing == Common.currentDirection.West && axis == 'x')
        {
            return (myPosition.x - 3f);
        }
        if (axis == 'x')
        {
            return myPosition.x;
        }
        else
        {
            return myPosition.z;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        if (crate)
        {
            crate.MoveCrate(sg.Facing.ToString());
        }
        if (pc)
        {
            pc.transform.position = new Vector3(MovePoint('x'), pc.transform.position.y, MovePoint('y'));
            Debug.Log("Moved Player to: " + MovePoint('x') + ", " + pc.transform.position.y + ", " + MovePoint('y'));
        }
    }
}
