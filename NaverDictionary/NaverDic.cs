using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace NaverDictionary
{
    public partial class NaverDic : Form
    {
        public NaverDic()
        {
            InitializeComponent();
        }

        private void NaverDic_Load(object sender, EventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = Properties.Resources.icon1;

            RegisterHotKey((int)this.Handle, 0, 0x2, (int)Keys.Space); //(여기로가져와, 니 ID는 0이야, 조합키안써, F5눌러지면)
            // 0x1은 알트, 0x2는 콘트롤, 0x3은 쉬프트

            webBrowser1.ScriptErrorsSuppressed = true;
            LoadWeb(1);
        }

        private void LoadWeb(int web)
        {
            string txtUrl = "";
            string title = "";
            int width = 0;
            int height = 0;

            //IniUtil ini = LoadINI();

            switch (web)
            {
                case 1:
                    title = Properties.Settings.Default.title1;
                    txtUrl = Properties.Settings.Default.url1;
                    width = Properties.Settings.Default.width1;
                    height = Properties.Settings.Default.height1;
                    //txtUrl = ini.GetIniValue("URL1", "url");
                    //width =  Convert.ToInt32(ini.GetIniValue("URL1", "width"));
                    //height = Convert.ToInt32(ini.GetIniValue("URL1", "width"));

                    //MessageBox.Show("[" + txtUrl + "][" + width + "][" + height);

                    break;
                case 2:
                    title = Properties.Settings.Default.title2;
                    txtUrl = Properties.Settings.Default.url2;
                    width = Properties.Settings.Default.width2;
                    height = Properties.Settings.Default.height2;
                    //txtUrl = ini.GetIniValue("URL2", "url");
                    //width =  Convert.ToInt32(ini.GetIniValue("URL2", "width"));
                    //height = Convert.ToInt32(ini.GetIniValue("URL2", "width"));
                    break;
            }

            this.Text = title;
            this.Width = width;
            this.Height = height;

            webBrowser1.Navigate(new Uri(txtUrl));
        }

        //핫키등록
        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(int hwnd, int id, int fsModifiers, int vk);

        //핫키제거
        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(int hwnd, int id);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);


        //private bool isActived = true;

        protected override void WndProc(ref Message m) //<span class="searchword">윈도우</span>프로시저 콜백함수
        {
            base.WndProc(ref m);

            if (m.Msg == (int)0x312) //핫키가 눌러지면 312 정수 메세지를 받게됨
            {
                if (Form.ActiveForm == null/*isActived == false*/)
                {
                    ShowDic();
                }
                else
                {
                    HideDic();
                }
                
            }
        }

        private void HideDic()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
        }

        private void ShowDic()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            SetForegroundWindow((int)this.Handle);
            webBrowser1.Focus();
            webBrowser1.Document.Focus();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDic();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
            //this.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowDic();
        }

        private void NaverDic_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    HideDic();
                    break;
                case CloseReason.ApplicationExitCall:
                case CloseReason.FormOwnerClosing:
                case CloseReason.MdiFormClosing:
                case CloseReason.None:
                case CloseReason.TaskManagerClosing:
                case CloseReason.WindowsShutDown:
                default:
                    notifyIcon1.Visible = false;
                    break;
            } 
        }

        private void NaverDic_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey((int)this.Handle, 0); //이 폼에 ID가 0인 핫키 해제
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    HideDic();
                    break;
                case Keys.F1:
                    MessageBox.Show("Ctrl + Space  :  Show / Hide\r\n" +
                                    "F1   :  Shortcuts\r\n" +
                                    "F2   :  Naver Dictionary\r\n" +
                                    "F3   :  Google Translator");
                    break;
                case Keys.F2:
                    LoadWeb(1);
                    break;
                case Keys.F3:
                    LoadWeb(2);
                    break;
            }
        }


    }

}
