using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;


public class GameBoard: MonoBehaviour{
	public int m_size;
	 GameObject m_puzzlePiece;
	public GameObject[] puzzlepieces;
	public int m_randomPasses;
	public GameObject puzzlepanel;
	public GameObject antrenmanPanel;
	public GameObject menuPanel;
	public GameObject oyunPaneli;
	public GameObject pausePanel;
	public GameObject tutorialpanel;
	public GameObject winPanel;
	public GameObject losePanel;
	public GameObject levelbasitpanel;
	public GameObject levelortapanel;
	public GameObject levelzorpanel;
	public GameObject storePanel;
	public GameObject settingsPanel;
	public GameObject infoPanel;
	int index;

	//kazandin text'i
	public Text sayitext;
	//kaybettin panel text'i
	public Text sayitext1;
	float puan=5.0f;
	//
	int kontrol;



	public void Win ()
	{
		Time.timeScale = 0;
		sayitext.text = GameObject.Find ("GameManager").GetComponent<countUp> ().zamanlayici.text;
		winPanel.SetActive (true);
		Debug.Log ("Kazandın");

	}

	private PuzzleSection[,] m_puzzle;
	private PuzzleSection m_puzzleSelection;



	void Awake(){
		index = Random.Range (0, puzzlepieces.Length);
		m_puzzlePiece = puzzlepieces [index];
		
		m_puzzlePiece.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width/1.1f, Screen.height/1.4f);
		//PuzzleSection yeri değişecek
		m_puzzlePiece.GetComponent<RectTransform>().pivot=new Vector2(-0.15f,-0.55f);

	}


	void Start(){
		
		//Time.timeScale = 0;
		GameObject temp;
		m_puzzle = new PuzzleSection[m_size, m_size];

		for (int i = 0; i < m_size; i++) {
			for (int j = 0; j < m_size; j++) {

				temp = (GameObject)Instantiate (m_puzzlePiece, new Vector2 (i * (Screen.width/1.1f)/ m_size, j * (Screen.height/1.4f) / m_size), Quaternion.identity);
				temp.transform.SetParent (transform);
				m_puzzle [i, j] = (PuzzleSection)temp.GetComponent ("PuzzleSection");
				m_puzzle [i, j].CreatePuzzlePiece (m_size);

			}

		}

		SetupBoard ();
		RandomizePlacement ();

	}
	void RandomizePlacement(){

		VectorInt2[] puzzleLocation = new VectorInt2[2];
		Vector2[] puzzleOffset = new Vector2[2];
		do {

			for (int i = 0; i < m_randomPasses; i++) {
				puzzleLocation [0].x = Random.Range (0, m_size);
				puzzleLocation [0].y = Random.Range (0, m_size);
				puzzleLocation [1].x = Random.Range (0, m_size);
				puzzleLocation [1].y = Random.Range (0, m_size);
				puzzleOffset [0] = m_puzzle [puzzleLocation [0].x, puzzleLocation [0].y].GetImageOffset ();
				puzzleOffset [1] = m_puzzle [puzzleLocation [1].x, puzzleLocation [1].y].GetImageOffset ();

				m_puzzle [puzzleLocation [0].x, puzzleLocation [0].y].AssignImage (puzzleOffset [1]);
				m_puzzle [puzzleLocation [1].x, puzzleLocation [1].y].AssignImage (puzzleOffset [0]);

			} 
		} while (CheckBoard () == true);

	} 


	void SetupBoard(){
		Vector2 offset;
		Vector2 m_scale = new Vector2 (1f / m_size, 1f / m_size);
		for (int i = 0; i < m_size; i++) {
			for (int j = 0; j < m_size; j++) {
				offset = new Vector2 (i * (1f / m_size), j * (1f / m_size));
				m_puzzle [i, j].AssignImage (m_scale, offset);


			}

		}


	}

	public PuzzleSection GetSelection(){
		return m_puzzleSelection;



	}

	public void SetSelection(PuzzleSection selection){

		m_puzzleSelection = selection;
	}

	public bool CheckBoard(){

		for (int i=0; i<m_size; i++) {

			for (int j = 0; j < m_size; j++) {
				if (m_puzzle [i, j].CheckGoodPlacement () == false)
					return false;


			} 

		}

		return true;


	}
	//-----------------------------------------------------------Buttons---------------------------------------
	public void basit(int a)
	{
		
		Time.timeScale = 1;

		puzzlepanel.SetActive (true);
		oyunPaneli.SetActive (true);
		antrenmanPanel.SetActive (false);
		menuPanel.SetActive (false);

		puzzlepanel.GetComponent<GameBoard> ().m_size = a;

	}
	public void orta(int a)
	{
		Time.timeScale = 1;

		puzzlepanel.SetActive (true);
		oyunPaneli.SetActive (true);
		antrenmanPanel.SetActive (false);
		menuPanel.SetActive (false);

		puzzlepanel.GetComponent<GameBoard> ().m_size = a;

	}
	public void zor(int a)
	{
		Time.timeScale = 1;

		puzzlepanel.SetActive (true);
		antrenmanPanel.SetActive (false);
		menuPanel.SetActive (false);
		oyunPaneli.SetActive (true);
	
		puzzlepanel.GetComponent<GameBoard> ().m_size = a;
	}
	public void antrenman()
	{
		oyunPaneli.SetActive (false);
		menuPanel.SetActive (false);
		antrenmanPanel.SetActive (true);

	}
	public void geri()
	{
		antrenmanPanel.SetActive (false);
		oyunPaneli.SetActive (false);
		menuPanel.SetActive (true);
		puzzlepanel.SetActive (false);
		storePanel.SetActive (false);
		settingsPanel.SetActive (false);
		infoPanel.SetActive (false);
	}
	public void gerioyunici()
	{
		
		oyunPaneli.SetActive (false);
		menuPanel.SetActive (false);
		antrenmanPanel.SetActive (true);
		puzzlepanel.SetActive (false);

	}
	public void pausemenu()
	{
		
		Time.timeScale = 0;
		pausePanel.SetActive (true);

	}
	public void pausecarpi()
	{
		Time.timeScale = 1;
		pausePanel.SetActive (false);
		losePanel.SetActive (false);

	}

	public void anasayfa()
	{
		
		Application.LoadLevel ("0");
		oyunPaneli.SetActive (false);
		pausePanel.SetActive (false);
		menuPanel.SetActive (true);

	}
	public void tutorialkapat()
	{
		tutorialpanel.SetActive (false);
		menuPanel.SetActive (true);

	}
	public void yeniden()
	{
		
		if (kontrol == 1) 
		{
			countUp.saniye = puan * 10.0f;
			Start ();
			pausecarpi ();

		}
		if (kontrol == 2) 
		{
			countUp.saniye = puan * 20.0f;
			Start ();
			pausecarpi ();
		}
		if (kontrol == 3) 
		{
			countUp.saniye = puan * 30.0f;
			Start ();
			pausecarpi ();
		}






	}
	public void butonbasit()
	{	
		kontrol = 1;
		countUp.saniye = puan * 10.0f;
		basit (3);


	}
	public void butonorta()
	{	
		kontrol = 2;
		countUp.saniye = puan * 20.0f;
		orta (4);

	}
	public void butonzor()
	{
		kontrol = 3;
		countUp.saniye = puan * 30.0f;
		zor (6);

	}
	public int basitkontrol()
	{
		return 1;
	}
	public int ortakontrol()
	{
		return 2;
	}
	public int zorkontrol()
	{
		return 3;
	}
	public void lose()
	{
		
			Time.timeScale = 0;
			sayitext1.text = GameObject.Find ("GameManager").GetComponent<countUp> ().zamanlayici.text;
			losePanel.SetActive (true);
			

	}
	public void store()
	{
		menuPanel.SetActive (false);
		storePanel.SetActive (true);

	}
	public void settings()
	{
		menuPanel.SetActive (false);
		settingsPanel.SetActive (true);
	}
	public void info()
	{
		menuPanel.SetActive (false);
		infoPanel.SetActive (true);

	}

			
		

	}








