using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex2_Version
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            setButton1();
            setButton2();
        }

        private void setButton1()
        {
            this.button1.Click += new System.EventHandler(this.button1Click);

            button1.Click += new EventHandler(delegate (object s, EventArgs e)
            {
                MessageBox.Show(" new EventHandler(  delegate(object s, EventArgs e) {}\n버튼1 클릭");
            });

            button1.Click += (EventHandler)delegate (object s, EventArgs e)
            {
                MessageBox.Show("(EventHandler)delegate(object s, EventArgs e) {}\n버튼1 클릭");
            };

            button1.Click += delegate (object s, EventArgs e)
            {
                MessageBox.Show("delegate(object s, EventArgs e) {}\n버튼1 클릭");
            };

            button1.Click += delegate
            {
                MessageBox.Show("delegate {}\n버튼1 클릭");
            };

            // this.Invoke( delegate { button1.Text = s; } );                               // 틀림: 컴파일 에러 발생

            MethodInvoker mi = new MethodInvoker(delegate () { button1.Text = "1"; });      // s; } );
            //this.Invoke(mi);                                                              // InvalidOperationException  : 창 핸들을 만들기 전까지 Invoke()나 BeginInvoke()를 실행할 수 없습니다.

            //this.Invoke((MethodInvoker)delegate () { button1.Text = "2"; });              //  s;} );  InvalidOperationException

            button1.Click += button1Click;
            button1.Click += (sender, e) => ((Button)sender).BackColor = Color.Red;         // 람다식
        }

        private void button1Click(object s, EventArgs e)
        {
            MessageBox.Show("new System.EventHandler(this.button1Click)\n버튼1 클릭");
            runThreads();
        }

        private void runThreads()
        {
            runThread();
            runUIThread();
            runContinueWith();
        }

        private void setButton2()
        {
            this.button2.Click += delegate (object s, EventArgs e)
            {
                MessageBox.Show("버튼2 클릭");
            };
        }
    }
}
