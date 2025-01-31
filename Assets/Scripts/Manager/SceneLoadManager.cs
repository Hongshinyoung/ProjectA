using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneName
{
    Intro,
    InGame
}

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    private UILoadingScreen loadingUI;

    protected override void Awake()
    {
        base.Awake();
        loadingUI = UIManager.Instance.Show<UILoadingScreen>();
        loadingUI.gameObject.SetActive(false);
        progressBar = loadingUI.GetComponentInChildren<Slider>();
        progressText = loadingUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void LoadScene(string sceneName, Action callback = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, callback));
    }

    private IEnumerator LoadSceneAsync(string sceneName, Action callback = null)
    {

        loadingUI.gameObject.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float lerpProgress = 0f;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            lerpProgress = Mathf.Lerp(lerpProgress, progress, Time.deltaTime * 5f);
            progressBar.value = progress;
            progressText.text = "Loading..." + (progress * 100f).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        callback?.Invoke();

        if (loadingUI != null)
        {
            loadingUI.gameObject.SetActive(false);
        }
    }
}
