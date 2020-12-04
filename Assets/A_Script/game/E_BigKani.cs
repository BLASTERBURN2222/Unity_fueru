using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BigKani : ENE_COMMON
{



	private int eneshot_cnt;
	private int eneshot_cnt2;
	private int move_cnt;
	private int move_dir;
	private int anime_cnt;

	private GameObject pf_eneshot;
	private GameObject pf_slimemini;

	private GameObject pf_typhoon;
	private int typhoon_cnt;

	private bool eneshot_ok;


	public Sprite spr_1;
	public Sprite spr_2;
	public Sprite spr_3;
	public Sprite spr_4;
	public Sprite spr_5;
	public Sprite spr_6;
	private SpriteRenderer sprenderer;

	private Sprite spr_01;
	private Sprite spr_02;

	private float vxf;

	private int mini_shutugen;
	private int mini_shutugen_cnt;

	private int kani_type;

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



		eneshot_cnt = UnityEngine.Random.Range(120, 300); ;
		move_cnt = 0;
		move_dir = GG.DIR_DOWN;
		anime_cnt = 0;
		eneshot_cnt2 = 0;

		typhoon_cnt = 30;

	}

	public void SetType(int tp){
		kani_type = tp;
	}

	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	// Start is called before the first frame update
	void Start()
	{
		if (kani_type == 0)
		{
			spr_01 = spr_1;
			spr_02 = spr_2;
			vxf = 0.02f;
		}
		else if (kani_type == 1)
		{
			spr_01 = spr_3;
			spr_02 = spr_4;
			vxf = 0.02f;
		}
		else //if (kani_type == 2)
		{
			spr_01 = spr_5;
			spr_02 = spr_6;
			vxf = 0.03f;
		}
		SetSprite(spr_01);

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

		if (move_dir == GG.DIR_DOWN)
		{//下へ
			pos.y -= 0.04f;

			if (pos.y <= 2.0f)
			{
				eneshot_ok = true;
				move_dir = GG.DIR_LEFT;
				eneshot_cnt2 = 0;

			}

		}
		else if (move_dir == GG.DIR_LEFT)
		{//左へ

			pos.x -= vxf;
			if (pos.x < -3.35f)
			{
				move_dir = GG.DIR_RIGHT;
			}

		}
		else if (move_dir== GG.DIR_RIGHT)
		{//右へ
			pos.x += vxf;
			if (pos.x >= 3.35f)
			{
				move_dir= GG.DIR_LEFT;
			}

		}


		anime_cnt++;
		anime_cnt &= 15;
		if (anime_cnt < 7)
		{
			SetSprite(spr_01);
		}
		else
		{
			SetSprite(spr_02);
		}

		transform.position = pos;

		if ((kani_type == 0)|| (kani_type == 2)){
			const int c_base = 60;
			const int pp = 10;
			if (eneshot_ok)
			{

				eneshot_cnt2++;
				if (eneshot_cnt2 == c_base) tamaset(200);
				if (eneshot_cnt2 == (c_base + (pp * 1))) tamaset(210);
				if (eneshot_cnt2 == (c_base + (pp * 2))) tamaset(220);
				if (eneshot_cnt2 == (c_base + (pp * 3))) tamaset(230);
				if (eneshot_cnt2 == (c_base + (pp * 4))) tamaset(240);
				if (eneshot_cnt2 == (c_base + (pp * 5))) tamaset(250);
				if (eneshot_cnt2 == (c_base + (pp * 6))) tamaset(260);
				if (eneshot_cnt2 == (c_base + (pp * 7))) tamaset(270);
				if (eneshot_cnt2 == (c_base + (pp * 8))) tamaset(280);
				if (eneshot_cnt2 == (c_base + (pp * 9))) tamaset(290);
				if (eneshot_cnt2 == (c_base + (pp * 10))) tamaset(300);
				if (eneshot_cnt2 == (c_base + (pp * 11))) tamaset(310);
				if (eneshot_cnt2 == (c_base + (pp * 12))) tamaset(320);
				if (eneshot_cnt2 == (c_base + (pp * 13))) tamaset(330);
				if (eneshot_cnt2 == (c_base + (pp * 14))) tamaset(340);
				if (eneshot_cnt2 == (c_base + (pp * 15))) tamaset(350);
				if (eneshot_cnt2 == (c_base + (pp * 16)))
				{
					eneshot_cnt2 = 0;
				}
			}




		}



		if ((kani_type == 1)||(kani_type == 2))
		{
			eneshot_cnt--;
			if (eneshot_cnt <= 0)
			{
				Vector3 shotpos = transform.position;
				shotpos.z = GG.minislime_z_cnt;
				GG.minislime_z_cnt += 0.0001f;
				if(kani_type == 1){
					eneshot_cnt = 50 + UnityEngine.Random.Range(0, 5);
				}
				else{
					eneshot_cnt = 30 + UnityEngine.Random.Range(0, 10);
				}

				int dr = GG.DIR_LEFT;
				if (UnityEngine.Random.Range(0, 100) < 50)
				{
					dr = GG.DIR_RIGHT;
				}

				GameObject obj_eneshot = (GameObject)Instantiate(pf_slimemini, shotpos, Quaternion.identity);
				obj_eneshot.GetComponent<E_SmallKani>().DirSet(dr);


			}
		}
	}





	void tamaset(int kakudo)
	{
		GameObject obj_eneshot = (GameObject)Instantiate(pf_eneshot, transform.position, Quaternion.identity);
		obj_eneshot.GetComponent<EneShot>().set_kakudo(kakudo, 0.06f);

	}




}
