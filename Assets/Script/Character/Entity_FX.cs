using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_FX : MonoBehaviour
{
    private SpriteRenderer sr;
    private CapsuleCollider2D cp;

    [Header("ÉÁË¸Ð§¹û")]
    [SerializeField] private Material hitMat;
    [SerializeField]private  float flashDuration;
     private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        cp = GetComponentInChildren<CapsuleCollider2D>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFx()
    {
        sr.material = hitMat;
        
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
       
    }

    private void RedBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;

    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;

    }

    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;

    }

}
