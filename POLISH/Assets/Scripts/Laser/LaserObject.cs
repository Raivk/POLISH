using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour {

    public LaserController parent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CALL THINGS HERE

        if (gameObject.activeSelf)
        {
            parent.KillOneLaserObject();
        }

        gameObject.SetActive(false);
    }
}
