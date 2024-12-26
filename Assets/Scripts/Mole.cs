//using System.Collections;
//using UnityEngine;

//public class Mole : MonoBehaviour
//{
//    [Header("Graphics")]
//    [SerializeField] private Sprite mole;
//    [SerializeField] private Sprite moleHardHat;
//    [SerializeField] private Sprite moleHatBroken;
//    [SerializeField] private Sprite moleHit;
//    [SerializeField] private Sprite moleHatHit;

//    [Header("GameManager")]
//    [SerializeField] private GameManager gameManager;

//    [Header("AudioManager")]
//    [SerializeField] private AudioManager audioManager;  // Reference to AudioManager

//    // The offset of the sprite to hide it.
//    private Vector2 startPosition = new Vector2(0f, -2.56f);
//    private Vector2 endPosition = Vector2.zero;
//    // How long it takes to show a mole.
//    private float showDuration = 0.5f;
//    private float duration = 1f;

//    private SpriteRenderer spriteRenderer;
//    private Animator animator;
//    private BoxCollider2D boxCollider2D;
//    private Vector2 boxOffset;
//    private Vector2 boxSize;
//    private Vector2 boxOffsetHidden;
//    private Vector2 boxSizeHidden;

//    private bool hittable = true;
//    public enum MoleType { Standard, HardHat, Bomb };
//    private MoleType moleType;
//    private float hardRate = 0.25f;
//    private float bombRate = 0f;
//    private int lives;
//    private int moleIndex = 0;

//    private IEnumerator ShowHide(Vector2 start, Vector2 end)
//    {
//        transform.localPosition = start;
//        float elapsed = 0f;
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//        transform.localPosition = end;
//        boxCollider2D.offset = boxOffset;
//        boxCollider2D.size = boxSize;
//        yield return new WaitForSeconds(duration);
//        elapsed = 0f;
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//        transform.localPosition = start;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;

//        if (hittable)
//        {
//            hittable = false;
//            gameManager.Missed(moleIndex, moleType != MoleType.Bomb);
//        }
//    }

//    public void Hide()
//    {
//        transform.localPosition = startPosition;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;
//    }

//    private IEnumerator QuickHide()
//    {
//        yield return new WaitForSeconds(0.25f);
//        if (!hittable)
//        {
//            Hide();
//        }
//    }

//    private void OnMouseDown()
//    {
//        if (hittable)
//        {
//            switch (moleType)
//            {
//                case MoleType.Standard:
//                    spriteRenderer.sprite = moleHit;
//                    gameManager.AddScore(moleIndex);
//                    audioManager.PlayMoleClickSound();  // Play mole click sound
//                    StopAllCoroutines();
//                    StartCoroutine(QuickHide());
//                    hittable = false;
//                    break;
//                case MoleType.HardHat:
//                    if (lives == 2)
//                    {
//                        spriteRenderer.sprite = moleHatBroken;
//                        lives--;
//                    }
//                    else
//                    {
//                        spriteRenderer.sprite = moleHatHit;
//                        gameManager.AddScore(moleIndex);
//                        audioManager.PlayMoleClickSound();  // Play mole click sound
//                        StopAllCoroutines();
//                        StartCoroutine(QuickHide());
//                        hittable = false;
//                    }
//                    break;
//                case MoleType.Bomb:
//                    gameManager.GameOver(1);
//                    break;
//                default:
//                    break;
//            }
//        }
//    }

//    private void CreateNext()
//    {
//        float random = Random.Range(0f, 1f);
//        if (random < bombRate)
//        {
//            moleType = MoleType.Bomb;
//            animator.enabled = true;
//        }
//        else
//        {
//            animator.enabled = false;
//            random = Random.Range(0f, 1f);
//            if (random < hardRate)
//            {
//                moleType = MoleType.HardHat;
//                spriteRenderer.sprite = moleHardHat;
//                lives = 2;
//            }
//            else
//            {
//                moleType = MoleType.Standard;
//                spriteRenderer.sprite = mole;
//                lives = 1;
//            }
//        }
//        hittable = true;
//    }

//    private void SetLevel(int level)
//    {
//        bombRate = Mathf.Min(level * 0.025f, 0.25f);
//        hardRate = Mathf.Min(level * 0.025f, 1f);
//        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
//        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
//        duration = Random.Range(durationMin, durationMax);
//    }

//    private void Awake()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        animator = GetComponent<Animator>();
//        boxCollider2D = GetComponent<BoxCollider2D>();
//        boxOffset = boxCollider2D.offset;
//        boxSize = boxCollider2D.size;
//        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
//        boxSizeHidden = new Vector2(boxSize.x, 0f);
//    }

//    public void Activate(int level)
//    {
//        SetLevel(level);
//        CreateNext();
//        StartCoroutine(ShowHide(startPosition, endPosition));
//    }

//    public void SetIndex(int index)
//    {
//        moleIndex = index;
//    }

//    public void StopGame()
//    {
//        hittable = false;
//        StopAllCoroutines();
//    }
//}


//using System.Collections;
//using UnityEngine;

//public class Mole : MonoBehaviour
//{
//    [Header("Graphics")]
//    [SerializeField] private Sprite mole;
//    [SerializeField] private Sprite moleHardHat;
//    [SerializeField] private Sprite moleHatBroken;
//    [SerializeField] private Sprite moleHit;
//    [SerializeField] private Sprite moleHatHit;

//    [Header("GameManager")]
//    [SerializeField] private GameManager gameManager;

//    [Header("AudioManager")]
//    [SerializeField] private AudioManager audioManager;

//    // Position and movement parameters
//    private Vector2 startPosition = new Vector2(0f, -2.56f);
//    private Vector2 endPosition = Vector2.zero;
//    private float showDuration = 0.5f;
//    private float duration = 1f;

//    // Collider and visual components
//    private SpriteRenderer spriteRenderer;
//    private BoxCollider2D boxCollider2D;
//    private Vector2 boxOffset;
//    private Vector2 boxSize;
//    private Vector2 boxOffsetHidden;
//    private Vector2 boxSizeHidden;

//    // Mole state and type
//    private bool hittable = true;
//    public enum MoleType { Standard, HardHat, Bomb };
//    private MoleType moleType;
//    private float hardRate = 0.25f;
//    private float bombRate = 0f;
//    private int lives;
//    public int Index { get; set; } // Property to track the mole's position in the list

//    private IEnumerator ShowHide(Vector2 start, Vector2 end)
//    {
//        transform.localPosition = start;
//        float elapsed = 0f;

//        // Show mole animation
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = end;
//        boxCollider2D.offset = boxOffset;
//        boxCollider2D.size = boxSize;

//        // Wait while mole is active
//        yield return new WaitForSeconds(duration);

//        // Hide mole animation
//        elapsed = 0f;
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = start;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;

//        if (hittable)
//        {
//            hittable = false;
//            gameManager.Missed(Index, moleType != MoleType.Bomb);
//        }
//    }

//    public void Hide()
//    {
//        transform.localPosition = startPosition;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;
//    }

//    private IEnumerator QuickHide()
//    {
//        yield return new WaitForSeconds(0.25f);
//        if (!hittable)
//        {
//            Hide();
//        }
//    }

//    private void OnMouseDown()
//    {
//        if (hittable)
//        {
//            switch (moleType)
//            {
//                case MoleType.Standard:
//                    spriteRenderer.sprite = moleHit;
//                    gameManager.AddScore(Index);
//                    audioManager.PlayMoleClickSound();
//                    StopAllCoroutines();
//                    StartCoroutine(QuickHide());
//                    hittable = false;
//                    break;

//                case MoleType.HardHat:
//                    if (lives == 2)
//                    {
//                        spriteRenderer.sprite = moleHatBroken;
//                        lives--;
//                    }
//                    else
//                    {
//                        spriteRenderer.sprite = moleHatHit;
//                        gameManager.AddScore(Index);
//                        audioManager.PlayMoleClickSound();
//                        StopAllCoroutines();
//                        StartCoroutine(QuickHide());
//                        hittable = false;
//                    }
//                    break;

//                case MoleType.Bomb:
//                    gameManager.GameOver(1);
//                    break;

//                default:
//                    break;
//            }
//        }
//    }

//    private void CreateNext()
//    {
//        float random = Random.Range(0f, 1f);
//        if (random < bombRate)
//        {
//            moleType = MoleType.Bomb;
//        }
//        else
//        {
//            random = Random.Range(0f, 1f);
//            if (random < hardRate)
//            {
//                moleType = MoleType.HardHat;
//                spriteRenderer.sprite = moleHardHat;
//                lives = 2;
//            }
//            else
//            {
//                moleType = MoleType.Standard;
//                spriteRenderer.sprite = mole;
//                lives = 1;
//            }
//        }
//        hittable = true;
//    }

//    private void SetLevel(int level)
//    {
//        bombRate = Mathf.Min(level * 0.025f, 0.25f);
//        hardRate = Mathf.Min(level * 0.025f, 1f);
//        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
//        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
//        duration = Random.Range(durationMin, durationMax);
//    }

//    private void Awake()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        boxCollider2D = GetComponent<BoxCollider2D>();
//        boxOffset = boxCollider2D.offset;
//        boxSize = boxCollider2D.size;
//        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
//        boxSizeHidden = new Vector2(boxSize.x, 0f);
//    }

//    public void Activate(int level)
//    {
//        SetLevel(level);
//        CreateNext();
//        StartCoroutine(ShowHide(startPosition, endPosition));
//    }

//    public bool IsHit(Vector3 clickPosition)
//    {
//        Collider2D collider = GetComponent<Collider2D>();
//        return collider != null && collider.OverlapPoint(clickPosition);
//    }

//    public void StopGame()
//    {
//        hittable = false;
//        StopAllCoroutines();
//    }
//}

//using System.Collections;
//using UnityEngine;

//public class Mole : MonoBehaviour
//{
//    [Header("Graphics")]
//    [SerializeField] private Sprite mole;
//    [SerializeField] private Sprite moleHardHat;
//    [SerializeField] private Sprite moleHatBroken;
//    [SerializeField] private Sprite moleHit;
//    [SerializeField] private Sprite moleHatHit;
//    [SerializeField] private Sprite moleBomb;

//    [Header("GameManager")]
//    [SerializeField] private GameManager gameManager;

//    [Header("AudioManager")]
//    [SerializeField] private AudioManager audioManager;

//    // Position and movement parameters
//    private Vector2 startPosition = new Vector2(0f, -2.56f);
//    private Vector2 endPosition = Vector2.zero;
//    private float showDuration = 0.5f;
//    private float duration = 1f;

//    // Collider and visual components
//    private SpriteRenderer spriteRenderer;
//    private BoxCollider2D boxCollider2D;
//    private Vector2 boxOffset;
//    private Vector2 boxSize;
//    private Vector2 boxOffsetHidden;
//    private Vector2 boxSizeHidden;

//    // Mole state and type
//    private bool hittable = true;
//    public enum MoleType { Standard, HardHat, Bomb };
//    private MoleType moleType;
//    private float hardRate = 0.25f;
//    private float bombRate = 0f;
//    private int lives;
//    public int Index { get; set; } // Property to track the mole's position in the list

//    private IEnumerator ShowHide(Vector2 start, Vector2 end)
//    {
//        transform.localPosition = start;
//        float elapsed = 0f;

//        // Show mole animation
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = end;
//        boxCollider2D.offset = boxOffset;
//        boxCollider2D.size = boxSize;

//        // Wait while mole is active
//        yield return new WaitForSeconds(duration);

//        // Hide mole animation
//        elapsed = 0f;
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = start;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;

//        if (hittable)
//        {
//            hittable = false;
//            gameManager.Missed(Index, moleType != MoleType.Bomb);
//        }
//    }

//    public void Hide()
//    {
//        transform.localPosition = startPosition;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;
//    }

//    private IEnumerator QuickHide()
//    {
//        yield return new WaitForSeconds(0.25f);
//        if (!hittable)
//        {
//            Hide();
//        }
//    }

//    private void OnMouseDown()
//    {
//        if (hittable)
//        {
//            switch (moleType)
//            {
//                case MoleType.Standard:
//                    spriteRenderer.sprite = moleHit;
//                    gameManager.AddScore(1); // Ensuring +1 score for standard mole
//                    audioManager.PlayMoleClickSound();
//                    StopAllCoroutines();
//                    StartCoroutine(QuickHide());
//                    hittable = false;
//                    break;

//                case MoleType.HardHat:
//                    if (lives == 2)
//                    {
//                        spriteRenderer.sprite = moleHatBroken;
//                        lives--;
//                    }
//                    else
//                    {
//                        spriteRenderer.sprite = moleHatHit;
//                        gameManager.AddScore(2); // Keeping +2 score for hard hat mole
//                        audioManager.PlayMoleClickSound();
//                        StopAllCoroutines();
//                        StartCoroutine(QuickHide());
//                        hittable = false;
//                    }
//                    break;

//                case MoleType.Bomb:
//                    spriteRenderer.sprite = moleBomb; // Fixing bomb sprite issue
//                    gameManager.GameOver(1);
//                    break;

//                default:
//                    break;
//            }
//        }
//    }

//    private void CreateNext()
//    {
//        float random = Random.Range(0f, 1f);
//        if (random < bombRate)
//        {
//            moleType = MoleType.Bomb;
//            spriteRenderer.sprite = moleBomb;
//        }
//        else
//        {
//            random = Random.Range(0f, 1f);
//            if (random < hardRate)
//            {
//                moleType = MoleType.HardHat;
//                spriteRenderer.sprite = moleHardHat;
//                lives = 2;
//            }
//            else
//            {
//                moleType = MoleType.Standard;
//                spriteRenderer.sprite = mole;
//                lives = 1;
//            }
//        }
//        hittable = true;
//    }

//    private void SetLevel(int level)
//    {
//        bombRate = Mathf.Min(level * 0.025f, 0.25f);
//        hardRate = Mathf.Min(level * 0.025f, 1f);
//        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
//        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
//        duration = Random.Range(durationMin, durationMax);
//    }

//    private void Awake()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        boxCollider2D = GetComponent<BoxCollider2D>();
//        boxOffset = boxCollider2D.offset;
//        boxSize = boxCollider2D.size;
//        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
//        boxSizeHidden = new Vector2(boxSize.x, 0f);
//    }

//    public void Activate(int level)
//    {
//        SetLevel(level);
//        CreateNext();
//        StartCoroutine(ShowHide(startPosition, endPosition));
//    }

//    public bool IsHit(Vector3 clickPosition)
//    {
//        Collider2D collider = GetComponent<Collider2D>();
//        return collider != null && collider.OverlapPoint(clickPosition);
//    }

//    public void StopGame()
//    {
//        hittable = false;
//        StopAllCoroutines();
//    }
//}


//using System.Collections;
//using UnityEngine;

//public class Mole : MonoBehaviour
//{
//    [Header("Graphics")]
//    [SerializeField] private Sprite mole;
//    [SerializeField] private Sprite moleHardHat;
//    [SerializeField] private Sprite moleHatBroken;
//    [SerializeField] private Sprite moleHit;
//    [SerializeField] private Sprite moleHatHit;
//    [SerializeField] private Sprite moleBomb;

//    [Header("GameManager")]
//    [SerializeField] private GameManager gameManager;

//    [Header("AudioManager")]
//    [SerializeField] private AudioManager audioManager;

//    private Vector2 startPosition = new Vector2(0f, -2.56f);
//    private Vector2 endPosition = Vector2.zero;
//    private float showDuration = 0.5f;
//    private float duration = 1f;

//    private SpriteRenderer spriteRenderer;
//    private BoxCollider2D boxCollider2D;
//    private Vector2 boxOffset;
//    private Vector2 boxSize;
//    private Vector2 boxOffsetHidden;
//    private Vector2 boxSizeHidden;

//    private bool hittable = true;

//    public enum MoleType { Standard, HardHat, Bomb };
//    private MoleType moleType;
//    private float hardRate = 0.25f;
//    private float bombRate = 0f;
//    private int lives;
//    public int Index { get; set; }

//    // Adding IsHittable property to expose the hittable status
//    public bool IsHittable
//    {
//        get { return hittable; }
//    }

//    private IEnumerator ShowHide(Vector2 start, Vector2 end)
//    {
//        transform.localPosition = start;
//        float elapsed = 0f;

//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = end;
//        boxCollider2D.offset = boxOffset;
//        boxCollider2D.size = boxSize;

//        yield return new WaitForSeconds(duration);

//        elapsed = 0f;
//        while (elapsed < showDuration)
//        {
//            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
//            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
//            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        transform.localPosition = start;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;

//        if (hittable)
//        {
//            hittable = false;
//            gameManager.Missed(Index, moleType != MoleType.Bomb);
//        }
//    }

//    public void Hide()
//    {
//        transform.localPosition = startPosition;
//        boxCollider2D.offset = boxOffsetHidden;
//        boxCollider2D.size = boxSizeHidden;
//    }

//    private IEnumerator QuickHide()
//    {
//        yield return new WaitForSeconds(0.25f);
//        if (!hittable)
//        {
//            Hide();
//        }
//    }

//    private void OnMouseDown()
//    {
//        if (hittable)
//        {
//            switch (moleType)
//            {
//                case MoleType.Standard:
//                    spriteRenderer.sprite = moleHit;
//                    gameManager.AddScore(1); // This will now call the AddScore method properly
//                    audioManager.PlayMoleClickSound();
//                    StopAllCoroutines();
//                    StartCoroutine(QuickHide());
//                    hittable = false;
//                    break;

//                case MoleType.HardHat:
//                    if (lives == 2)
//                    {
//                        spriteRenderer.sprite = moleHatBroken;
//                        lives--;
//                    }
//                    else
//                    {
//                        spriteRenderer.sprite = moleHatHit;
//                        gameManager.AddScore(2);
//                        audioManager.PlayMoleClickSound();
//                        StopAllCoroutines();
//                        StartCoroutine(QuickHide());
//                        hittable = false;
//                    }
//                    break;

//                case MoleType.Bomb:
//                    spriteRenderer.sprite = moleBomb;
//                    gameManager.GameOver(1);
//                    break;

//                default:
//                    break;
//            }
//        }
//    }

//    private void CreateNext()
//    {
//        float random = Random.Range(0f, 1f);
//        if (random < bombRate)
//        {
//            moleType = MoleType.Bomb;
//            spriteRenderer.sprite = moleBomb;
//        }
//        else
//        {
//            random = Random.Range(0f, 1f);
//            if (random < hardRate)
//            {
//                moleType = MoleType.HardHat;
//                spriteRenderer.sprite = moleHardHat;
//                lives = 2;
//            }
//            else
//            {
//                moleType = MoleType.Standard;
//                spriteRenderer.sprite = mole;
//                lives = 1;
//            }
//        }
//        hittable = true;
//    }

//    private void SetLevel(int level)
//    {
//        bombRate = Mathf.Min(level * 0.025f, 0.25f);
//        hardRate = Mathf.Min(level * 0.025f, 1f);
//        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
//        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
//        duration = Random.Range(durationMin, durationMax);
//    }

//    private void Awake()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        boxCollider2D = GetComponent<BoxCollider2D>();
//        boxOffset = boxCollider2D.offset;
//        boxSize = boxCollider2D.size;
//        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
//        boxSizeHidden = new Vector2(boxSize.x, 0f);
//    }

//    public void Activate(int level)
//    {
//        SetLevel(level);
//        CreateNext();
//        StartCoroutine(ShowHide(startPosition, endPosition));
//    }

//    public bool IsHit(Vector3 clickPosition)
//    {
//        Collider2D collider = GetComponent<Collider2D>();
//        return collider != null && collider.OverlapPoint(clickPosition);
//    }

//    public void StopGame()
//    {
//        hittable = false;
//        StopAllCoroutines();
//    }
//}

using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite mole;
    [SerializeField] private Sprite moleHardHat;
    [SerializeField] private Sprite moleHatBroken;
    [SerializeField] private Sprite moleHit;
    [SerializeField] private Sprite moleHatHit;
    [SerializeField] private Sprite moleBomb;

    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    [Header("AudioManager")]
    [SerializeField] private AudioManager audioManager;

    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;
    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    private bool hittable = true;

    public enum MoleType { Standard, HardHat, Bomb };
    [SerializeField] private MoleType moleType;

    private float hardRate = 0.25f;
    private float bombRate = 0f;
    private int lives;
    public int Index { get; set; }

    public bool IsHittable => hittable;

    public MoleType GetMoleType() => moleType;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;
        float elapsed = 0f;

        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        yield return new WaitForSeconds(duration);

        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;

        if (hittable)
        {
            hittable = false;
            gameManager.Missed(Index, moleType != MoleType.Bomb);
        }
    }

    public void Hide()
    {
        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Hide();
        }
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            switch (moleType)
            {
                case MoleType.Standard:
                    spriteRenderer.sprite = moleHit;
                    gameManager.AddScore(1);
                    audioManager.PlayMoleClickSound();
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    hittable = false;
                    break;

                case MoleType.HardHat:
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = moleHatBroken;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = moleHatHit;
                        gameManager.AddScore(2);
                        audioManager.PlayMoleClickSound();
                        StopAllCoroutines();
                        StartCoroutine(QuickHide());
                        hittable = false;
                    }
                    break;

                case MoleType.Bomb:
                    spriteRenderer.sprite = moleBomb;
                    gameManager.GameOver(1);
                    break;

                default:
                    break;
            }
        }
    }

    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if (random < bombRate)
        {
            moleType = MoleType.Bomb;
            spriteRenderer.sprite = moleBomb;
            Debug.Log("Generated: Bomb Mole");
        }
        else
        {
            random = Random.Range(0f, 1f);
            if (random < hardRate)
            {
                moleType = MoleType.HardHat;
                spriteRenderer.sprite = moleHardHat;
                lives = 2;
                Debug.Log("Generated: HardHat Mole");
            }
            else
            {
                moleType = MoleType.Standard;
                spriteRenderer.sprite = mole;
                lives = 1;
                Debug.Log("Generated: Standard Mole");
            }
        }
        hittable = true;
    }

    private void SetLevel(int level)
    {
        bombRate = Mathf.Clamp(level * 0.025f, 0.05f, 0.25f);
        hardRate = Mathf.Clamp(level * 0.1f, 0.1f, 0.9f);
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.5f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.5f, 2f);
        duration = Random.Range(durationMin, durationMax);
        Debug.Log($"Level: {level}, Bomb Rate: {bombRate}, Hard Rate: {hardRate}, Duration: {duration}");
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }

    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    public bool IsHit(Vector3 clickPosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider != null && collider.OverlapPoint(clickPosition);
    }

    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
}
