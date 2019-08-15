using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MultiTetris.GameObjects {
    class Arena : GameObject {
        public readonly int width;
        public readonly int height;
        public readonly Vector2 position;

        public int[,] size;
        public const int blockSize = 24;

        private Texture2D arena;

        public List<Tetromino> tetrominos; //Tetrominos which have landed
        private Tetromino curTetromino; //Current tetromino which player is controlling

        public Arena(int width, int height) {
            this.width = width ;
            this.height = height;

            size = new int[width, height];
            tetrominos = new List<Tetromino>();

            curTetromino = new Tetromino(this, Tetromino.L);
        }

        public void LoadContent() {
            arena = Assets.TETRIS_ARENA;
            curTetromino.LoadContent();
        }

        public void Update(GameTime gameTime) {
            curTetromino.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(arena, Vector2.Zero);
            curTetromino.Draw(spriteBatch);
        }
    }
}
