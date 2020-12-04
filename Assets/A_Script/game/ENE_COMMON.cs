using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENE_COMMON : MonoBehaviour
{

	private int HP;
	private int SCORE;
	private int EXP;
	private int dead_flg;
	private int stage_enemy_minus;
	private int hansha_flg = 0;
	private int utikaesi = 0;

	private GameObject pf_baku;
	private GameObject pf_getscore;
	private GameObject GO_gameMgr;
	private GameObject pf_item;
	private GameObject GO_my;
	private GameObject pf_bossbaku;
	private GameObject pf_eneshot_hansha;

	private int boss_num = 0;

	// Start is called before the first frame update
	void Start()
	{
		dead_flg = 0;
	}

	// Update is called once per frame
	void Update()
	{
	}
	public void set_hp_score_exp(int hpp = 1, int sc = 100, int ex = 1)
	{
		HP = hpp;
		SCORE = sc;
		EXP = ex;
		if ((0 < boss_num) && (boss_num < 100))
		{
			GG.boss_hp = hpp;
			GG.boss_hp_max = hpp;
			GO_gameMgr.gameObject.SendMessage("boss_hp_gage_put", 1);
			GO_gameMgr.gameObject.SendMessage("boss_hp_gage_on", 1);
		}
	}

	public void boss_num_set(int num)
	{
		boss_num = num;
	}

	public void init_zako(GameObject gob = null, int mins = 1,int hansha = 0,int uti = 0)
	{
		GO_my = gob;
		dead_flg = 0;
		GO_gameMgr = GameObject.Find("GO_W_MgrGame");
		pf_item = (GameObject)Resources.Load("ITEM");
		pf_baku = (GameObject)Resources.Load("BAKU");
		pf_getscore = (GameObject)Resources.Load("getpoint");
		stage_enemy_minus = mins;
		pf_bossbaku = (GameObject)Resources.Load("BOSSBAKU");
		pf_eneshot_hansha = (GameObject)Resources.Load("ENESHOT");
		hansha_flg = hansha;
		utikaesi = uti;
	}

	public void damage(int dam = 1)
	{
		if (dead_flg == 1) return;


		{
			HP -= dam;

			if ((0 < boss_num) && (boss_num < 100))
			{
				GG.boss_hp -= dam;
				if (GG.boss_hp < 0)
				{
					GG.boss_hp = 0;
					GO_gameMgr.gameObject.SendMessage("boss_hp_gage_off", 1);
				}
				else
				{
					GO_gameMgr.gameObject.SendMessage("boss_hp_gage_put", 1);
				}
			}
			else{
				if(hansha_flg != 0){
					GameObject obj_eneshot = (GameObject)Instantiate(pf_eneshot_hansha, transform.position, Quaternion.identity);
					obj_eneshot.GetComponent<EneShot>().set_kakudo(270, 0.1f);


				}
			}

			if (HP <= 0)
			{
				dead_flg = 1;
				GG.sound_play("SE_BAKU");

				Vector3 ppp = GO_my.transform.position;
				if ((0 < boss_num) && (boss_num < 100))
				{
					Instantiate(pf_bossbaku, ppp, Quaternion.identity);
					GO_gameMgr.gameObject.SendMessage("game_clear", 1);

					setscore_put(SCORE);
					GO_gameMgr.gameObject.SendMessage("score_add", SCORE);

				}
				else
				{
					Instantiate(pf_baku, ppp, Quaternion.identity);
				}
				int plus_score = SCORE * GG.combo_counter;
				if (plus_score > 100) plus_score = 100;
				int plus_exp = EXP;
				GG.combo_counter++;

				setscore_put(plus_score);
				GO_gameMgr.gameObject.SendMessage("score_add", plus_score);
				GO_gameMgr.gameObject.SendMessage("exp_add", plus_exp);
				GG.item_cnt++;
				if (GG.item_cnt >= 20)
				{
					GG.item_cnt = 0;
					GameObject obj_takara = (GameObject)Instantiate(pf_item, ppp, Quaternion.identity);
					obj_takara.GetComponent<Item>().item_kind_set(get_item_num());
				}

				if (utikaesi != 0)
				{
					GameObject obj_eneshot = (GameObject)Instantiate(pf_eneshot_hansha, transform.position, Quaternion.identity);
					obj_eneshot.GetComponent<EneShot>().set_kakudo(997, 0.05f);
				}
				Destroy(this.gameObject);
				return;
			}
			else
			{
				GG.sound_play("SE_H");
			}


		}



	}

	//2がオノ
	//3がヤリ
	//4が剣
	private int[] item_arr= {
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,3,	
		2,2,2,3,3,3,2,3,4,4,	
		2,3,2,2,3,2,2,3,4,2,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	

		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	

		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	

		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	
		2,3,2,3,2,3,2,3,2,4,	

	};

	private int get_item_num(){
		int nm = GG.stage *10;
		int rn = UnityEngine.Random.Range(0, 10);
		int ret = item_arr[nm + rn];
		return ret;

	}

	void limitup(int up)
	{

	}

	void setscore_put(int sc)
	{
		int tmp = 0;
		List<int> number = new List<int>();
		if (sc == 0)
		{
			number.Add(0);
		}

		while (sc != 0)
		{
			tmp = sc % 10;
			sc = sc / 10;
			number.Add(tmp);
		}

		GameObject getsc;
		Vector3 pos = this.transform.position;
		for (int i = 0; i < number.Count; i++)
		{
			getsc = (GameObject)Instantiate(pf_getscore, pos, Quaternion.identity);
			getsc.GetComponent<getscore>().set_point(number[i]);
			pos.x -= 0.125f;
		}

	}

	public float get_ziki_x(int pos)
	{
		float ppp = -3.28f + (pos * 0.64f);
		return ppp;
	}



}




