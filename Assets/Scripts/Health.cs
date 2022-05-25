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

    [SerializeField] private AudioSource hurtSound;
    private bool isWait = false;

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
        hurtSound.Play();
        PlayerPrefs.SetInt("health", health);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Enemy" && !isWait)
        {
            minusHealth();
            isWait = true;
            Invoke("setWaitFalse", 2);
            indicator.SetActive(true);
            Invoke("turnOfIndicator", 2);
        }
    }

    private void turnOfIndicator()
    {
        indicator.SetActive(false);
    }

    private void setWaitFalse()
    {
        isWait = false;
    }
}
