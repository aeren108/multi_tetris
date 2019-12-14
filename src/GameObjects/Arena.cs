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

        public int[,] arena;
        public const int blockSize = 32;

        private Texture2D arenaTexture;
        private SpriteFont font;
        
        private int score = 0;

        private Tetromino curTetromino; //Current tetromino which player is controlling

        public Arena(int width, int height) {
            this.width = width;
            this.height = height;

            arena = new int[width, height];

            ClearArena();
            curTetromino = new Tetromino(this);      
        }

        public void LoadContent() {
            arenaTexture = Assets.TETRIS_ARENA;
            font = Assets.SCORE_FONT;
            curTetromino.LoadContent();
        }

        private void ClearArena() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    arena[i, j] = 8;
                }
            }
        }

        private void SaveTetromino(Tetromino t) {
            for (int i = 0; i < t.positions.Length; i++) {
                Vector2 pos = t.positions[i];
                if (pos.Y >= 0 && pos.Y < height && pos.X >= 0 && pos.X < width) {
                    arena[(int) pos.X, (int) pos.Y] = t.id;
                }
            }
        }

        private void GameOver() {
            ClearArena();
            Console.WriteLine("Score" + score);

            // TO-DO: Game over screen
        }

        private void DeleteRow(int row) {
            for (int i = row; i > 0; i--) {
                for (int j = 0; j < width; j++) {
                    //Slide every block
                    arena[j, i] = arena[j, i - 1];
                }
            }
        }

        public void Update(GameTime gameTime) {
            curTetromino.Update(gameTime);

            if (curTetromino.isLanded) {
                SaveTetromino(curTetromino);

                if (curTetromino.GetMinPosition(curTetromino.positions).Y <= 0) {
                    //GameOver();
                } else {

                    Console.WriteLine(curTetromino.GetMinPosition(curTetromino.positions).Y + " : Y pos");

                    curTetromino = new Tetromino(this);
                    curTetromino.LoadContent();
                }
            }

            int rowsFilled = 0;

            //Check if any row is filled
            for (int i = 0; i < height; i++) {
                bool isFilled = true;
                for (int j = 0; j < width; j++) {
                    if (arena[j, i] == 8)
                        isFilled = false;
                }

                if (isFilled) {
                    DeleteRow(i);
                    rowsFilled++;

                }
            }

            if (rowsFilled != 0)
                score += rowsFilled * (rowsFilled * 5 + 50);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(blockSize * 5, blockSize * 18), Color.Red); //Draw score

            //Draw background
            for (int i = 0; i < arena.GetLength(0); i++) {
                for (int j = 0; j < arena.GetLength(1); j++) {
                    spriteBatch.Draw(Assets.ARENA, new Vector2(i * blockSize, j * blockSize));
                }
            }

            //Draw current tetromino
            curTetromino.Draw(spriteBatch); 

            //Draw landed tetrominoes
            for (int i = 0; i < arena.GetLength(0); i++) {
                for (int j = 0; j < arena.GetLength(1); j++) {
                    int id = arena[i, j];

                    if (id != 8) {
                        spriteBatch.Draw(Assets.BLOCKS[id], new Vector2(i * blockSize, j * blockSize));
                    }
                }
            }
        }
    }
}
