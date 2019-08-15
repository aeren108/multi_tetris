using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MultiTetris.GameObjects;

namespace MultiTetris {

    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Assets assets;

        Arena arena;

        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            assets = new Assets();
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            arena = new Arena(16, 16);

            base.Initialize();
        }


        protected override void LoadContent() {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets.LoadContent(Content);
            arena.LoadContent();
        }

        protected override void UnloadContent() {
            
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            arena.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.SlateGray);

            spriteBatch.Begin();

            arena.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
