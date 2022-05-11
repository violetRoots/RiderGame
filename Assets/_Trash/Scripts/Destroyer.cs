using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private void OnCollisionExit(Collision col)
    {
        Destroy(col.gameObject);
    }
    private void OnTriggerExit(Collider collision)
    {
        Destroy(collision.gameObject);
    }

}
