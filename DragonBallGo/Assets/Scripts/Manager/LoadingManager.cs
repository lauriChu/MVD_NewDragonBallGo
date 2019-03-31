using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    // Instance
    public static LoadingManager Shared = null;
    
    // Minimum time for show loading
    private const float MIN_TIME_TO_SHOW = 18f;

    // Elapsed time since loading shows
    private float timeElapsed;

    // Flag for detect if it's loading next Scene or not
    private bool isLoading = false;

    // Flag whether the fade out animation was triggered.
    private bool didTriggerFadeOutAnimation;

    // The reference to the current loading operation running in the background:
    private AsyncOperation currentLoadingOperation;

    // The animator of the loading UI
    private Animator animator;

    // Initialize singletone
    void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
            // Don't destroy the loading screen while switching scenes:
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        animator = GetComponent<Animator>();

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            // If the loading is complete and the fade out animation has not been triggered yet, trigger it:
            if (currentLoadingOperation.isDone && !didTriggerFadeOutAnimation)
            {
                animator.SetTrigger("Hide");
                didTriggerFadeOutAnimation = true;
            }
            else
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= MIN_TIME_TO_SHOW)
                {
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }

    // Show the loading screen.
    public void Show(AsyncOperation loadingOperation)
    {
        // Enable the loading screen:
        gameObject.SetActive(true);

        // Store the reference:
        currentLoadingOperation = loadingOperation;

        // Stop the loading operation from finishing, even if it technically did:
        currentLoadingOperation.allowSceneActivation = false;

        // Reset the time elapsed:
        timeElapsed = 0f;

        // Play the fade in animation:
        animator.SetTrigger("Show");

        // Reset the fade out animation flag:
        didTriggerFadeOutAnimation = false;

        isLoading = true;
    }

    // Call this to hide it:
    public void Hide()
    {
        // Disable the loading screen:
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        isLoading = false;
    }
}
