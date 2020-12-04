using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SmallSnake : ENE_COMMON
{





	public Sprite spr_a_1;
	public Sprite spr_a_2;
	public Sprite spr_b_1;
	public Sprite spr_b_2;
	public Sprite spr_c_1;
	public Sprite spr_c_2;
	private SpriteRenderer sprenderer;

	private float vxf = 0.04f;

	private int lifetime;
	private int anime_cnt;

	private int type = 0;

	private void Awake()
	{
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		init_zako(this.gameObject, 0,1);

		set_hp_score_exp(1000, 30, 0);




		lifetime = 1100;

	}

	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	public void set_type(int tp,int dir){
		type = tp;
		if(dir == 4){
			vxf = -0.04f;
		}
		else{
			vxf = 0.04f;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		anime_cnt = 0;
		if (type == 0)
		{
			SetSprite(spr_a_1);
		}
		else if (type == 1)
		{
			SetSprite(spr_b_1);
		}
		else
		{
			SetSprite(spr_c_1);
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;
		EneMove();
		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(this.gameObject);
		}

	}


	private void EneMove()
	{

		if (vxf > 0)
		{
			transform.Rotate(new Vector3(0, 0, -4.0f));
		}
		else{
			transform.Rotate(new Vector3(0, 0, 4.0f));

		}
		Vector3 pos = transform.position;

		pos.x += vxf;

		anime_cnt++;
		anime_cnt &= 15;
		if (anime_cnt == 0)
		{
			if (type == 0) SetSprite(spr_a_1);
			else if (type == 1) SetSprite(spr_b_1);
			else SetSprite(spr_c_1);
		}
		else if(anime_cnt == 8)
		{
			if (type == 0) SetSprite(spr_a_2);
			else if (type == 1) SetSprite(spr_b_2);
			else SetSprite(spr_c_2);

		}


		transform.position = pos;
	}





}
