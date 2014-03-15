using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte_Blast
{
    class Monster
    {
        private float m_XPosition;
        private List<Monster> m_Monsters = new List<Monster>();
        private int m_Level, m_Attack, m_HP, m_MaxHP;
        private int m_TextureID;

        public Monster(int level, ref List<Monster> monsterlist)
        {
            m_Level = level;
            m_Attack = m_Level;
            m_MaxHP = m_Level * 10;
            m_HP = m_MaxHP;

            m_XPosition = 740.0f;

            m_TextureID = new Random(System.DateTime.Now.Millisecond).Next(0, 0);

            m_Monsters = monsterlist;
        }

        public void Update(float speed)
        {
            m_XPosition -= speed;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D monstertexture, Texture2D guitexture, Texture2D hptexture, SpriteFont levelfont)
        {
            float hpwidth = (float)hptexture.Width * ((float)m_HP / (float)m_MaxHP);

            spriteBatch.Draw(monstertexture, new Vector2(m_XPosition, 170.0f), Color.White);
            spriteBatch.Draw(hptexture, new Rectangle((int)m_XPosition + 116, 432, (int)hpwidth, hptexture.Height), new Rectangle(0, 0, (int)hpwidth, hptexture.Height), Color.White);
            spriteBatch.Draw(guitexture, new Vector2(m_XPosition + 55.0f, 425.0f), Color.White);
            spriteBatch.DrawString(levelfont, m_Level.ToString("00"), new Vector2(m_XPosition + 92.0f, 429.0f), Color.Cyan);
        }

        public int GetTextureID() { return m_TextureID; }
        public float GetXPosition() { return m_XPosition; }

        public void TakeDamage(int damage)
        {
            m_HP -= damage;
        }

        public bool IsDead()
        {
            if (m_HP <= 0)
            {
                m_Monsters.Remove(this);
                return true;
            }

            return false;
        }
    }
}
