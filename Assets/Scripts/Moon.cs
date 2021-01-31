using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Moon : MonoBehaviour
{
	Light2D light2D;
	public float decaySpeed;
	public float growSpeed;
	public float maxLight;

	private bool isDying, isDying2;
	public float deadThreshold;
	public float growThreshold;
	public GameMenu gameMenu;



	void Start()
	{
		light2D = GetComponent<Light2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(isDying){
			if(isDying2){
				light2D.intensity -= Time.deltaTime * growSpeed * 2;
			}else{
				light2D.intensity += Time.deltaTime * growSpeed;
				if(light2D.intensity > growThreshold){
					isDying2 = true;
				}
			}
		}else{
			if(light2D.intensity < deadThreshold){
				isDying = true;
			}else{
				light2D.intensity -= Time.deltaTime * decaySpeed;
			}
		}

		if(light2D.intensity <= deadThreshold && isDying2){
			// end game
			GameObject.Instantiate(gameMenu.gameObject, Vector3.zero, Quaternion.identity);
			GameMenu.menu.isPlayerDead(true);
		}
	}
}
