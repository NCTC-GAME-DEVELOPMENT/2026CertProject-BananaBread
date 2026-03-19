using UnityEngine;
using UnityEngine.InputSystem;
public class LocationController : MonoBehaviour
{
    // this is some code I had sitting arount it could possible be used for the ghost pathfinding idk (:
    public static LocationController instance;
    public Camera mainCamera;
    public Vector3 WorldLocation;
    public float MaxDistance = 1000;
    private void Awake()
    {
        instance = this;
    }
  
    // Update is called once per frame
    void Update()
    {
        Vector2 mouseLocation = Vector2.zero;
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            mouseLocation = mouse.position.ReadValue();
        }
        Ray mouseRay = mainCamera.ScreenPointToRay(mouseLocation);

        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, MaxDistance))
        {
            WorldLocation = hit.point;
            gameObject.transform.position = WorldLocation;
        }
    
    }
}
