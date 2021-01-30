using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
	public GameObject prefab;

	public float maxSpawnTime = 2f;
	private float currSpawnTime = 2f;

	void Start(){
		currSpawnTime += Random.Range(0f, maxSpawnTime);
	}

	void FixedUpdate()
	{
		if (currSpawnTime < 0)
		{
			if (Random.Range(0, 10.0f) < 9.99f)
			{
				int max = Random.Range(1, 2);
				for (int i = 0; i < max; i++)
				{
					Vector2 pos = transform.position;
					pos.x += Random.Range(-2.0f, 2);
					pos.y += Random.Range(-2.0f, 2);

					GameObject.Instantiate(prefab, pos, Quaternion.identity, transform);
				}
			}
			currSpawnTime = maxSpawnTime;
		}
		currSpawnTime -= Time.fixedDeltaTime;
	}
}
