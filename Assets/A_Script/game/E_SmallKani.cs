using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SmallKani : ENE_COMMON
{


	private int eneshot_cnt;
	private int move_cnt;
	private int move_mode;
	private int move_dir = 4;
	private int anime_cnt;

	public Sprite spr_1;
	public Sprite spr_2;
	private SpriteRenderer sprenderer;

	private Rigidbody2D m_rigidbody2D;
	private float jumpPower = 300f;


	private float vxf;
	private float vyf;

	private int lifetime;

	private void Awake()
	{
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		init_zako(this.gameObject, 0,0,1);


		set_hp_score_exp(1, 10, 0);
		SetSprite(spr_1);


		eneshot_cnt = UnityEngine.Random.Range(120, 300); ;
		move_cnt = 0;
		move_mode = 0;
		anime_cnt = 0;
		lifetime = 1100;

	}


	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	// Start is called before the first frame update
	void Start()
	{
		vxf = 0.04f;
		vyf = 0.01f;
		move_mode = 1;
		move_cnt = 120;
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

	public void DirSet(int dr){
		move_dir = dr;
	}

	private void EneMove()
	{
		Vector3 pos = transform.position;

		if (move_dir == 6)
		{
			pos.x += vxf;
			pos.y -= vyf;
			move_cnt--;
			if(pos.x > 3.5f){ 
				move_cnt = 120;
				move_dir = 4;
			}
		}
		else
		{
			pos.x -= vxf;
			pos.y -= vyf;
			move_cnt--;
			if (pos.x < -3.5f)
			{
				move_cnt = 120;
				move_dir = 6;
			}

		}

		anime_cnt++;
		anime_cnt &= 15;
		if (anime_cnt < 7)
		{
			SetSprite(spr_1);
		}
		else
		{
			SetSprite(spr_2);

		}


		transform.position = pos;
	}







}
