using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkController.Plugin.Keyboard
{
    public partial class Gui : Form
    {
        public KeyboardDriverAbstracter Abstracter;
        

        public Gui(KeyboardDriverAbstracter abstracter)
        {
            Abstracter = abstracter;
            InitializeComponent();
        }

        private void Gui_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Abstracter != null) Abstracter.SetState(false);
            Abstracter = null;
        }


        private void Gui_KeyDown(object sender, KeyEventArgs e)
        {
            if (Abstracter != null) Abstracter.SetKeyState(e.KeyCode.ToString(), true);
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void Gui_KeyUp(object sender, KeyEventArgs e)
        {
            if (Abstracter != null) Abstracter.SetKeyState(e.KeyCode.ToString(), false);
            e.SuppressKeyPress = true;
            e.Handled = true;
        }
        
        private void Gui_Deactivate(object sender, EventArgs e)
        {
            if (Abstracter!= null) Abstracter.SetState(false);
        }

        private void Gui_Activated(object sender, EventArgs e)
        {
            if (Abstracter != null) Abstracter.SetState(true);
        }


    }
}
