using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleSection : MonoBehaviour 
{

	Vector2 m_goodOffset;
	Vector2 m_offset;
	Vector2 m_scale;
	GameBoard m_gameBoard;
	void Start()
	{

		m_gameBoard = (GameBoard)GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<GameBoard>();

	}


	public void CreatePuzzlePiece(int size)
	{
		transform.localScale = new Vector3 (1 * transform.localScale.x / size, 1 * transform.localScale.z / size, 1);

	}
	public void AssignImage(Vector2 scale, Vector2 offset)
	{

		m_goodOffset = offset;
		m_scale = scale;
		AssignImage (offset);


	}

	public void AssignImage(Vector2 offset)
	{
		m_offset = offset;
		GetComponent<RawImage> ().uvRect = new Rect (offset.x, offset.y, m_scale.x, m_scale.y);


	}

	public void onClick()
	{

		PuzzleSection previousSelection = m_gameBoard.GetSelection ();
		if(previousSelection!=null) {

			previousSelection.GetComponent<RawImage>().color=Color.white;
			Vector2 tempOffset=previousSelection.GetImageOffset();
			previousSelection.AssignImage(m_offset);
			AssignImage(tempOffset);
			m_gameBoard.SetSelection(null);
			if (m_gameBoard.CheckBoard () == true)
			{
				Time.timeScale = 0;
				m_gameBoard.Win ();

			}
				
		} else {
				
				GetComponent<RawImage> ().color = Color.gray;
				m_gameBoard.SetSelection (this);
			}
		}






	public Vector2 GetImageOffset()
	{
		return m_offset;


	}

	public bool CheckGoodPlacement()
	{

		return(m_goodOffset == m_offset);
	}

}
