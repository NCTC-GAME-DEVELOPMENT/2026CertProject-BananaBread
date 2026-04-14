using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PushTrigger : MonoBehaviour
{
    Collider PushVolume;
    public PlayerController pc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = gameObject.GetComponentInParent<PlayerController>();
        PushVolume = gameObject.GetComponent<BoxCollider>();
        PushVolume.enabled = false;
    }

    public IEnumerator PushAction()
    {
        yield return new WaitForSeconds(0.1f);
        PushVolume.enabled = true;
        yield return new WaitForSeconds(0.25f);
        PushVolume.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Crate crate = other.gameObject.GetComponent<Crate>();
        ghost g = other.gameObject.GetComponent<ghost>();

        if (crate)
        {
            crate.MoveCrate(pc.Facing.ToString());
        }
    }
}
