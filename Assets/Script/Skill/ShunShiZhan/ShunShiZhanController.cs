using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShunShiZhanController : MonoBehaviour
{
    private float freezeTimeDuration;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetupShunShiZhan(float _freezeTimeDuration,Player _player)
    {
        freezeTimeDuration = _freezeTimeDuration;
        player = _player;
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyZhanJi", 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
            player.stats.DoDamage(collision.GetComponent<CharacterStats>());
            DestroyZhanJi();
        }
    }

    private void DestroyZhanJi()
    {
        Destroy(gameObject);
    }
    


}
