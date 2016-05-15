
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;
using System.IO;

/// <summary>
/// The EndingGameController is responsible for managing the interactions at the end
/// of a game.
/// </summary>

static class EndingGameController
{
	private const int NAME_WIDTH = 3;
	public static List<Ship> destroyed_AI, destroyed_human;
	private static int count = 0;

	private struct Score : IComparable
	{
		public string Name;

		public int Value;
		/// <summary>
		/// Allows scores to be compared to facilitate sorting
		/// </summary>
		/// <param name="obj">the object to compare to</param>
		/// <returns>a value that indicates the sort order</returns>
		public int CompareTo(object obj)
		{
			if (obj is Score) {
				Score other = (Score)obj;

				return other.Value - this.Value;
			} else {
				return 0;
			}
		}
	}
	/// <summary>
	/// Draw the end of the game screen, shows the win/lose state
	/// </summary>
	public static void DrawEndOfGame()
	{
		UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		string result = "";
		int lineno = 0;
		if (GameController.HumanPlayer.IsDestroyed) 
		{
			DrawResult("YOU LOSE!", lineno);
		} else 
		{
			LoadScores();
			//is it a high score
			if (GameController.HumanPlayer.Score > _Scores[_Scores.Count - 1].Value) 
			{
				result = "HIGH SCORE: "+ GameController.HumanPlayer.Score;
			}
			else 
			{
				result = "SCORE: "+ GameController.HumanPlayer.Score;
			}
			DrawResult(result, lineno);
		}
		lineno += 1;
		result = "You have destroyed " + destroyed_AI.Count + " enemy's ship(s)";
		DrawResult(result, lineno);
		foreach(Ship s in destroyed_AI)
		{
			lineno += 1;
			DrawResult(s.Name, lineno);
		}
		
		lineno += 1;
		result = "You have lost " + destroyed_human.Count + " ship(s)";
		DrawResult(result, lineno);
		foreach(Ship s in destroyed_human)
		{
			lineno += 1;
			DrawResult(s.Name, lineno);
		}
	}
	
	private static void DrawResult(string txt, int i)
	{
		const int SCORES_LEFT = 300;
		const int SCORES_TOP = 130;
		const int SCORE_GAP = 30;
		//SwinGame.DrawText(txt, Color.White, GameResources.GameFont("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
		SwinGame.DrawTextLines(txt, Color.White, Color.Transparent, GameResources.GameFont("Courier"), FontAlignment.AlignCenter, 
			SCORES_LEFT, SCORES_TOP+SCORE_GAP*i, SwinGame.ScreenWidth()*6/10, SwinGame.ScreenHeight());
	}

	private static List<Score> _Scores = new List<Score>();
	/// <summary>
	/// Loads the scores from the highscores text file.
	/// </summary>
	/// <remarks>
	/// The format is
	/// # of scores
	/// NNNSSS
	/// 
	/// Where NNN is the name and SSS is the score
	/// </remarks>
	private static void LoadScores()
	{
		string filename = null;
		filename = SwinGame.PathToResource("highscores.txt");


		StreamReader input = default(StreamReader);
		input = new StreamReader(filename);

		//Read in the # of scores
		int numScores = 0;
		numScores = Convert.ToInt32(input.ReadLine());

		_Scores.Clear();

		int i = 0;

		for (i = 1; i <= numScores; i++) {
			Score s = default(Score);
			string line = null;

			line = input.ReadLine();

			s.Name = line.Substring(0, NAME_WIDTH);
			s.Value = Convert.ToInt32(line.Substring(NAME_WIDTH));
			_Scores.Add(s);
		}
		input.Close();
	}
	
	
	/// <summary>
	/// Handle the input during the end of the game. Any interaction
	/// will result in it reading in the highsSwinGame.
	/// </summary>
	public static void HandleEndOfGameInput()
	{
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_RETURN) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			count += 1;
			if (count>1)
			{
				HighScoreController.ReadHighScore(GameController.HumanPlayer.Score);
				GameController.EndCurrentState();
				count = 0;
			}
		}
	}

}
