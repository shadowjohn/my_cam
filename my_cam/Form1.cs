using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using utility;
using System.Diagnostics;
using System.Windows;
using Accord.Video.FFMPEG;
using System.Drawing.Imaging;
using AviFile;
using NAudio.Wave;
using System.Collections;
//From : https://stackoverflow.com/questions/18812224/c-sharp-recording-audio-from-soundcard
//using CSCore;
//using CSCore.SoundIn;
//using CSCore.Codecs.WAV;

namespace my_cam
{


    public partial class Form1 : Form
    {
        class VD
        {
            public Bitmap bitmap;
            public TimeSpan timespan;
            public bool isLastSec;
        }
        class VideoOption
        {
            public ArrayList bps = new ArrayList();
            public int V_t = 0;
            public int V_l = 0;
            public int V_w = 0;
            public int V_h = 0;
        }
        public bool write_finish = false;
        public WasapiLoopbackCapture system_audio = null;  //WasapiLoopbackCapture      
        public WaveFileWriter waveFile = null;
        static VideoOption videoOption = new VideoOption();
        public DateTime RecordingStartTime { get; private set; }
        public TimeSpan RecordingDuration { get; private set; }
        bool isNeedAudio = true;
        bool isRecording = false;
        string version = "1.0"; //版本說明
        static Accord.Video.FFMPEG.VideoFileWriter writer = new VideoFileWriter();

        private string mn = "";
        private string audio_path = "";
        private string video_path = "";
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = null;


        private static IntPtr _hookID = IntPtr.Zero;
        private static Button r_btn = null;
        // [DllImport("winmm.dll")]
        //private static extern long mciSendString(string command, StringBuilder retstring, int Returnlength, IntPtr callback);
        /*public static void Main()

        {

            _hookID = SetHook(_proc);

            Application.Run();

            UnhookWindowsHookEx(_hookID);

        }
        */
        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }

        private delegate IntPtr LowLevelKeyboardProc(

            int nCode, IntPtr wParam, IntPtr lParam);

        IntPtr HookCallback(

           int nCode, IntPtr wParam, IntPtr lParam)

        {

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)

            {

                int vkCode = Marshal.ReadInt32(lParam);

                //Console.WriteLine((Keys)vkCode);
                switch (((Keys)vkCode).ToString())
                {
                    case "F2":
                        {
                            Run_Btn_Click(new Object(), new EventArgs());
                        }
                        break;
                    case "F3":
                        {
                            GoToDir_Btn_Click(new Object(), new EventArgs());
                        }
                        break;
                    case "F8":
                        {
                            HelpMe_Btn_Click(new Object(), new EventArgs());
                        }
                        break;
                }

            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);
        Thread sc = null;
        Thread msc = null;
        static Form2 f2 = null;
        myinclude my = new myinclude();


        static public void show_hide_f2(bool show)
        {
            switch (show)
            {
                case false:
                    f2.lt.Hide();
                    f2.t.Hide();
                    f2.rt.Hide();
                    f2.l.Hide();
                    f2.r.Hide();
                    f2.c.Hide();
                    f2.C_txt.Hide();
                    f2.l.Hide();
                    f2.lb.Hide();
                    f2.b.Hide();
                    f2.rb.Hide();
                    break;
                default:
                    f2.lt.Show();
                    f2.t.Show();
                    f2.rt.Show();
                    f2.l.Show();
                    f2.r.Show();
                    f2.c.Show();
                    f2.C_txt.Show();
                    f2.l.Show();
                    f2.lb.Show();
                    f2.b.Show();
                    f2.rb.Show();
                    break;
            };
        }
        public void thread_msc()
        {
            while(true)
            {
                //Thread.Sleep(1000);
                Thread.Sleep(10);                
                for (int i = 0; i < videoOption.bps.Count; i++)
                {
                    VD vd = (VD)videoOption.bps[i];
                    writer.WriteVideoFrame(vd.bitmap, vd.timespan);                                                 
                }
                videoOption.bps.RemoveRange(0, videoOption.bps.Count);
                //GC.Collect();

                if (isRecording==false)
                {

                    //GC.Collect();
                    write_finish = true;
                    break;
                }
                
            }
          
        }
        public void thread_sc()
        {
            int step = 0;
            //audio.Start();

            DateTime dt = DateTime.Now;
            bool is_first = true;
            TimeSpan timespan;
            Bitmap bitmap;
            while (true)
            {
                step++;
                if(step>=30)
                {
                    step = 0;
                    //GC.Collect();
                }
                if (isRecording == false)
                {
                    write_finish = true;
                    sc.Abort();
                    return;
                }
                                               
                
                if (is_first)
                {
                    timespan = TimeSpan.Zero;
                    is_first = false;
                }
                else
                {
                    timespan = DateTime.Now - dt;
                }
                //vd.bitmap = CaptureScreen(true, videoOption.V_l, videoOption.V_t, videoOption.V_w, videoOption.V_h);

                bitmap = CaptureScreen(true, videoOption.V_l, videoOption.V_t, videoOption.V_w, videoOption.V_h);
                if(bitmap == null)
                {
                    Thread.Sleep(15);
                    
                    continue;
                }
                writer.WriteVideoFrame(bitmap, timespan);
               
                //videoOption.bps.Add(vd);
                Thread.Sleep(15);
            }
        }

        public Form1()
        {
            InitializeComponent();

        }
        private void alert(string data)
        {
            MessageBox.Show(data);
        }
        private void alert(int data)
        {
            MessageBox.Show(string.Format("{0}", data));
        }
        private void log(string data)
        {
            Console.WriteLine(data);
        }
        private void log(int data)
        {
            Console.WriteLine(data);
        }
        private void log(Size data)
        {
            Console.WriteLine(data.Width + "," + data.Height);
        }

        /* private void run_f2()
         {
             f2 = new Form2();
             f2.InitializeComponent();
             f2.Show();
             f2.TopMost=true;
             f2.Load += new System.EventHandler();


         }
         */

        private void Form1_Load(object sender, EventArgs e)
        {

            //AviManager aviManager = new AviManager(@"E:\5.program\C#\my_cam_old\my_cam\bin\Debug\video\2019-06-23_20_08_40.avi", true);

            //添加音频
            //String fileName = @"E:\5.program\C#\my_cam_old\my_cam\bin\Debug\video\aaa.wav";
            //aviManager.AddAudioStream(fileName, 0);
            //aviManager.Close();

            //AviManager aviManager = new AviManager(@"E:\5.program\C#\my_cam_old\my_cam\bin\Debug\video\2019-06-23_20_41_08.avi", true);
            //aviManager.AddAudioStream(@"E:\5.program\C#\my_cam_old\my_cam\bin\Debug\video\2019-06-23_20_41_08.wav", 0);
            //aviManager.Close();


            _proc = HookCallback;
            r_btn = this.run_btn;
            // From : https://dotblogs.com.tw/huanlin/2008/04/23/3320
            // From : https://dotblogs.com.tw/huanlin/2008/04/23/3319
            _hookID = SetHook(_proc);


            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 120;
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height - 120;

            //f2_t = new Thread(run_f2);
            //f2_t.Start();
            f2 = new Form2();
            f2.Show();
            f2.UI_Init();

            video_path = my.pwd() + "\\video";
            log(video_path);
            if (!my.is_dir(video_path))
            {
                my.mkdir(video_path);
            }
            mn = my.date("Y-m-d_H_i_s");
            video_path = my.pwd() + "\\video\\" + mn + ".avi";
            audio_path = my.pwd() + "\\video\\" + mn + ".wav";
            log(video_path);
            log(audio_path);



        }
        public void renew_path()
        {
            mn = my.date("Y-m-d_H_i_s");
            video_path = my.pwd() + "\\video\\" + mn + ".avi";
            audio_path = my.pwd() + "\\video\\" + mn + ".wav";
            log(video_path);
            log(audio_path);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("你確定要離開嗎.", "離開", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                // Do something 
                if (sc != null)
                {
                    sc.Abort();
                }
                GC.Collect();
                //Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }
        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);
        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        const Int32 CURSOR_SHOWING = 0x00000001;
        public static Bitmap CaptureScreen(bool CaptureMouse, int l, int t, int w, int h)
        {
            //Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height

            Bitmap result = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            try
            {
                
                using (Graphics g = Graphics.FromImage(result))
                {
                    //Screen.PrimaryScreen.Bounds.Size
                    g.CopyFromScreen(l, t, 0, 0, new Size(w, h), CopyPixelOperation.SourceCopy);

                    if (CaptureMouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                DrawIcon(g.GetHdc(), pci.ptScreenPos.x - l, pci.ptScreenPos.y - t, pci.hCursor);
                                g.ReleaseHdc();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = null;
            }
            //myinclude my = new myinclude();
            //result.Save("C:\\temp\\a.bmp");
            //Application.Exit();
            return result;
        }
        private void audioDevice_NewFrame(object sender, Accord.Audio.NewFrameEventArgs e)
        {
            //try
            //{

            writer.WriteAudioFrame(e.Signal);

            //}
            //catch (Exception ee)
            //{
            //}



        }
        public void Run_Btn_Click(object sender, EventArgs e)
        {
            isNeedAudio = isNeedAudio_Checkbox.Checked;
            switch (run_btn.Text)
            {
                case "開始錄影 (F2)":
                    write_finish = false;
                    renew_path();
                    isRecording = true;
                    run_btn.Text = "停止錄影 (F2)";
                    f2.can_drag = false;

                    videoOption.V_l = f2.Left;
                    videoOption.V_t = f2.Top;
                    videoOption.V_w = f2.Width;
                    videoOption.V_h = f2.Height;


                    int width = videoOption.V_w;
                    int height = videoOption.V_h;
                    //from https://en.code-bude.net/2013/04/17/how-to-create-video-files-in-c-from-single-images/
                    /*my.file_put_contents(video_path, "");*/

                    //writer.Open("C:\\temp\\a.avi", V_w, V_h);
                    show_hide_f2(false);
                    writer = new VideoFileWriter();
                    int w = Convert.ToInt32(Math.Ceiling(videoOption.V_w / 10.0)) * 10;
                    int h = Convert.ToInt32(Math.Ceiling(videoOption.V_h / 10.0)) * 10;
                    int videoBitRate = w*h*3*30;
                    //int videoBitRate = 1200 * 1000;
                    //int audioBitRate = 320 * 1000;
                    if (isNeedAudio)
                    {

                        //audio = new AudioCaptureDevice();
                        //audio.Format = Accord.Audio.SampleFormat.Format16Bit;
                        //audio.SampleRate = 22050;// Accord.DirectSound.Accord.Audio.Tools.Default.SampleRate;
                        //audio.DesiredFrameSize = 4096;
                        //audio.NewFrame += audioDevice_NewFrame;
                        //audio.DesiredFrameSize = 4096;



                        //mciSendString("open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
                        //mciSendString("set capture time format ms bitspersample 16 channels 2 samplespersec 48000 bytespersec 192000 alignment 4", null, 0, IntPtr.Zero);
                        //mciSendString("record recsound", null, 0, IntPtr.Zero);
                        //alert("gg");
                        //writer.Open(video_path, w, h, 30, VideoCodec.H264, videoBitRate, AudioCodec.MP3, audioBitRate, 44100,1);
                        //writer.Open(video_path, w, h, 30, VideoCodec.H264, videoBitRate);
                        //audio = new WasapiLoopbackCapture();

                        //audio.Initialize();
                        //audio_w = new WaveWriter(audio_path, audio.WaveFormat);

                        //setup an eventhandler to receive the recorded data
                        //audio.DataAvailable += (s, ee) =>
                        // {
                        //save the recorded audio
                        //    audio_w.Write(ee.Data, ee.Offset, ee.ByteCount);
                        // };

                        //start recording
                        //audio.Start();
                        //Console.ReadKey();
                        system_audio = new WasapiLoopbackCapture();
                        //system_audio = new WasapiLoopbackCapture();

                        //wtf.DataAvailable
                        //waveSource.WaveFormat = new WaveFormat(44100, 2);

                        system_audio.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);

                        //system_audio.DataAvailable += (s, ee) =>
                        // {
                        //save the recorded audio
                        //    waveSource_DataAvailable(s, ee);
                        //audio_w.Write(ee.Data, ee.Offset, ee.ByteCount);
                        // }; 

                        system_audio.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

                        waveFile = new WaveFileWriter(audio_path, system_audio.WaveFormat);

                        system_audio.StartRecording();
                        // system_audio.StartRecording();
                    }

                    writer.Open(video_path, w, h, 30, VideoCodec.H264, videoBitRate);
                    //

                    //msc = new Thread(thread_msc);
                    // msc.Start();
                    timer1.Enabled=true;
                    sc = new Thread(thread_sc);
                    sc.Start();



                    break;
                default:
                    show_hide_f2(true);
                    run_btn.Text = "開始錄影 (F2)";
                    f2.can_drag = true;
                    isRecording = false;
                    if (isNeedAudio)
                    {
                        //mciSendString("save recsound " + audio_path, null, 0, IntPtr.Zero);
                        //mciSendString("close recsound", null, 0, IntPtr.Zero);
                        //https://github.com/accord-net/framework/issues/418
                        //byte[] bffr = File.ReadAllBytes(audio_path);
                        //writer.WriteAudioFrame()
                        //writer.WriteAudioFrame()
                        //
                        // audio.Stop();
                        // audio.Dispose();
                        //audio = null;
                        //audio_w.Dispose();
                        //audio_w = null;
                        //GC.Collect();
                        //audio.Dispose();
                        system_audio.StopRecording();
                        if (system_audio != null)
                        {
                            system_audio.Dispose();
                            system_audio = null;
                        }

                        if (waveFile != null)
                        {
                            waveFile.Dispose();
                            waveFile = null;
                        }
                        // system_audio.StopRecording();
                        //  system_audio.Dispose();
                        //my.copy(audio_path, _nt);

                        //sc.Abort();
                        //Thread.Sleep(1000);
                        //GC.Collect();
                        
                        while(write_finish==false)
                        {
                            Thread.Sleep(10);
                        }
                        writer.Close();
                        writer = null;

                        AviManager aviManager = new AviManager(video_path, true);
                        aviManager.AddAudioStream(audio_path, 0);
                        aviManager.Close();
                        timer1.Enabled = false;

                        my.unlink(audio_path);
                    }


                    break;
            }
        }
        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }
        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {

            if (system_audio != null)
            {
                system_audio.Dispose();
                system_audio = null;
            }

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)//縮小時
            {
                this.notifyIcon1.Visible = true;//顯示Icon
                this.Hide();//隱藏Form
                //if (ReadFileTimer.Enabled)//有執行時 眨眼
                //{
                //    Eye_timer.Enabled = true;//開始眨眼
                //    Eye_timer.Start();//開始眨眼
                //}
            }
            else//放大時
            {
                //this.notifyIcon1.Visible = false;//隱藏Icon
                //Eye_timer.Stop();//停止眨眼
                //Eye_timer.Enabled = false;//停止眨眼
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();//顯示Form
            this.WindowState = FormWindowState.Normal;//回到正常大小
            this.Activate();//焦點
            this.Focus();//焦點
        }

        private void HelpMe_Btn_Click(object sender, EventArgs e)
        {
            string message = @"
程式名稱：錄桌面機
開發者：羽山 (http://3wa.tw)
版本：" + version + @"
程式說明：
　　1、框好你想錄的螢幕範圍
　　2、按下「F2」就開始錄
　　3、再按一次「F2」就結束錄影
　　4、錄好的桌面影像，會放在本程式 Video 目錄下
";
            alert(message);
        }

        private void GoToDir_Btn_Click(object sender, EventArgs e)
        {
            string path = my.pwd() + "\\Video";
            my.system("explorer.exe \"" + path + "\"");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}
