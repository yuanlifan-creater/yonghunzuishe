using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePort : MonoBehaviour
{
    [SceneName]
    public string sceneToGo;
    public Vector3 positionToGo;
    // Start is called before the first frame update




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            EventHandler.CallTranslationEvent(sceneToGo,positionToGo);
        }
    }
}
