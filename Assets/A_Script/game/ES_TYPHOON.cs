using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_TYPHOON : MonoBehaviour
{

	private float xf = 0.0f;
	private float yf = 0.0f;
	private int hp;

	private float vxf = 0.0f;
	private float vyf = 0.0f;
	private int lifetime;

	public Sprite sprite_0;
	public Sprite sprite_1;

	private SpriteRenderer spRenderer;

	private int anime_cnt;

	private int cos_cnt;

	private void Awake()
	{
		hp = 1;
		lifetime = 300;

		anime_cnt = 0;
		spRenderer = GetComponent<SpriteRenderer>();
		cos_cnt = 0;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;

		anime_cnt++;
		anime_cnt &= 15;
		if (anime_cnt < 8)
		{
			spRenderer.sprite = sprite_0;
		}
		else
		{
			spRenderer.sprite = sprite_1;
		}

		Vector3 pos = transform.position;

		pos.x += (Util.CosEx(cos_cnt) * 0.12f);
		pos.y -= 0.05f;
		cos_cnt += 8;
		if (cos_cnt > 359)
		{
			cos_cnt -= 360;
		}

		transform.position = pos;

		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(this.gameObject);
		}
	}

}
