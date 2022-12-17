using Chess.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class MarkableButtonPanel
    {
        private List<OptionsButton> panel;

        public MarkableButtonPanel()
        {
            panel = new List<OptionsButton>();
        }

        public int GetMarkedIndex()
        {
            for (int i = 0; i < panel.Count; i++)
            {
                if (panel[i].MarkedState == OptionButtonState.Marked)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetColor(int idx, Color clr)
        {
            panel[idx].Color = clr;
        }

        public void SetMarked(int idx)
        {
            if (idx >= panel.Count)
                throw new IndexOutOfRangeException();
            for (int i = 0; i < panel.Count; i++)
            {
                if (i != idx)
                {
                    panel[i].UnMark();
                }
            }
            panel[idx].Mark();
        }

        public void UnmarkAll()
        {
            for (int i = 0; i < panel.Count; i++)
            {
                panel[i].UnMark();
            }
        }

        public Button GetMarked()
        {
            int idx = GetMarkedIndex();
            if (idx == -1)
            {
                return panel[0];
            }
            return panel[idx];
        }

        public void Add(OptionsButton button)
        {
            panel.Add(button);
            panel[panel.Count - 1].AllowClickToUnmark = false;
        }

        public void Remove(OptionsButton ob)
        {
            panel.Remove(ob);
            UnmarkAll();
        }

        public OptionsButton this[int index]
        {
            get
            {
                if (panel.Count <= index)
                {
                    throw new IndexOutOfRangeException();
                }
                return panel[index];
            }
            set
            {
                if (panel.Count >= index)
                {
                    throw new IndexOutOfRangeException();
                }
                panel[index] = value;
                panel[index].AllowClickToUnmark = false;
            }
        }

        public int Count
        {
            get
            {
                return panel.Count;
            }
        }


        public void Update(Input current, Input previous)
        {
            int marked = -1;
            for (int i = 0; i < panel.Count; i++)
            {
                OptionsButton btn = panel[i];
                OptionButtonState state = btn.MarkedState;
                btn.Update(current, previous);
                if (btn.MarkedState != state)
                {
                    marked = i;
                }
            }
            if (marked != -1)
            {
                for (int i = 0; i < panel.Count; i++)
                {
                    if (i != marked)
                    {
                        panel[i].UnMark();
                    }
                }
            }

        }

        public void Update(MouseState current, Input previous)
        {
            int marked = -1;
            for (int i = 0; i < panel.Count; i++)
            {
                OptionsButton btn = panel[i];
                OptionButtonState state = btn.MarkedState;
                btn.Update(current, previous);
                if (btn.MarkedState != state)
                {
                    marked = i;
                }
            }
            if (marked != -1)
            {
                for (int i = 0; i < panel.Count; i++)
                {
                    if (i != marked)
                    {
                        panel[i].UnMark();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var btn in panel)
            {
                btn.Draw(spriteBatch);
            }
        }
    }
}
