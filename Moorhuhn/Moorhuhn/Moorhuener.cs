using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moorhuhn
{
    class Moorhuener
    {
        public Vector2 Position;
        public AnimatedSprite Huhn;
        public Random rnd;

        public Moorhuener(AnimatedSprite huhn, Random rnd)
        {
            this.Huhn = huhn;
            this.rnd = rnd;
            this.Position = new Vector2((float)rnd.Next(100, 900), (float)rnd.Next(100, 500));
          
        }

       
            
                    
        public void Draw(SpriteBatch spriteBatch)
        {
            
            Huhn.Draw(spriteBatch, Position);
       }
    }
}
