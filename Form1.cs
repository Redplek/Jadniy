using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
             
            InitializeComponent();

            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = 2;

            dataGridView1.Rows[0].Cells[0].Value = "1065";
            dataGridView1.Rows[0].Cells[1].Value = "60";
            dataGridView1.Rows[1].Cells[0].Value = "1080";
            dataGridView1.Rows[1].Cells[1].Value = "20";
            dataGridView1.Rows[2].Cells[0].Value = "420";
            dataGridView1.Rows[2].Cells[1].Value = "40";

            textBox1.Text = "2150";           
            
        }

        private void mainthread()
        {
            this.Invoke(new Action(() => progressBar1.Visible = true));
            //progressBar1.Visible = true;
            this.Invoke(new Action(() => progressBar1.Minimum = 1));
            //progressBar1.Minimum = 1;
            //dataGridView2.Rows.Clear();
            int k = 0;
            int lin = dataGridView1.RowCount;
            List<int> matrix = new List<int>();
            for (int j = 0; j < lin; j++)
            {
                matrix.Add(Convert.ToInt32(dataGridView1.Rows[j].Cells[0].Value));   // получаем массив с длинами
            }

            List<int> matrix2 = new List<int>();
            for (int j = 0; j < lin; j++)
            {
                matrix2.Add(Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value));   // получаем массив количеств
            }
            /*сортировка пузырьком*/
            List<int> BubbleSort(List<int> A)
            {
                for (int i = 0; i < A.Count; i++)
                {
                    for (int j = 0; j < A.Count - i - 1; j++)
                    {
                        if (A[j] < A[j + 1])
                        {
                            int temp = A[j];
                            A[j] = A[j + 1];
                            A[j + 1] = temp;
                        }
                    }
                }
                return A;
            }
            /*сортировка пузырьком конец*/

            List<int> aArr = new List<int>();
            List<bool> bArr = new List<bool>();
            for (int i = 0; i < lin; i++)
            {
                if (matrix[i] > 0)
                {
                    for (int j = 0; j < matrix2[i]; j++)
                    {
                        aArr.Add(matrix[i]);
                        bArr.Add(false);
                        k = k + 1;
                    }
                }
            }
            aArr = BubbleSort(aArr);
            bool bFlag = false;
            int l = 0;
            int s = 0;
            int s1 = Convert.ToInt32(textBox1.Text);
            List<int> results = new List<int>();
            List<int> ostatok = new List<int>();
            List<int> kold = new List<int>();
            List<string> GLres = new List<string>();
            List<string> GLost = new List<string>();
            List<int>[] mas = new List<int>[k];
            //progressBar1.Maximum = k;
            this.Invoke(new Action(() => progressBar1.Maximum = k));
            this.Invoke(new Action(() => progressBar1.Value = 1));
            this.Invoke(new Action(() => progressBar1.Step = 1));
            //progressBar1.Value = 1;
            //progressBar1.Step = 1;
            //Debugger.Break();
            for (int p = 0; p < k; p++)
            {
                bFlag = false;
                l = 0;
                s = 0;
                results.Clear();
                ostatok.Clear();
                this.Invoke(new Action(() => progressBar1.PerformStep()));
               // progressBar1.PerformStep();
                for (int y = 0; y < bArr.Count; y++)
                {
                    bArr[y] = false;
                }
                if (p > 0)
                {
                    aArr = newaArr(aArr);
                }
                do
                {
                    //Debugger.Break();
                    ostatok.Add(s);
                    s = s1;
                    bFlag = false;
                    l = l + 1;
                    results.Add(-1);
                    for (int i = 0; i < k; i++)
                    {
                        if (s >= aArr[i] && bArr[i] == false)
                        {
                            s = s - aArr[i];
                            results.Add(aArr[i]);
                            bArr[i] = true;
                            bFlag = true;
                        }
                    }

                } while (bFlag && s < s1);
                // kold.Add(l);
                // Debugger.Break();               


                //label2.Text = Convert.ToString(l - 1);

                /*обработка и вывод результатов*/
                int q = 1;
                results.RemoveAt(results.Count - 1);
                //dataGridView2.RowCount = 1000;
                //dataGridView2.ColumnCount = 500;
                int sum = 0;
                foreach (int el in ostatok)
                {
                    sum = sum + el;
                }
                //dataGridView2.Rows[0].Cells[p].Value = 1;
                /* foreach (int el in results)
                 {
                     dataGridView2.Rows[q].Cells[p].Value = el;                    
                     q = q + 1;
                 }*/
                results.Add(l - 1);/*количество деталей*/
                results.Add(sum);/*сумма остатков*/
                                 // dataGridView2.Rows[q + 1].Cells[p].Value = "кол.д" + Convert.ToString(l - 1);
                                 // dataGridView2.Rows[q + 2].Cells[p].Value = "остаток" + Convert.ToString(sum);
                                 //Debugger.Break();                
                mas[p] = results.GetRange(0, results.Count);
                //Debugger.Break();
                //dataGridView2.Rows[0].Cells[1].Value = sum;
                /*обработка и вывод результатов конец*/
                //GLres.Add("кол.д" + Convert.ToString(l-1));
                //GLost.Add("остаток" + Convert.ToString(sum));
            }
            int min = int.MaxValue;
            int numb = 0;
            int list = 0;
            foreach (List<int> item in mas)
            {
                int u = item[item.Count - 2];
                //Debugger.Break();
                if (min > item[item.Count - 2])
                {
                    min = item[item.Count - 2];
                    list = numb;
                }
                numb = numb + 1;
            }
            //Debugger.Break();

            /*выбираем нужный список и убираем лишнее*/
            List<int> bestlist = mas[list];
            this.Invoke(new Action(() => label3.Text = Convert.ToString(bestlist[bestlist.Count - 2])));
            this.Invoke(new Action(() => label4.Text = Convert.ToString(bestlist[bestlist.Count - 1])));
            bestlist.Remove(bestlist[bestlist.Count - 2]);
            bestlist.Remove(bestlist[bestlist.Count - 1]);
            /*выбираем нужный список и убираем лишнее конец*/

            /*вывод списка*/
            /*int d = 0;
            foreach (int ele in bestlist)
            {
                this.Invoke(new Action(() => dataGridView3.Rows.Add()));
                this.Invoke(new Action(() => dataGridView3.Rows[d].Cells[0].Value = ele));
                d = d + 1;
            }*/
            /*вывод списка конец*/
            string best = "";
            foreach(int el in bestlist)
            {
                best += Convert.ToString(el) + " ";
            }
            string splitter = "<";/* символ по которому будем парсить*/
            best = best.Replace("-1", "<"); /*замена -1 на символ потому что по -1 парсить не хочет*/
            string[] words = best.Split(Convert.ToChar(splitter));
            List<string> srav = new List<string>();
            foreach(string word in words)
            {
                srav.Add(word);
            }
            srav.RemoveAt(0);
            /*ищем уникальные и не уникальные значения в списке*/
            List<string> uniq = new List<string>(); // список уникальных
            List<string> dbls = new List<string>(); // список неуникальных
            for (int i = 0; i < srav.Count; i++)
            {
                if (!uniq.Contains(srav[i]))
                { //если нет ни в одном списке  - заносится в уникальный
                    if (!dbls.Contains(srav[i])) { uniq.Add(srav[i]); }
                }
                else
                {  //если уже есть в списке уникальных
                   //- удаляется оттуда и вносится в список неуникальных но только один раз
                    uniq.Remove(srav[i]);
                    dbls.Add(srav[i]);
                }
            }
            /*ищем уникальные и не уникальные значения в списке конец*/
            foreach(string item in dbls)
            {
                uniq.Add(item);//склеиваем списки
            }
            List<string> resl = new List<string>();
            int counter = 0;
            for(int r = 0; r < uniq.Count; r++)
            {
                counter = 0;
                for(int u = 0; u < srav.Count; u++)
                {
                    if(uniq[r] == srav[u])
                    {
                        counter = counter + 1;
                    }
                }
                resl.Add(uniq[r] + 'x' + Convert.ToString(counter));
            }
            this.Invoke(new Action(() => dataGridView4.Rows.Clear()));
            int z = 0;
            foreach(string elem in resl)
            {
                this.Invoke(new Action(() => dataGridView4.Rows.Add()));
                this.Invoke(new Action(() => dataGridView4.Rows[z].Cells[0].Value = elem));
                z = z + 1;
            }            
            //Debugger.Break();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }       

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(mainthread);
            thread1.Start();
        }

        private List<int> newaArr(List<int> aArr2)/*сотрируем список по цыклу*/
        {
            var tmp = aArr2[aArr2.Count - 1];
            for (var i = aArr2.Count - 1; i != 0; --i) aArr2[i] = aArr2[i - 1];
            aArr2[0] = tmp;
            return aArr2;
        }        
        


        private void button2_Click(object sender, EventArgs e)
        {        

        }
    }
}
