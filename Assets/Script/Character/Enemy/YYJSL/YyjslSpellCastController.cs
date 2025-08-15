using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslSpellCastController : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private  Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;
    private CharacterStats mystats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpSpell(CharacterStats _stats) => mystats = _stats;

    private void AnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize,whatIsPlayer);
        foreach (var hit in colliders)
        {
             if (hit.GetComponent<Player>() != null)
                 mystats.DoDamage(hit.GetComponent<CharacterStats>());
           
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(check.position, boxSize);
    }

    private void SelfDestroy() => Destroy(gameObject);

}
