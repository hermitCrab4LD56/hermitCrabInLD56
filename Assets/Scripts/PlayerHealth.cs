using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // ���Ѫ��
    public int currentHealth;    // ��ǰѪ��
    public Slider healthSlider;  // Ѫ�� UI ������
    public int lives = 3;

    void Start()
    {
        currentHealth = maxHealth;  // ��ʼ��ʱ����Ϊ���Ѫ��
        healthSlider.maxValue = maxHealth;  // �� Slider �����ֵ����Ϊ���Ѫ��
        healthSlider.value = currentHealth;  // ��ʼ��ʱ��ʾ��Ѫ
    }

    void Update()
    {
        healthSlider.value = currentHealth;
    }

    // ����ܵ��˺�ʱ����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // ���ٵ�ǰѪ��
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // ȷ��Ѫ��������� 0
        healthSlider.value = currentHealth;  // ����Ѫ���� UI

        if (currentHealth <= 0)
        {
            Die();  // ���Ѫ����Ϊ 0��������������
        }
    }


    public TwoPlayerRockPaperScissors TP;
    void Die()
    {
        Debug.Log("Battle START!");
        TP.Update();
        
    }

    // ��һָ�Ѫ��ʱ����
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // ȷ��Ѫ�����ᳬ�����ֵ
        healthSlider.value = currentHealth;  // ����Ѫ���� UI
    }
}