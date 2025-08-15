using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiZhuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int damage;
    private CharacterStats myStats;

    private void Update()
    {
        DestroyShiZhu();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Player>() != null)
        {
            myStats = collision.GetComponent<CharacterStats>();
            myStats.takeDamage(damage);
        }
                

        }

    private void DestroyShiZhu()
    {
        Destroy(gameObject,1.5f);
    }
    
}
