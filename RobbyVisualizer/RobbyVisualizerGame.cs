using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
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

        private Vector2 previousPosition;
        
        private Vector2 currentPosition;

        private int[] currentCoordinates = new int[2];

        private string _generationText = "Generation: 1";

        private string _moveText = "Move: 0/200";

        private int currentMove;

        private string fileName;

        private double score;

        private string _pointsText = "Points: 0/500";

        private int[,] setOfMoves;

        private int numberOfGenerations;

        private int timeToWait;

        private int maxMoves;
        
        private int currentGeneration;
        private int[] _displayGeneration;

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

            currentMove = 1;
            currentGeneration = 0;
            timeToWait = 50;
            _displayGeneration = new int[]{1, 20, 100, 200, 500, 1000};


            openFile();

            readFromFile();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            if (timeToWait == 0){
                moveRobby();
                timeToWait = 5;
            }
            else {
                timeToWait = timeToWait - 1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (currentMove == 0) {
                Random rand = new Random();
                int x = rand.Next(_robby.GridSize);
                int y = rand.Next(_robby.GridSize);   
                currentPosition = new Vector2((x*70)+3, (y*70)+3);
                currentCoordinates[0] = x;
                currentCoordinates[1] = y;
                //drawBlackTile(x, y);
                currentMove+=1;
            }

            _spriteBatch.Begin();
            
            drawBoard();
            drawRobby(currentPosition);
            
            Vector2 generationTextPosition = new Vector2(30, 720);
            _spriteBatch.DrawString(_font, _generationText, generationTextPosition, Color.White);
            Vector2 moveTextPosition = new Vector2(30, 780);
            _spriteBatch.DrawString(_font, _moveText, moveTextPosition, Color.White);
            Vector2 pointsTextPosition = new Vector2(30, 840);
            _spriteBatch.DrawString(_font, _pointsText, pointsTextPosition, Color.White);  
            
            //reset a tile
            drawBlackTile(1, 1);
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

        protected void drawBlackTile(int x, int y){
            int height = 70;
            Texture2D blackBox = new Texture2D(this.GraphicsDevice, height, height);
            Color[] colorArray = new Color[height*height];
            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = Color.Black;
            }
            blackBox.SetData(colorArray);
            Vector2 coordinates = new Vector2(x*height, y*height);
            _spriteBatch.Draw(blackBox, coordinates, Color.Black);

            drawTile(x*height, y*height, height);
        }
        protected void drawCan(Vector2 position){
            _spriteBatch.Draw(_canTexture, position, Color.White);
        }

        protected void drawRobby(Vector2 position){
            _spriteBatch.Draw(_robotTexture, position, Color.White);
        }

        protected void openFile(){
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "../Iterations/" ;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true ;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            } 
        }

        protected void readFromFile(){
            string[] lines = File.ReadAllLines(fileName); 
            int i = 0;
            numberOfGenerations = lines.Length;

            int numOfMoves = Int16.Parse(lines[0].Split(',')[1]);

            int[,] arrayofmoves = new int[lines.Length,numOfMoves]; 
            foreach (string line in lines)
            {
                List<int> listOfMoves = new List<int>();
                string[] info = line.Split(',');
                char[] allMoves = info[2].ToCharArray();
                maxMoves = allMoves.Length;
                int j = 0;
                foreach (char move in allMoves)
                {
                    arrayofmoves[i, j] = Int16.Parse(move.ToString());
                    j++;
                }
                i++;
            }
            setOfMoves = arrayofmoves;
        }
    
        protected void moveRobby(){
            currentMove++;
            if (currentMove == maxMoves)
            {
                currentGeneration++;
                currentMove = 1;
                score = 0;
            }
            else{
                int move = setOfMoves[currentGeneration,currentMove];

                int[] moves = new int[maxMoves];
                for (int i = 0; i < maxMoves; i++)
                {
                    moves[i] = setOfMoves[currentGeneration, i];
                }

                score += RobbyHelper.ScoreForAllele(moves, testGrid, new Random(), ref currentCoordinates[0], ref currentCoordinates[1]);
                move++;

                _generationText = $"Generation: {_displayGeneration[currentGeneration]}/1000";
                _moveText = $"Move: {currentMove}/{maxMoves}";
                _pointsText = $"Points: {score}/500";
                
            }
        }
    }
}
