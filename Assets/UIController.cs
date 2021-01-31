using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject flashSquare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashWhite(bool fadeToWhite){
        if(fadeToWhite){
            StartCoroutine(FadeWhiteOutSquare());
        }else{
            StartCoroutine(FadeWhiteOutSquare(false));
        }
    }

    public IEnumerator FadeWhiteOutSquare(bool fadeToWhite = true, int fadeSpeed = 5){
        Color objectColor = flashSquare.GetComponent<Image>().color;
        float fadeAmount;

        if(fadeToWhite){
            while(flashSquare.GetComponent<Image>().color.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                flashSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        } else {
            while(flashSquare.GetComponent<Image>().color.a > 0){
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                flashSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        
        yield return new WaitForEndOfFrame();
    }
}
