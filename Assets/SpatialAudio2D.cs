using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpatialAudio2D : MonoBehaviour
{
	public AudioSource audioSource;
	public GameObject player;

	public float listenDistance;

	private bool isDying = false;
	void Start()
	{
		if (!audioSource)
		{
			audioSource = GetComponent<AudioSource>();
		}
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(!isDying) audioSource.volume = 1 - (Vector2.Distance(transform.position, player.transform.position) / listenDistance);
	}

	public void Remove()
	{
		isDying = true;
		StartCoroutine(FadeAudio());
	}

	private IEnumerator FadeAudio()
	{
		while(audioSource.volume > 0){
			audioSource.volume -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		Destroy(gameObject);
	}
}
