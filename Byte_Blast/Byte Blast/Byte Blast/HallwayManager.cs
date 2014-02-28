using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Byte_Blast
{
    class HallwayManager
    {
        public const int NumberOfTextures = 1;
        private Texture2D[] m_HallwaySprites = new Texture2D[NumberOfTextures];
        private List<Hallway> m_Hallways = new List<Hallway>();

        public HallwayManager()
        {
            m_Hallways.Add(new Hallway());
            m_Hallways.Add(new Hallway());
            m_Hallways.Add(new Hallway());
            m_Hallways.Add(new Hallway());

            m_Hallways[0].SetXPosition(-740.0f);
            m_Hallways[1].SetXPosition(0.0f);
            m_Hallways[2].SetXPosition(740.0f);
            m_Hallways[3].SetXPosition(1480.0f);
        }

        public void Load(ContentManager Content)
        {
            for (int i = 0; i < NumberOfTextures; i++)
            {
                try
                {
                    m_HallwaySprites[i] = Content.Load<Texture2D>("hallway_" + i.ToString());
                }
                catch (Exception E)
                {
                    Console.WriteLine("Could not load Hallway Segments, Error: " + E.Message);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            bool HallwayRemoved = false;
            int HallwaysUpdated = 0;

            foreach (Hallway h in m_Hallways)
            {
                h.Update();

                if (h.GetXPos() < -1480.0f)
                {
                    m_Hallways.Remove(h);
                    m_Hallways.Add(new Hallway());
                    HallwayRemoved = true;

                    HallwaysUpdated++;
                }

                if (HallwayRemoved) break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Hallway h in m_Hallways)
            {
                try
                {
                    spriteBatch.Draw(m_HallwaySprites[h.GetTextureID()], new Vector2(30 + h.GetXPos(), 30), Color.White);
                }
                catch (Exception E)
                {
                    Console.WriteLine("Error Drawing Hallway Segments: " + E.Message);
                }
            }
        }
    }
}
