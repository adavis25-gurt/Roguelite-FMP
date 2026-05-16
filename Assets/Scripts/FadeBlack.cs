using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeBlack : MonoBehaviour
{
    [SerializeField] CanvasGroup CG;

    public async void FadeIn(string sceneName)
    {
        Time.timeScale = 1f;
        var CG = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        print(CG.transform.parent);

        while (CG.alpha < 1f)
        {
            print(CG.alpha);
            CG.alpha += 0.75f * Time.deltaTime;
            await Awaitable.NextFrameAsync();   
            print(Time.timeScale);  
        }

        CG.alpha = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public async void FadeOut()
    {
        Time.timeScale = 1f;
        var CG = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
        print(CG.transform.parent);

        while (CG.alpha > 0f)
        {
            print(CG.alpha);
            CG.alpha -= 0.25f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
            print(Time.timeScale);
        }

        CG.alpha = 0f;
    }
}
