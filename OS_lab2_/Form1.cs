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

        private void button1_Click(object sender, EventArgs e) {
            if (t1.Status != TaskStatus.Running)
                t1.Start();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (t2.Status != TaskStatus.Running)
                t2.Start();
        }

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
            textBox_main.Invoke(new del(() => textBox_main.Text = ""));
            t1 = new Task(Bubble);
            t2 = new Task(BackBubble);
            t3 = new Task(APrint);
            t1_sync = new Task(BubbleSync);
            t2_sync = new Task(BackBubbleSync);
        }
    }
}

