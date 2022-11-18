using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobbyTheRobot;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        
        private Texture2D _robotTexture;

        private Texture2D _canTexture;

        private SpriteFont _font;

        private RobbyTheRobot.IRobbyTheRobot _robby;

        private ContentsOfGrid[,] testGrid;

        private string _generationText = "Generation: 1";

        private int currentMove;
        private string _moveText = "Move: 0/200";

        private string _pointsText = "Points: 0/500";

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 702;
            _graphics.PreferredBackBufferHeight = 900;
            
            GraphicsDevice.Clear(Color.Black);

            _font = this.Content.Load<SpriteFont>("LetterFont");
            _canTexture = this.Content.Load<Texture2D>("can");
            _robotTexture = this.Content.Load<Texture2D>("robot2");

            _robby = Robby.createRobby(200,1,1,1,1,1,1,1,10);
            testGrid = _robby.GenerateRandomTestGrid();
            _graphics.ApplyChanges();

            currentMove = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            drawBoard();

            if (currentMove == 0) {
                Random rand = new Random();
                int x = rand.Next(_robby.GridSize);
                int y = rand.Next(_robby.GridSize);   
                Vector2 position = new Vector2((x*70)+3, (y*70)+3);
                drawRobby(position);
                currentMove++;
            }

            Vector2 generationTextPosition = new Vector2(30, 720);
            _spriteBatch.DrawString(_font, _generationText, generationTextPosition, Color.White);
            Vector2 moveTextPosition = new Vector2(30, 780);
            _spriteBatch.DrawString(_font, _moveText, moveTextPosition, Color.White);
            Vector2 pointsTextPosition = new Vector2(30, 840);
            _spriteBatch.DrawString(_font, _pointsText, pointsTextPosition, Color.White);  
            

            //each tile is 70 x 70
            //add 3 pixels to each side to make it look better
            Vector2 robotPosition = new Vector2(73, 73);
            _spriteBatch.Draw(_robotTexture, robotPosition, Color.White);

            //reset a tile
            //drawBlackTile(70, 70, 70);

            _spriteBatch.End();

            base.Draw(gameTime);   
        }

        protected void drawBoard(){
            int row = 10;
            int column = 10;
            int rectangleSideLength = 70;
            Texture2D rectangle = new Texture2D(GraphicsDevice, row, column);
            for (int i = 0; i < row; ++i){
                for (int j = 0; j < column; ++j)
                {
                    drawTile(i * rectangleSideLength, j * rectangleSideLength, rectangleSideLength);
                }
            }

            for (int i = 0; i < testGrid.GetLength(0); i++)
            {
                for (int j = 0; j < testGrid.GetLength(1); j++)
                {
                    if (testGrid[i, j] == ContentsOfGrid.Can){
                        Vector2 posiiton = new Vector2(i*70, j*70); 
                        drawCan(posiiton);
                    }
                }
            }
        }

        protected void drawTile(int x, int y, int height){
            GridTileSprite.DrawTile(_spriteBatch, new Rectangle(x, y, height, height), Color.White, 2);
        }

        protected void drawBlackTile(int x, int y, int height){
            Texture2D blackBox = new Texture2D(this.GraphicsDevice, height, height);
            Color[] colorArray = new Color[height*height];
            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = Color.Black;
            }
            blackBox.SetData(colorArray);
            Vector2 coordinates = new Vector2(x, y);
            _spriteBatch.Draw(blackBox, coordinates, Color.Black);

            drawTile(x, y, height);
        }
        protected void drawCan(Vector2 position){
            _spriteBatch.Draw(_canTexture, position, Color.White);
        }

        protected void drawRobby(Vector2 position){
            _spriteBatch.Draw(_robotTexture, position, Color.White);
        }
    }
}
