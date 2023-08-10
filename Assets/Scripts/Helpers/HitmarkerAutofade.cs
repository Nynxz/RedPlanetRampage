using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitmarkerAutofade : MonoBehaviour
{
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }
    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
        if(image.color.a <= 0) {
            Destroy(gameObject);
        }
    }
}
