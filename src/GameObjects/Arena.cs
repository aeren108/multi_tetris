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

        public static Dictionary<int, Vector2[]> PATTERNS;
        private Tetromino curTetromino; //Current tetromino which player is controlling

        //All tetromino patterns
        private readonly Vector2[] T = { new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2), new Vector2(0, 1) };
        private static readonly Vector2[] Z = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 2) };
        private static readonly Vector2[] S = { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(0, 2) };
        private static readonly Vector2[] L = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 1) };
        private static readonly Vector2[] J = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(2, 0) };
        private static readonly Vector2[] I = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) };
        private static readonly Vector2[] O = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

        public Arena(int width, int height) {
            this.width = width ;
            this.height = height;

            arena = new int[width, height];
            PATTERNS = new Dictionary<int, Vector2[]>();

            PATTERNS.Add(0, T);
            PATTERNS.Add(1, Z);
            PATTERNS.Add(2, S);
            PATTERNS.Add(3, L);
            PATTERNS.Add(4, J);
            PATTERNS.Add(5, I);
            PATTERNS.Add(6, O);

            ClearArena();
            curTetromino = new Tetromino(this);      
        }

        public void LoadContent() {
            arenaTexture = Assets.TETRIS_ARENA;
            font = Assets.SCORE_FONT;
            curTetromino.LoadContent();
        }

        private void SaveTetromino(Tetromino t) {
            for (int i = 0; i < t.positions.Length; i++) {
                Vector2 pos = t.positions[i];
                if (pos.Y < height)
                    arena[(int) pos.X, (int) pos.Y] = t.id;
                else
                    GameOver();
            }
        }

        private void ClearArena() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    arena[i, j] = 8;
                }
            }
        }

        public void Update(GameTime gameTime) {
            curTetromino.Update(gameTime);

            if (curTetromino.isLanded) {
                SaveTetromino(curTetromino);

                curTetromino = new Tetromino(this);
                curTetromino.LoadContent();    
            }

            int rowsFilled = 0;

            for (int i = 0; i < width; i++) {
                bool isFilled = true;
                for (int j = 0; j < height; j++) {
                    if (arena[j, i] == 8)
                        isFilled = false;
                }

                if (isFilled) {
                    DeleteRow(i);
                    rowsFilled++;

                }
            }

            if (rowsFilled != 0)
                score += rowsFilled * (rowsFilled + 50);
        }

        private void GameOver() {

        }

        private void DeleteRow(int row) {
            for (int i = row; i > 0; i--) {
                for (int j = 0; j < width; j++) {
                    arena[j, i] = arena[j, i-1];
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(arenaTexture, Vector2.Zero);
            curTetromino.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(blockSize * 17, blockSize), Color.GhostWhite);

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
