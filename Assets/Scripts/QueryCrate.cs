using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.Rendering.DebugUI;

public class QueryCrate : Crate
{
    public IEnumerator RemoveChest()
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("IsInDoor", true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Crate removed");
        Destroy(this.gameObject);
    }
}
