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

        public static Texture2D TETRIS_ARENA;
        public void LoadContent(ContentManager Content) {
            BLOCK_RED = Content.Load<Texture2D>("block");

            TETRIS_ARENA = Content.Load<Texture2D>("tetris_arena");
        }
    }
}
