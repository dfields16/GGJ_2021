using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Moon : MonoBehaviour
{
	Light2D light;
	public float decaySpeed;
	public float growSpeed;
	public float maxLight;

	private bool isDying, isDying2;
	public float deadThreshold;
	public float growThreshold;


	void Start()
	{
		light = GetComponent<Light2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(isDying){
			if(isDying2){
				light.intensity -= Time.deltaTime * growSpeed * 2;
			}else{
				light.intensity += Time.deltaTime * growSpeed;
				if(light.intensity > growThreshold){
					isDying2 = true;
				}
			}
		}else{
			if(light.intensity < deadThreshold){
				isDying = true;
			}else{
				light.intensity -= Time.deltaTime * decaySpeed;
			}
		}
	}
}
