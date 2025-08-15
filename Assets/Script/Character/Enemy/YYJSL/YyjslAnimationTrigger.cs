using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslAnimationTrigger : EnemyAnimationTrigger
{
    private Enemy_YYJSL yyjsl => GetComponentInParent<Enemy_YYJSL>();
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void Relocate() => yyjsl.FindPosition();
    private void MakeInvisible() => MakeTransprent(true);
    private void Makevisible() => MakeTransprent(false);


    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;

    }

}
