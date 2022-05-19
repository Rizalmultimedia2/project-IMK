using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public int health;
    public GameObject character;
    public GameObject indicator;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        PlayerPrefs.SetInt("health", health);
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        if (health == 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void minusHealth()
    {
        health -= 1;
        PlayerPrefs.SetInt("health", health);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Enemy")
        {
            minusHealth();
            indicator.SetActive(true);
            Invoke("turnOfIndicator", 2);
        }
    }

    private void turnOfIndicator()
    {
        indicator.SetActive(false);
    }
}
