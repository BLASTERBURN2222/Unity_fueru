using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundsound : MonoBehaviour
{
	/* ACB file name (CueSheet name) */
	public string cueSheetName = "CueSheet_0";

	private CriAtomSource atomSourceMusic;
	static private soundsound instance = null;

	private int volset = 0;

	static private CriAtomExPlayback[] cpb = new CriAtomExPlayback[128];

	void Awake()
	{
		Sound_init();
		if (instance != null)
		{
			/*JP
			 * 多重生成を回避するため, 後から作られたSoundManagerは自分自身を破棄する.
			 * ただし, SoundManagerをコンポーネントにもつGameObject自体は破棄されない. 
			 * そのため, Unityエディタのヒエラルキー上には, 同じ名前のGameObjectが複数存在することになる
			 */
			/*EN
			 * To prevent multiple generation, the SoundManager created later destroys itself.
			 * However, the GameObject with the SoundManager as a component is not destroyed. 
			 * Therefore, in the hierarchy of Unity Editor, multiple GameObjects of the same name exist. 
			 */
			GameObject.Destroy(this);
			return;
		}

		/* Create the CriAtomSource for BGM. */
		atomSourceMusic = gameObject.AddComponent<CriAtomSource>();
		atomSourceMusic.cueSheet = cueSheetName;//	

		/* Do not destroy the SoundManger when scenes are switched. */
		GameObject.DontDestroyOnLoad(this.gameObject);
		instance = this;
		volumeset(GG.SoundVolume);

	}


	static public void PlayCueId(int cueId)
	{
		cpb[cueId] = instance.atomSourceMusic.Play(cueId);
	}


	public static List<string> soundlst = new List<string>();
	public static List<string> soundlst_old = new List<string>();

	public static void Sound_init()
	{
		soundlst.Clear();
		soundlst_old.Clear();
	}

	public static void sound_set(string oto)
	{
		if (soundlst.Contains(oto))
		{
		}
		else
		{
			soundlst.Add(oto);
		}
	}

	public static void sound_play()
	{
		{
			foreach (string soundname in soundlst)
			{
				if (soundlst_old.Contains(soundname))
				{
				}
				else
				{
					playsnd2(soundname);
				}
			}
		}
		soundlst_old.Clear();
		soundlst_old = new List<string>(soundlst);
		soundlst.Clear();
	}


	//modeが1だと止める（BGMを止める)
	static public void playsnd2(string st, int mode = 0)
	{
		string[] ary1 = new string[] {

"SE_6",
"SE_CLEAR",
"SE_D",
"SE_JUMP",
"SE_MISS",
"SE_START",
"SE_Q_SEIKAI",
"SE_WIN",
"SE_LOSE",
"SE_STAGE_START",
"SE_STAGE_END",
"SE_Q_X",
"SE_PLUS",
"SE_MINUS",
"SE_GAME_CLEAR",
"SE_GAME_OVER",
"SE_QUESTION",
"SE_QUIZTIMER",
"SE_SWITCH1",
"BGM_GAME",
"SE_1",
"SE_2",
"SE_3",
"SE_4",
"SE_5",
"SE_E",
"SE_F",
"SE_G",
"SE_H",


};

		int playno = 9999;

		for (int i = 0; i < ary1.Length; i++)
		{
			if (ary1[i].Equals(st))
			{
				playno = i;
				break;
			}
		}

		if (playno != 9999)
		{
			if (mode == 0)
			{
				PlayCueId(playno);
			}
			else
			{
				StopCueId(playno);

			}
		}
		else
		{
			Debug.Log("音が無い");
		}
	}


	//止める
	static public void StopCueId(int cueId)
	{
		cpb[cueId].Stop();
	}


	void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	static public void volumeset(float atai)
	{
		if (instance != null)
		{
			instance.atomSourceMusic.volume = atai;
		}
		else{
		}
	}

	static public void OnPitchSliderChanged(int atai)
	{
		if (instance != null)
		{
			instance.atomSourceMusic.pitch = atai;
		}
		else{
		}
	}


}



