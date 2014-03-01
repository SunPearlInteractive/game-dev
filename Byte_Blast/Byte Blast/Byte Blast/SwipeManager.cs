using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Byte_Blast
{
    class SwipeManager
    {
        private List<SwipeSequence> m_SwipeSequences = new List<SwipeSequence>();
        private Texture2D m_ArrowGraphics;
        private bool m_ToAttack = false;

        public SwipeManager()
        {
            m_SwipeSequences.Add(new SwipeSequence());
            m_SwipeSequences.Add(new SwipeSequence());
        }

        public void Load(ContentManager Content)
        {
            try 
            {
                m_ArrowGraphics = Content.Load<Texture2D>("arrows");
            }
            catch (Exception E)
            {
                Console.WriteLine("Could not load Swipe Arrow Graphics, Error: " + E.Message);
            }
        }

        public void Update()
        {
            foreach (SwipeSequence ss in m_SwipeSequences)
            {
                ss.Update();

                if (ss.GetWin())
                {
                    m_ToAttack = true;
                    m_SwipeSequences.Remove(ss);
                    m_SwipeSequences.Add(new SwipeSequence());
                    break;
                }
                else if (ss.GetLose())
                {
                    m_ToAttack = false;
                    m_SwipeSequences.Remove(ss);
                    m_SwipeSequences.Add(new SwipeSequence());
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int i = 0;
            float ArrowTransparency = 1.0f;
            foreach (SwipeSequence ss in m_SwipeSequences)
            {
                ss.Draw(spriteBatch, m_ArrowGraphics, new Vector2(100.0f, 40.0f) + new Vector2((64 * (SwipeSequence.NumberOfSwipes - m_SwipeSequences[i].CurrentSwipe()) + 30) * i, 0.0f), ArrowTransparency);
                i++;
                ArrowTransparency = MathHelper.Lerp(ArrowTransparency, 0.0f, 0.9f);
            }
        }
    }
}
