using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTetris {
    class Assets {
        public static Texture2D BLOCK_RED;
        public static Texture2D BLOCK_BLUE;
        public static Texture2D BLOCK_GREEN;
        public static Texture2D BLOCK_YELLOW;
        public static Texture2D BLOCK_PURPLE;
        public static Texture2D BLOCK_DARKBLUE;
        public static Texture2D BLOCK_BURGUNDY;
        public static Texture2D[] BLOCKS;

        public static Texture2D ARENA;
        public static Texture2D TETRIS_ARENA;

        public static SpriteFont SCORE_FONT;
        public void LoadContent(ContentManager Content) {
            BLOCK_RED = Content.Load<Texture2D>("block_red");
            BLOCK_BLUE = Content.Load<Texture2D>("block_blue");
            BLOCK_GREEN = Content.Load<Texture2D>("block_green");
            BLOCK_YELLOW = Content.Load<Texture2D>("block_yellow");
            BLOCK_PURPLE = Content.Load<Texture2D>("block_purple");
            BLOCK_DARKBLUE = Content.Load<Texture2D>("block_dblue");
            BLOCK_BURGUNDY = Content.Load<Texture2D>("block_burgundy");

            TETRIS_ARENA = Content.Load<Texture2D>("tetris_arena");
            ARENA = Content.Load<Texture2D>("arena");

            SCORE_FONT = Content.Load<SpriteFont>("File");

            BLOCKS = new Texture2D[7];
            BLOCKS[0] = BLOCK_RED;
            BLOCKS[1] = BLOCK_BLUE;
            BLOCKS[2] = BLOCK_GREEN;
            BLOCKS[3] = BLOCK_YELLOW;
            BLOCKS[4] = BLOCK_PURPLE;
            BLOCKS[5] = BLOCK_DARKBLUE;
            BLOCKS[6] = BLOCK_BURGUNDY;
        }
    }
}
