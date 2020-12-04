using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ziki : MonoBehaviour, IPointerEnterHandler
{

	private int dead_flg;
	private int damage_muteki_time;

	public GameObject pf_myshot;
	public GameObject pf_myAxe;
	public GameObject pf_myYari;
	public GameObject pf_mySword;
	public GameObject pf_shield;
	public GameObject pf_tap_hikari;
	public GameObject pf_Hikari_ItemGet;
	private GameObject GO_gameMgr;

	public Sprite[] ziki_spr;


	private SpriteRenderer sprenderer;

	private int up_cnt;
	private int HP;
	private int shot_timer;
	private int shot_push_cnt;
	private GameObject GO_ItemWindow;

	private GameObject pf_MoveItem;

	private int shot_kind;
	private int shot_tokushu_timer;

	private int anime_cnt;
	private int anime_idx;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (GG.pause == 1) return;


		if (damage_muteki_time > 0) return;

		GG.sound_play("SE_SWITCH1");

		if (GG.window_item != 0){
			Vector3 pos = GO_ItemWindow.gameObject.transform.position;
			GameObject getsc = (GameObject)Instantiate(pf_MoveItem, pos, Quaternion.identity);
			getsc.GetComponent<MoveItem>().set_start_goal(GG.window_item-1,this.gameObject);

			GO_ItemWindow.gameObject.SendMessage("window_item_set", -1);

			GG.window_item = 0;


		}

		Vector3 shieldpos = transform.position;
		shieldpos.y += 0.24f;
		shieldpos.z = GG.shield_z_cnt;
		GG.shield_z_cnt += 0.001f;
		Instantiate(pf_shield, shieldpos, Quaternion.identity);
		shot_timer = 60;

		Instantiate(pf_tap_hikari, transform.position, Quaternion.identity);

		

	}

	private void Awake()
	{
		GO_gameMgr = GameObject.Find("GO_W_MgrGame");
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		GO_ItemWindow = GameObject.Find("ItemWindow");
		pf_MoveItem = (GameObject)Resources.Load("MoveItem");

		up_cnt = 20;
		HP = 1;
		shot_timer = 0;
		shot_push_cnt = 0;
		shot_kind = 0;
		shot_tokushu_timer = 0;
		anime_cnt = 0;
		anime_idx = 0;

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

		if (damage_muteki_time > 0)
		{
			damage_muteki_time--;

		}
		else
		{

			zikimove();
		}

	}

	void zikimove()
	{

		Vector3 pos = transform.position;

		if (GG.GameMode != GG.GAMEMODE_GAME) return;


		if (up_cnt > 0)
		{
			up_cnt--;
			pos.y += 0.1f;

		}
		else
		{

			if(shot_tokushu_timer > 0){
				shot_tokushu_timer--;
				if(shot_tokushu_timer <= 0){
					shot_tokushu_timer = 0;
					shot_kind = 0;
				}
			}


			if (shot_timer > 0)
			{
				shot_timer--;
				shot_push_cnt++;
				shot_push_cnt &= 7;
				if (shot_push_cnt == 0)
				{
					shot_set();
				}
			}
		}


		anime_cnt++;
		anime_cnt &= 15;
		if(anime_cnt == 0){
			anime_idx++;
			if(anime_idx >= ziki_spr.Length){
				anime_idx = 0;
			}
			SetSprite(ziki_spr[anime_idx]);
		}


		transform.position = pos;


	}


	void shot_set(){
		Vector3 pp = transform.position;

		if (shot_kind == 0)
		{
			GameObject obj_zikishot = (GameObject)Instantiate(pf_myshot, pp, Quaternion.Euler(0, 0, 45));
		}
		else if (shot_kind == 1)
		{
			GameObject obj_zikishot = (GameObject)Instantiate(pf_myAxe, pp, Quaternion.Euler(0, 0, 45));
		}
		else if (shot_kind == 2)
		{
			GameObject obj_zikishot = (GameObject)Instantiate(pf_myAxe, pp, Quaternion.Euler(0, 0, 45));
		}
		else if (shot_kind == 3)
		{
			GameObject obj_zikishot = Instantiate(pf_myYari, pp, Quaternion.Euler(0, 0, 30));
		}
		else if (shot_kind == 4)
		{
			GameObject obj_zikishot = (GameObject)Instantiate(pf_mySword, pp, Quaternion.Euler(0, 0, 45));
		}



	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (GG.GameMode != GG.GAMEMODE_GAME) return;
		if (damage_muteki_time > 0) return;

		if (dead_flg == 1) return;

		if ((other.gameObject.tag == "TAG_ENEMY") ||
			(other.gameObject.tag == "TAG_ENESHOT"))
		{

			GG.sound_play("SE_MISS");

			if (GG.debug_muteki_mode == 0){
				HP--;
			}

			Destroy(other.gameObject);

			if (HP <= 0){
				dead_flg = 1;

				GO_gameMgr.gameObject.SendMessage("alldeadcheck", 1);

				Destroy(this.gameObject);
			}
			else
			{
				damage_muteki_time = 60;
			}

		}

		if (other.gameObject.tag == "TAG_MOVEITEM")
		{
			int itemnum = other.GetComponent<MoveItem>().get_itemkind();
			shot_kind = itemnum;
			shot_tokushu_timer = 120;
			other.GetComponent<MoveItem>().kieru();

			Instantiate(pf_Hikari_ItemGet, transform.position, Quaternion.identity);
			shot_timer = 60;

		}



	}


}
