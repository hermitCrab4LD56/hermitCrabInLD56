using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // ���Ѫ��
    public int currentHealth;    // ��ǰѪ��

    public Image healthBarImage;  // ���ڱ�ʾѪ���������
    public int lives = 3;
    public List<Image> livesImages;  // ���ڱ�ʾ lives ��ͼƬ�б�
    private bool isDefending = false;
    public Transform attacker;  // �����ߵ� Transform

    void Start()
    {
        currentHealth = maxHealth;  // ��ʼ��ʱ����Ϊ���Ѫ��
        UpdateHealthBar();  // ��ʼ��ʱ��ʾ��Ѫ
    }

    void Update()
    {
        // ����Ѫ����
        UpdateHealthBar();
    }

    // ����Ѫ������ʾ
    void UpdateHealthBar()
    {
        // ���㵱ǰѪ��ռ���Ѫ���ı�����������Ѫ���� fillAmount
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    // ����ܵ��˺�ʱ����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // ���ٵ�ǰѪ��
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // ȷ��Ѫ��������� 0
        //UpdateHealthBar();  // ����Ѫ���� UI

        if (currentHealth <= 0)
        {
            Die();  // ���Ѫ����Ϊ 0��������������
        }
    }

    public TwoPlayerRockPaperScissors TP;
    void Die()
    {
        //Debug.Log("Battle START!");
        TP.RockStart();
    }

    // ��һָ�Ѫ��ʱ����
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // ȷ��Ѫ�����ᳬ�����ֵ
        //UpdateHealthBar();  // ����Ѫ���� UI
    }
    // ���� lives ͼƬ��ʾ
    void UpdateLives()
    {
        // ѭ������ lives ͼƬ����ʾ����������
        for (int i = 0; i < livesImages.Count; i++)
        {
            if (i < lives)
            {
                livesImages[i].enabled = true;  // ��ʾ��ǰ�� lives ͼƬ
            }
            else
            {
                livesImages[i].enabled = false;  // ���س�����ǰ lives ������ͼƬ
            }
        }
    }
    // ���� lives����������ʾ
    void LoseLife()
    {
        lives--;
        UpdateLives();  // ���� lives ͼƬ
    }

    // �����������������Ѫ��
    void Respawn()
    {
        currentHealth = maxHealth;  // ����ʱ�ָ���Ѫ
        UpdateHealthBar();  // ����Ѫ��
    }
}