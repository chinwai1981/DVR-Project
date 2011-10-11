using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NetDVR
{
    public partial class Form1 : Form
    {
        const int m_iTotalDevice = 2;
        ArrayList m_Devicehandle = null;
        ArrayList m_DeviceClientInfo = null;

        public Form1()
        {
            InitializeComponent();
        }


        private void MyRealDataCallBack(int lRealHandle, uint dwDataType, [MarshalAs(UnmanagedType.LPArray)]  byte[] pBuffer, uint dwBufSize, uint dwUser)
        {
            AddMessage("Received Data :" + pBuffer.Length + " " + dwBufSize + "");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            if (HIK.Hik_HCNetSDK.NET_DVR_Init())
            {
                AddMessage("Init Success");
                MessageBox.Show("Success");
                m_Devicehandle = new ArrayList();
                m_DeviceClientInfo = new ArrayList();
                StartUpDVR();

            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HIK.Hik_HCNetSDK.NET_DVR_Cleanup();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartUpDVR();
        }


        private void StartUpDVR()
        {
            try
            {
                m_Devicehandle.Add(AddDevices(txt_ip.Text, txt_port.Text));
                m_Devicehandle.Add(AddDevices(txt_ip2.Text, txt_port2.Text));

                m_DeviceClientInfo.Add(DisplayClient(panel1, Convert.ToInt32(m_Devicehandle[0])));
                m_DeviceClientInfo.Add(DisplayClient(panel2, Convert.ToInt32(m_Devicehandle[1])));


                HIK.RealDataCallBack x = new HIK.RealDataCallBack(MyRealDataCallBack);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private int AddDevices(string strIP, string strPort)
        {
            int iHandle = -1;
            HIK.NET_DVR_DEVICEINFO dev = new HIK.NET_DVR_DEVICEINFO();
            return iHandle = HIK.Hik_HCNetSDK.NET_DVR_Login(strIP, ushort.Parse(strPort), "admin", "12345", ref dev);
        }

        private int DisplayClient(Panel pnl, int iDeviceHandle)
        {
            HIK.NET_DVR_CLIENTINFO cli = new HIK.NET_DVR_CLIENTINFO();
            int iClientInfo = -1;
            cli.lChannel = 1;
            cli.lLinkMode = 0;
            cli.hPlayWnd = pnl.Handle;

            iClientInfo = HIK.Hik_HCNetSDK.NET_DVR_RealPlay(iDeviceHandle, ref cli);
            AddMessage("Start Stream +" + iClientInfo + "!");

            return iClientInfo;
        }



        private void btnStop_Click(object sender, EventArgs e)
        {

            int i = 0;
            for (i = 0; i < m_DeviceClientInfo.Count - 1; i++)
            {

                if (HIK.Hik_HCNetSDK.NET_DVR_StopRealPlay(Convert.ToInt32(m_DeviceClientInfo[i])) == true)
                    AddMessage(string.Format("Cam {0} Stop Streaming!", i));
            }


            int j = 0;
            for (j = 0; j < m_Devicehandle.Count - 1; i++)
            {
                if (HIK.Hik_HCNetSDK.NET_DVR_StopRealPlay(Convert.ToInt32(m_DeviceClientInfo[j])) == true)
                    AddMessage(string.Format("Cam {0} Successful Logout !", j));
            }


        }


        private void CapturePhoto()
        {
            //foreach (int iHandle in m_Devicehandle)
            //{ }
            HIK.Hik_HCNetSDK.NET_DVR_CapturePicture(Convert.ToInt32(m_Devicehandle[0]),
                string.Format("G:\\images\\cam1_{0}.bmp", GetDateString()));

            HIK.Hik_HCNetSDK.NET_DVR_CapturePicture(Convert.ToInt32(m_Devicehandle[1]),
                string.Format("G:\\images\\cam2_{0}.bmp", GetDateString()));
        }


        private string GetDateString()
        {
            return string.Format("{0}{1}{2}_{3}{4}{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        private void AddMessage(string strMsg)
        {
            this.txt_Msg.Items.Add(strMsg);
            this.txt_Msg.TopIndex = this.txt_Msg.Items.Count - 1;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            CapturePhoto();
        }



    }
}