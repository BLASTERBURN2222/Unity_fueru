using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SlimeMini : ENE_COMMON
{

	private int eneshot_cnt;
	private int move_cnt;
	private int move_mode;
	private int move_dir;
	private int anime_cnt;

	public Sprite spr_1;
	public Sprite spr_2;
	public Sprite spr_1_st2;
	public Sprite spr_2_st2;
	public Sprite spr_1_st3;
	public Sprite spr_2_st3;
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
		init_zako(this.gameObject, 0);


		if (GG.stage == 1)
		{
			set_hp_score_exp(1, 10, 0);
			SetSprite(spr_1);
		}
		if (GG.stage == 2)
		{
			set_hp_score_exp(1, 10, 0);
			SetSprite(spr_1_st2);
		}
		if (GG.stage == 3)
		{
			set_hp_score_exp(2, 10, 0);
			SetSprite(spr_1_st3);
		}


		eneshot_cnt = UnityEngine.Random.Range(120, 300); ;
		move_cnt = 0;
		move_mode = 0;
		move_dir = 4;
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
		int fff = UnityEngine.Random.Range(-120, 120);
		Vector2 force = new Vector2(fff, 1 * jumpPower);
		m_rigidbody2D.AddForce(force);

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
		Vector3 pos = transform.position;

		if (move_mode == 0)
		{
			move_cnt++;
			if (move_cnt == 70)
			{
				move_mode = 1;
				vxf = 0.0f;
				vyf = -0.01f;
				m_rigidbody2D.gravityScale = 0;
				m_rigidbody2D.velocity = new Vector2(0.0f, 0.0f);

			}
		}
		else if (move_mode == 1)
		{
			anime_cnt++;
			anime_cnt &= 15;
			if (anime_cnt < 7)
			{
				if (GG.stage == 1) SetSprite(spr_1);
				if (GG.stage == 2) SetSprite(spr_1_st2);
				if (GG.stage == 3) SetSprite(spr_1_st3);
			}
			else
			{
				if (GG.stage == 1) SetSprite(spr_2);
				if (GG.stage == 2) SetSprite(spr_2_st2);
				if (GG.stage == 3) SetSprite(spr_2_st3);

			}
			pos.x += vxf;
			pos.y += vyf;

		}
		transform.position = pos;
	}











}
