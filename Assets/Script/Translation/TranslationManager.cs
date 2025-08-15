using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranslationManager : MonoBehaviour
{
    [SceneName]
    public string startSceneName = string.Empty;
    private CanvasGroup fadeCanvesGroup;
    public float fadeDuratin;
    private bool isFade;
    private void Start()
    {
        StartCoroutine(LoadSceneSetActive(startSceneName));
        fadeCanvesGroup = FindObjectOfType<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventHandler.TranslationEvent += OnTranslationEvent;
    }
    private void OnDisable()
    {
        EventHandler.TranslationEvent -= OnTranslationEvent;
    }

    private void OnTranslationEvent(string sceneToGo, Vector3 targetPos)
    {
        if(!isFade)
        StartCoroutine(Translation(sceneToGo,targetPos));
    }

    private IEnumerator Translation(string sceneName,Vector3 targetPos)
    {
        EventHandler.CallBeforeSceneUnLoadEvent();
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
              
        yield return LoadSceneSetActive(sceneName);
        EventHandler.CallMoveToPosition(targetPos);
        EventHandler.CallAfterSceneUnLoadEvent();
        yield return Fade(0);
       
    }

    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvesGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvesGroup.alpha - targetAlpha) / fadeDuratin;

        while(!Mathf.Approximately(fadeCanvesGroup.alpha, targetAlpha))
        {
            fadeCanvesGroup.alpha = Mathf.MoveTowards(fadeCanvesGroup.alpha, targetAlpha, speed*Time.deltaTime);
            yield return null;
        }
        fadeCanvesGroup.blocksRaycasts = false;
        isFade = false;
    }



}
