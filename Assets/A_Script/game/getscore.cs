using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getscore : MonoBehaviour
{
	private int score_p = 0;
	private int cnt = 0;

	public Sprite[] spr;//0-9
	private int color_cnt;

	private SpriteRenderer SpriteRen;
	// Use this for initialization
	void Start()
	{
		color_cnt = 0;
		cnt = 0;
	}

	private Color[] colorarr = new Color[] {
		Color.red,
		Color.yellow,
		Color.green,
		Color.cyan,
		Color.blue,
		Color.magenta,
		Color.white,
	};

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;

		cnt++;
		if((cnt & 7) == 0){
			color_cnt++;
			SpriteRen.color = colorarr[color_cnt % 6];
		}

		Vector3 pos = transform.localPosition;
		pos.y += 0.01f;
		transform.localPosition = pos;


		if (cnt > 60)
		{
			Destroy(this.gameObject);
		}
	}

	public void set_point(int param)
	{
		SpriteRen = GetComponent<SpriteRenderer>();

		if ((param >= 0) && (param <= 9))
		{
			SpriteRen.sprite = spr[param];
		}


	}


}
