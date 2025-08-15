using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
   

    [SerializeField] private Sprite[] moneySprite;
    private MoneyTtpe moneyType;
    private Rigidbody2D rb;
    private SpriteRenderer currentRender;
    private Player player;


    private void Awake()
    {
        currentRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

        player = PlayerManager.instance.player;
    }

    public void ManualInitialize(int index)
    {
        if (moneySprite == null || index < 0 || index >= moneySprite.Length)
        {
           
            return;
        }

        currentRender = GetComponent<SpriteRenderer>();
        currentRender.sprite = moneySprite[index];
        moneyType = (MoneyTtpe)index;
        rb.velocity = new Vector2(Random.Range(-5, 5), Random.Range(3, 5));

    }

    private void Update()
    {
       
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. �����ײ�����Ƿ������
        if (collision.gameObject.GetComponent<Player>())
        {
            // 2. ��ȡ������ϵ�Player�ű�
            
                // 3. ���ݻ����������ӽ��
                switch (moneyType)
                {
                    case MoneyTtpe.tongqian:
                        player.playerMoney += 50;
                        break;
                    case MoneyTtpe.yinLiang:
                        player.playerMoney += 200;
                        break;
                    case MoneyTtpe.hunangJing: 
                        player.playerMoney += 500;
                        break;
                }

                // 4. ������Ʒ����ѡ��
                Destroy(gameObject);
            }
        }
    
    
       
    
}

