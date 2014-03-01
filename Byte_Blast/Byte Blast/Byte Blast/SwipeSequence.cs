using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte_Blast
{
    class SwipeSequence
    {
        public enum SwipeDirection { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3 }

        private List<int> m_Swipes;
        private int m_CurrentSwipe = 0;
        public const int NumberOfSwipes = 5;
        private bool m_Finsihed, m_Successful;

        public int CurrentSwipe() { return m_CurrentSwipe; }
        public List<int> Swipes() { return m_Swipes; }

        public SwipeSequence()
        {
            Random myRand = new Random(System.DateTime.Now.Millisecond);
            m_Swipes = new List<int>();
            for (int i = 0; i < NumberOfSwipes; i++)
            {
                m_Swipes.Add(myRand.Next(0, 4));
            }

            m_Finsihed = false;
            m_Successful = true;
        }

        public void Update()
        {
            GetInput();

            if (m_CurrentSwipe == NumberOfSwipes && m_Successful)
            {
                m_Finsihed = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D arrowgaphics, Vector2 position, float transparency)
        {
            float xpos = 0;
            float sectransparency = transparency;

            for (int i = m_CurrentSwipe; i < NumberOfSwipes; i++)
            {
                spriteBatch.Draw(arrowgaphics, position + new Vector2(xpos, 0.0f), new Rectangle(m_Swipes[i] * 64, 0, 64, 64), Color.White * sectransparency);

                xpos += 64;
                sectransparency = MathHelper.Lerp(sectransparency, 0.0f, 0.3f);
            }
        }

        private void GetInput()
        {
            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gs = TouchPanel.ReadGesture();

                switch (gs.GestureType)
                {
                    case GestureType.Flick:
                        HandleSwipe(gs.Delta);
                        break;

                    default:
                        break;
                }
            }
        }

        private void HandleSwipe(Vector2 velocity)
        {
            if (velocity.X < 0.5f)
            {
                if (m_Swipes[m_CurrentSwipe] == (int)SwipeDirection.LEFT)
                    m_CurrentSwipe++;
                else
                {
                    m_Finsihed = true;
                    m_Successful = false;
                }
            }
            else if (velocity.X > 0.5f)
            {
                if (m_Swipes[m_CurrentSwipe] == (int)SwipeDirection.RIGHT)
                    m_CurrentSwipe++;
                else
                {
                    m_Finsihed = true;
                    m_Successful = false;
                }
            }
            else if (velocity.Y < 0.5f)
            {
                if (m_Swipes[m_CurrentSwipe] == (int)SwipeDirection.UP)
                    m_CurrentSwipe++;
                else
                {
                    m_Finsihed = true;
                    m_Successful = false;
                }
            }
            else if (velocity.Y > 0.5f)
            {
                if (m_Swipes[m_CurrentSwipe] == (int)SwipeDirection.DOWN)
                    m_CurrentSwipe++;
                else
                {
                    m_Finsihed = true;
                    m_Successful = false;
                }
            }
        }

        public bool GetWin()
        {
            if (m_Finsihed && m_Successful)
                return true;

            return false;
        }

        public bool GetLose()
        {
            if (m_Finsihed && !m_Successful)
                return true;

            return false;
        }
    }
}
