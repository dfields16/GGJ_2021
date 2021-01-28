using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PerspectiveSwitcher : MonoBehaviour
{
	private Matrix4x4 ortho, perspective;
	public float near = .3f;
	public float far = 1000f;
	public float orthographicSize = 5f;
	private float aspect;
	public bool shouldSwitch = true;

	private bool orthoOn;

	private Camera cam;


	public Transform orthoTransform, perspTransform;


	void Start()
	{

		aspect = (float)Screen.width / (float)Screen.height;
		ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
		// perspective = Matrix4x4.Perspective(fov, aspect, near, far);
		perspective = Camera.main.projectionMatrix;
		orthoOn = false;
		cam = GetComponent<Camera>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			orthoOn = !orthoOn;
			if (orthoOn)
			{
				cam.transform.SetParent(orthoTransform);
				BlendToMatrix(cam, ortho, .75f);
				BlendToParent(cam, 4f);
			}
			else
			{
				cam.transform.SetParent(perspTransform);
				BlendToMatrix(cam, perspective, .75f);
				BlendToParent(cam, 4f);
			}
		}
	}

	private Coroutine BlendToMatrix(Camera camera, Matrix4x4 targetMatrix, float duration)
	{
		StopAllCoroutines();
		return StartCoroutine(MatrixLerpFromTo(camera, camera.projectionMatrix, targetMatrix, duration));
	}
	private Coroutine BlendToParent(Camera camera, float duration)
	{
		return StartCoroutine(TransformFromToParent(camera.transform, duration));
	}

	private static IEnumerator TransformFromToParent(Transform t, float duration)
	{
		float startTime = Time.time;
		while (Time.time - startTime < duration)
		{
			t.position = Vector3.Lerp(t.position, t.parent.position, (Time.time - startTime) / duration);
			t.rotation = Quaternion.Lerp(t.rotation, t.parent.rotation, (Time.time - startTime) / duration);
			yield return 1;
		}
		t.position = t.parent.position;
		t.rotation = t.parent.rotation;
	}

	private static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
	{
		Matrix4x4 ret = new Matrix4x4();
		for (int i = 0; i < 16; i++)
			ret[i] = Mathf.Lerp(from[i], to[i], time);
		return ret;
	}

	private static IEnumerator MatrixLerpFromTo(Camera camera, Matrix4x4 src, Matrix4x4 dest, float duration)
	{
		float startTime = Time.time;
		while (Time.time - startTime < duration)
		{
			camera.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
			yield return 1;
		}
		camera.projectionMatrix = dest;
	}
}