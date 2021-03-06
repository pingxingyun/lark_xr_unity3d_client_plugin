using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LarkXR
{
    public class XRApi
    {
        #region native structs.
        //
        // user config
        //
        public struct UserSetting
        {
            // bitrate. kbps. default to 30 * 1000 Kbps;
            public int bitrateKbps;
            // room hight.
            public float roomHight;
        }

        //
        // system info.
        //
        public enum SystemType
        {
            System_VR_HEADSET = 0,
            Systeim_Mobile,
            System_Other,
        }

        public enum PlatFromType
        {
            Platform_HTC_FOCUS = 0,
            Platform_HTC_FOCUS_PLUS = 1,
            Platform_PICO_NEO = 2,
            Platform_PICO_NEO_PLUS = 3,
            Platform_PICO_G2_4k = 4,
            Platform_DPVR_P1 = 5,
            Platform_Oculus_Quest = 6,
            Platform_GENERAL_WAVE = 1000,
        }

        public enum SDKVersion
        {
            SDK_HTC_WAVE = 0,
            SDK_OCULUS,
            SDK_GOOGLE_CARDBOARD = 1000,
        }

        public struct SystemInfo
        {
            public bool initd;
            public SystemType sysType;
            public PlatFromType platFromType;
            public SDKVersion sdkVersion;
            public string deviceName;
            public string macAddress;
            public string appVersion;
            public string supportServerVersion;
            public UInt64 diskTotal;
            public UInt64 diskUsed;
            public UInt64 sdTotal;
            public UInt64 sdUsed;
        }

        public struct DeviceMemInfo
        {
            public UInt64 memTotal;
            public UInt64 memUsed;
        }
        // end system typs.

        //
        // render info
        //
        public enum Eye
        {
            EYE_LEFT = 0,
            EYE_RIGHT = 1,
            EYE_COUNT = 2,
        }

        public struct RenderFov
        {
            public float left;
            public float right;
            public float top;
            public float bottom;
        }

        public struct RenderInfo
        {
            public int fps;
            public float ipd;
            // double eye.
            public int renderWidth;
            public int renderHeight;
            // ??????????????????????????????????????????
            public RenderFov fovLeft;
            public RenderFov fovRight;
        }
        // end render info

        //
        // device types.
        //
        // ????????????
        public enum DeviceType
        {
            // ??????
            Device_Type_HMD = 0,
            // 1????????????
            Device_Type_Controller_Left = 1,
            // 2????????????
            Device_Type_Controller_Right = 2,
        }
        public enum ControllerType
        {
            Controller_Left = DeviceType.Device_Type_Controller_Left,
            Controller_Right = DeviceType.Device_Type_Controller_Right
        }
        public enum PoseOriginModel
        {
            PoseOriginModel_OriginOnHead = 0, /**< The origin of 6 DoF pose is on head. */
            PoseOriginModel_OriginOnGround = 1, /**< The origin of 6 DoF pose is on ground. */
            PoseOriginModel_OriginOnTrackingObserver = 2, /**< The raw pose from tracking system. */
            PoseOriginModel_OriginOnHead_3DoF = 3, /**< The origin of 3 DoF pose is on head. */
        }
        public enum NumDoF
        {
            NumDoF_3DoF = 0,
            NumDoF_6DoF = 1,
        }
        public enum Intensity
        {
            Intensity_Weak = 1,   /**< The Intensity of vibrate is Weak. */
            Intensity_Light = 2,   /**< The Intensity of vibrate is Light. */
            Intensity_Normal = 3,   /**< The Intensity of vibrate is Normal. */
            Intensity_Strong = 4,   /**< The Intensity of vibrate is Strong. */
            Intensity_Severe = 5,   /**< The Intensity of vibrate is Severe. */
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct TrackedPose
        {
            public DeviceType device;
            public bool isConnected;
            public bool is6Dof;
            public bool isValidPose;
            public int status;
            public long timestamp;                  /**< Absolute time (in nanosecond) of pose. */
            public float predictedMilliSec;            /**< Number of milliseconds from now to predict poses. */
            public PoseOriginModel poseOriginModel;
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 velocity;
            public Vector3 angularVelocity;
            public Vector3 acceleration;
            public Vector3 angularAcceleration;
        }

        // C flags
        [Flags]
        public enum InputButtonFlag
        {
            larkxr_Input_System_Click = 0,
            larkxr_Input_Application_Menu_Click,
            larkxr_Input_Grip_Click,
            larkxr_Input_Grip_Value,
            larkxr_Input_Grip_Touch,
            larkxr_Input_DPad_Left_Click,
            larkxr_Input_DPad_Up_click,
            larkxr_Input_DPad_Right_Click,
            larkxr_Input_DPad_Down_Click,
            larkxr_Input_A_Click,
            larkxr_Input_A_Touch,
            larkxr_Input_B_Click,
            larkxr_Input_B_Touch,
            larkxr_Input_X_Click,
            larkxr_Input_X_Touch,
            larkxr_Input_Y_Click,
            larkxr_Input_Y_Touch,
            larkxr_Input_Trigger_Left_Value,
            larkxr_Input_Trigger_Right_Value,
            larkxr_Input_Shoulder_Left_Click,
            larkxr_Input_Shoulder_Right_Click,
            larkxr_Input_Joystick_Left_Click,
            larkxr_Input_Joystick_Left_X,
            larkxr_Input_Joystick_Left_Y,
            larkxr_Input_Joystick_Right_Click,
            larkxr_Input_Joystick_Right_X,
            larkxr_Input_Joystick_Right_Y,
            larkxr_Input_Joystick_Click,
            larkxr_Input_Joystick_X,
            larkxr_Input_Joystick_Y,
            larkxr_Input_Joystick_Touch,
            larkxr_Input_Back_Click,
            larkxr_Input_Guide_Click,
            larkxr_Input_Start_Click,
            larkxr_Input_Trigger_Click,
            larkxr_Input_Trigger_Value,
            larkxr_Input_Trigger_Touch,
            larkxr_Input_Trackpad_X,
            larkxr_Input_Trackpad_Y,
            larkxr_Input_Trackpad_Click,
            larkxr_Input_Trackpad_Touch,

            larkxr_Input_MAX = larkxr_Input_Trackpad_Touch,
            larkxr_Input_COUNT = larkxr_Input_MAX + 1
        };

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ControllerInputStateNative
        {
            public DeviceType deviceType;
            public bool isConnected;
            public UInt64 buttons;
            /// d          ! y = 1
            /// d          |
            /// d          |
            /// d  --------|--------> x 1
            /// d          |
            /// d          | -1
            /// d        openvr
            public Vector2 touchPadAxis;
            public float triggerValue; // trigger axis 0 - 1.0f
            public float gripValue;
            public uint batteryPercentRemaining;

            public void AddButtonState(InputButtonFlag flag)
            {
                buttons |= (UInt64)GetLarkXRButtonFlag(flag);
            }
        }
        // htc ????????????
        public struct ControllerInputState
        {
            public DeviceType deviceType;
            public bool isConnected;
            public bool touchPadPressed;
            public bool triggerPressed;
            public bool digitTriggerPressed;
            public bool appMenuPressed;
            public bool homePressed;
            public bool gripPressed;
            public bool volumUpPressed;
            public bool volumDownPressed;
            public bool touchPadTouched;
            // trigger axis 0 - 1.0f
            public float triggerAxis;
            /// d          ! x = -1
            /// d          |
            /// d          |
            /// d  --------|--------> x 1
            /// d          |
            /// d          | -1
            /// d        openvr
            public Vector2 touchPadAxis;
            public Vector2 axis1;
        }
        public struct ControllerDeviceState
        {
            public DeviceType deviceType;
            public TrackedPose pose;
            public ControllerInputStateNative inputState;
        }

        // ????????????
        public enum BatteryStatus
        {
            BatteryStatus_Unknown = 0,
            BatteryStatus_Normal = 1,
            BatteryStatus_Low = 2,
            BatteryStatus_UltraLow = 3,
        }

        public const short BATTERY_STATUS_NUMBER_NORMAL = 100;
        public const short BATTERY_STATUS_NUMBER_LOW = 50;
        public const short BATTERY_STATUS_NUMBER_ULTRALLOW = 10;

        public enum AsyncTaskType
        {
            Controller_Left = 0,
            Controller_Right = 1
        }

        public const short TOTAL_CONTROLLER_COUNT = 2;


        //
        // contexts
        //
        public struct TrackingFrame
        {
            public bool avaliable;
            public TrackedPose tracking;
            public UInt64 frameIndex;
            public UInt64 fetchTime;
            public double displayTime;
        }

        public enum FrameType
        {
            kNone = -1,
            kYUV420P,           ///< planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
            kRGB24,             ///< packed RGB 8:8:8, 24bpp, RGBRGB...
            kNV12,              ///< planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 plane for the UV components, which are interleaved (first byte U and the following byte V)
            kNV21,              ///<  as above, but U and V bytes are swapped
            kNative_Multiview,  /// android opengl multiview ??????????????????
            kNative_Stereo,     /// android opengl ???????????????????????????????????????????????????
            kNative_D3D11,      /// windows  d3d11 native texture ??????????????????
            kNative_D3D11_NV12, /// windows  d3d11 nv12 native texture ??????????????????
        }

        //
        // ??????????????????????????????
        //
        public enum HwRenderTextureType
        {
            // ???????????????
            larkxrHwRenderTextureType_None = -1,
            // Android gltexture2d ?????? ??????????????????
            larkxrHwRenderTextureType_Android_Multiview = 4,
            // android gltexture2d opengl ???????????????????????????????????????????????????
            larkxrHwRenderTextureType_Android_Stereo = 5,
            // windows d3d11 native texture ShaderResourceView ??????????????????
            larkxrHwRenderTextureType_D3D11_Multiview = 6,
            // windows d3d11 native texture ShaderResourceView ????????????
            larkxrHwRenderTextureType_D3D11_Stereo = 7,
            // windows d3d11 nv12 native texture ??????????????????
            larkxrHwRenderTextureType_D3D11_NV12 = 8,
            // windows d3d11 yuv texture, ShaderResourceView ??????
            // ?????? y ??? uv ?????????????????? nv12 ????????????????????????????????????
            larkxrHwRenderTextureType_D3D11_Y_UV_SRV = 9,
        }

        //
        // ???????????????????????????
        // 
        [StructLayout(LayoutKind.Sequential)]
        public struct HwRenderTexture
        {
            public HwRenderTextureType type;
            public IntPtr textureSlot1;
            public IntPtr textureSlot2;
            public int width;
            public int height;
        }

        //
        // ????????????
        // 
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ColorCorrention
        {
            public bool enableColorCorrection;      // true????????? ??????????????????(?????????false)
            public float brightness;                // ??????:??????[-1; 1],????????????0???-1???????????????,1???????????????
            public float contrast;                  // ?????????:??????[-1; 1],????????????0???-1??????????????????
            public float saturation;                // ?????????:??????[-1; 1],????????????0???-1?????????
            public float gamma;                     // ??????:??????[0; 5],????????????1????????????2.2?????????sRGB???RGB???????????????
            public float sharpening;                // ??????:??????[-1; 5],?????????0???-1???????????????,5???????????????

            public override string ToString()
            {
                return String.Format("enableColorCorrection={0}; brightness={1}; contrast={2}; saturation={3}; gamma={4}; sharpening={5}",
                    enableColorCorrection, brightness, contrast, saturation, gamma, sharpening);
            }
        }

        //
        // Fov ??????
        // 
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FoveatedRendering
        {
            public bool enableFoveateRendering;      //true????????? ??????????????????(?????????true)
            public float foveationStrength;          //????????????          [0.5-10.0]          ?????? 2 ?????????,???????????????????????????????????????,????????????
            public float foveationShape;             //????????????          [0.2-2.0]           ?????? 1.5  ?????????????????????
            public float foveationVerticalOffset;    //??????????????????      [-0.05-0.05]        ?????? 0  ????????????????????????????????????????????????????????????

            public override string ToString()
            {
                return String.Format("enableFoveateRendering={0}; foveationStrength={1}; foveationShape={2}; foveationVerticalOffset={3}",
                    enableFoveateRendering, foveationStrength, foveationShape, foveationVerticalOffset);
            }
        }

        //
        // ????????????
        // 
        public enum HeadSetType
        {
            larkHeadSetType_HTC = 0,
            larkHeadSetType_OCULUS = 1,
            larkHeadSetType_PICO_2 = 2,
            larkHeadSetType_PICO_3 = 3,
            larkHeadSetType_NOLO_Sonic_1 = 4,
        };

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct HeadSetControllerDesc
        {
            public HeadSetType type;
            public bool forece3dof; //??????3dof ???oculus go
            public float controllerposeTimeOffset;//??????:0.01 ??????:-0.03 ??????:-1 ?????????????????????"??????????????????????????????????????????????????????????????????????????? ?????????????????????????????????Skyrim???????????????????????????",
            public float hapticsIntensity;//?????????????????????0-5

            public override string ToString()
            {
                return String.Format("type={0}; forece3dof={1}; controllerposeTimeOffset={2}; hapticsIntensity={3}",
                    type, forece3dof, controllerposeTimeOffset, hapticsIntensity);
            }
        }

        public enum StreamType
        {
            larkStreamType_UDP = 0,                        //??????UDP
            larkStreamType_TCP = 1,                        //??????TCP
            larkStreamType_THROTTLED_UDP = 2,              //??????(???????????????)+??????UDP
            larkStreamType_KCP = 3,
        };

        // quick setup level.
        public enum QuickConfigLevel
        {
            QuickConfigLevel_Manual = 0,
            QuickConfigLevel_Auto = 1,
            QuickConfigLevel_Fast = 2,
            QuickConfigLevel_Normal = 3,
            QuickConfigLevel_Extreme = 4,
        }
        #endregion

        #region input healper
        // get larkxr button flag
        public static UInt64 GetLarkXRButtonFlag(InputButtonFlag flag)
        {
            return 1UL << (int)flag;
        }

        #endregion

        #region system info api
#if UNITY_ANDROID
        private static AndroidJavaObject j_xrSystem = null;
        public static void InitSystemInfo()
        {
            AndroidJNIHelper.debug = true;
            // call java method on android.
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            j_xrSystem = new AndroidJavaObject("com.pxy.cloudlarkxrkit.XrSystem");
            if (j_xrSystem != null) { 
                j_xrSystem.Call("init", unityActivity);
            }
        }
        public static void ReleaseSystemInfo()
        {
            if (j_xrSystem != null)
            { 
                // save config when release.
                j_xrSystem.Call("onDestroy");
                j_xrSystem = null;
            }
        }
#endif
        // cloudlark apis. system.
        public static bool SystemInited()
        {
            return larkxr_SystemInited();
        }

        public static SystemInfo GetSystemInfo()
        {
            SystemInfo info = new XRApi.SystemInfo();
            larkxr_GetSystemInfo2(ref info.initd, ref info.sysType, ref info.platFromType, ref info.sdkVersion);
            return info;
        }
        #endregion
        #region client context api.
        private static UInt64 frameIndex = 0;

        #region win32 ?????????????????????
        public static void InitContext()
        {
#if UNITY_ANDROID
            larkxr_InitContext();
            // call larkxr_InitGLShareContext on render thread.
            IssuePluginEvent(1000);
#endif
        }
        public static void ReleaseContext()
        {
#if UNITY_ANDROID
            larkxr_ReleaseContext();
            // call larkxr_ReleaseGLShareContext on render thread.
            IssuePluginEvent(2000);
#else
#endif
        }
        #endregion

        public static bool InitSdkAuthorization(string sdkId)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            return larkxr_InitSdkAuthorization(sdkId);
#endif
            return false;
        }
        public static bool InitSdkAuthorizationWithSecret(string sdkId, string secret)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            return larkxr_InitSdkAuthorizationWithSecret(sdkId, secret);
#endif
            return false;
        }
        public static int GetLastError()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            return larkxr_GetLastError();
#endif
            return 0;
        }
        public static void ClearError()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_ClearError();
#endif
        }

        public static void OnResume()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_OnResume();
#if UNITY_ANDROID
            if (j_xrSystem != null)
            {
                // save config when release.
                j_xrSystem.Call("onResume");
            }
#endif
#else
#endif
        }
        public static void OnPause()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_OnPause();
#if UNITY_ANDROID
            if (j_xrSystem != null)
            {
                // save config when release.
                j_xrSystem.Call("onPause");
            }
#endif
#else
#endif
        }
        public static void OnDestory()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_OnDestory();
#if UNITY_ANDROID
            if (j_xrSystem != null)
            {
                // save config when release.
                j_xrSystem.Call("onDestroy");
            }
#endif
#else
#endif
        }
        //apis.
        public static bool IsConnected()
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            return larkxr_IsConnected();
#endif
            return false;
        }

        public static bool IsPause() { return larkxr_IsPause(); }

        public static bool IsFrameInited() { return larkxr_IsFrameInited(); }

        public static int GetRednerTexture() { return larkxr_GetRenderTexture(); }

        public static FrameType GetRenerTextureType()
        {
            return (FrameType)larkxr_GetRenerTextureType();
        }

        public static HwRenderTexture GetHwRenerTexture()
        {
            return larkxr_GetHwRenerTexture();
        }

        public static void ClearTexture() { larkxr_ClearTexture(); }

        public static bool HasNewFrame() { return larkxr_HasNewFrame(); }

        // ?????????????????????????????????????????? trakcingFrame ????????????????????????????????????
        // ???????????????????????????????????????????????????????????????????????????????????????????????????
        // ???????????? trackingFrame.avaliable ??? false???????????????????????????
        public static TrackingFrame Render()
        {
            TrackingFrame trackingFrame = new TrackingFrame();
            bool result = larkxr_Render2(ref trackingFrame.frameIndex, ref trackingFrame.fetchTime, ref trackingFrame.displayTime,
                ref trackingFrame.tracking.position.x, ref trackingFrame.tracking.position.y, ref trackingFrame.tracking.position.z,
                ref trackingFrame.tracking.rotation.x, ref trackingFrame.tracking.rotation.y, ref trackingFrame.tracking.rotation.y, ref trackingFrame.tracking.rotation.w);
            trackingFrame.avaliable = result;
            return trackingFrame;
        }

        // ??????????????????????????????????????????????????????
        public static void CollectorrSubmit(TrackingFrame trackingFrame)
        {
            if (!trackingFrame.avaliable)
                return;

            larkxr_LatencyCollectorrSubmit(trackingFrame.frameIndex, 0);
        }
        // devices apis
        public static void UpdateDevicePose(DeviceType device, TrackedPose pose)
        {
            larkxr_UpdateDevicePose(device, ref pose);
        }
        public static void UpdateDevicePose(DeviceType device, Transform transform) {
            OpenVrPose openVrPose = new OpenVrPose(transform);
            UpdateDevicePose(device, openVrPose.Position, openVrPose.Rotation);
        }

        public static void UpdateDevicePose(DeviceType device, Vector3 position, Quaternion rotation)
        {
            larkxr_UpdateDevicePose2(device, position.x, position.y, position.z, rotation.x, rotation.y, rotation.z, rotation.w);
        }
        public static void UpdateControllerInput(ControllerType controllerType, ControllerInputStateNative inputState)
        {
            larkxr_UpdateControllerInputState(controllerType, ref inputState);
        }
        public static void UpdateControllerInput(ControllerType controllerType, ControllerInputState inputState)
        {
            larkxr_UpdateControllerInput2(controllerType, inputState.isConnected, inputState.touchPadPressed, inputState.triggerPressed, inputState.digitTriggerPressed,
                inputState.appMenuPressed, inputState.homePressed, inputState.gripPressed, inputState.volumUpPressed, inputState.volumDownPressed, inputState.touchPadTouched);
        }
        // send deivce pari to cloud.
        public static void SendDeivcePair()
        {
            XRApi.frameIndex++;
            larkxr_SendDevicePair(XRApi.frameIndex, 0, 0);
        }
        // send deivce pari to cloud.
        public static void SendDeivcePair(UInt64 frameIndex, UInt64 fetchTime = 0, double displayTime = 0)
        {
            larkxr_SendDevicePair(frameIndex, 0, 0);
        }
        public static void SetServerAddr(string ip, int port)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_SetServerAddr(ip, port);
#endif
        }
        public static void EnterAppli(string ip)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_EnterAppli(ip);
#endif
        }
        public static void EnterAppliWithJsonString(string jsonStr)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_EnterAppliWithJsonString(jsonStr);
#endif
        }
        public static void Close()
        {
            larkxr_Close();
        }
        #endregion
        #region system setting.
        public static void SetupBitrateKbps(int bitrate) {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_SetupBitrateKbps(bitrate);
#endif
        }

        public static int GetBitrateKbps() { return larkxr_GetBitrateKbps(); }

        public static int GetDefaultBitrateKbps() { return larkxr_GetDefaultBitrateKbps(); }

        public static void SetupRoomHeight(float roomHeight) { larkxr_SetupRoomHeight(roomHeight); }

        public static float GetRoomHeight() { return larkxr_GetRoomHeight(); }

        public static float GetDefaultRoomHeight() { return larkxr_GetDefaultRoomHeight(); }

        // render apis
        public static RenderInfo GetRenderInfo()
        {
            RenderInfo info = new RenderInfo();

#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_GetRenderResolution(ref info.renderWidth, ref info.renderHeight);
            info.ipd = larkxr_GetIpd();
            info.fps = larkxr_GetRenderFps();
            larkxr_GetFov2(ref info.fovLeft.left, ref info.fovLeft.right, ref info.fovLeft.top, ref info.fovLeft.bottom,
                ref info.fovRight.left, ref info.fovRight.right, ref info.fovRight.top, ref info.fovRight.bottom);
#endif

            return info;
        }
        public static RenderInfo GetDefaultRenderInfo()
        {
            RenderInfo info = new RenderInfo();

#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_GetDefaultResolution(ref info.renderWidth, ref info.renderHeight);
            info.ipd = larkxr_GetDefaultIpd();
            info.fps = larkxr_GetDefaultFps();
            larkxr_GetDefaultFov2(ref info.fovLeft.left, ref info.fovLeft.right, ref info.fovLeft.top, ref info.fovLeft.bottom,
                ref info.fovRight.left, ref info.fovRight.right, ref info.fovRight.top, ref info.fovRight.bottom);
#endif

            return info;
        }
        public static void SetRenderInfo(RenderInfo info)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_SetRenderResolution(info.renderWidth, info.renderHeight);
            larkxr_SetIpd(info.ipd);
            larkxr_SetRenderFps(info.fps);
            larkxr_SetFov2(info.fovLeft.left, info.fovLeft.right, info.fovLeft.top, info.fovLeft.bottom,
                info.fovRight.left, info.fovRight.right, info.fovRight.top, info.fovRight.bottom);
#endif
        }
        public static void SetEnableFoveatedRendering(bool enable)
        {
            FoveatedRendering fov = GetDefaultFoveatedRendering();
            fov.enableFoveateRendering = enable;
            Debug.Log("FoveatedRendering " + fov.enableFoveateRendering + " " + fov.foveationShape + " " + fov.foveationStrength + " " + fov.foveationVerticalOffset);
            larkxr_SetFoveatedRendering(ref fov);
        }
        public static void SetFoveatedRendering(FoveatedRendering foveatedRendering) {
            Debug.Log("SetFoveatedRendering " + foveatedRendering.enableFoveateRendering + " " + foveatedRendering.foveationShape + " " + foveatedRendering.foveationStrength + " " + foveatedRendering.foveationVerticalOffset);
            larkxr_SetFoveatedRendering(ref foveatedRendering);
        }
        public static FoveatedRendering GetFoveatedRendering() {
            FoveatedRendering foveatedRendering = new FoveatedRendering();
            larkxr_GetFoveatedRendering(ref foveatedRendering);
            return foveatedRendering;
        }
        public static FoveatedRendering GetDefaultFoveatedRendering() {
            FoveatedRendering foveatedRendering = new FoveatedRendering();
            larkxr_GetDefaultFoveatedRendering(ref foveatedRendering);
            return foveatedRendering;
        }

        public static void SetColorCorrention(ColorCorrention colorCorrection) {
            larkxr_SetColorCorrention(ref colorCorrection);
        }

        public static ColorCorrention GetColorCorrention() {
            ColorCorrention colorCorrention = new ColorCorrention();
            larkxr_GetColorCorrention(ref colorCorrention);
            return colorCorrention;
        }

        public static ColorCorrention GetDefaultColorCorrention() {
            ColorCorrention colorCorrention = new ColorCorrention();
            larkxr_GetDefaultColorCorrention(ref colorCorrention);
            return colorCorrention;
        }

        public static void SetUseMultiview(bool use) {
            larkxr_SetUseMultiview(use);
        }

        public static void SetFlipDraw(bool flip) {
            larkxr_SetFlipDraw(flip);
        }

        public static void SetHeadSetControllerDesc(HeadSetControllerDesc headSetControllerDesc)
        {
            lakrxr_SetHeadSetControllerDesc(ref headSetControllerDesc);
        }
        public static HeadSetControllerDesc GetHeadSetControllerDesc()
        {
            HeadSetControllerDesc headSetControllerDesc = new HeadSetControllerDesc();
            lakrxr_GetHeadSetControllerDesc(ref headSetControllerDesc);
            return headSetControllerDesc;
        }
        public static HeadSetControllerDesc GetDefaultHeadSetControllerDesc()
        {
            HeadSetControllerDesc headSetControllerDesc = new HeadSetControllerDesc();
            lakrxr_GetDefaultHeadSetControllerDesc(ref headSetControllerDesc);
            return headSetControllerDesc;
        }

        public static void QuickConfigWithDefaulSetup(QuickConfigLevel level)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || PLATFORM_STANDALONE_WIN
            larkxr_QuickConfigWithDefaulSetup((int)level);	
			// WARNING
			// win32 not support ffr yet
            SetEnableFoveatedRendering(false);
			// WARNING
			// some zspace not support h265.
			// disable h265 on win32 quick config.
			XRApi.SetUseH265(false);
#else
			larkxr_QuickConfigWithDefaulSetup((int)level);		
#endif
        }
        public static QuickConfigLevel GetDefaultQuickConfigLevel()
        {
            return (QuickConfigLevel)larkxr_GetDefaultQuickConfigLevel();
        }
        public static void SetResolutionScale(float scale)
        {
            larkxr_SetResolutionScale(scale);
        }
        public static float GetResolutionScale()
        {
            return larkxr_GetResolutionScale();
        }
		public static void SetUseH265(bool use) {
			larkxr_SetUseH265(use);
		}
		public static bool GetUseH265() {
			return larkxr_GetUseH265();
		}
        public static StreamType GetStreamType()
        {
            StreamType type = StreamType.larkStreamType_UDP;
            larkxr_GetStreamType(ref type);
            return type;
        }
        public static StreamType GetDefaultStreamType()
        {
            StreamType type = StreamType.larkStreamType_UDP;
            larkxr_GetDefaultStreamType(ref type);
            return type;
        }
        public static void SetStreamType(StreamType type)
        {
            larkxr_SetStreamType(type);
        }
        /**
         * ????????????????????????????????????
         */
        public static void SendData(byte[] buffer)
        {
            larkxr_SendData(ref buffer[0], buffer.Length);
        }
        public static void SendText(string text)
        {
            larkxr_SendString(text);
        }
        public static void SendVoiceData(byte[] buffer)
        {
            larkxr_SendAudioData(ref buffer[0], buffer.Length);
        }
        #endregion

        #region IssuePluginEvent
        public static void IssuePluginEvent(int e = 1)
        {
            // Queue a specific callback to be called on the render thread
            GL.IssuePluginEvent(GetRenderEventFunc(), e);
        }
#endregion
        #region native call xr conext.
        // ????????????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_InitContext();
        // ????????? SDK ID
        [DllImport("lark_xr")]
        private static extern bool larkxr_InitSdkAuthorization(string sdkid);
        [DllImport("lark_xr")]
        private static extern bool larkxr_InitSdkAuthorizationWithSecret(string sdkid, string secret);
        // ???????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetLastError();
        // ???????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_ClearError();
        // ?????????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_ReleaseContext();
        // ????????????????????????
        [DllImport("lark_xr.dll")]
        private static extern bool larkxr_IsConnected();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern bool larkxr_IsPause();

        // ?????????????????????????????????
        [DllImport("lark_xr")]
        private static extern bool larkxr_IsFrameInited();

        // ?????????????????????????????????????????? oes texture???
        // win32 ?????? DXGI_FORMAT_NV12 ??????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetRenderTexture();

        // ?????????????????? 
        // ????????? XRVideoFrame::FrameType ????????????
        // win32 ?????? DXGI_FORMAT_NV12 ?????????????????????????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetRenerTextureType();

        // 
        // ????????????????????????
        [DllImport("lark_xr")]
        private static extern HwRenderTexture larkxr_GetHwRenerTexture();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_ClearTexture();

        // ???????????????????????????????????????????????? larkxr_Render?????????????????????
        [DllImport("lark_xr")]
        private static extern bool larkxr_HasNewFrame();

        // ?????????????????????????????????????????? trakcingFrame ????????????????????????????????????
        // ???????????????????????????????????????????????????????????????????????????????????????????????????
        [DllImport("lark_xr")]
        private static extern bool larkxr_Render2(ref UInt64 frameIndex, ref UInt64 fetchTime, ref double displayTime,
            ref float px, ref float py, ref float pz, 
            ref float rx, ref float ry, ref float rz, ref float rw);

        // ??????????????????????????????????????????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_LatencyCollectorrSubmit(UInt64 frameIndex, float blackDegree);

        // ??????????????? addr
        [DllImport("lark_xr")]
        private static extern void larkxr_SetServerAddr(string ip, int port);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_EnterAppli(string appId);

        /**
         * ?????? json ????????????????????????
         * ???????????? id ???????????????????????????????????????
         * json ????????????????????????
         * https://www.pingxingyun.com/online/api3_2.html?id=532
         * 1.2.2 ??????????????????
         * @param jsonStr
         */
        [DllImport("lark_xr")]
        private static extern void larkxr_EnterAppliWithJsonString(string jsonStr);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_Close();
        // ??????????????????
        // android ????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_OnCreated();
        // ??????????????????
        // android ??????
        [DllImport("lark_xr")]
        private static extern void larkxr_OnResume();
        // ??????????????????
        // android ??????
        [DllImport("lark_xr")]
        private static extern void larkxr_OnPause();
        // ??????????????????
        // android ??????
        [DllImport("lark_xr")]
        private static extern void larkxr_OnDestory();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_UpdateDevicePose(DeviceType deviceType, ref TrackedPose pose);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_UpdateDevicePose2(DeviceType deviceType, float px, float py, float pz,
            float rx, float ry, float rz, float rw);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_UpdateControllerInputState(ControllerType controllerType, ref ControllerInputStateNative controllerInputState);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_UpdateControllerInput2(ControllerType controllerType, bool isConnected,
            bool touchPadPressed, bool triggerPressed, bool digitTriggerPressed, bool appMenuPressed,
            bool homePressed, bool gripPressed, bool volumUpPressed,
            bool volumDownPressed, bool touchPadTouched);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SendDevicePair(UInt64 frameIndex, UInt64 fetchTime, double displayTime);
#endregion

#region native xrconfig
        // ???????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SetupBitrateKbps(int bitrate);

        // ???????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetBitrateKbps();

        // ?????????????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetDefaultBitrateKbps();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SetupRoomHeight(float roomHeight);

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern float larkxr_GetRoomHeight();

        // ????????????????????????
        [DllImport("lark_xr")]
        private static extern float larkxr_GetDefaultRoomHeight();
#endregion

#region native call systrem.h
        // ?????????????????????
        [DllImport("lark_xr")]
        private static extern bool larkxr_SystemInited();

        // ????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_GetSystemInfo2(ref bool isInited, ref SystemType systemType, ref PlatFromType platFromType, ref SDKVersion SDKVersion);
#endregion

#region native render.h
        // ?????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SetRenderResolution(int renderWidth, int renderHeight);

        // ???????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_GetRenderResolution(ref int renderWidth, ref int renderHeight);

        // ?????????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_GetDefaultResolution(ref int renderWidth, ref int renderHeight);

        // ????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SetIpd(float ipd);

        // ????????????
        [DllImport("lark_xr")]
        private static extern float larkxr_GetIpd();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern float larkxr_GetDefaultIpd();

        // ??????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SetRenderFps(int fps);

        // ????????????????????????
        [DllImport("lark_xr")]
        private static extern int larkxr_GetRenderFps();

        // ?????? FPS
        [DllImport("lark_xr")]
        private static extern int larkxr_GetDefaultFps();

        // ?????? fov
        [DllImport("lark_xr")]
        private static extern void larkxr_SetFov2(float eyeLeft_Left, float eyeLeft_Right, float eyeLeft_Top, float eyeLeft_Bottom,
            float eyeRight_Left, float eyeRight_Right, float eyeRight_Top, float eyeRight_Bottom);

        // ?????? fov
        [DllImport("lark_xr")]
        private static extern void larkxr_GetFov2(ref float eyeLeft_Left, ref float eyeLeft_Right, ref float eyeLeft_Top, ref float eyeLeft_Bottom,
            ref float eyeRight_Left, ref float eyeRight_Right, ref float eyeRight_Top, ref float eyeRight_Bottom);

        // ????????????fov
        [DllImport("lark_xr")]
        private static extern void larkxr_GetDefaultFov2(ref float eyeLeft_Left, ref float eyeLeft_Right, ref float eyeLeft_Top, ref float eyeLeft_Bottom,
            ref float eyeRight_Left, ref float eyeRight_Right, ref float eyeRight_Top, ref float eyeRight_Bottom);

        // setup fov rendeing
        [DllImport("lark_xr")]
        private static extern void larkxr_SetFoveatedRendering(ref FoveatedRendering fovRending);
        // get fov rendeing setup
        [DllImport("lark_xr")]
        private static extern void larkxr_GetFoveatedRendering(ref FoveatedRendering fovRending);
        [DllImport("lark_xr")]
        private static extern void larkxr_GetDefaultFoveatedRendering(ref FoveatedRendering fovRending);

        // setup color corretion
        [DllImport("lark_xr")]
        private static extern void larkxr_SetColorCorrention(ref ColorCorrention colorCorrection);
        [DllImport("lark_xr")]
        private static extern void larkxr_GetColorCorrention(ref ColorCorrention colorCorrection);
        [DllImport("lark_xr")]
        private static extern void larkxr_GetDefaultColorCorrention(ref ColorCorrention colorCorrection);

        // set use mutiview. stereo mode only support in android.
        [DllImport("lark_xr")]
        private static extern void larkxr_SetUseMultiview(bool useMulti);
        // set flip drawing. only suuport in android.
        [DllImport("lark_xr")]
        private static extern void larkxr_SetFlipDraw(bool flipDraw);

        // set larkHeadSetControllerDesc 
        [DllImport("lark_xr")]
        private static extern void lakrxr_SetHeadSetControllerDesc(ref HeadSetControllerDesc headset_desc);
        // get larkHeadSetControllerDesc
        [DllImport("lark_xr")]
        private static extern void lakrxr_GetHeadSetControllerDesc(ref HeadSetControllerDesc headset_desc);
        // get default head set control
        [DllImport("lark_xr")]
        private static extern void lakrxr_GetDefaultHeadSetControllerDesc(ref HeadSetControllerDesc headset_desc);
        // quick config with level
        [DllImport("lark_xr")]
        private static extern void  larkxr_QuickConfigWithDefaulSetup(int level);

        [DllImport("lark_xr")]
        private static extern int larkxr_GetDefaultQuickConfigLevel();

        [DllImport("lark_xr")]
        private static extern void larkxr_SetResolutionScale(float scale);

        [DllImport("lark_xr")]
        private static extern float larkxr_GetResolutionScale();
		
		[DllImport("lark_xr")]
		private static extern void larkxr_SetUseH265(bool use);
		
		[DllImport("lark_xr")]
		private static extern bool larkxr_GetUseH265();

        [DllImport("lark_xr")]
        private static extern bool larkxr_GetStreamType(ref StreamType type);

        [DllImport("lark_xr")]
        private static extern bool larkxr_GetDefaultStreamType(ref StreamType type);

        [DllImport("lark_xr")]
        private static extern bool larkxr_SetStreamType(StreamType type);
        #endregion

        #region datachannels
        //????????????????????????
        [DllImport("lark_xr")]
        private static extern void larkxr_SendData(ref byte data, int length);
        [DllImport("lark_xr")]
        private static extern void larkxr_SendString(string txt);
        [DllImport("lark_xr")]
        private static extern void larkxr_SendAudioData(ref byte data, int length);
        #endregion

        #region render callback
        [DllImport("lark_xr")]
        private static extern IntPtr GetRenderEventFunc();
        #endregion
    }
}
