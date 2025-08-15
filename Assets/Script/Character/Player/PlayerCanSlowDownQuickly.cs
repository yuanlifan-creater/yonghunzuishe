using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanSlowDownQuickly : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            PlayerManager.instance.player.canSlowDownQuickly = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            PlayerManager.instance.player.canSlowDownQuickly = true;
    }

}
