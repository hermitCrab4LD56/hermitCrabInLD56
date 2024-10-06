using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public KeyCode attackKey;      // ������
    public Transform clawTransform; // ǯ�ӵ�λ��
    public float attackSwingSpeed = 5f; // ǯ�ӻӶ����ٶ�
    public Vector3 attackPositionOffset = new Vector3(1, 0, 0); // ����ʱǯ�ӻӶ���λ��ƫ��
    public float attackCooldown = 0.8f;  // ������ȴʱ��
    public float attackRange = 1f;  // ��������Ч��Χ
    public int damage = 10;         // ÿ�ι�����ɵ��˺�
    //public AudioSource attackSound; // ������Ч

    private Vector3 originalClawPosition;  // ǯ�ӵĳ�ʼλ��
    public bool canAttack = true;         // �Ƿ���Թ���
    private PlayerMovement playerMovement; // ���ڼ��Է��Ƿ����

    public PlayerMovement pm;

    void Start()
    {
        originalClawPosition = clawTransform.localPosition; // ��¼ǯ�ӳ�ʼλ��
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleAttack(); // ��⹥����
    }

    // ��Ⲣ�������߼�
    void HandleAttack()
    {
        if (Input.GetKeyDown(attackKey) && canAttack && !pm.isDefending)
        {
            StartCoroutine(SwingClaw()); // ����ǯ�ӻӶ�
        }
    }

    // ǯ�ӵĻӶ�����
    System.Collections.IEnumerator SwingClaw()
    {
        canAttack = false;  // ��ʱ��ֹ�ٴι���
        Debug.Log("Started swinging claw");

        // ����ǯ�ӻӶ���Ŀ��λ��
        Vector3 attackPosition = originalClawPosition + attackPositionOffset;
        float progress = 0f;

        // �Ӷ�ǯ��
        while (progress <= 1f)
        {
            clawTransform.localPosition = Vector3.Lerp(originalClawPosition, attackPosition, progress);
            progress += Time.deltaTime * attackSwingSpeed;
            yield return null;
        }

        // ����ǯ�ӵ�ԭʼλ��
        clawTransform.localPosition = originalClawPosition;
        CheckForHit();  // ���ǯ���Ƿ������Ŀ��

        Debug.Log("Waiting for cooldown: " + attackCooldown + " seconds");
        yield return new WaitForSeconds(attackCooldown);  // �ȴ�������ȴ����

        canAttack = true;  // �ָ����Թ�����״̬�������ٴι���
        Debug.Log("canAttack reset to true");
    }

    // ���ǯ���Ƿ�����˶Է�
    void CheckForHit()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(clawTransform.position, attackRange);
        foreach (Collider2D target in hitTargets)
        {
            if (target.gameObject.tag != this.gameObject.tag)
            {
                PlayerHealth targetHealth = target.GetComponent<PlayerHealth>();
                PlayerMovement targetMovement = target.GetComponent<PlayerMovement>();

                if (targetHealth != null && targetMovement != null)
                {
                    // ����Ƿ񹥻�����Ŀ��ı��󣬲��ҳ���ͬ
                    if (targetMovement.IsAttackedFromBehind(this.transform) && targetMovement.IsFacingAway(this.transform))
                    {
                        // ��������Ŀ�걳���ҳ����෴��ֱ������˺�
                        targetHealth.TakeDamage(damage);
                        Debug.Log("Hit from behind with opposite facing! Damage dealt.");
                    }
                    else
                    {
                        // ���Ŀ���Ƿ��ڷ���״̬
                        if (targetMovement.IsDefending())
                        {
                            Debug.Log("Attack blocked! No damage.");
                        }
                        else
                        {
                            // ����Ŀ��ǰ����Ŀ��δ����������˺�
                            targetHealth.TakeDamage(damage);
                            Debug.Log("Hit! Damage dealt.");
                        }
                    }
                }
            }
        }
    }
}