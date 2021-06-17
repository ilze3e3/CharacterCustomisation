using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    float maxTimeDelay = 5;
    float timeDelay = 5;
    int sceneIndex;

    [SerializeField] Image progressBar;
    // Start is called before the first frame update
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        while(timeDelay > 0)
        {
            timeDelay -= 1 * Time.deltaTime;

            progressBar.fillAmount = (maxTimeDelay - timeDelay) / maxTimeDelay;
        }

        if(timeDelay <= 0)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

    }
}
