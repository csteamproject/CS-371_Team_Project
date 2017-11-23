using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Scratch
{
	public class Menu : AnimatedSprite
	{

		//variables for start menu 
		Texture2D startButton;
		Texture2D exitButton;
		//private Texture2D pauseButton;
		//private Texture2D resumeButton;
		//private Texture2D loadingScreen;
		private Vector2 startButtonPos;
		private Vector2 exitButtonPosition;
		//private Vector2 resumeButtonPosition;
		private Vector2 textPos;
		private SpriteFont spriteFont;
		GraphicsDeviceManager graphics;

		//private Thread backgroundThread;
		GameState gameState;

		//variable for music
		private Song backGround;

		//game states
		enum GameState
		{

			StartMenu,

			Loading,

			Playing,

			Paused
		}
		public Menu(Texture2D sB, Texture2D eB, int row, int col) : base(sB, row, col)
		{
			startButton = sB;
			exitButton = eB;

		}

		//public static GameState getState()
		//{
		//	return Menu.GameState();
		//}

		public void initialize()
		{
			startButtonPos = new Vector2(100, 200);
			exitButtonPosition = new Vector2(150, 250);
			textPos = new Vector2(100, 300);
			gameState = GameState.StartMenu;

		}//end initialize


	}//end menu 
}//end class
