using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 最大血量
    public int currentHealth;    // 当前血量
    public Slider healthSlider;  // 血量 UI 滑动条

    void Start()
    {
        currentHealth = maxHealth;  // 初始化时设置为最大血量
        healthSlider.maxValue = maxHealth;  // 将 Slider 的最大值设置为最大血量
        healthSlider.value = currentHealth;  // 初始化时显示满血
    }

    // 玩家受到伤害时调用
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 减少当前血量
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // 确保血量不会低于 0
        healthSlider.value = currentHealth;  // 更新血量条 UI

        if (currentHealth <= 0)
        {
            Die();  // 如果血量降为 0，调用死亡处理
        }
    }

    // 玩家死亡处理
    void Die()
    {
        Debug.Log("Player has died!");
        // 可以在这里处理玩家死亡的逻辑，比如重生或者结束游戏
    }

    // 玩家恢复血量时调用
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // 确保血量不会超过最大值
        healthSlider.value = currentHealth;  // 更新血量条 UI
    }
}