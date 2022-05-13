using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_lab2_ {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        int[] bubbleSortMin(int[] _arr, int N) {
            for (int i = 0; i < N; i++) {
                int min = _arr[i];
                for (int j = i; j < N; j++) {
                    if (_arr[j] < min) {
                        min = _arr[j];
                        int tmp = _arr[i];
                        _arr[i] = _arr[j];
                        _arr[j] = tmp;
                    }
                }
            }
            return _arr;
        }
        int[] bubbleSortMax(int[] _arr, int N) {
            for (int i = 0; i < N; i++) {
                int max = _arr[i];
                for (int j = i; j < N; j++) {
                    if (_arr[j] > max) {
                        max = _arr[j];
                        int tmp = _arr[i];
                        _arr[i] = _arr[j];
                        _arr[j] = tmp;
                    }
                }
            }
            return _arr;
        }

        private void button1_Click(object sender, EventArgs e) {
            string[] tmp = InputBox.Text.Split(' ');
            int[] arr = new int[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) {
                arr[i] = Convert.ToInt32(tmp[i]);
            }
            arr = bubbleSortMin(arr, arr.Length);
            richTextBox2.Clear();
            for (int i = 0; i < arr.Length; i++) {
                richTextBox2.Text += arr[i].ToString() + '\n';
            }
        }
        int[] arr;
        private void button2_Click(object sender, EventArgs e) {
            Thread thread1 = new Thread(threadFunc1);
            Thread thread2 = new Thread(threadFunc2);
            string[] tmp = InputBox.Text.Split(' ');
            if(arr != null) {
                arr = null;
            }
            arr = new int[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) {
                arr[i] = Convert.ToInt32(tmp[i]);
            }
            thread1.Start();
        }
        void printArr(int[] _arr) {

            this.richTextBox1.BeginInvoke((MethodInvoker)(() => this.richTextBox1.Clear()));
            for (int i1 = 0; i1 < _arr.Length; i1++) {
                this.richTextBox1.BeginInvoke((MethodInvoker)(() => this.richTextBox1.Text += arr[i1].ToString() + '\n'));
            }
        }
        void threadFunc1() {
            //arr = bubbleSortMax(arr, arr.Length);
            printArr(arr);
            //richTextBox1.Text += "123\n";
        }
        static void threadFunc2() { }
        //this.richTextBox1.BeginInvoke((MethodInvoker)(() => this.richTextBox1.Text += "text"));
    }
}
