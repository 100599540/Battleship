
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using System.IO;
using SwinGameSDK;

/// <summary>
/// Displays the help menu
/// </summary>
static class HelpMenu
{
	private const int NAME_WIDTH = 3;

	private const int HELP_TOP = 275;


	/// <summary>
	/// Draws the help page text to the screen.
	/// </summary>
	public static void DrawHelpScreen()
	{
		const int HELP_HEADING = 40;
		const int HELP_BODY = 70;

		SwinGame.DrawText("   Battleship Help Page   ", Color.White, GameResources.GameFont("Courier"), HELP_TOP, HELP_HEADING);
		
	}

	/// <summary>
	/// Handles the user input during the showing of the screen
	/// </summary>
	/// <remarks></remarks>
	public static void HandleHighScoreInput()
	{
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE) || SwinGame.KeyTyped(KeyCode.vk_RETURN)) {
			GameController.EndCurrentState();
		}
	}
}
