using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moorhuhn
{
    class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Zeilen { get; set; }
        public int Spalten { get; set; }
        public int breite;
        public int hoehe;
        private int aktFrame;
        private int anzFrames;


        public AnimatedSprite(Texture2D texture, int zeilen, int spalten)
        {
            this.Texture = texture;
            this.Zeilen = zeilen;
            this.Spalten = spalten;
            this.aktFrame = 0;
            this.anzFrames = this.Zeilen * this.Spalten;
            
        }

        public void Update()
        {
            aktFrame++;
            if (aktFrame == anzFrames)
            {
                aktFrame = 0;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            this.breite = Texture.Width / Spalten;
            this.hoehe = Texture.Height / Zeilen;
            //int breite = Texture.Width / Spalten;
            //int hoehe = Texture.Height / Zeilen;

            int zeile = (int)((float)aktFrame / (float)Spalten);
            int spalte = aktFrame % Spalten;

            Rectangle rechteck_1 = new Rectangle(breite * spalte, hoehe * zeile, breite, hoehe);
            Rectangle rechteck_2 = new Rectangle((int)position.X, (int)position.Y, breite, hoehe);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, rechteck_2, rechteck_1, Color.White);
            spriteBatch.End();
        }
    }
}
