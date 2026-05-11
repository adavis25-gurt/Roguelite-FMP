using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    [SerializeField] CanvasGroup CG;

    public async Task<bool> FadeUI()
    {
        while (CG.alpha < 1f)
        {
            CG.alpha += 0.5f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        CG.alpha = 1f;

        return true;
    }
}
