using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HIK
{

    public delegate void fVoiceDataCallBack(int lVoiceComHandle, [MarshalAs(UnmanagedType.LPArray)]  byte[] pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

    public delegate void SerialDataCallBack(int lSerialHandle, [MarshalAs(UnmanagedType.LPArray)]  byte[] pRecvDataBuffer, uint dwBufSize, uint dwUser);

    public delegate void DrawFun(int lRealHandle, System.Drawing.Graphics hDc, uint dwUser);

    public delegate void RealDataCallBack(int lRealHandle, uint dwDataType, [MarshalAs(UnmanagedType.LPArray, SizeConst = 11520)] byte[] pBuffer, uint dwBufSize, uint dwUser);

    public delegate void PlayDataCallBack(int lPlayHandle, uint dwDataType, [MarshalAs(UnmanagedType.LPArray)]  byte[] pBuffer, uint dwBufSize, uint dwUser);

    public struct NET_DVR_DEVICEINFO
    {
        // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] sSerialNumber;
        public byte byAlarmInPortNum;  
        public byte byAlarmOutPortNum; 
        public byte byDiskNum;   
        public byte byDVRType;   
        public byte byChanNum;   
        public byte byStartChan;   
    }


    public struct NET_DVR_DISKSTATE
    {
        public uint dwVolume;
        public uint dwFreeSpace;
        public uint dwHardDiskStatic;
    }


    public struct NET_DVR_TIME
    {
        public uint dwYear;  
        public uint dwMonth;  
        public uint dwDay;  
        public uint dwHour; 
        public uint dwMinute; 
        public uint dwSecond;  
    }

    public struct NET_DVR_FIND_DATA
    {
        public string sFileName;
        public NET_DVR_TIME struStartTime;
        public NET_DVR_TIME struStopTime;
        public uint dwFileSize;
    }


    public struct NET_DVR_CHANNELSTATE
    {
        public byte byRecordStatic; 
        public byte bySignalStatic; 
        public byte byHardwareStatic;
        public char reservedData;
        public uint dwBitRate;
        public uint dwLinkNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]  //MAX_LINK
        public uint[] dwClientIP;
    }

    public struct NET_DVR_WORKSTATE
    {

        public uint dwDeviceStatic; 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public NET_DVR_DISKSTATE[] struHardDiskStatic; //MAX_DISKNUM

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public NET_DVR_CHANNELSTATE[] struChanStatic;//MAX_CHANNUM

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] byAlarmInStatic; //MAX_ALARMIN

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] byAlarmOutStatic; //MAX_ALARMOUT
        public uint dwLocalDisplay;
    }

    public struct NET_DVR_ETHERNET
    {
        public string sDVRIP;          
        public string sDVRIPMask;      
        public uint dwNetInterface;   
        public ushort wDVRPort; 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] byMACAddr; 
    }


    public struct NET_DVR_NETCFG
    {
        public uint dwSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public NET_DVR_ETHERNET[] struEtherNet;  
        public string sManageHostIP; 
        public short wManageHostPort; 
        public string sDNSIP;           
        public string sMultiCastIP;    
        public string sGatewayIP;      
        public string sNFSIP;   
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] sNFSDirectory;
        public uint dwPPPOE;  
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] sPPPoEUser; 
        //  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public string sPPPoEPassword;
        public string sPPPoEIP;  
        public ushort wHttpPort;    
    }

    public struct NET_DVR_ALARMOUTSTATUS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Output;//MAX_ALARMOUT
    }

    public struct NET_DVR_CLIENTINFO
    {
        public int lChannel;
        public int lLinkMode;
        public System.IntPtr hPlayWnd;
        public string sMultiCastIP;
    }

    /// <summary>
  
    /// </summary>
    public sealed class Hik_HCNetSDK
    {
        #region
        public const int NET_DVR_NOERROR = 0; 
        public const int NET_DVR_PASSWORD_ERROR = 1; 
        public const int NET_DVR_NOENOUGHPRI = 2; 
        public const int NET_DVR_NOINIT = 3; 
        public const int NET_DVR_CHANNEL_ERROR = 4;  
        public const int NET_DVR_OVER_MAXLINK = 5; 
        public const int NET_DVR_VERSIONNOMATCH = 6;
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;
        public const int NET_DVR_NETWORK_SEND_ERROR = 8; 
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10; 
        public const int NET_DVR_NETWORK_ERRORDATA = 11;
        public const int NET_DVR_ORDER_ERROR = 12;
        public const int NET_DVR_OPERNOPERMIT = 13;
        public const int NET_DVR_COMMANDTIMEOUT = 14;
        public const int NET_DVR_ERRORSERIALPORT = 15;
        public const int NET_DVR_ERRORALARMPORT = 16; 
        public const int NET_DVR_PARAMETER_ERROR = 17;
        public const int NET_DVR_CHAN_EXCEPTION = 18;
        public const int NET_DVR_NODISK = 19;
        public const int NET_DVR_ERRORDISKNUM = 20;
        public const int NET_DVR_DISK_FULL = 21;
        public const int NET_DVR_DISK_ERROR = 22;
        public const int NET_DVR_NOSUPPORT = 23; 
        public const int NET_DVR_BUSY = 24;
        public const int NET_DVR_MODIFY_FAIL = 25; 
        public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;
        public const int NET_DVR_DISK_FORMATING = 27; 
        public const int NET_DVR_DVRNORESOURCE = 28;
        public const int NET_DVR_DVROPRATEFAILED = 29;
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30; 
        public const int NET_DVR_DVRVOICEOPENED = 31;
        public const int NET_DVR_TIMEINPUTERROR = 32; 
        public const int NET_DVR_NOSPECFILE = 33; 
        public const int NET_DVR_CREATEFILE_ERROR = 34;
        public const int NET_DVR_FILEOPENFAIL = 35; 
        public const int NET_DVR_OPERNOTFINISH = 36; 
        public const int NET_DVR_GETPLAYTIMEFAIL = 37; 
        public const int NET_DVR_PLAYFAIL = 38; 
        public const int NET_DVR_FILEFORMAT_ERROR = 39; 
        public const int NET_DVR_DIR_ERROR = 40; 
        public const int NET_DVR_ALLOC_RESOUCE_ERROR = 41; 
        public const int NET_DVR_AUDIO_MODE_ERROR = 42; 
        public const int NET_DVR_NOENOUGH_BUF = 43;
        public const int NET_DVR_CREATESOCKET_ERROR = 44; 
        public const int NET_DVR_SETSOCKET_ERROR = 45;
        public const int NET_DVR_MAX_NUM = 46;
        public const int NET_DVR_USERNOTEXIST = 47;
        public const int NET_DVR_WRITEFLASHERROR = 48; 
        public const int NET_DVR_UPGRADEFAIL = 49;
        public const int NET_DVR_CARDHAVEINIT = 50;
        public const int NET_DVR_PLAYERFAILED = 51;
        public const int NET_DVR_MAX_USERNUM = 52; 
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53; 
        public const int NET_DVR_NOENCODEING = 54;
        public const int NET_DVR_IPMISMATCH = 55; 
        public const int NET_DVR_MACMISMATCH = 56;
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;
        public const int NET_DVR_DDRAWDEVICENOSUPPORT = 58;



        public const int NET_DVR_FILE_SUCCESS = 1000; 
        public const int NET_DVR_FILE_NOFIND = 1001;
        public const int NET_DVR_ISFINDING = 1002;
        public const int NET_DVR_NOMOREFILE = 1003; 
        public const int NET_DVR_FILE_EXCEPTION = 1004; 

        //NET_DVR_IsSupport()
       
        public const int NET_DVR_SUPPORT_DDRAW = 0x01;
        public const int NET_DVR_SUPPORT_BLT = 0x02;
        public const int NET_DVR_SUPPORT_BLTFOURCC = 0x04;
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 0x08;
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 0x10;
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 0x20;
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 0x40;
        public const int NET_DVR_SUPPORT_SSE = 0x80;
        public const int NET_DVR_SUPPORT_MMX = 0x100;

        public const int SET_PRESET = 8;
        public const int CLE_PRESET = 9; 
        public const int GOTO_PRESET = 39;

        public const int LIGHT_PWRON = 2; 
        public const int WIPER_PWRON = 3; 
        public const int FAN_PWRON = 4;
        public const int HEATER_PWRON = 5;
        public const int AUX_PWRON = 6;

        public const int ZOOM_IN = 11;
        public const int ZOOM_OUT = 12;
        public const int FOCUS_NEAR = 13; 
        public const int FOCUS_FAR = 14;
        public const int IRIS_OPEN = 15; 
        public const int IRIS_CLOSE = 16; 
        public const int TILT_UP = 21;
        public const int TILT_DOWN = 22;
        public const int PAN_LEFT = 23;
        public const int PAN_RIGHT = 24; 
        public const int PAN_AUTO = 29; 

        public const int RUN_CRUISE = 36; 
        public const int RUN_SEQ = 37; 
        public const int STOP_SEQ = 38;

   
        public enum DispMode { NORMALMODE = 0, OVERLAYMODE };
    
        public enum TransMode { PTOPTCPMODE, PTOPUDPMODE, MULTIMODE, RTPMODE, AUDIODETACH, NOUSEMODE };

        public const int NET_DVR_SYSHEAD = 1;
        public const int NET_DVR_STREAMDATA = 2; 

        // NET_DVR_PlayBackControl,NET_DVR_PlayControlLocDisplay,NET_DVR_DecPlayBackCtrl
        public const int NET_DVR_PLAYSTART = 1;
        public const int NET_DVR_PLAYSTOP = 2;
        public const int NET_DVR_PLAYPAUSE = 3;
        public const int NET_DVR_PLAYRESTART = 4;
        public const int NET_DVR_PLAYFAST = 5;
        public const int NET_DVR_PLAYSLOW = 6;
        public const int NET_DVR_PLAYNORMAL = 7;
        public const int NET_DVR_PLAYFRAME = 8;
        public const int NET_DVR_PLAYSTARTAUDIO = 9;
        public const int NET_DVR_PLAYSTOPAUDIO = 10;
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;
        public const int NET_DVR_PLAYSETPOS = 12;
        public const int NET_DVR_PLAYGETPOS = 13;
        public const int NET_DVR_PLAYGETTIME = 14;
        public const int NET_DVR_PLAYGETFRAME = 15;
        public const int NET_DVR_GETTOTALFRAMES = 16;
        public const int NET_DVR_GETTOTALTIME = 17;
        public const int NET_DVR_THROWBFRAME = 20;

        //NET_DVR_GetDVRConfig,NET_DVR_GetDVRConfig
        public const int NET_DVR_GET_DEVICECFG = 100;  
        public const int NET_DVR_SET_DEVICECFG = 101; 
        public const int NET_DVR_GET_NETCFG = 102; 
        public const int NET_DVR_SET_NETCFG = 103;
        public const int NET_DVR_GET_PICCFG = 104; 
        public const int NET_DVR_SET_PICCFG = 105; 
        public const int NET_DVR_GET_COMPRESSCFG = 106;
        public const int NET_DVR_SET_COMPRESSCFG = 107;
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204; 
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205; 
        public const int NET_DVR_GET_RECORDCFG = 108;
        public const int NET_DVR_SET_RECORDCFG = 109; 
        public const int NET_DVR_GET_DECODERCFG = 110; 
        public const int NET_DVR_SET_DECODERCFG = 111; 
        public const int NET_DVR_GET_RS232CFG = 112; 
        public const int NET_DVR_SET_RS232CFG = 113;
        public const int NET_DVR_GET_ALARMINCFG = 114; 
        public const int NET_DVR_SET_ALARMINCFG = 115; 
        public const int NET_DVR_GET_ALARMOUTCFG = 116; 
        public const int NET_DVR_SET_ALARMOUTCFG = 117; 
        public const int NET_DVR_GET_TIMECFG = 118;
        public const int NET_DVR_SET_TIMECFG = 119; 
        public const int NET_DVR_GET_PREVIEWCFG = 120;
        public const int NET_DVR_SET_PREVIEWCFG = 121; 
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;
        public const int NET_DVR_SET_VIDEOOUTCFG = 123; 
        public const int NET_DVR_GET_USERCFG = 124; 
        public const int NET_DVR_SET_USERCFG = 125; 
        public const int NET_DVR_GET_EXCEPTIONCFG = 126; 
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;  
        public const int NET_DVR_GET_SHOWSTRING = 130;
        public const int NET_DVR_SET_SHOWSTRING = 131; 
        public const int NET_DVR_GET_AUXOUTCFG = 140; 
        public const int NET_DVR_SET_AUXOUTCFG = 141;  
    
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142; 
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143; 
        public const int NET_DVR_GET_PICCFG_EX = 200;
        public const int NET_DVR_SET_PICCFG_EX = 201; 
        public const int NET_DVR_GET_USERCFG_EX = 202; 
        public const int NET_DVR_SET_USERCFG_EX = 203; 

     
        public const int COMM_ALARM = 0x1100;
        public const int COMM_TRADEINFO = 0x1500;

       
        public const int EXCEPTION_AUDIOEXCHANGE = 0x8001;
        public const int EXCEPTION_ALARM = 0x8002;
        public const int EXCEPTION_PREVIEW = 0x8003; 
        public const int EXCEPTION_SERIAL = 0x8004;
        public const int EXCEPTION_RECONNECT = 0x8005;

        public const int NAME_LEN = 32;
        public const int SERIALNO_LEN = 48;
        public const int MACADDR_LEN = 6;
        public const int MAX_ETHERNET = 2;
        public const int PATHNAME_LEN = 128;
        public const int PASSWD_LEN = 16;
        public const int MAX_CHANNUM = 16;
        public const int MAX_ALARMOUT = 4;
        public const int MAX_TIMESEGMENT = 4;
        public const int MAX_PRESET = 128;
        public const int MAX_DAYS = 7;
        public const int PHONENUMBER_LEN = 32;
        public const int MAX_DISKNUM = 16;
        public const int MAX_WINDOW = 16;
        public const int MAX_VGA = 1;
        public const int MAX_USERNUM = 16;
        public const int MAX_EXCEPTIONNUM = 16;
        public const int MAX_LINK = 6;
        public const int MAX_ALARMIN = 16;
        public const int MAX_VIDEOOUT = 2;
        public const int MAX_NAMELEN = 16; 
        public const int MAX_RIGHT = 32;
        public const int CARDNUM_LEN = 20;
        public const int MAX_SHELTERNUM = 4;
        public const int MAX_DECPOOLNUM = 4;
        public const int MAX_DECNUM = 4;
        public const int MAX_TRANSPARENTNUM = 2;
        public const int MAX_STRINGNUM = 4;
        public const int MAX_AUXOUT = 4;

  
        public const int NET_IF_10M_HALF = 1;    /* 10M ethernet */
        public const int NET_IF_10M_FULL = 2;
        public const int NET_IF_100M_HALF = 3;    /* 100M ethernet */
        public const int NET_IF_100M_FULL = 4;
        public const int NET_IF_AUTO = 5;

      
        public const int DVR = 1;
        public const int ATMDVR = 2;
        public const int DVS = 3;
        public const int DEC = 4; /* 6001D */
        public const int ENC_DEC = 5; /* 6001F */
        public const int DVR_HC = 6;
        public const int DVR_HT = 7;
        public const int DVR_HF = 8;
        public const int DVR_HS = 9;
        public const int DVR_HTS = 10;
        public const int DVR_HB = 11;
        public const int DVR_HCS = 12;
        public const int DVS_A = 13;
        public const int DVR_HC_S = 14;
        public const int DVR_HT_S = 15;
        public const int DVR_HF_S = 16;
        public const int DVR_HS_S = 17;
        public const int ATMDVR_S = 18;
        public const int LOWCOST_DVR = 19;
        #endregion

        //  public static readonly  int  SERIALNO_LEN = 48;


        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();

        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, ref  NET_DVR_DEVICEINFO lpDeviceInfo);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(int lUserID);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, System.IntPtr hWnd);
        //public static extern bool NET_DVR_SetDVRMessCallBack(BOOL (CALLBACK *fMessCallBack)(int  lCommand,char *sDVRIP,char *pBuf,uint  dwBufLen));
        //public static extern bool NET_DVR_SetDVRMessCallBack_EX(BOOL (CALLBACK *fMessCallBack_EX)(int  lCommand,int  lUserID,char *pBuf,uint  dwBufLen));
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);
        [DllImport("HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKVersion();
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_IsSupport();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartListen(string sLocalIP, ushort wLocalPort);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen();

        [DllImport("HCNetSDK.dll")]
        public static extern uint NET_DVR_GetLastError();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetShowMode(uint dwShowType, System.Drawing.Color colorKey);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr(string sServerIP, ushort wServerPort, string sDVRName, ushort wDVRNameLen, string sDVRSerialNumber, ushort wDVRSerialLen, string sGetIP);

        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay(int lUserID, ref NET_DVR_CLIENTINFO lpClientInfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int lRealHandle);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(int lRealHandle, uint pBrightValue, uint pContrastValue, uint pSaturationValue, uint pHueValue);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterDrawFun(int lRealHandle, DrawFun x, uint dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayerBufNumber(int lRealHandle, uint dwBufNum);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ThrowBFrame(int lRealHandle, uint dwNum);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(uint dwMode);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(int lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(int lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(int lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(int lRealHandle, ushort wVolume);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData(int lRealHandle, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopSaveRealData(int lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(int lRealHandle, RealDataCallBack x, uint dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture(int lRealHandle, string sPicFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(int lUserID, int lChannel);
   
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(int lRealHandle, uint dwPTZCommand, uint dwStop);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(int lRealHandle, byte[] pPTZCodeBuf, uint dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(int lUserID, int lChannel, byte[] pPTZCodeBuf, uint dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_EX(int lRealHandle, byte[] pPTZCodeBuf, uint dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_EX(int lRealHandle, uint dwPTZCommand, uint dwStop);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_EX(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_EX(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_EX(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop, uint dwSpeed);

     
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile(int lFindHandle, ref NET_DVR_FIND_DATA lpFindData);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(int lFindHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, System.IntPtr hWnd);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, System.IntPtr hWnd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, uint lpOutValue);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(int lPlayHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(int lPlayHandle, PlayDataCallBack x, uint dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(int lPlayHandle, ref NET_DVR_TIME lpOsdTime);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);
      
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(int lUserID);
  
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);
     
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);
    
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade(int lUserID, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(int lUpgradeHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(int lUpgradeHandle);
    
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, int pCurrentFormatDisk, int pCurrentDiskPos, int pFormatStatic);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);
       
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan(int lUserID);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan(int lAlarmHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut(int lUserID, ref NET_DVR_ALARMOUTSTATUS lpAlarmOutState);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);




      
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom(int lUserID, fVoiceDataCallBack x, uint dwUser);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(int lVoiceComHandle);
     
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_AddDVR(int lUserID);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR(int lUserID);
       
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartVoiceCom_MR(int lUserID, fVoiceDataCallBack x, uint dwUser);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, byte[] pSendBuf, uint dwBufSize);


        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_SerialStart(int lUserID, int lSerialPort, SerialDataCallBack x, uint dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, byte[] pSendBuf, uint dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, byte[] pSendBuf, uint dwBufSize);
     
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(int lUserID, int lKeyIndex);


        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID, int lChannel, int lRecordType);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID, int lChannel);
    }
}
