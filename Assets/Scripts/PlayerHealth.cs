using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 最大血量
    public int currentHealth;    // 当前血量

    public Image healthBarImage;  // 用于表示血量的填充条
    public int lives = 3;
    public List<Image> livesImages;  // 用于表示 lives 的图片列表
    private bool isDefending = false;
    public Transform attacker;  // 攻击者的 Transform

    void Start()
    {
        currentHealth = maxHealth;  // 初始化时设置为最大血量
        UpdateHealthBar();  // 初始化时显示满血
    }

    void Update()
    {
        // 更新血量条
        UpdateHealthBar();
    }

    // 更新血量条显示
    void UpdateHealthBar()
    {
        // 计算当前血量占最大血量的比例，并更新血条的 fillAmount
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    // 玩家受到伤害时调用
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 减少当前血量
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // 确保血量不会低于 0
        //UpdateHealthBar();  // 更新血量条 UI

        if (currentHealth <= 0)
        {
            Die();  // 如果血量降为 0，调用死亡处理
        }
    }

    public TwoPlayerRockPaperScissors TP;
    void Die()
    {
        //Debug.Log("Battle START!");
        TP.RockStart();
    }

    // 玩家恢复血量时调用
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // 确保血量不会超过最大值
        //UpdateHealthBar();  // 更新血量条 UI
    }
    // 更新 lives 图片显示
    void UpdateLives()
    {
        // 循环所有 lives 图片，显示或隐藏它们
        for (int i = 0; i < livesImages.Count; i++)
        {
            if (i < lives)
            {
                livesImages[i].enabled = true;  // 显示当前的 lives 图片
            }
            else
            {
                livesImages[i].enabled = false;  // 隐藏超出当前 lives 数量的图片
            }
        }
    }
    // 减少 lives，并更新显示
    void LoseLife()
    {
        lives--;
        UpdateLives();  // 更新 lives 图片
    }

    // 重生函数，重置玩家血量
    void Respawn()
    {
        currentHealth = maxHealth;  // 重生时恢复满血
        UpdateHealthBar();  // 更新血条
    }
}