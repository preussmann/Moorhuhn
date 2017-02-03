using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Moorhuhn
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Moorhuener moorhuener;
        public Texture2D[] texturen;
        Vector2 Position;
        Vector2 [] aktuelleHuhn_pos;
        Vector2 mousePosition;
        SpriteFont font;
        AnimatedSprite sprite;
        AnimatedSprite erschossenesHuhn;
        float timer;
        int counter;
        MouseState mouse;
        MouseState mouse_alt;
        Random rnd;
        LinkedList<AnimatedSprite> sprite_liste;
        LinkedList<Moorhuener> moorhuener_liste;
        LinkedList<AnimatedSprite> fallende_huener_liste;
        Texture2D fadenkreuz;
        Texture2D schuss_voll;
        Texture2D schuss1;
        Texture2D schuss2;
        Texture2D schuss3;
        Texture2D schuss4;
        Texture2D schuss5;
        Texture2D schuss6;
        Texture2D schuss7;
        Texture2D schuss8;
        int schuesse;
        bool magazin_leer;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();
            IsMouseVisible = false;
            rnd = new Random();
            counter = 0;
            schuesse = 8;
            timer = 0.0f;
            moorhuener_liste = new LinkedList<Moorhuener>();
            sprite_liste = new LinkedList<AnimatedSprite>();
            fallende_huener_liste = new LinkedList<AnimatedSprite>();
            magazin_leer = false;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            // TODO: use this.Content to load your game content here

            texturen = new Texture2D[3];
            texturen[0] = Content.Load<Texture2D>("moorhuhn_rechts");
            texturen[1] = Content.Load<Texture2D>("moorhuhn_links");
            texturen[2] = Content.Load<Texture2D>("moorhuhn_rechts_shot");
            //100,50,2,15,25
            background = Content.Load<Texture2D>("background");
            fadenkreuz = Content.Load<Texture2D>("fadenkreuz");

            schuss_voll = Content.Load<Texture2D>("schuss_voll");
            schuss1 = Content.Load<Texture2D>("schuss1");
            schuss2 = Content.Load<Texture2D>("schuss2");
            schuss3 = Content.Load<Texture2D>("schuss3");
            schuss4 = Content.Load<Texture2D>("schuss4");
            schuss5 = Content.Load<Texture2D>("schuss5");
            schuss6 = Content.Load<Texture2D>("schuss6");
            schuss7 = Content.Load<Texture2D>("schuss7");
            schuss8 = Content.Load<Texture2D>("schuss8");


            font = Content.Load<SpriteFont>("SpriteFont1");

            int i = rnd.Next(0, 2);
            sprite = new AnimatedSprite(texturen[i], 2, 5);
            sprite_liste.AddFirst(sprite);
            
            erschossenesHuhn = new AnimatedSprite(texturen[2], 2, 2);
            fallende_huener_liste.AddFirst(erschossenesHuhn);

            moorhuener = new Moorhuener(sprite, rnd);
            moorhuener_liste.AddFirst(moorhuener);

            Position = moorhuener.Position;
            mousePosition = new Vector2(mouse.X, mouse.Y);
            aktuelleHuhn_pos = new Vector2[1];
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

           
                if (moorhuener_liste.Count < 20)
            {
                if (timer >= 0.5f)
                {
                    int i = rnd.Next(0, 2);
                    sprite = new AnimatedSprite(texturen[i], 2, 5);
                   
                        moorhuener = new Moorhuener(sprite, rnd);
                    moorhuener_liste.AddFirst(moorhuener);
                    sprite_liste.AddFirst(sprite);
                    timer = 0;
                    

                        Console.WriteLine(i);

                }
            }
            sprite.Update();
            foreach (AnimatedSprite a in sprite_liste)
            {
                a.Update();
            }

            //foreach (AnimatedSprite a in fallende_huener_liste)
            //{
              //  a.Update();
            //}



            if (moorhuener_liste.Count >= 19)
            {
                moorhuener_liste.RemoveLast();
                sprite_liste.RemoveLast();
            }
            

            mouse_alt = mouse;
            mouse = Mouse.GetState();
            Rectangle rechteck_mouse = new Rectangle(mouse.X, mouse.Y, 1, 1);
            
            
            
           

            List<Moorhuener> abgeschossen = new List<Moorhuener>();
            List<Moorhuener> druassen = new List<Moorhuener>();
            
            

            foreach (Moorhuener a in moorhuener_liste)
            {
                if (a.Huhn.Texture == texturen[0])
                {
                    a.Position.X += 4.0f;
                    Rectangle rechteck_huhn = new Rectangle((int)a.Position.X, (int)a.Position.Y, a.Huhn.breite, a.Huhn.hoehe);
                    if (a.Position.X <= 1000)
                    {
                        
                        
                        if (rechteck_huhn.Intersects(rechteck_mouse))
                        {
                            if (mouse.LeftButton == ButtonState.Pressed && mouse_alt.LeftButton == ButtonState.Released)
                            {

                                        
                                        abgeschossen.Add(a);
                                      //  fallende_huener_liste.AddFirst(erschossenesHuhn);
                            }
                        }
                    }
                    else
                    {
                        druassen.Add(a);

      
                    } 
                } else if(a.Huhn.Texture == texturen[1])
                {
                    a.Position.X -= 4.0f;
                    Rectangle rechteck_huhn = new Rectangle((int)a.Position.X, (int)a.Position.Y, a.Huhn.breite, a.Huhn.hoehe);
                    if (a.Position.X <= 1000)
                    {


                        if (rechteck_huhn.Intersects(rechteck_mouse))
                        {
                            if (mouse.LeftButton == ButtonState.Pressed && mouse_alt.LeftButton == ButtonState.Released)
                            {

                               
                                abgeschossen.Add(a);
                               // fallende_huener_liste.AddFirst(erschossenesHuhn);
                            }
                        }
                    }
                    else
                    {
                        druassen.Add(a);


                    }
                }
                    
                

            }

            foreach(Moorhuener i in druassen)
            {
                moorhuener_liste.Remove(i);
            }

            


            foreach (Moorhuener i in abgeschossen)
            {
                if (i.Huhn.Texture == texturen[0])
                {
                    counter += 5;
                }else if (i.Huhn.Texture == texturen[1])
                        {
                    
                                counter += 10;
                           
                                
                            

                            // sprite = new AnimatedSprite(texturen[2], 2, 2);
                            //moorhuener = new Moorhuener(sprite, rnd);
                        }
                        else
                        {
                            counter += 20;
                        }
                moorhuener_liste.Remove(i);





            }
            
            Rectangle rechteck_fadenkreuz = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, fadenkreuz.Width, fadenkreuz.Height);
            if (mouse.X != rechteck_fadenkreuz.X && mouse.Y != rechteck_fadenkreuz.Y)
            {
                mousePosition = new Vector2(mouse.X-(fadenkreuz.Width/2), mouse.Y-(fadenkreuz.Height/2));
                
            }
            if (mouse.LeftButton == ButtonState.Pressed && mouse_alt.LeftButton == ButtonState.Released)
            {
                schuesse -= 1;
                if(schuesse == 0)
                {
                    magazin_leer = true;

                }
               
            }
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.R))
                
            {
                if (schuesse <= 0)
                {
                    magazin_leer = false;
                    schuesse = 8;
                }
               
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here

            

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Punkte: " + counter, new Vector2(10, 2), Color.Black);
            
            spriteBatch.End();

            foreach (Moorhuener a in moorhuener_liste)
            {
                a.Draw(spriteBatch);
            }

           
            spriteBatch.Begin();

            spriteBatch.Draw(fadenkreuz, mousePosition, Color.White);
            if(magazin_leer == false)
            { 

                if(schuesse == 8)
                {
                    spriteBatch.Draw(schuss_voll, new Vector2(graphics.PreferredBackBufferWidth-272, graphics.PreferredBackBufferHeight-92), Color.White);
                }else if(schuesse == 7)
                {
                    spriteBatch.Draw(schuss1, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 6)
                {
                    spriteBatch.Draw(schuss2, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 5)
                {
                    spriteBatch.Draw(schuss3, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 4)
                {
                    spriteBatch.Draw(schuss4, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 3)
                {
                    spriteBatch.Draw(schuss5, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 2)
                {
                    spriteBatch.Draw(schuss6, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
                else if (schuesse == 1)
                {
                    spriteBatch.Draw(schuss7, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
                }
            }else
            {
                spriteBatch.Draw(schuss8, new Vector2(graphics.PreferredBackBufferWidth - 272, graphics.PreferredBackBufferHeight - 92), Color.White);
            }
           
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
