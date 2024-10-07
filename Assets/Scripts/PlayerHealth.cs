using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // ×î´óÑªÁ¿
    public int currentHealth;    // µ±Ç°ÑªÁ¿

    public Image healthBarImage;  // ÓÃÓÚ±íÊ¾ÑªÁ¿µÄÌî³äÌõ
    public int lives = 3;
    public List<Image> livesImages;  // ÓÃÓÚ±íÊ¾ lives µÄÍ¼Æ¬ÁÐ±í
    public SpriteRenderer[] livessocket;  // ÓÃÓÚ±íÊ¾ lives µÄ²å²Û
    public Sprite[] livessocketimages;
    private bool isDefending = false;
    public Transform attacker;  // ¹¥»÷ÕßµÄ Transform

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;  // ³õÊ¼»¯Ê±ÉèÖÃÎª×î´óÑªÁ¿
        UpdateHealthBar();  // ³õÊ¼»¯Ê±ÏÔÊ¾ÂúÑª
    }

    void Update()
    {
        // ¸üÐÂÑªÁ¿Ìõ
        UpdateHealthBar();
        if(lives == 0)
        {
            //game over!!!!!!!!!!!!!!!!!!!!!!!!
            if(this.gameObject.tag == "PlayerOne" && GameObject.Find("Playertwo").GetComponent<PlayerHealth>().lives != 0)
            {
                Debug.Log("PlayerOne is dead");
                SceneManager.LoadScene("Pink Win");
            }
            else if(this.gameObject.tag == "PlayerTwo" && GameObject.Find("Playerone").GetComponent<PlayerHealth>().lives != 0)
            {
                Debug.Log("PlayerTwo is dead");
                SceneManager.LoadScene("Orange Win");
            }
            else
            {
                SceneManager.LoadScene("Tie");
            }
        }
    }

    // ¸üÐÂÑªÁ¿ÌõÏÔÊ¾
    void UpdateHealthBar()
    {
        // ¼ÆËãµ±Ç°ÑªÁ¿Õ¼×î´óÑªÁ¿µÄ±ÈÀý£¬²¢¸üÐÂÑªÌõµÄ fillAmount
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    // Íæ¼ÒÊÜµ½ÉËº¦Ê±µ÷ÓÃ
    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Stun");
        currentHealth -= damage;  // ¼õÉÙµ±Ç°ÑªÁ¿
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // È·±£ÑªÁ¿²»»áµÍÓÚ 0
        //UpdateHealthBar();  // ¸üÐÂÑªÁ¿Ìõ UI

        //条件不对，到零了也又触发一次。而且规则改了，还要判断生命小于3
        if (currentHealth <= 60 && currentHealth >40 && lives <3)
        {
            TrashFight();
        }

        else if (currentHealth <= 0)
        {
            Die();               
        }
    }

    public FallingTrash FT;
    void TrashFight()
    {
        SfxManager.instance.PlaySfx("RockMode");
        FT.RockStart();
    }

    public TwoPlayerRockPaperScissors TP;
    void Die()
    {
        SfxManager.instance.PlaySfx("RockMode");
        //Debug.Log("Battle START!");
        TP.RockStart();
    }

    // Íæ¼Ò»Ö¸´ÑªÁ¿Ê±µ÷ÓÃ
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // È·±£ÑªÁ¿²»»á³¬¹ý×î´óÖµ
        //UpdateHealthBar();  // ¸üÐÂÑªÁ¿Ìõ UI
    }
    // ¸üÐÂ lives Í¼Æ¬ÏÔÊ¾
    public void UpdateLives()
    {
        for (int i = 0; i < livesImages.Count; i++)
        {
            if (i < lives)
            {
                livesImages[i].enabled = true;  // Show the current lives image
            }
            else
            {
                livesImages[i].enabled = false;
            }
        }
    }
    // ¼õÉÙ lives£¬²¢¸üÐÂÏÔÊ¾
    void LoseLife()
    {
        lives--;
        UpdateLives();  // ¸üÐÂ lives Í¼Æ¬
    }

    // ÖØÉúº¯Êý£¬ÖØÖÃÍæ¼ÒÑªÁ¿
    void Respawn()
    {
        currentHealth = maxHealth;  // ÖØÉúÊ±»Ö¸´ÂúÑª
        UpdateHealthBar();  // ¸üÐÂÑªÌõ
    }
}