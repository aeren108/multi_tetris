using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTetris.GameObjects {
    class Tetromino : GameObject {
        private Vector2[] pattern; // coordinates on 4x4 plane
        private Vector2[] buffer; // a buffer array
        public  Vector2[] positions; // positions of tetromino blocks
        public Vector2 position; // position of tetromino

        private Arena arena;
        public Texture2D block;

        private int rotation = 0; //Rotation value 0 = 0, 1 = 90, 2 = 180, 3 = 270;
        public int id;
        public bool isLanded = false;

        private int velLeft = 1;
        private int velRight = 1;
        private int velDown = 1;

        private bool space = true;
        private bool right = true;
        private bool left = true;

        private const float TIMER = 0.5f;
        private float timer = TIMER;

        private Random random = new Random();

        public Tetromino(Arena arena) {
            this.arena = arena;
            id = random.Next(0, 7);
            this.pattern = Arena.PATTERNS[id];

            positions = new Vector2[pattern.Length];
            buffer = new Vector2[pattern.Length];
            position = new Vector2(random.Next(0, arena.width), 0);

            for (int i = 0; i < pattern.Length; i++) {
                Vector2 v = pattern[i];
                int index = (int) (v.X + (v.Y * 4)); // index on 1 dimensional array (from 4x4 plane);
                int valX = (int) (index % 4);
                int valY = (int) (index / 4);

                buffer[i] = new Vector2(valX, valY);
            }

            SetPositions();
        }

        public void LoadContent() {
            block = Assets.BLOCKS[id];
        }

        private void Rotate(int value) {
            switch(value % 4) {
                case 0: // 0 = 0 degrees rotation
                    for(int i = 0; i < pattern.Length; i++) {
                        Vector2 v = pattern[i];
                        int index = (int)(v.X + (v.Y * 4)); // index on 1 dimensional array (from 4x4 plane);
                        buffer[i].X = index % 4;
                        buffer[i].Y = (int) (index / 4);
                    }
                    break;
                case 1: // 1 = 90 degrees rotation
                    for (int i = 0; i < pattern.Length; i++) {
                        Vector2 v = pattern[i];
                        int index = (int) (12 + v.Y - (v.X * 4)); // index on 1 dimensional and 90 degrees rotated array (from 4x4 plane);
                        buffer[i].X = index % 4;
                        buffer[i].Y = (int) (index / 4);
                    }
                    break;
                case 2: // 2 = 180 degrees rotation
                    for (int i = 0; i < pattern.Length; i++) {
                        Vector2 v = pattern[i];
                        int index = (int) (15 - v.X - (v.Y * 4)); // index on 1 dimensional and 180 degrees rotated array (from 4x4 plane);
                        buffer[i].X = index % 4;
                        buffer[i].Y = (int) (index / 4);
                    }
                    break;
                case 3: // 3 = 270 degrees rotation
                    for (int i = 0; i < pattern.Length; i++) {
                        Vector2 v = pattern[i];
                        int index = (int) (3 - v.Y + (v.X * 4)); // index on 1 dimensional and 270 degrees rotated array (from 4x4 plane);
                        buffer[i].X = index % 4;
                        buffer[i].Y = (int) (index / 4);
                    }
                    break;
            }
        }

        public void SetPositions() {
            Vector2 minVec = GetMinPosition(buffer);

            for (int i = 0; i < positions.Length; i++) {
                positions[i].X = buffer[i].X - minVec.X;
                positions[i].Y = buffer[i].Y - minVec.Y;

                positions[i] += position;
            }
        }

        private Vector2 GetMinPosition(Vector2[] pos) {
            int minX = 4, minY = 4;

            for (int i = 0; i < pos.Length; i++) {
                int minX1 = (int) pos[i].X;
                int minY1 = (int) pos[i].Y;

                if (minX1 <= minX)
                    minX = minX1;
                if (minY1 <= minY)
                    minY = minY1;
            }

            return new Vector2(minX, minY);

        }

        private Vector2 GetMaxPosition(Vector2[] pos) {
            int maxX = 0, maxY = 0;

            for (int i = 0; i < pos.Length; i++) {
                int maxX1 = (int) pos[i].X;
                int maxY1 = (int) pos[i].Y;

                if (maxX1 >= maxX)
                    maxX = maxX1;
                if (maxY1 >= maxY)
                    maxY = maxY1;
            }

            return new Vector2(maxX, maxY);
        }

        private void DetectCollision() {
            bool lb = false, rb = false;

            for (int i = 0; i < positions.Length; i++) {
                int x = (int) positions[i].X;
                int y = (int) positions[i].Y;

                if (x - 1 < 0)
                    lb = true;
                else if (x + 1 >= arena.width)
                    rb = true;

                if (y + 1 >= arena.height) {
                    isLanded = true;
                    velDown = 0;
                }

                if (x >= 0 && x < arena.width && y + 1 < arena.height) {
                    if (arena.arena[x, y + 1] != 8) {
                        isLanded = true;
                        velDown = 0;
                    }
                }

                if (x + 1 < arena.width && x + 1 >= 0 && y < arena.height) {
                    if (arena.arena[x + 1, y] != 8) {
                        rb = true;
                    }
                } if (x - 1 >= 0 && x - 1 < arena.width && y < arena.height) {
                    if (arena.arena[x - 1, y] != 8) {
                        lb = true;
                    }
                }
            }

            velLeft = lb ? 0 : 1;
            velRight = rb ? 0 : 1;
        }

        public void Update(GameTime gameTime) {
            KeyboardState state = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.Space) && space) {
                rotation++;
                Rotate(rotation);
                space = false;
            } if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) {               
                position.Y += velDown;               
            } else if ((state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) && left) { 
                position.X += -velLeft; 
                left = false;
            } else if ((state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) && right ) {              
                position.X += velRight;
                right = false;
            }

            if (state.IsKeyUp(Keys.Space))
                space = true;
            if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.Left))
                left = true;
            if (state.IsKeyUp(Keys.D) && state.IsKeyUp(Keys.Right))
                right = true;

            // TO-DO handle moving down (every 2 seconds)
            timer -= elapsed;
            if (timer <= 0) {
                position.Y += velDown;
                timer = TIMER;
            }

            SetPositions();
            DetectCollision();
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (positions != null) {
                for (int i = 0; i < positions.Length; i++) {
                    spriteBatch.Draw(block, (positions[i] * Arena.blockSize));
                }
            }
        }
    }
}
