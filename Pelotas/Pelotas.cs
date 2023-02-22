using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Pelotas
{
    public partial class Pelotas : Form
    {
        static List<Pelota> balls;
        static Bitmap bmp;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;

        public Pelotas()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;

            balls       = new List<Pelota>();
            bmp         = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g           = Graphics.FromImage(bmp);
            deltaTime   = 1;
            PCT_CANVAS.Image = bmp;

            for (int b = 0; b < 16; b++)
                balls.Add(new Pelota(rand, PCT_CANVAS.Size, b));
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
              g.Clear(Color.Transparent);
            // RectangleF srcRect = new RectangleF(10.0f, 160.0f, 160.0f);
             GraphicsUnit unit = GraphicsUnit.Pixel;


            Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
            {
                Pelota P;
                balls[b].Update(deltaTime, balls);
                P = balls[b];               
            });

            Pelota p;
            for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];
                g.DrawImage(Resource1.burbu,p.x,p.y);
            }

            PCT_CANVAS.Invalidate();
            deltaTime += .1f;
        }
    }
}
