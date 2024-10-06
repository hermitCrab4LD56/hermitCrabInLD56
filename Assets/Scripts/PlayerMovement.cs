using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayerTwo;
    public float moveSpeed = 5f;  // ����ƶ��ٶ�
    public KeyCode moveLeft;      // ���ƶ���
    public KeyCode moveRight;     // ���ƶ���
    public KeyCode defenseKey;    // ������
    public Transform clawTransform; // ǯ�ӵ�λ�ã���Ϊ��������һ����
    public Vector3 defensePositionOffset = new Vector3(0, 1, 0); // ǯ�ӷ���ʱ��λ��ƫ��
    //public AudioSource defenseSound;  // ������Ч
    public AudioSource blockSuccessSound;  // �񵲳ɹ���Ч

    public bool isDefending = false;  // �Ƿ��ڷ���״̬
    private Rigidbody2D rb;       // ��Ҹ��壬���ڿ��������ƶ�
    private Vector3 clawOffset;   // ǯ���������ҵ�ƫ��
    public Collider2D backCollider;// ������ײ��������ڼ�⹥�����Ƿ�����ұ���
    public PlayerAttack pa;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ��¼ǯ���������ҵĳ�ʼƫ��
        clawOffset = clawTransform.localPosition;
    }

    void Update()
    {
        HandleMovement();
        HandleDefense();
    }

    // ��������ƶ��߼�
    // ��������ƶ��߼�
    void HandleMovement()
    {
        float moveDirection = 0f;

        if (Input.GetKey(moveLeft))
        {
            moveDirection = -1f;
            if (isPlayerTwo)
            {
                this.GetComponent<SpriteRenderer>().flipX = false; // PlayerTwo ����
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;  // PlayerOne ����
            }
        }
        else if (Input.GetKey(moveRight))
        {
            moveDirection = 1f;
            if (isPlayerTwo)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;  // PlayerTwo ����
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false; // PlayerOne ����
            }
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
    // ��������߼�
    void HandleDefense()
    {
        if (Input.GetKey(defenseKey) && pa.canAttack)
        {
            Debug.Log("111");
            isDefending = true;
            clawTransform.localPosition = clawOffset + defensePositionOffset; // ��ǯ���ƶ���ͷ��
            //if (!defenseSound.isPlaying)
            //{
            //    if(defenseSound.clip != null)
            //    {
            //        defenseSound.Play();  // ���ŷ�����Ч
            //    }
            //}
                
            Debug.Log("Player is defending");
        }
        if(Input.GetKeyUp(defenseKey))
        {
            Debug.Log("222");
            isDefending = false;
            clawTransform.localPosition = clawOffset; // �ָ�ǯ�ӵ���ʼλ��
        }
    }

    // �������Ƿ��ڷ���״̬
    public bool IsDefending()
    {
        return isDefending;
    }

    // ���Ÿ񵲳ɹ���Ч
    public void PlayBlockSuccessSound()
    {
        blockSuccessSound.Play();
    }
    public bool IsFacingAway(Transform attacker)
    {
        // ��ȡ��Һ͹����ߵĳ���
        bool attackerFacingRight = attacker.GetComponent<SpriteRenderer>().flipX == (attacker.GetComponent<PlayerMovement>().isPlayerTwo ? true : false);
        bool playerFacingRight = this.GetComponent<SpriteRenderer>().flipX == (this.isPlayerTwo ? true : false);

        // ���һ�����ң�һ����������Ϊ�����෴
        return attackerFacingRight == playerFacingRight;
    }
    // �жϹ������Ƿ�����ҵı�����ײ�����
    public bool IsAttackedFromBehind(Transform attacker)
    {
        Debug.Log("Player is onback");
        return backCollider.bounds.Contains(attacker.position);
    }
}
