using UnityEngine;
using UnityEngine.UI;

// Used to fade the alpha of an image to 0, then destroy the gameobject this script is attached to
public class HitmarkerAutofade : MonoBehaviour {

    private Image image;

    protected void Awake() {
        image = GetComponent<Image>();
    }
    protected void Update() {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
        if (image.color.a <= 0) {
            Destroy(gameObject);
        }
    }
}
