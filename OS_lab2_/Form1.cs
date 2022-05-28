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
        Task t1;
        Task t2;
        Task t3;
        Task t1_sync;
        Task t2_sync;
        RichTextBox textBox_main;
        RichTextBox textBox;

        public delegate void del();

        int[] array = new int[10];
        bool sort1 = false;
        bool sort2 = false;
        private readonly object locker = new object();

        public Form1() {
            InitializeComponent();
            t1 = new Task(Bubble);
            t2 = new Task(BackBubble);
            t3 = new Task(APrint);
            t1_sync = new Task(BubbleSync);
            t2_sync = new Task(BackBubbleSync);

            textBox_main = this.richTextBox1;
            textBox = this.richTextBox2;

            Random random = new Random();
            string str = "";
            for (int i = 0; i < 10; i++) {
                array[i] = random.Next(-41, 41);
                str += ($"{array[i]} ");
            }
            str += "\n";
            textBox.Text += str;
            t3.Start();
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
            if (t1.Status != TaskStatus.Running)
                t1.Start();
            /*string[] tmp = InputBox.Text.Split(' ');
            int[] arr = new int[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) {
                arr[i] = Convert.ToInt32(tmp[i]);
            }
            arr = bubbleSortMin(arr, arr.Length);
            richTextBox2.Clear();
            for (int i = 0; i < arr.Length; i++) {
                richTextBox2.Text += arr[i].ToString() + '\n';
            }*/
        }
        int[] arr;
        private void button2_Click(object sender, EventArgs e) {
            if (t2.Status != TaskStatus.Running)
                t2.Start();
            /*Thread thread1 = new Thread(threadFunc1);
            Thread thread2 = new Thread(threadFunc2);
            string[] tmp = InputBox.Text.Split(' ');
            if(arr != null) {
                arr = null;
            }
            arr = new int[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) {
                arr[i] = Convert.ToInt32(tmp[i]);
            }
            thread1.Start();*/
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
        public void Print() {
            string str = "";
            for (int i = 0; i < array.Length; i++) {
                str += ($"{array[i]} ");
            }
            str += "\n";
            textBox_main.Invoke(new del(() => textBox_main.Text += str));
        }

        public void Bubble() {
            int t = 0;
            for (int p = 0; p <= array.Length - 2; p++) {
                for (int i = 0; i <= array.Length - 2; i++) {
                    if (array[i] > array[i + 1]) {
                        lock (locker) {
                            t = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = t;
                            sort1 = true;
                            Thread.Sleep(50);
                            //Print();
                        }
                    }
                }
            }
        }

        public void BackBubble() {
            int t = 0;
            for (int p = 0; p <= array.Length - 2; p++) {
                for (int i = 0; i <= array.Length - 2; i++) {
                    if (array[i] < array[i + 1]) {
                        lock (locker) {
                            t = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = t;
                            sort2 = true;
                            Thread.Sleep(50);
                            //Print();
                        }
                    }
                }
            }
        }

        public void BubbleSync() {
            int t = 0;
            lock (locker) {
                for (int p = 0; p <= array.Length - 2; p++) {
                    for (int i = 0; i <= array.Length - 2; i++) {
                        if (array[i] > array[i + 1]) {
                            t = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = t;
                            sort1 = true;
                            Thread.Sleep(10);
                        }
                    }
                }
            }
        }


        public void BackBubbleSync() {
            int t = 0;
            lock (locker) {
                for (int p = 0; p <= array.Length - 2; p++) {
                    for (int i = 0; i <= array.Length - 2; i++) {
                        if (array[i] < array[i + 1]) {
                            t = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = t;
                            sort2 = true;
                            Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        public void APrint() {
            while (true) {
                if (sort1 == true || sort2 == true) {
                    string text = sort1 == true ? "[1]:" : "[2]:";
                    textBox_main.Invoke(new del(() => textBox_main.Text += text));
                    Print();
                    sort1 = false;
                    sort2 = false;
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e) {
            if (t1.Status != TaskStatus.Running && t2.Status != TaskStatus.Running) {
                t1_sync.Start();
                t2_sync.Start();
            }

        }

        private void button4_Click(object sender, EventArgs e) {

        }
    }
}

