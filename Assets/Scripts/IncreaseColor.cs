using UnityEngine;
using UnityEngine.UI;

public class IncreaseColor : MonoBehaviour
{
    public RawImage Screen;
    float amount = 0f;

    public void Increase(float amount)
    {
        Screen.material.SetFloat("_ColorAmount", amount);
        print("Its working apparently");
    }
}
