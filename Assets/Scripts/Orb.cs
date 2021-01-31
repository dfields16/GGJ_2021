using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Orb : MonoBehaviour
{
	bool following = false;
	GameObject parent;


	public float pulseSpeed = 1f;
	public float size = 0.75f;

    // Cutscene light stuff
	[SerializeField] Light2D orbLight;
    bool needToGrow = false;
    float innerGoal = 3.88f;
    float outerGoal = 7.12f;
    float intensityGoal = 3.57f;

    private void Start()
    {
        if (!orbLight) orbLight = GetComponent<Light2D>();
    }

    void Update(){
		transform.localScale = Vector3.one * size * (2 + Mathf.Sin(Time.time * pulseSpeed) / 2f);
        if (needToGrow)
        {
            orbLight.pointLightInnerRadius += Time.deltaTime * 2 /innerGoal;
            orbLight.pointLightOuterRadius += Time.deltaTime * 5 /outerGoal;
            orbLight.intensity += Time.deltaTime * 2/intensityGoal;
        }
        CheckBrightness();
        if (orbLight.pointLightInnerRadius >= innerGoal && orbLight.pointLightOuterRadius >= outerGoal && orbLight.intensity >= intensityGoal)
        {
            needToGrow = false;
        }
	}

	void FixedUpdate()
	{
		if (following)
		{
			transform.position = parent.transform.Find("HoldPoint").transform.position;
		}
	}

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "Player")
        {
            parent = collider2D.gameObject;
            following = true;
            parent.GetComponent<PlayerMovement>().holdingOrb = true;
            parent.GetComponent<PlayerMovement>().SetOrb(gameObject);
        }
    }

	public void TriggerCinematicGrow()
    {
        StartCoroutine(CinematicGrow());
    }

    IEnumerator CinematicGrow()
    {
        yield return new WaitForSeconds(2f);
        needToGrow = true;
    }

    private void CheckBrightness()
    {
        if (orbLight.pointLightInnerRadius > innerGoal) orbLight.pointLightInnerRadius = innerGoal;
        if (orbLight.pointLightOuterRadius > outerGoal) orbLight.pointLightOuterRadius = outerGoal;
        if (orbLight.intensity > intensityGoal) orbLight.intensity = intensityGoal;
    }
}
