using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private float _deathTimer = 0f;
    public int currWave;
    public int enemyCount;
    public float enemySpeed;
    public int maxEnemySpeed;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;
    public int maxEnemyAllowedOnScreen;
    public GameObject deadCanvas;
    public RectTransform deadBlackScreen;
    public bool isDead = false;
    public int score = 0;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI deadScore;

    private PlayerMovement _playerController;

    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void UpdateWave()
    {
        _playerController.dashDuration += 0.05f;
        _playerController.movementSpeed += 0.25f;
        ++currWave;
        enemyCount += 5;
        if (maxEnemyAllowedOnScreen < 15)
        {
            maxEnemyAllowedOnScreen += 2;
        }
        
        if (enemySpeed < maxEnemySpeed)
        {
            enemySpeed += 0.2f;
        }

        waveText.text = "WAVE " + currWave.ToString();
    }

    public void UpdateHealth()
    {
        healthText.text = _playerController.playerHealth.ToString();
        
        if (_playerController.playerHealth <= 0)
        {
            isDead = true;
            deadCanvas.SetActive(true);
            StartCoroutine(DeadAnimation());
        }
    }

    public void UpdateScore()
    {
        uiScore.text = score.ToString() + " KILLS";
    }

    private IEnumerator DeadAnimation()
    {
        deadCanvas.SetActive(true);
        deadScore.text = "Your score is " + score;
        for (var i = 0; i < 255; ++i)
        {
            yield return new WaitForSeconds(0.01f);
            deadBlackScreen.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
    }

    private void Update()
    {
        if (isDead)
        {
            _deathTimer += Time.deltaTime;
            if (_deathTimer > 2f && Input.anyKey)
            {
                SceneManager.LoadScene("Mainmenu");
            }
        }
    }
}
