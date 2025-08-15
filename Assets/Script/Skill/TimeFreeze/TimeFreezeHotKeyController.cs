using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeFreezeHotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;

    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEneny;
    private TimeFreezeController timeFreeze;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupHotKey(KeyCode _myNewHotKey,Transform _myEnemy, TimeFreezeController _myTimeFreeze)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEneny = _myEnemy;
        timeFreeze = _myTimeFreeze;
        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            timeFreeze.AddEnemyToList(myEneny);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }

    


}
