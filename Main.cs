using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using System.IO;
using System.Text;

public class Main : MonoBehaviour {

	public static List<Char> charlist = new List<Char> ();

	public TextAsset ta;

	public int SelectedID;

	// Use this for initialization
	void Awake () {
		Init ();
		SelectedID = 0;
		Debug.Log ("URL is: " +Application.persistentDataPath);
		StartCoroutine(ReadFileOutSide ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init () {
		string text = ReadThisFile(ta);
		//string text = ReadFile("Assets", "CharConfig");
		if (ta == null) {
			for (int j = 0; j < 10; j++) {
				Char cn = new Char ();
				cn.name = j.ToString ();
				charlist.Add (cn);
				Debug.Log(cn.name);
			}
		}else {
			JsonData cljd = JsonMapper.ToObject (text);
			for (int i = 0; i < 10; i++) {
				Char c = new Char ();
				c.SetChar (cljd[i]);
				charlist.Add (c);			
			}	
		}
		
	}

	public void ChangeChar () {
		charlist[SelectedID].ID = int.Parse(gameObject.transform.Find("IDInput").GetComponent<InputField> ().text);
		charlist[SelectedID].name = gameObject.transform.Find("NameInput").GetComponent<InputField> ().text;
		charlist[SelectedID].str = int.Parse(gameObject.transform.Find("Str").GetComponent<InputField> ().text);
		charlist[SelectedID].agi = int.Parse(gameObject.transform.Find("Agi").GetComponent<InputField> ().text);
		charlist[SelectedID].sta = int.Parse(gameObject.transform.Find("Sta").GetComponent<InputField> ().text);
		charlist[SelectedID].wise = int.Parse(gameObject.transform.Find("Wise").GetComponent<InputField> ().text);

		charlist[SelectedID].strGrowth = int.Parse(gameObject.transform.Find("StrGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].agiGrowth = int.Parse(gameObject.transform.Find("AgiGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].staGrowth = int.Parse(gameObject.transform.Find("StaGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].wiseGrowth = int.Parse(gameObject.transform.Find("WiseGrowth").GetComponent<InputField> ().text);

		charlist[SelectedID].strStarGrowth = int.Parse(gameObject.transform.Find("StrStarGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].agiStarGrowth = int.Parse(gameObject.transform.Find("AgiStarGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].staStarGrowth = int.Parse(gameObject.transform.Find("StaStarGrowth").GetComponent<InputField> ().text);
		charlist[SelectedID].wiseStarGrowth = int.Parse(gameObject.transform.Find("WiseStarGrowth").GetComponent<InputField> ().text);
	}

	public void CreateAJson () {
		Char c = new Char ();
		c.name = gameObject.transform.Find("NameInput").Find("Text").GetComponent<Text> ().text;
		c.str = int.Parse(gameObject.transform.Find("Str").Find("Text").GetComponent<Text> ().text);
		c.agi = int.Parse(gameObject.transform.Find("Agi").Find("Text").GetComponent<Text> ().text);
		c.sta = int.Parse(gameObject.transform.Find("Sta").Find("Text").GetComponent<Text> ().text);
		c.wise = int.Parse(gameObject.transform.Find("Wise").Find("Text").GetComponent<Text> ().text);
		charlist.Add (c);
	}

	public void PrintJson () {
		string s = JsonMapper.ToJson (charlist);
		Debug.Log (s);
		WriteFile ("Assets", "CharConfig", s);
		ExportJsonFileOutside ("D:/", "CharConfig", s);
	}

	public void WriteFile (string path, string name, string text) {
		StreamWriter sw;
		FileInfo t = new FileInfo(path + "//" + name +".json");
		if (!t.Exists) {
			sw = t.CreateText();
		} else {
			t.Delete();
			sw = t.CreateText();
			//sw = t.AppendText ();
		}
		sw.WriteLine(text);
		sw.Close();
		sw.Dispose();
	}

	public void ExportJsonFileOutside (string path, string name, string text) {
		StreamWriter sw;
		FileInfo t = new FileInfo(path + name + ".json");
		if (!t.Exists) {
			sw = t.CreateText();
		} else {
			t.Delete();
			sw = t.CreateText();
			//sw = t.AppendText ();
		}
		sw.WriteLine(text);
		sw.Close();
		sw.Dispose();
	}

	public IEnumerator ReadFileOutSide () {
		WWW www = new WWW("file:///E:/CharConfig.json");
		yield return www;
		Debug.Log (www.text);
	}

	public string ReadThisFile (TextAsset ta) {
		
		return ta.text;
	}

	public string ReadFile (string path, string name) {
		StreamReader sr = null;
		try {sr = File.OpenText("file://" + Application.dataPath + "//" +path + "//" + name);}catch(Exception e) {return null;}
		string text = sr.ReadLine();
		sr.Close();
		sr.Dispose();
		return text;
	}

	public static List<Char> GetList () {
		return charlist;
	}

	public void SetBySelected (int ID) {
		this.SelectedID = ID;
		gameObject.transform.Find("IDInput").GetComponent<InputField> ().text = charlist[ID].ID.ToString(); 
		gameObject.transform.Find("NameInput").GetComponent<InputField> ().text = charlist[ID].name;
		gameObject.transform.Find("Str").GetComponent<InputField> ().text = charlist[ID].str.ToString();
		gameObject.transform.Find("Agi").GetComponent<InputField> ().text = charlist[ID].agi.ToString();
		gameObject.transform.Find("Sta").GetComponent<InputField> ().text = charlist[ID].sta.ToString();
		gameObject.transform.Find("Wise").GetComponent<InputField> ().text = charlist[ID].wise.ToString();

		gameObject.transform.Find("StrGrowth").GetComponent<InputField> ().text = charlist[ID].strGrowth.ToString();
		gameObject.transform.Find("AgiGrowth").GetComponent<InputField> ().text = charlist[ID].agiGrowth.ToString();
		gameObject.transform.Find("StaGrowth").GetComponent<InputField> ().text = charlist[ID].staGrowth.ToString();
		gameObject.transform.Find("WiseGrowth").GetComponent<InputField> ().text = charlist[ID].wiseGrowth.ToString();

		gameObject.transform.Find("StrStarGrowth").GetComponent<InputField> ().text = charlist[ID].strStarGrowth.ToString();
		gameObject.transform.Find("AgiStarGrowth").GetComponent<InputField> ().text = charlist[ID].agiStarGrowth.ToString();
		gameObject.transform.Find("StaStarGrowth").GetComponent<InputField> ().text = charlist[ID].staStarGrowth.ToString();
		gameObject.transform.Find("WiseStarGrowth").GetComponent<InputField> ().text = charlist[ID].wiseStarGrowth.ToString();
	}
}
