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
        private int m_Level, m_Attack, m_HP;
        private int m_TextureID;

        public Monster(int level, ref List<Monster> monsterlist)
        {
            m_Level = level;
            m_Attack = m_Level;
            m_HP = m_Level * 10;

            m_XPosition = 810.0f;

            m_TextureID = new Random(System.DateTime.Now.Millisecond).Next(0, 0);

            m_Monsters = monsterlist;
        }

        public void Update(float speed)
        {
            m_XPosition -= speed;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D monstertexture)
        {
            spriteBatch.Draw(monstertexture, new Vector2(m_XPosition, 170.0f), Color.White);
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
