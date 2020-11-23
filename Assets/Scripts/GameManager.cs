using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject deadUI;
    public GameObject gameSecret;
    public GameObject[] lifes;
    public GameObject groundMG;
    public GameObject backgroundMG;
    public GameObject butonSecret;
    public Sprite lifeFull;
    public Sprite lifeEmpty;
    public float spawnTime;

    public GameObject[] spawnPrefab;
    public GameObject[] keyUI;
    public GameObject[] keyUIFull;
    public ShakeBehavior cameraShake;

    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip keySound;
    public AudioClip snailSound;
    public AudioClip hurtSound;
    public AudioClip buttonSound;
    public AudioClip deadSound;

    private AudioSource jumpSource;
    private AudioSource coinSource;
    private AudioSource keySource;
    private AudioSource snailSource;
    private AudioSource hurtSource;
    private AudioSource buttonSource;
    private AudioSource deadSource;

    private int currentLife;
    private ScoreManager scoreMG;
    private PlayerManager playerMG;
    private float deltaTime;

    private bool isPlaying;

    private int deltaSpawnInnerStructure;

    internal int keyStatue;

    internal bool inSecret;
    private bool isDead;

    void Start()
    {
        //
        // Add audio 
        jumpSource = gameObject.AddComponent<AudioSource>();
        coinSource = gameObject.AddComponent<AudioSource>();
        keySource = gameObject.AddComponent<AudioSource>();
        snailSource = gameObject.AddComponent<AudioSource>();
        hurtSource = gameObject.AddComponent<AudioSource>();
        buttonSource = gameObject.AddComponent<AudioSource>();
        deadSource = gameObject.AddComponent<AudioSource>();
        jumpSource.clip = jumpSound;
        coinSource.clip = coinSound;
        keySource.clip = keySound;
        snailSource.clip = snailSound;
        hurtSource.clip = hurtSound;
        buttonSource.clip = buttonSound;
        deadSource.clip = deadSound;

        //
        // Find score manager
        scoreMG = GetComponent<ScoreManager>();

        playerMG = GetComponent<PlayerManager>();
        playerMG.player.SetActive(false);
        gameSecret.SetActive(false);

        //
        // Hide the game
        gameUI.SetActive(false);

        deadUI.SetActive(false);
        //
        // Set values
        currentLife = 3;
        isPlaying = false;
        deltaSpawnInnerStructure = 0;
        isDead = false;

        //
        // Key init
        foreach (GameObject key in keyUIFull)
        {
            key.GetComponent<SpriteRenderer>().enabled = false;
        }

        inSecret = false;

        // Debug
        //GoToSecret();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        //
        // Secret update
        if (keyStatue >= 4)
        {
            butonSecret.gameObject.SetActive(true);
            scoreMG.AddScore(500);
        }
        if (isPlaying && Input.GetKey(KeyCode.D))
        {
            if (Input.GetMouseButtonDown(1))
            {
                //BackToMenu();
            }

            foreach (Transform child in transform)
            {
                Vector3 position = child.transform.position;
                position.x -= Time.deltaTime * groundMG.GetComponent<GroundManager>().groundSpeed;
                child.transform.position = position;

                //
                // Check the position
                if (position.x < -250)
                {
                    // Destroy prefab
                    Destroy(child.gameObject);
                }
            }

            deltaTime += Time.deltaTime;
            if (deltaTime > spawnTime)
            {
                //
                // Random
                int val;
                if (deltaSpawnInnerStructure < 0)
                {
                    val = UnityEngine.Random.Range(0, spawnPrefab.Length);
                    if (val == 0)
                    {
                        deltaSpawnInnerStructure = 3;
                    }
                }
                else
                {
                    val = UnityEngine.Random.Range(1, spawnPrefab.Length);

                }

                deltaTime = 0.0f;
                GameObject obj = (GameObject)Instantiate(spawnPrefab[val],
                    new Vector3(groundMG.GetComponent<GroundManager>().spawnPosition.transform.position.x,
                    -7.2f,
                    0.0f),
                    Quaternion.identity);
                obj.transform.parent = this.transform;

                deltaSpawnInnerStructure--;
            }
        }
    }

    void GoToSecret()
    {
        inSecret = true;
        gameSecret.SetActive(true);
        playerMG.player.SetActive(false);
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        deadUI.SetActive(false);
        butonSecret.gameObject.SetActive(false);
        isPlaying = false;
    }

    void BackToGame()
    {
        inSecret = false;
        gameSecret.SetActive(false);
        gameUI.SetActive(true);
        if (!isDead)
        {
            playerMG.player.SetActive(true);
            butonSecret.gameObject.SetActive(true);
            isPlaying = true;
        }
        else
        {
            deadUI.SetActive(true);
        }
    }

    void QuitButton()
    {
        Application.Quit();
    }

    void PlayButton()
    {
        //
        // Hide the menu
        menuUI.SetActive(false);

        //
        // Show the game
        gameUI.SetActive(true);
        deadUI.SetActive(false);
        playerMG.player.SetActive(true);
        playerMG.Restart();
        butonSecret.gameObject.SetActive(false);

        //
        // fulfill the lifes
        foreach (GameObject heart in lifes)
        {
            heart.GetComponent<SpriteRenderer>().sprite = lifeFull;
        }
        currentLife = 3;

        //
        // Init the score
        scoreMG.SetScore(0);

        //
        // Init the player
        isPlaying = true;
        groundMG.GetComponent<GroundManager>().isPlaying = true;
        backgroundMG.GetComponent<BackgroundManager>().isPlaying = true;
        inSecret = false;

        PlayButtonSound();
        isDead = false;
    }

    public void BackToMenu()
    {
        //
        // Show the menu
        menuUI.SetActive(true);

        //
        // Hide the game
        gameUI.SetActive(false);
        deadUI.SetActive(false);
        playerMG.player.SetActive(false);

        //
        // Reinit
        deltaSpawnInnerStructure = 0;

        //
        // Key init
        foreach (GameObject key in keyUIFull)
        {
            key.GetComponent<SpriteRenderer>().enabled = false;
        }
        keyStatue = 0;
        butonSecret.gameObject.SetActive(false);

        PlayButtonSound();
    }

    public void GetTouch() {
        currentLife--;
        lifes[currentLife].GetComponent<SpriteRenderer>().sprite = lifeEmpty;
        if(currentLife <= 0)
        {
            isDead = true;
            playerMG.Die();
            PlayDeadSound();
            deadUI.SetActive(true);
            //playerMG.player.SetActive(false);
            isPlaying = false;
            groundMG.GetComponent<GroundManager>().isPlaying = false;
            backgroundMG.GetComponent<BackgroundManager>().isPlaying = false;
            butonSecret.gameObject.SetActive(true);

            foreach (Transform child in transform)
            {
                Destroy(child.transform.gameObject);
            }
        }
        else
        {
            playerMG.GetTouch();
        }

        cameraShake.TriggerShake();
    }

    internal void AddKey(int keyColor)
    {
        keyUIFull[keyStatue].GetComponent<SpriteRenderer>().enabled = true;
        keyStatue++;
    }

    internal void PlayButtonSound()
    {
        buttonSource.Play();
    }

    internal void PlayHurtSound()
    {
        hurtSource.Play();
    }

    internal void PlayKeySound()
    {
        keySource.Play();
    }

    internal void PlayCoinSound()
    {
        coinSource.Play();
    }

    internal void PlaySnailSound()
    {
        snailSource.Play();
    }

    internal void PlayJumpSound()
    {
        jumpSource.Play();
    }

    internal void PlayDeadSound()
    {
        deadSource.Play();
    }
}
