using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG : MonoBehaviour
{

	private const string MAGICNUMBER = "202008";
	public static int hiscore = 0;
	public static int score = 0;
	public static int stage = 1;    //ステージ
	public static int stage_max = 6;//最終ステージ
	public static int hp = 3;
	public static int hp_init = 3;
	public static int pause = 0;//1でポーズ中



	public static int debug_muteki_mode = 0;

	public static int question_mode = 0;
	public static int game_time = 60;

	public static int GameMode = 0;
	public static int GameModeNext = 99;
	public const int GAMEMODE_TITLE = 0;
	public const int GAMEMODE_GAME = 1;
	public const int GAMEMODE_OVER = 2;
	public const int GAMEMODE_CLEAR = 3;

	public static int nannido = 0;
	public const int SEIGENJIKAN = 60;
	public const int SEIGENJIKAN2 = 120;
	public const int SEIGENJIKAN3 = 180;

	public const int SEIGENJIKAN_NASI = 0;

	public static int first_read = 0;

	public static int all_balloon_count = 0;

	public static float shield_z_cnt = 0.0f;
	public static float minislime_z_cnt = 0.0f;

	public static int combo_counter;


	public static int boss_hp;
	public static int boss_hp_max;
	public static int item_cnt = 0;
	public static int window_item = 0;
	public static float SoundVolume = 0.5f;

	public const int DIR_RIGHT = 6;
	public const int DIR_LEFT = 4;
	public const int DIR_UP = 8;
	public const int DIR_DOWN = 2;
	public const int DIR_X = 0;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	/// <summary>
	/// 
	/// </summary>

	//---------------------------------------
	// サウンド
	//---------------------------------------

	//--  ADX2LEで使う
	public static int bgm_number = 0;

	//--


	public static int sound_load = 0;

	public static int sound_off = 0;//1で音が出ない。開発用

	//ADX2LE
	public static void sound_play(string st1)
	{
		//SE_BAKUがなかったので変換する
		if(st1 == "SE_BAKU"){
			st1 = "SE_D";
		}

		soundsound.sound_set(st1);
	}



	public static void data_save()
	{
		PlayerPrefs.SetString("MAGICNUMB", MAGICNUMBER);

		string hiscore_long_str = hiscore.ToString();
		PlayerPrefs.SetString("HISCORE", hiscore_long_str);
		PlayerPrefs.SetFloat("SOUNDV", SoundVolume);

		PlayerPrefs.Save();
	}

	public static void data_load()
	{

		string magicnum = PlayerPrefs.GetString("MAGICNUMB", "");
		if (magicnum != MAGICNUMBER)
		{
			data_init();
		}
		else
		{
			string lll = PlayerPrefs.GetString("HISCORE");
			hiscore = int.Parse(lll);
			SoundVolume = PlayerPrefs.GetFloat("SOUNDV", 0.5f);
		}


	}

	public static void data_init()
	{
		hiscore = 0;
		SoundVolume = 0.5f;
	}

}
