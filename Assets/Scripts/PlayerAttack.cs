using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public KeyCode attackKey;      // 攻击键
    public Transform clawTransform; // 钳子的位置
    public float attackSwingSpeed = 5f; // 钳子挥动的速度
    public Vector3 attackPositionOffset = new Vector3(1, 0, 0); // 攻击时钳子挥动的位置偏移
    public float attackCooldown = 0.8f;  // 攻击冷却时间
    public float attackRange = 1f;  // 攻击的有效范围
    public int damage = 10;         // 每次攻击造成的伤害
    //public AudioSource attackSound; // 攻击音效

    private Vector3 originalClawPosition;  // 钳子的初始位置
    public bool canAttack = true;         // 是否可以攻击
    private PlayerMovement playerMovement; // 用于检测对方是否防御

    public PlayerMovement pm;

    void Start()
    {
        originalClawPosition = clawTransform.localPosition; // 记录钳子初始位置
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleAttack(); // 检测攻击键
    }

    // 检测并处理攻击逻辑
    void HandleAttack()
    {
        if (Input.GetKeyDown(attackKey) && canAttack && !pm.isDefending)
        {
            StartCoroutine(SwingClaw()); // 启动钳子挥动
        }
    }

    // 钳子的挥动动作
    System.Collections.IEnumerator SwingClaw()
    {
        canAttack = false;  // 暂时禁止再次攻击
        Debug.Log("Started swinging claw");

        // 计算钳子挥动到目标位置
        Vector3 attackPosition = originalClawPosition + attackPositionOffset;
        float progress = 0f;

        // 挥动钳子
        while (progress <= 1f)
        {
            clawTransform.localPosition = Vector3.Lerp(originalClawPosition, attackPosition, progress);
            progress += Time.deltaTime * attackSwingSpeed;
            yield return null;
        }

        // 返回钳子到原始位置
        clawTransform.localPosition = originalClawPosition;
        CheckForHit();  // 检查钳子是否击中了目标

        Debug.Log("Waiting for cooldown: " + attackCooldown + " seconds");
        yield return new WaitForSeconds(attackCooldown);  // 等待攻击冷却结束

        canAttack = true;  // 恢复可以攻击的状态，允许再次攻击
        Debug.Log("canAttack reset to true");
    }

    // 检测钳子是否击中了对方
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
                    // 检查是否攻击者在目标的背后，并且朝向不同
                    if (targetMovement.IsAttackedFromBehind(this.transform) && targetMovement.IsFacingAway(this.transform))
                    {
                        // 攻击者在目标背后并且朝向相反，直接造成伤害
                        targetHealth.TakeDamage(damage);
                        Debug.Log("Hit from behind with opposite facing! Damage dealt.");
                    }
                    else
                    {
                        // 检查目标是否在防御状态
                        if (targetMovement.IsDefending())
                        {
                            Debug.Log("Attack blocked! No damage.");
                        }
                        else
                        {
                            // 攻击目标前方且目标未防御，造成伤害
                            targetHealth.TakeDamage(damage);
                            Debug.Log("Hit! Damage dealt.");
                        }
                    }
                }
            }
        }
    }
}