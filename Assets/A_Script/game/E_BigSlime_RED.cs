﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BigSlime_RED : ENE_COMMON
{

	private int eneshot_cnt;
	private int eneshot_cnt2;
	private int move_cnt;
	private int move_mode;
	private int move_dir;
	private int anime_cnt;

	private GameObject pf_eneshot;
	private GameObject pf_slimemini;

	private GameObject pf_typhoon;
	private int typhoon_cnt;



	public Sprite spr_1;
	public Sprite spr_2;
	private SpriteRenderer sprenderer;


	private int mini_shutugen;
	private int mini_shutugen_cnt;


	private void Awake()
	{
		mini_shutugen = 15;
		mini_shutugen_cnt = 0;

		pf_eneshot = (GameObject)Resources.Load("ENESHOT");

		pf_slimemini = (GameObject)Resources.Load("MiniSlime_Green");
		pf_typhoon = (GameObject)Resources.Load("ES_TYPHOON");
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		init_zako(this.gameObject);
		boss_num_set(1);
		set_hp_score_exp(255, 100000, 50);



		eneshot_cnt = UnityEngine.Random.Range(120, 300); ;
		move_cnt = 0;
		move_dir = 4;
		anime_cnt = 0;
		eneshot_cnt2 = 0;

		typhoon_cnt = 30;

	}

	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;
		EneMove();

	}

	private void EneMove()
	{
		Vector3 pos = transform.position;

		if (move_mode == 0)
		{//下へ
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


			pos.y -= 0.04f;

			if (pos.y <= 2.0f)
			{
				move_mode = 1;
				move_cnt = 0;
				eneshot_cnt2 = 1;

			}

		}
		else if (move_mode == 1)
		{//左へ
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

			move_dir = 4;
			pos.x -= 0.01f;
			move_cnt++;
			if (move_cnt >= 32)
			{
				move_cnt = 0;
				move_mode = 2;
			}

		}
		else if (move_mode == 2)
		{//右へ
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
			move_dir = 6;
			pos.x += 0.01f;
			move_cnt++;
			if (move_cnt >= 32)
			{
				move_cnt = 0;
				move_mode = 1;
			}

		}
		transform.position = pos;

		eneshot_cnt--;
		if (eneshot_cnt <= 0)
		{
			Vector3 shotpos = transform.position;
			shotpos.z = GG.minislime_z_cnt;
			GG.minislime_z_cnt += 0.0001f;
			eneshot_cnt = mini_shutugen + UnityEngine.Random.Range(0, 2);
			mini_shutugen_cnt++;
			if (mini_shutugen_cnt > 100)
			{
				mini_shutugen--;
				if (mini_shutugen < 2) mini_shutugen = 2;
			}


			GameObject obj_eneshot = (GameObject)Instantiate(pf_slimemini, shotpos, Quaternion.identity);
		}

		if (eneshot_cnt2 != 0)
		{
			eneshot_cnt2++;

			if ((eneshot_cnt2 == 96) ||
			   (eneshot_cnt2 == 112) ||
			   (eneshot_cnt2 == 128)
			   )
			{
				tamaset(0);
				tamaset(45);
				tamaset(90);
				tamaset(135);
				tamaset(180);
				tamaset(225);
				tamaset(270);
				tamaset(315);

				tamaset(997);

			}

			if (eneshot_cnt2 == 365)
			{
				eneshot_cnt2 = 1;
			}


		}

	}





	void tamaset(int kakudo)
	{
		GameObject obj_eneshot = (GameObject)Instantiate(pf_eneshot, transform.position, Quaternion.identity);
		obj_eneshot.GetComponent<EneShot>().set_kakudo(kakudo, 0.06f);

	}



}
