using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gluttonous_Snake_Rhino;

namespace Gluttonous_Snake_Rhino
{
    public partial class ControlForm : Form
    {

        public GluttonousSnakeRhinoComponent Component;
        

        public ControlForm(GluttonousSnakeRhinoComponent component)
        {
            InitializeComponent();

            Component = component;
        }


        private void UpBtn_Click(object sender, EventArgs e)
        {
            Component.NowDirection = Direction.Up;
        }

        private void DownBtn_Click(object sender, EventArgs e)
        {

            Component.NowDirection = Direction.Down;

        }

        private void LeftBtn_Click(object sender, EventArgs e)
        {
            Component.NowDirection = Direction.Left;

        }

        private void RightBtn_Click(object sender, EventArgs e)
        {
            Component.NowDirection = Direction.Right;

        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            Component.NowDirection = Direction.Forward;

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Component.NowDirection = Direction.Back;

        }
    }
}
