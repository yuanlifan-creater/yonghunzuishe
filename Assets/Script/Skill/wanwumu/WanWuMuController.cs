using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanWuMuController : MonoBehaviour
{
    
    private float maxSize;
    private float growSpeed;
    private bool canGrow = true;
    private float shrinkSpeed;
    private bool canShrink;
    private float wanWuMuDuration;
    
    
    // Start is called before the first frame update


    void Start()
    {
        
    }

    public void SetUpWanWuMu(float _maxSize,float _growSpeed, float _shrinkSpeed, float _wanWuMuDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        wanWuMuDuration = _wanWuMuDuration;
        
            
    }


    // Update is called once per frame
    void Update()
    {
        
        wanWuMuDuration -= Time.deltaTime;

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            PlayerManager.instance.player.currentRenter.color = new Color(1, 1, 1, 0);


        }

        if (wanWuMuDuration <= 0)
        {
            canShrink = true;
        }
           
       
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.lossyScale.x < 0)
                Destroy(gameObject);
            
            PlayerManager.instance.player.PlayerExitState();

        }
        


    }

    
   
    
}
