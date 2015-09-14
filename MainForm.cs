using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int DEFAULT = 3;
        private int NUM_CELLS = DEFAULT;
        private const int GRID_OFFSET = 25;
        private const int GRID_LENGTH = 200;
        private const int NUM_CELLS_3X3 = 3;
        private const int NUM_CELLS_4X4 = 4;
        private const int NUM_CELLS_5X5 = 5;
        private int CELL_LENGTH = GRID_LENGTH / NUM_CELLS_3X3;

        private bool [,] grid;
        private Random rand;

        public MainForm()
        {
            InitializeComponent();

         
 
            rand = new Random();
            grid = new bool[NUM_CELLS, NUM_CELLS];

            // Turn entire grid on
            for (int r = 0; r < NUM_CELLS; r++)
              for (int c = 0; c < NUM_CELLS; c++)
                 grid[r, c] = true;
  
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            if (x3ToolStripMenuItem.Checked)
            {
                NUM_CELLS = DEFAULT;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];

            }

            if (x4ToolStripMenuItem.Checked)
            {
               
                NUM_CELLS = NUM_CELLS_4X4;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];
            }

            if (x5ToolStripMenuItem.Checked)
            {
                NUM_CELLS = NUM_CELLS_5X5;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];
            }
            

            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    Brush brush;
                    Pen pen;

                    if (grid[r,c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }
                  

                    int x = c * CELL_LENGTH + GRID_OFFSET;
                    int y = r * CELL_LENGTH + GRID_OFFSET;

                    g.DrawRectangle(pen, x, y, CELL_LENGTH, CELL_LENGTH);
                    g.FillRectangle(brush, x + 1, y + 1, CELL_LENGTH - 1, CELL_LENGTH - 1);

                    
                }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure click was inside the grid
            if (e.X < GRID_OFFSET || e.X > CELL_LENGTH * NUM_CELLS + GRID_OFFSET ||
            e.Y < GRID_OFFSET || e.Y > CELL_LENGTH * NUM_CELLS + GRID_OFFSET)
                return; 

            // Find row, col of mouse press
            int r = (e.Y - GRID_OFFSET) / CELL_LENGTH;
            int c = (e.X - GRID_OFFSET) / CELL_LENGTH; 

            for (int i = r-1; i < r+ 1; i++)
                for (int j = c-1; j < c+1; j++)
                {
                    if (i>= 0 && i < NUM_CELLS && j >= 0 && j <= NUM_CELLS)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }

            this.Invalidate();

            if (PlayerWon())
            {
                // Display winner dialog box
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private bool PlayerWon()
        {
            bool WinCondition = true;

            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    if (grid[r,c])
                    {
                        WinCondition = false;
                    }
                }
            return WinCondition;
        }

        

      

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameButton_MouseClick(sender, e);
        }

        private void NewGameButton_MouseClick(object sender, EventArgs e)
        {
            if (x3ToolStripMenuItem.Checked)
            {
                NUM_CELLS = DEFAULT;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];
                rand = new Random();

            }

            if (x4ToolStripMenuItem.Checked)
            {
                NUM_CELLS = NUM_CELLS_4X4;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];
                rand = new Random();
            }

            if (x5ToolStripMenuItem.Checked)
            {
                NUM_CELLS = NUM_CELLS_5X5;
                CELL_LENGTH = GRID_LENGTH / NUM_CELLS;
                grid = new bool[NUM_CELLS, NUM_CELLS];
                rand = new Random();
            }

            // Turn entire grid on
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = true;

            
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate(); 
            
        }

        private void ExitButton_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x3ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x4ToolStripMenuItem.Checked = true;
            x3ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x5ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x3ToolStripMenuItem.Checked = false;
        }

      


    }
}
