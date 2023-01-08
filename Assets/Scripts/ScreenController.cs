using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenController : MonoBehaviour
{
    private Image _deadImage;
    void OnEnable()
    {
        _deadImage = GetComponent<Image>();
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        for (var i = 0; i < 255; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            _deadImage.tintColor += new Color(0, 0, 0, 1);
        }
    }
}
