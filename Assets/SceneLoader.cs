using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider bar;

    void Start()
    {
        bar = GameObject.Find("Canvas").transform.Find("ProgressBar").GetComponent<Slider>();
    }

    public void Load(){
        bar.gameObject.SetActive(true);
        StartCoroutine(GetSceneLoadProgress(1));
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float progress = 0;
    public IEnumerator GetSceneLoadProgress(int id){
        yield return new WaitForSeconds(.75f);

        scenesLoading.Add(SceneManager.LoadSceneAsync(id, LoadSceneMode.Single));
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                progress = 0;
                foreach (AsyncOperation op in scenesLoading)
                {
                    print(op);
                    progress += op.progress;
                }
                progress *= 100/scenesLoading.Count;
                bar.value = progress;
                yield return null;
            }
        }
        
    }
}
