using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        
        private string _generationText = "Generation: 0";

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
            _graphics.ApplyChanges();
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
            GraphicsDevice.Clear(Color.Black);

            //SpriteFont font = this.Content.Load<SpriteFont>("LetterFont");


            _spriteBatch.Begin();
            drawBoard();
            Texture2D canTexture = this.Content.Load<Texture2D>("can");

            Vector2 generationTextPosition = new Vector2(50, 730);
            //_spriteBatch.DrawString(font, _generationText, generationTextPosition, Color.White);

            Vector2 moveTextPosition = new Vector2(50, 750);
            //_spriteBatch.DrawString(font, _moveText, moveTextPosition, Color.White);

            Vector2 pointsTextPosition = new Vector2(50, 770);
            //_spriteBatch.DrawString(font, _pointsText, pointsTextPosition, Color.White);  
            
            Vector2 canPosition = new Vector2(0, 0);
            _spriteBatch.Draw(canTexture, canPosition, Color.White);

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
                    GridTileSprite.DrawTile(_spriteBatch, new Rectangle(i * rectangleSideLength, j * rectangleSideLength, rectangleSideLength, rectangleSideLength), Color.White, 2);
                }
            }
        }

        protected void drawCircle(Texture2D png){
            
        }
    }
}
