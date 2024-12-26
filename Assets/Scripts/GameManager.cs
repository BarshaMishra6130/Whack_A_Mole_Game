//using UnityEngine;
//using System.Collections.Generic;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Elements")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    public GameObject pauseMenuScreen;
//    public GameObject modeMenuPanel; // Mode selection panel at start
//    public GameObject levelMenuPanel;

//    private float startingTime;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;
//    private bool isModeSelected = false;

//    private void Start()
//    {
//        // Initialize UI visibility
//        ShowModeMenu();
//    }

//    private void ShowModeMenu()
//    {
//        // Show the mode selection panel at the start
//        modeMenuPanel.SetActive(true);
//        playButton.SetActive(false);
//        gameUI.SetActive(false);
//        pauseMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);
//    }

//    public void SetModeEasy()
//    {
//        startingTime = 45f;
//        isModeSelected = true;
//        PrepareForPlay();
//    }

//    public void SetModeMedium()
//    {
//        startingTime = 30f;
//        isModeSelected = true;
//        PrepareForPlay();
//    }

//    public void SetModeHard()
//    {
//        startingTime = 15f;
//        isModeSelected = true;
//        PrepareForPlay();
//    }

//    private void PrepareForPlay()
//    {
//        // Hide the mode selection panel and show the play button
//        modeMenuPanel.SetActive(false);
//        playButton.SetActive(true);
//    }

//    public void StartGame()
//    {
//        if (!isModeSelected)
//        {
//            Debug.LogWarning("Please select a mode before starting the game.");
//            return;
//        }

//        // UI setup for game start
//        playButton.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);
//        gameUI.SetActive(true);

//        // Reset game state
//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        timeRemaining = startingTime;
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        // Play background music at game start
//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }
//        playing = false;
//        playButton.SetActive(true); // Show play button again for restart
//    }

//    private void Update()
//    {
//        if (playing)
//        {
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0);
//            }
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);
//                if (!currentMoles.Contains(moles[index]))
//                {
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//            }
//        }
//    }

//    public void AddScore(int moleIndex)
//    {
//        score += 1;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}





//using UnityEngine.SceneManagement;
//using System.Collections.Generic;
//using UnityEngine;


//public class GameManager : MonoBehaviour
//{
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager; // Reference to AudioManager

//    [Header("UI objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    public GameObject pauseMenuScreen;
//    public GameObject modeMenuScreen;
//    private float startingTime = 30f;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    public void StartGame()
//    {
//        // UI setup for game start
//        playButton.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);
//        gameUI.SetActive(true);

//        // Reset moles and states
//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        timeRemaining = startingTime;
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        // Play background music at game start
//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        // Display game over messages
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();  // Play time up sound
//            //AudioManager.instance.PlaySFX(AudioManager.instance.outoftime);
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();  // Play bomb fail sound
//        }

//        // Stop game and reset moles
//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }
//        playing = false;
//        playButton.SetActive(true);
//    }

//    private void Update()
//    {
//        if (playing)
//        {
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0);
//            }
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);
//                if (!currentMoles.Contains(moles[index]))
//                {
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//            }
//        }
//    }

//    public void AddScore(int moleIndex)
//    {
//        score += 1;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);

//        // Play mole click sound
//        audioManager.PlayMoleClickSound();
//    }
//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        modeMenuScreen.SetActive(true);
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        modeMenuScreen.SetActive(false);
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}




//using UnityEngine;
//using System.Collections.Generic;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager; // Reference to AudioManager

//    [Header("UI objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    public GameObject pauseMenuScreen;
//    public GameObject modeMenuScreen; // Mode selection panel with Easy, Medium, Hard buttons
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initial setup to show only the play button
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);
//    }

//    public void ShowModeMenu()
//    {
//        // Display mode selection options and hide play button
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        // Easy mode timer setup
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        // Medium mode timer setup
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        // Hard mode timer setup
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        // Initialize game settings and UI
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        // Play background music at game start
//        audioManager.PlayBackgroundMusic();
//    }

//    private void Update()
//    {
//        if (playing)
//        {
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0);
//            }
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);
//                if (!currentMoles.Contains(moles[index]))
//                {
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//            }
//        }
//    }

//    public void GameOver(int type)
//    {
//        // Show game over message
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        // Stop game
//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }
//        playing = false;
//        playButton.SetActive(true); // Show play button for restart
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        score += 1;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}




//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen; // Mode selection panel

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;
//    [SerializeField] private Vector2 hammerOffset = new Vector2(-0.5f, 0.5f);

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        Cursor.visible = false;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;
//    }

//    private void Update()
//    {
//        // Update hammer position to follow the mouse
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        hammerObject.transform.position = mousePosition + (Vector3)hammerOffset;

//        // Detect mouse click for hammer animation
//        if (Input.GetMouseButtonDown(0))
//        {
//            SetHammerSprite(true);
//        }

//        if (playing)
//        {
//            // Update timer
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0); // Out of time
//            }

//            // Update UI timer display
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            // Handle mole spawning logic
//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);
//                if (!currentMoles.Contains(moles[index]))
//                {
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//            }
//        }
//    }

//    public void ShowModeMenu()
//    {
//        // Display mode selection and hide play button
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        // Easy mode timer setup
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        // Medium mode timer setup
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        // Hard mode timer setup
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        // Initialize game settings and UI
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        // Play background music
//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        // Handle game over scenarios
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        // Stop game logic
//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true); // Show play button for restart
//    }

//    public void PauseGame()
//    {
//        // Pause the game
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        // Resume the game
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        // Increment score and update UI
//        score++;
//        scoreText.text = $"{score}";
//        timeRemaining += 1; // Reward player with extra time
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        // Handle penalties for missing a mole
//        if (isMole)
//        {
//            timeRemaining -= 2; // Penalize with reduced time
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }

//    public void SetHammerSprite(bool isClicking)
//    {
//        // Change hammer sprite when hitting or not hitting
//        hammerRenderer.sprite = isClicking ? hitHammerSprite : normalHammerSprite;

//        if (isClicking)
//        {
//            StartCoroutine(ResetHammerSprite());
//        }
//    }

//    private IEnumerator ResetHammerSprite()
//    {
//        // Reset hammer sprite after a short delay
//        yield return new WaitForSeconds(0.2f);
//        hammerRenderer.sprite = normalHammerSprite;
//    }
//}



//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen; // Mode selection panel

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;
//    [SerializeField] private Vector2 hammerOffset = new Vector2(-0.5f, 0.5f);

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        Cursor.visible = false;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;
//    }

//    private void Update()
//    {
//        // Update hammer position
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        hammerObject.transform.position = mousePosition + (Vector3)hammerOffset;

//        // Detect mouse click for hammer animation
//        if (Input.GetMouseButtonDown(0))
//        {
//            SetHammerSprite(true);
//        }

//        if (playing)
//        {
//            // Update timer
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0); // Out of time
//            }

//            // Update UI timer display
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            // Spawn moles
//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);
//                Debug.Log($"Trying to activate mole at index {index}");

//                if (!currentMoles.Contains(moles[index]))
//                {
//                    Debug.Log($"Activating mole at index {index}");
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//                else
//                {
//                    Debug.Log($"Mole at index {index} is already active.");
//                }
//            }
//        }
//    }

//    public void ShowModeMenu()
//    {
//        // Display mode selection and hide play button
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        // Easy mode timer setup
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        // Medium mode timer setup
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        // Hard mode timer setup
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        // Initialize game settings and UI
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        // Play background music
//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        // Handle game over scenarios
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        // Stop game logic
//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true); // Show play button for restart
//    }

//    public void PauseGame()
//    {
//        // Pause the game
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        // Resume the game
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        // Increment score and update UI
//        score++;
//        scoreText.text = $"{score}";
//        timeRemaining += 1; // Reward player with extra time
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        // Handle penalties for missing a mole
//        if (isMole)
//        {
//            timeRemaining -= 2; // Penalize with reduced time
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }

//    public void SetHammerSprite(bool isClicking)
//    {
//        // Change hammer sprite when hitting or not hitting
//        hammerRenderer.sprite = isClicking ? hitHammerSprite : normalHammerSprite;

//        if (isClicking)
//        {
//            StartCoroutine(ResetHammerSprite());
//        }
//    }

//    private IEnumerator ResetHammerSprite()
//    {
//        // Reset hammer sprite after a short delay
//        yield return new WaitForSeconds(0.2f);
//        hammerRenderer.sprite = normalHammerSprite;
//    }
//}



//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen;

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;
//    [SerializeField] private Vector2 hammerOffset = new Vector2(-0.5f, 0.5f);

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        Cursor.visible = false;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;
//    }

//    private void Update()
//    {
//        if (!hammerObject)
//            return;

//        // Update hammer position
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0; // Keep the hammer on the same z-plane
//        hammerObject.transform.position = mousePosition + (Vector3)hammerOffset;

//        // Detect mouse click for hammer animation
//        if (Input.GetMouseButtonDown(0))
//        {
//            SetHammerSprite(true);
//        }

//        if (playing)
//        {
//            // Update timer
//            timeRemaining -= Time.deltaTime;
//            if (timeRemaining <= 0)
//            {
//                timeRemaining = 0;
//                GameOver(0); // Out of time
//            }

//            // Update UI timer display
//            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//            // Spawn moles
//            if (currentMoles.Count <= (score / 10))
//            {
//                int index = Random.Range(0, moles.Count);

//                if (!currentMoles.Contains(moles[index]))
//                {
//                    currentMoles.Add(moles[index]);
//                    moles[index].Activate(score / 10);
//                }
//            }
//        }
//    }

//    public void SetHammerSprite(bool isClicking)
//    {
//        if (!hammerRenderer) return;

//        // Change hammer sprite when clicking or not
//        hammerRenderer.sprite = isClicking ? hitHammerSprite : normalHammerSprite;

//        if (isClicking)
//        {
//            StartCoroutine(ResetHammerSprite());
//        }
//    }

//    private IEnumerator ResetHammerSprite()
//    {
//        yield return new WaitForSeconds(0.2f);
//        hammerRenderer.sprite = normalHammerSprite;
//    }

//    public void ShowModeMenu()
//    {
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.SetIndex(moles.IndexOf(mole));
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true);
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        score++;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}




//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen;

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        hammerObject.SetActive(false); // Initially hidden
//        Cursor.visible = true;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;
//    }

//    private void Update()
//    {
//        if (!playing) return;

//        // Update timer
//        timeRemaining -= Time.deltaTime;
//        if (timeRemaining <= 0)
//        {
//            timeRemaining = 0;
//            GameOver(0); // Out of time
//        }

//        // Update UI timer display
//        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//        // Spawn moles
//        if (currentMoles.Count <= (score / 10))
//        {
//            int index = Random.Range(0, moles.Count);

//            if (!currentMoles.Contains(moles[index]))
//            {
//                currentMoles.Add(moles[index]);
//                moles[index].Activate(score / 10);
//            }
//        }

//        // Check for mouse click and handle mole hit
//        if (Input.GetMouseButtonDown(0))
//        {
//            HandleClick();
//        }
//    }

//    private void HandleClick()
//    {
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0; // Keep the hammer on the same z-plane

//        foreach (var mole in moles)
//        {
//            if (mole.IsHit(mousePosition)) // Check if the mole was hit
//            {
//                ShowHammer(mousePosition, true); // Show hammer hitting
//                AddScore(mole.Index); // Use mole's Index property
//                return;
//            }
//        }

//        // If no mole hit, show hammer briefly and then hide
//        ShowHammer(mousePosition, false);
//    }

//    private void ShowHammer(Vector3 position, bool isHit)
//    {
//        hammerObject.transform.position = position;
//        hammerRenderer.sprite = isHit ? hitHammerSprite : normalHammerSprite;
//        hammerObject.SetActive(true);

//        StartCoroutine(HideHammer());
//    }

//    private IEnumerator HideHammer()
//    {
//        yield return new WaitForSeconds(0.2f);
//        hammerObject.SetActive(false);
//    }

//    public void ShowModeMenu()
//    {
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.Index = moles.IndexOf(mole); // Set the mole's index
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true);
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        score++;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}




//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen;

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;

//    [Header("Party Blast Effect")] // New header for Party Blast Effect
//    [SerializeField] private ParticleSystem partyBlastEffect;

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        hammerObject.SetActive(false); // Initially hidden
//        Cursor.visible = true;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;

//        // Ensure Party Blast Effect is inactive at the start
//        if (partyBlastEffect != null)
//        {
//            partyBlastEffect.Stop();
//        }
//    }

//    private void Update()
//    {
//        if (!playing) return;

//        // Update timer
//        timeRemaining -= Time.deltaTime;
//        if (timeRemaining <= 0)
//        {
//            timeRemaining = 0;
//            GameOver(0); // Out of time
//        }

//        // Update UI timer display
//        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//        // Spawn moles
//        if (currentMoles.Count <= (score / 10))
//        {
//            int index = Random.Range(0, moles.Count);

//            if (!currentMoles.Contains(moles[index]))
//            {
//                currentMoles.Add(moles[index]);
//                moles[index].Activate(score / 10);
//            }
//        }

//        // Check for mouse click and handle mole hit
//        if (Input.GetMouseButtonDown(0))
//        {
//            HandleClick();
//        }
//    }

//    private void HandleClick()
//    {
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0; // Keep the hammer on the same z-plane

//        foreach (var mole in moles)
//        {
//            if (mole.IsHit(mousePosition)) // Check if the mole was hit
//            {
//                ShowHammer(mousePosition, true); // Show hammer hitting
//                AddScore(mole.Index); // Use mole's Index property
//                return;
//            }
//        }

//        // If no mole hit, show hammer briefly and then hide
//        ShowHammer(mousePosition, false);
//    }

//    private void ShowHammer(Vector3 position, bool isHit)
//    {
//        hammerObject.transform.position = position;
//        hammerRenderer.sprite = isHit ? hitHammerSprite : normalHammerSprite;
//        hammerObject.SetActive(true);

//        StartCoroutine(HideHammer());
//    }

//    private IEnumerator HideHammer()
//    {
//        yield return new WaitForSeconds(0.2f);
//        hammerObject.SetActive(false);
//    }

//    public void ShowModeMenu()
//    {
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.Index = moles.IndexOf(mole); // Set the mole's index
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        audioManager.PlayBackgroundMusic();
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();

//            // Play Party Blast Effect
//            if (partyBlastEffect != null)
//            {
//                partyBlastEffect.Play();
//            }
//            else
//            {
//                Debug.LogWarning("Party Blast Effect is not assigned in the Inspector.");
//            }
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true);
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void AddScore(int moleIndex)
//    {
//        score++;
//        scoreText.text = $"{score}";
//        timeRemaining += 1;
//        currentMoles.Remove(moles[moleIndex]);
//        audioManager.PlayMoleClickSound();
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}


//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen;

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;

//    [Header("Party Blast Effect")] // New header for Party Blast Effect
//    [SerializeField] private ParticleSystem partyBlastEffect;

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        hammerObject.SetActive(false); // Initially hidden
//        Cursor.visible = true;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;

//        // Ensure Party Blast Effect is inactive at the start
//        if (partyBlastEffect != null)
//        {
//            partyBlastEffect.Stop();
//        }
//    }

//    private void Update()
//    {
//        if (!playing) return;

//        // Update timer
//        timeRemaining -= Time.deltaTime;
//        if (timeRemaining <= 0)
//        {
//            timeRemaining = 0;
//            GameOver(0); // Out of time
//        }

//        // Update UI timer display
//        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//        // Spawn moles
//        if (currentMoles.Count <= (score / 10))
//        {
//            int index = Random.Range(0, moles.Count);

//            if (!currentMoles.Contains(moles[index]))
//            {
//                currentMoles.Add(moles[index]);
//                moles[index].Activate(score / 10);
//            }
//        }

//        // Check for mouse click and handle mole hit
//        if (Input.GetMouseButtonDown(0))
//        {
//            HandleClick();
//        }
//    }

//    private void HandleClick()
//    {
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0; // Keep the hammer on the same z-plane

//        foreach (var mole in moles)
//        {
//            if (mole.IsHit(mousePosition)) // Check if the mole was hit
//            {
//                ShowHammer(mousePosition, true); // Show hammer hitting

//                // Handle scoring based on mole type
//                if (mole.IsHittable)
//                {
//                    if (mole.GetMoleType() == Mole.MoleType.Standard)
//                    {
//                        AddScore(1); // Increment by 1 for standard mole
//                    }
//                    else if (mole.GetMoleType() == Mole.MoleType.HardHat)
//                    {
//                        AddScore(2); // Increment by 2 for hard hat mole
//                    }
//                }

//                timeRemaining += 1; // Add 1 second to the timer
//                currentMoles.Remove(mole);
//                audioManager.PlayMoleClickSound();
//                return;
//            }
//        }

//        // If no mole hit, show hammer briefly and then hide
//        ShowHammer(mousePosition, false);
//    }

//    private void ShowHammer(Vector3 position, bool isHit)
//    {
//        hammerObject.transform.position = position;
//        hammerRenderer.sprite = isHit ? hitHammerSprite : normalHammerSprite;
//        hammerObject.SetActive(true);

//        StartCoroutine(HideHammer());
//    }

//    private IEnumerator HideHammer()
//    {
//        yield return new WaitForSeconds(0.2f);
//        hammerObject.SetActive(false);
//    }

//    public void ShowModeMenu()
//    {
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.Index = moles.IndexOf(mole); // Set the mole's index
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        audioManager.PlayBackgroundMusic();
//    }

//    public void AddScore(int points)
//    {
//        score += points;
//        UpdateScoreUI(); // Update the score on the UI
//    }

//    // Method to update the score on UI
//    private void UpdateScoreUI()
//    {
//        scoreText.text = score.ToString(); // Update the score display in the UI
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();

//            // Play Party Blast Effect
//            if (partyBlastEffect != null)
//            {
//                partyBlastEffect.Play();
//            }
//            else
//            {
//                Debug.LogWarning("Party Blast Effect is not assigned in the Inspector.");
//            }
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true);
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}


//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    [Header("Moles and Audio")]
//    [SerializeField] private List<Mole> moles;
//    [SerializeField] private AudioManager audioManager;

//    [Header("UI Objects")]
//    [SerializeField] private GameObject playButton;
//    [SerializeField] private GameObject gameUI;
//    [SerializeField] private GameObject outOfTimeText;
//    [SerializeField] private GameObject bombText;
//    [SerializeField] private TMPro.TextMeshProUGUI timeText;
//    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

//    [Header("Menus")]
//    [SerializeField] private GameObject pauseMenuScreen;
//    [SerializeField] private GameObject modeMenuScreen;

//    [Header("Hammer Settings")]
//    [SerializeField] private Sprite normalHammerSprite;
//    [SerializeField] private Sprite hitHammerSprite;

//    [Header("Party Blast Effect")]
//    [SerializeField] private ParticleSystem partyBlastEffect;

//    private SpriteRenderer hammerRenderer;
//    private GameObject hammerObject;
//    private float timeRemaining;
//    private HashSet<Mole> currentMoles = new HashSet<Mole>();
//    private int score;
//    private bool playing = false;

//    private void Start()
//    {
//        // Initialize UI states
//        playButton.SetActive(true);
//        gameUI.SetActive(false);
//        modeMenuScreen.SetActive(false);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        // Initialize hammer
//        hammerObject = new GameObject("HammerCursor");
//        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
//        hammerRenderer.sprite = normalHammerSprite;
//        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
//        hammerObject.SetActive(false); // Initially hidden
//        Cursor.visible = true;

//        // Ensure time scale is normal at the start
//        Time.timeScale = 1;

//        // Ensure Party Blast Effect is inactive at the start
//        if (partyBlastEffect != null)
//        {
//            partyBlastEffect.Stop();
//        }
//    }

//    private void Update()
//    {
//        if (!playing) return;

//        // Update timer
//        timeRemaining -= Time.deltaTime;
//        if (timeRemaining <= 0)
//        {
//            timeRemaining = 0;
//            GameOver(0); // Out of time
//        }

//        // Update UI timer display
//        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

//        // Spawn moles
//        if (currentMoles.Count <= (score / 10))
//        {
//            int index = Random.Range(0, moles.Count);

//            if (!currentMoles.Contains(moles[index]))
//            {
//                currentMoles.Add(moles[index]);
//                moles[index].Activate(score / 10);
//            }
//        }

//        // Check for mouse click and handle mole hit
//        if (Input.GetMouseButtonDown(0))
//        {
//            HandleClick();
//        }
//    }

//    private void HandleClick()
//    {
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        mousePosition.z = 0; // Keep the hammer on the same z-plane

//        foreach (var mole in moles)
//        {
//            if (mole.IsHit(mousePosition)) // Check if the mole was hit
//            {
//                ShowHammer(mousePosition, true); // Show hammer hitting

//                // Handle scoring based on mole type
//                if (mole.IsHittable)
//                {
//                    if (mole.moleType == Mole.MoleType.Standard) // Access moleType directly
//                    {
//                        AddScore(1); // Increment by 1 for standard mole
//                    }
//                    else if (mole.moleType == Mole.MoleType.HardHat) // Access moleType directly
//                    {
//                        AddScore(2); // Increment by 2 for hard hat mole
//                    }
//                }

//                timeRemaining += 1; // Add 1 second to the timer
//                currentMoles.Remove(mole);
//                audioManager.PlayMoleClickSound();
//                return;
//            }
//        }

//        // If no mole hit, show hammer briefly and then hide
//        ShowHammer(mousePosition, false);
//    }

//    private void ShowHammer(Vector3 position, bool isHit)
//    {
//        hammerObject.transform.position = position;
//        hammerRenderer.sprite = isHit ? hitHammerSprite : normalHammerSprite;
//        hammerObject.SetActive(true);

//        StartCoroutine(HideHammer());
//    }

//    private IEnumerator HideHammer()
//    {
//        yield return new WaitForSeconds(0.2f);
//        hammerObject.SetActive(false);
//    }

//    public void ShowModeMenu()
//    {
//        playButton.SetActive(false);
//        modeMenuScreen.SetActive(true);
//    }

//    public void SetModeEasy()
//    {
//        timeRemaining = 60f;
//        StartGame();
//    }

//    public void SetModeMedium()
//    {
//        timeRemaining = 30f;
//        StartGame();
//    }

//    public void SetModeHard()
//    {
//        timeRemaining = 15f;
//        StartGame();
//    }

//    private void StartGame()
//    {
//        modeMenuScreen.SetActive(false);
//        gameUI.SetActive(true);
//        outOfTimeText.SetActive(false);
//        bombText.SetActive(false);

//        foreach (var mole in moles)
//        {
//            mole.Hide();
//            mole.Index = moles.IndexOf(mole); // Set the mole's index
//        }
//        currentMoles.Clear();
//        score = 0;
//        scoreText.text = "0";
//        playing = true;

//        audioManager.PlayBackgroundMusic();
//    }

//    public void AddScore(int points)
//    {
//        score += points;
//        UpdateScoreUI(); // Update the score on the UI
//    }

//    // Method to update the score on UI
//    private void UpdateScoreUI()
//    {
//        scoreText.text = score.ToString(); // Update the score display in the UI
//    }

//    public void GameOver(int type)
//    {
//        if (type == 0)
//        {
//            outOfTimeText.SetActive(true);
//            audioManager.PlayTimeUpSound();

//            // Play Party Blast Effect
//            if (partyBlastEffect != null)
//            {
//                partyBlastEffect.Play();
//            }
//            else
//            {
//                Debug.LogWarning("Party Blast Effect is not assigned in the Inspector.");
//            }
//        }
//        else
//        {
//            bombText.SetActive(true);
//            audioManager.PlayBombFailSound();
//        }

//        foreach (var mole in moles)
//        {
//            mole.StopGame();
//        }

//        playing = false;
//        playButton.SetActive(true);
//    }

//    public void PauseGame()
//    {
//        Time.timeScale = 0;
//        pauseMenuScreen.SetActive(true);
//        playing = false;
//    }

//    public void ResumeGame()
//    {
//        Time.timeScale = 1;
//        pauseMenuScreen.SetActive(false);
//        playing = true;
//    }

//    public void Missed(int moleIndex, bool isMole)
//    {
//        if (isMole)
//        {
//            timeRemaining -= 2;
//        }
//        currentMoles.Remove(moles[moleIndex]);
//    }
//}


using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Moles and Audio")]
    [SerializeField] private List<Mole> moles;
    [SerializeField] private AudioManager audioManager;

    [Header("UI Objects")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject bombText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenuScreen;
    [SerializeField] private GameObject modeMenuScreen;

    [Header("Hammer Settings")]
    [SerializeField] private Sprite normalHammerSprite;
    [SerializeField] private Sprite hitHammerSprite;

    [Header("Party Blast Effect")]
    [SerializeField] private ParticleSystem partyBlastEffect;

    private SpriteRenderer hammerRenderer;
    private GameObject hammerObject;
    private float timeRemaining;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    private void Start()
    {
        // Initialize UI states
        playButton.SetActive(true);
        gameUI.SetActive(false);
        modeMenuScreen.SetActive(false);
        outOfTimeText.SetActive(false);
        bombText.SetActive(false);

        // Initialize hammer
        hammerObject = new GameObject("HammerCursor");
        hammerRenderer = hammerObject.AddComponent<SpriteRenderer>();
        hammerRenderer.sprite = normalHammerSprite;
        hammerRenderer.sortingOrder = 10; // Ensure it appears above other objects
        hammerObject.SetActive(false); // Initially hidden
        Cursor.visible = true;

        // Ensure time scale is normal at the start
        Time.timeScale = 1;

        // Ensure Party Blast Effect is inactive at the start
        if (partyBlastEffect != null)
        {
            partyBlastEffect.Stop();
        }
    }

    private void Update()
    {
        if (!playing) return;

        // Update timer
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver(0); // Out of time
        }

        // Update UI timer display
        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";

        // Spawn moles
        if (currentMoles.Count <= (score / 10))
        {
            int index = Random.Range(0, moles.Count);

            if (!currentMoles.Contains(moles[index]))
            {
                currentMoles.Add(moles[index]);
                moles[index].Activate(score / 10);
            }
        }

        // Check for mouse click and handle mole hit
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Keep the hammer on the same z-plane

        foreach (var mole in moles)
        {
            if (mole.IsHit(mousePosition)) // Check if the mole was hit
            {
                ShowHammer(mousePosition, true); // Show hammer hitting

                // Handle scoring based on mole type using getter method
                if (mole.IsHittable)
                {
                    if (mole.GetMoleType() == Mole.MoleType.Standard) // Use getter method
                    {
                        AddScore(1); // Increment by 1 for standard mole
                    }
                    else if (mole.GetMoleType() == Mole.MoleType.HardHat) // Use getter method
                    {
                        AddScore(2); // Increment by 2 for hard hat mole
                    }
                }

                timeRemaining += 1; // Add 1 second to the timer
                currentMoles.Remove(mole);
                audioManager.PlayMoleClickSound();
                return;
            }
        }

        // If no mole hit, show hammer briefly and then hide
        ShowHammer(mousePosition, false);
    }

    private void ShowHammer(Vector3 position, bool isHit)
    {
        hammerObject.transform.position = position;
        hammerRenderer.sprite = isHit ? hitHammerSprite : normalHammerSprite;
        hammerObject.SetActive(true);

        StartCoroutine(HideHammer());
    }

    private IEnumerator HideHammer()
    {
        yield return new WaitForSeconds(0.2f);
        hammerObject.SetActive(false);
    }

    public void ShowModeMenu()
    {
        playButton.SetActive(false);
        modeMenuScreen.SetActive(true);
    }

    public void SetModeEasy()
    {
        timeRemaining = 60f;
        StartGame();
    }

    public void SetModeMedium()
    {
        timeRemaining = 30f;
        StartGame();
    }

    public void SetModeHard()
    {
        timeRemaining = 15f;
        StartGame();
    }

    private void StartGame()
    {
        modeMenuScreen.SetActive(false);
        gameUI.SetActive(true);
        outOfTimeText.SetActive(false);
        bombText.SetActive(false);

        foreach (var mole in moles)
        {
            mole.Hide();
            mole.Index = moles.IndexOf(mole); // Set the mole's index
        }
        currentMoles.Clear();
        score = 0;
        scoreText.text = "0";
        playing = true;

        audioManager.PlayBackgroundMusic();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI(); // Update the score on the UI
    }

    // Method to update the score on UI
    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString(); // Update the score display in the UI
    }

    public void GameOver(int type)
    {
        if (type == 0)
        {
            outOfTimeText.SetActive(true);
            audioManager.PlayTimeUpSound();

            // Play Party Blast Effect
            if (partyBlastEffect != null)
            {
                partyBlastEffect.Play();
            }
            else
            {
                Debug.LogWarning("Party Blast Effect is not assigned in the Inspector.");
            }
        }
        else
        {
            bombText.SetActive(true);
            audioManager.PlayBombFailSound();
        }

        foreach (var mole in moles)
        {
            mole.StopGame();
        }

        playing = false;
        playButton.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
        playing = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        playing = true;
    }

    public void Missed(int moleIndex, bool isMole)
    {
        if (isMole)
        {
            timeRemaining -= 2;
        }
        currentMoles.Remove(moles[moleIndex]);
    }
}



