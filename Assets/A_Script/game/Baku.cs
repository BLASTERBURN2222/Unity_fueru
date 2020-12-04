using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baku : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;

	float alp;
	float sc;

	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		alp = 1.0f;
		sc = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;

		alp -= 0.05f;

		sc -= 0.01f;
		if (sc <= 0.0f)
		{
			sc = 0.01f;
		}
		if (alp <= 0.0f)
		{
			Destroy(this.gameObject);
		}
		else
		{
			ChangeTransparency(alp);
		}

		Vector3 scale = transform.localScale;
		scale.x = sc;
		scale.y = sc;
		transform.localScale = scale;

	}

	void ChangeTransparency(float alpha)
	{
		spriteRenderer.color = new Color(1, 1, 1, alpha);
	}




}
