using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Byte_Blast
{
    class Hallway
    {
        private float m_XPos;
        private int m_TextureID;

        /// <summary>
        /// Get the X Position Value of the Hallway Section
        /// </summary>
        /// <returns>X Position as float</returns>
        public float GetXPos() { return m_XPos; }

        public int GetTextureID() { return m_TextureID; }

        public Hallway()
        {
            m_XPos = 1480.0f;
            m_TextureID = new Random().Next(0, HallwayManager.NumberOfTextures - 1);
        }

        public void Update(float speed)
        {
            m_XPos -= speed;
        }

        public void SetXPosition(float xpos)
        {
            m_XPos = xpos;
        }
    }
}
