using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class E_Big_Kani_White : ENE_COMMON
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


	private bool eneshot_ok;

	private int mini_shutugen;
	private int mini_shutugen_cnt;


	private void Awake()
	{
		mini_shutugen = 15;
		mini_shutugen_cnt = 0;
		eneshot_ok = false;

		pf_eneshot = (GameObject)Resources.Load("AWA");

		pf_slimemini = (GameObject)Resources.Load("SmallKani");
		pf_typhoon = (GameObject)Resources.Load("ES_TYPHOON");
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		init_zako(this.gameObject);
		boss_num_set(1);
		set_hp_score_exp(255, 100000, 50);



		eneshot_cnt = 0;
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
				eneshot_ok = true;
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

		int c_base = 60;
		int pp = 10;
		if (eneshot_ok)
		{

			eneshot_cnt++;
			if (eneshot_cnt == c_base) tamaset(200);
			if (eneshot_cnt == (c_base + (pp * 1))) tamaset(210);
			if (eneshot_cnt == (c_base + (pp * 2))) tamaset(220);
			if (eneshot_cnt == (c_base + (pp * 3))) tamaset(230);
			if (eneshot_cnt == (c_base + (pp * 4))) tamaset(240);
			if (eneshot_cnt == (c_base + (pp * 5))) tamaset(250);
			if (eneshot_cnt == (c_base + (pp * 6))) tamaset(260);
			if (eneshot_cnt == (c_base + (pp * 7))) tamaset(270);
			if (eneshot_cnt == (c_base + (pp * 8))) tamaset(280);
			if (eneshot_cnt == (c_base + (pp * 9))) tamaset(290);
			if (eneshot_cnt == (c_base + (pp * 10))) tamaset(300);
			if (eneshot_cnt == (c_base + (pp * 11))) tamaset(310);
			if (eneshot_cnt == (c_base + (pp * 12))) tamaset(320);
			if (eneshot_cnt == (c_base + (pp * 13))) tamaset(330);
			if (eneshot_cnt == (c_base + (pp * 14))) tamaset(340);
			if (eneshot_cnt == (c_base + (pp * 15))) tamaset(350);
			if (eneshot_cnt == (c_base + (pp * 16)))
			{
				eneshot_cnt = 0;
			}
		}



	}





	void tamaset(int kakudo)
	{
		GameObject obj_eneshot = (GameObject)Instantiate(pf_eneshot, transform.position, Quaternion.identity);
		obj_eneshot.GetComponent<EneShot>().set_kakudo(kakudo, 0.06f);

	}







}
