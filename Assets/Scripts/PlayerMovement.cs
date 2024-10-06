using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayerTwo;
    public float moveSpeed = 5f;  // 玩家移动速度
    public KeyCode moveLeft;      // 左移动键
    public KeyCode moveRight;     // 右移动键
    public KeyCode defenseKey;    // 防御键
    public Transform clawTransform; // 钳子的位置，作为玩家身体的一部分
    public Vector3 defensePositionOffset = new Vector3(0, 1, 0); // 钳子防御时的位置偏移
    //public AudioSource defenseSound;  // 防御音效
    public AudioSource blockSuccessSound;  // 格挡成功音效

    public bool isDefending = false;  // 是否在防御状态
    private Rigidbody2D rb;       // 玩家刚体，用于控制物理移动
    private Vector3 clawOffset;   // 钳子相对于玩家的偏移
    public Collider2D backCollider;// 背部碰撞体积，用于检测攻击者是否在玩家背后
    public PlayerAttack pa;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 记录钳子相对于玩家的初始偏移
        clawOffset = clawTransform.localPosition;
    }

    void Update()
    {
        HandleMovement();
        HandleDefense();
    }

    // 处理玩家移动逻辑
    // 处理玩家移动逻辑
    void HandleMovement()
    {
        float moveDirection = 0f;

        if (Input.GetKey(moveLeft))
        {
            moveDirection = -1f;
            if (isPlayerTwo)
            {
                this.GetComponent<SpriteRenderer>().flipX = false; // PlayerTwo 朝左
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;  // PlayerOne 朝左
            }
        }
        else if (Input.GetKey(moveRight))
        {
            moveDirection = 1f;
            if (isPlayerTwo)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;  // PlayerTwo 朝右
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false; // PlayerOne 朝右
            }
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
    // 处理防御逻辑
    void HandleDefense()
    {
        if (Input.GetKey(defenseKey) && pa.canAttack)
        {
            Debug.Log("111");
            isDefending = true;
            clawTransform.localPosition = clawOffset + defensePositionOffset; // 将钳子移动到头顶
            //if (!defenseSound.isPlaying)
            //{
            //    if(defenseSound.clip != null)
            //    {
            //        defenseSound.Play();  // 播放防御音效
            //    }
            //}
                
            Debug.Log("Player is defending");
        }
        if(Input.GetKeyUp(defenseKey))
        {
            Debug.Log("222");
            isDefending = false;
            clawTransform.localPosition = clawOffset; // 恢复钳子到初始位置
        }
    }

    // 检查玩家是否在防御状态
    public bool IsDefending()
    {
        return isDefending;
    }

    // 播放格挡成功音效
    public void PlayBlockSuccessSound()
    {
        blockSuccessSound.Play();
    }
    public bool IsFacingAway(Transform attacker)
    {
        // 获取玩家和攻击者的朝向
        bool attackerFacingRight = attacker.GetComponent<SpriteRenderer>().flipX == (attacker.GetComponent<PlayerMovement>().isPlayerTwo ? true : false);
        bool playerFacingRight = this.GetComponent<SpriteRenderer>().flipX == (this.isPlayerTwo ? true : false);

        // 如果一个朝右，一个朝左，则认为朝向相反
        return attackerFacingRight == playerFacingRight;
    }
    // 判断攻击者是否在玩家的背部碰撞体积内
    public bool IsAttackedFromBehind(Transform attacker)
    {
        Debug.Log("Player is onback");
        return backCollider.bounds.Contains(attacker.position);
    }
}
