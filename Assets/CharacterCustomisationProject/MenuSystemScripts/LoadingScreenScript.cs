using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenScript : MonoBehaviour
{ 
    [SerializeField]int sceneIndex;
    [SerializeField]float progress = 0;
    [SerializeField]float speed = 20;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI loadingText;
    // Start is called before the first frame update

    AsyncOperation load;
    bool loadOnce = false;
    void Start()
    {
        switch(PlayerPrefs.GetString("LoadTo"))
        {
            case "NewGame":
                sceneIndex = 3;
                break;
            case "LoadGame":
                sceneIndex = 4;
                break;
            case "PlayGame":
                sceneIndex = 4;
                break;
            case "BackToMenu":
                sceneIndex = 0;
                break;
            case "Options":
                sceneIndex = 1;
                break;
        }
        loadingText = FindObjectOfType<TextMeshProUGUI>();
        StartCoroutine("LoadBarLonger");

        
    }

    // Update is called once per frame
    void Update()
    {
        if(progress >= 95 && !loadOnce)
        {
            StopCoroutine("LoadBarLonger");
            load = SceneManager.LoadSceneAsync(sceneIndex);
            loadOnce = true;
        }

        if (loadOnce && load.progress > 0.8f)
        {
            progressBar.fillAmount = 1;
            loadingText.text = "Loading... 100%";
        }

    }

    IEnumerator LoadBarLonger()
    {
       
        while(progress <= 95)
        {
            progress += 10 * speed * Time.deltaTime;
            progressBar.fillAmount = progress / 100;
            loadingText.text = "Loading... " + (int)progress + "%";
            Debug.Log("progress is: " + progress);
            yield return null;
        }
    }
}
