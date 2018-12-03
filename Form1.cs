using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace BranchAndBound
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int vertecesCount = 0;
        public int cellsNumber = 0;

        static int d = (int)DateTime.Now.Ticks;
        Random rnd = new Random(d);

        private void Form1_Load(object sender, EventArgs e)
        {
            increaseVertex_ValueChanged(sender, e);
        }

        private void increaseVertex_ValueChanged(object sender, EventArgs e)
        {

            vertecesCount = (int)increaseVertex.Value;
            cellsNumber = (int)increaseVertex.Value + 1;

            gridMatrix.ColumnCount = cellsNumber;
            gridMatrix.RowCount = cellsNumber;

            gridMatrix.Rows[0].Cells[0].Style.BackColor = SystemColors.AppWorkspace;
            gridMatrix.Rows[0].Cells[0].Style.ForeColor = SystemColors.AppWorkspace;

            for (int i = 1; i < cellsNumber; i++)
            {
                gridMatrix[0, i].Value = i;
                gridMatrix[0, i].Style.BackColor = Color.LightGray;
                gridMatrix[i, 0].Value = gridMatrix[0, i].Value;
                gridMatrix[i, 0].Style.BackColor = gridMatrix[0, i].Style.BackColor;
            }

            //заполнение диагонали
            for (int i = 1; i < cellsNumber; i++)
            {
                gridMatrix[i, i].Value = 0;
                gridMatrix[i, i].Style.BackColor = Color.Bisque;
            }
        }

        private void gridMatrix_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //запрет на изменение значений по диагонали
            if (e.RowIndex == e.ColumnIndex)
                e.Cancel = true;

            //запрет на редактирование ячеек
            //отвечающих за нумерацию строк и столбцов
            for (int i = 1; i < cellsNumber; i++)
            {
                if (e.RowIndex == 0 && e.ColumnIndex == i || (e.RowIndex == i && e.ColumnIndex == 0))
                    e.Cancel = true;
            }
        }

        private void gridMatrix_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }


        }

        private void gridMatrix_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridMatrix.BeginEdit(true);
        }

        private void btnRandomly_Click(object sender, EventArgs e)
        {
            vertecesCount = (int)increaseVertex.Value;
            cellsNumber = (int)increaseVertex.Value + 1;

            gridMatrix.ColumnCount = cellsNumber;
            gridMatrix.RowCount = cellsNumber;

            gridMatrix.Rows[0].Cells[0].Style.BackColor = SystemColors.AppWorkspace;
            gridMatrix.Rows[0].Cells[0].Style.ForeColor = SystemColors.AppWorkspace;

            //нумерация строк и столбцов
            for (int i = 1; i < cellsNumber; i++)
            {
                gridMatrix[0, i].Value = i;
                gridMatrix[0, i].Style.BackColor = Color.LightGray;
                gridMatrix[i, 0].Value = gridMatrix[0, i].Value;
                gridMatrix[i, 0].Style.BackColor = gridMatrix[0, i].Style.BackColor;
            }

            //заполнение диагонали
            for (int i = 1; i < cellsNumber; i++)
            {
                gridMatrix[i, i].Value = 0;
                gridMatrix[i, i].Style.BackColor = Color.Bisque;
            }

            //случайное заполнение матрицы
            for (int i = 1; i < cellsNumber; i++)
            {
                for (int j = 1; j < cellsNumber; j++)
                {
                    if (i == j)
                        gridMatrix[i, i].Value = 0;
                    else
                        gridMatrix[i, j].Value = rnd.Next(1, 21);
                }
            }
        }

        private void btnPathFind_Click(object sender, EventArgs e)
        {}

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }
    }
}
