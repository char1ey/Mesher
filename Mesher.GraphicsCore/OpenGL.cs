using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore
{
    internal static class Gl
    {
        #region constants

        private const String LIBRARY_OPENGL = "opengl32.dll";
        private const String LIBRARY_GLU = "Glu32.dll";

        #endregion

        #region private variables

        private static readonly Dictionary<String, Delegate> ExtensionFunctions = new Dictionary<String, Delegate>();

        #endregion

        #region utils

        public static Boolean IsExtensionFunctionSupported(String extensionFunctionName)
        {
            var procAddress = Win32.wglGetProcAddress(extensionFunctionName);
            return procAddress != IntPtr.Zero;
        }
        private static T GetDelegateFor<T>() where T : class
        {
            var delegateType = typeof(T);
            var name = delegateType.Name;
            Delegate del;
            if (ExtensionFunctions.TryGetValue(name, out del) == false)
            {
                var proc = Win32.wglGetProcAddress(name);
                if (proc == IntPtr.Zero)
                    throw new Exception("Extension function " + name + " not supported");
                del = Marshal.GetDelegateForFunctionPointer(proc, delegateType);
                ExtensionFunctions.Add(name, del);
            }
            return del as T;
        }
        
        public static String GetErrorDescription(UInt32 errorCode)
        {
            switch (errorCode)
            {
                case GL_NO_ERROR:
                    return "No Error";
                case GL_INVALID_ENUM:
                    return "A GLenum argument was out of range.";
                case GL_INVALID_VALUE:
                    return "A numeric argument was out of range.";
                case GL_INVALID_OPERATION:
                    return "Invalid operation.";
                case GL_STACK_OVERFLOW:
                    return "Command would cause a stack overflow.";
                case GL_STACK_UNDERFLOW:
                    return "Command would cause a stack underflow.";
                case GL_OUT_OF_MEMORY:
                    return "Not enough memory left to execute command.";
                default:
                    return "Unknown Error";
            }
        }

        #endregion

        #region OpenGL constants
        public const UInt32 GL_VERSION_1_1 = 1;
        public const UInt32 GL_ACCUM = 0x0100;
        public const UInt32 GL_LOAD = 0x0101;
        public const UInt32 GL_RETURN = 0x0102;
        public const UInt32 GL_MULT = 0x0103;
        public const UInt32 GL_ADD = 0x0104;
        public const UInt32 GL_NEVER = 0x0200;
        public const UInt32 GL_LESS = 0x0201;
        public const UInt32 GL_EQUAL = 0x0202;
        public const UInt32 GL_LEQUAL = 0x0203;
        public const UInt32 GL_GREATER = 0x0204;
        public const UInt32 GL_NOTEQUAL = 0x0205;
        public const UInt32 GL_GEQUAL = 0x0206;
        public const UInt32 GL_ALWAYS = 0x0207;
        public const UInt32 GL_CURRENT_BIT = 0x00000001;
        public const UInt32 GL_POINT_BIT = 0x00000002;
        public const UInt32 GL_LINE_BIT = 0x00000004;
        public const UInt32 GL_POLYGON_BIT = 0x00000008;
        public const UInt32 GL_POLYGON_STIPPLE_BIT = 0x00000010;
        public const UInt32 GL_PIXEL_MODE_BIT = 0x00000020;
        public const UInt32 GL_LIGHTING_BIT = 0x00000040;
        public const UInt32 GL_FOG_BIT = 0x00000080;
        public const UInt32 GL_DEPTH_BUFFER_BIT = 0x00000100;
        public const UInt32 GL_ACCUM_BUFFER_BIT = 0x00000200;
        public const UInt32 GL_STENCIL_BUFFER_BIT = 0x00000400;
        public const UInt32 GL_VIEWPORT_BIT = 0x00000800;
        public const UInt32 GL_TRANSFORM_BIT = 0x00001000;
        public const UInt32 GL_ENABLE_BIT = 0x00002000;
        public const UInt32 GL_COLOR_BUFFER_BIT = 0x00004000;
        public const UInt32 GL_HINT_BIT = 0x00008000;
        public const UInt32 GL_EVAL_BIT = 0x00010000;
        public const UInt32 GL_LIST_BIT = 0x00020000;
        public const UInt32 GL_TEXTURE_BIT = 0x00040000;
        public const UInt32 GL_SCISSOR_BIT = 0x00080000;
        public const UInt32 GL_ALL_ATTRIB_BITS = 0x000fffff;
        public const UInt32 GL_POINTS = 0x0000;
        public const UInt32 GL_LINES = 0x0001;
        public const UInt32 GL_LINE_LOOP = 0x0002;
        public const UInt32 GL_LINE_STRIP = 0x0003;
        public const UInt32 GL_TRIANGLES = 0x0004;
        public const UInt32 GL_TRIANGLE_STRIP = 0x0005;
        public const UInt32 GL_TRIANGLE_FAN = 0x0006;
        public const UInt32 GL_QUADS = 0x0007;
        public const UInt32 GL_QUAD_STRIP = 0x0008;
        public const UInt32 GL_POLYGON = 0x0009;
        public const UInt32 GL_ZERO = 0;
        public const UInt32 GL_ONE = 1;
        public const UInt32 GL_SRC_COLOR = 0x0300;
        public const UInt32 GL_ONE_MINUS_SRC_COLOR = 0x0301;
        public const UInt32 GL_SRC_ALPHA = 0x0302;
        public const UInt32 GL_ONE_MINUS_SRC_ALPHA = 0x0303;
        public const UInt32 GL_DST_ALPHA = 0x0304;
        public const UInt32 GL_ONE_MINUS_DST_ALPHA = 0x0305;
        public const UInt32 GL_DST_COLOR = 0x0306;
        public const UInt32 GL_ONE_MINUS_DST_COLOR = 0x0307;
        public const UInt32 GL_SRC_ALPHA_SATURATE = 0x0308;
        public const UInt32 GL_TRUE = 1;
        public const UInt32 GL_FALSE = 0;
        public const UInt32 GL_CLIP_PLANE0 = 0x3000;
        public const UInt32 GL_CLIP_PLANE1 = 0x3001;
        public const UInt32 GL_CLIP_PLANE2 = 0x3002;
        public const UInt32 GL_CLIP_PLANE3 = 0x3003;
        public const UInt32 GL_CLIP_PLANE4 = 0x3004;
        public const UInt32 GL_CLIP_PLANE5 = 0x3005;
        public const UInt32 GL_BYTE = 0x1400;
        public const UInt32 GL_UNSIGNED_BYTE = 0x1401;
        public const UInt32 GL_SHORT = 0x1402;
        public const UInt32 GL_UNSIGNED_SHORT = 0x1403;
        public const UInt32 GL_INT = 0x1404;
        public const UInt32 GL_UNSIGNED_INT = 0x1405;
        public const UInt32 GL_FLOAT = 0x1406;
        public const UInt32 GL_2_BYTES = 0x1407;
        public const UInt32 GL_3_BYTES = 0x1408;
        public const UInt32 GL_4_BYTES = 0x1409;
        public const UInt32 GL_DOUBLE = 0x140A;
        public const UInt32 GL_NONE = 0;
        public const UInt32 GL_FRONT_LEFT = 0x0400;
        public const UInt32 GL_FRONT_RIGHT = 0x0401;
        public const UInt32 GL_BACK_LEFT = 0x0402;
        public const UInt32 GL_BACK_RIGHT = 0x0403;
        public const UInt32 GL_FRONT = 0x0404;
        public const UInt32 GL_BACK = 0x0405;
        public const UInt32 GL_LEFT = 0x0406;
        public const UInt32 GL_RIGHT = 0x0407;
        public const UInt32 GL_FRONT_AND_BACK = 0x0408;
        public const UInt32 GL_AUX0 = 0x0409;
        public const UInt32 GL_AUX1 = 0x040A;
        public const UInt32 GL_AUX2 = 0x040B;
        public const UInt32 GL_AUX3 = 0x040C;
        public const UInt32 GL_NO_ERROR = 0;
        public const UInt32 GL_INVALID_ENUM = 0x0500;
        public const UInt32 GL_INVALID_VALUE = 0x0501;
        public const UInt32 GL_INVALID_OPERATION = 0x0502;
        public const UInt32 GL_STACK_OVERFLOW = 0x0503;
        public const UInt32 GL_STACK_UNDERFLOW = 0x0504;
        public const UInt32 GL_OUT_OF_MEMORY = 0x0505;
        public const UInt32 GL_2D = 0x0600;
        public const UInt32 GL_3D = 0x0601;
        public const UInt32 GL_4D_COLOR = 0x0602;
        public const UInt32 GL_3D_COLOR_TEXTURE = 0x0603;
        public const UInt32 GL_4D_COLOR_TEXTURE = 0x0604;
        public const UInt32 GL_PASS_THROUGH_TOKEN = 0x0700;
        public const UInt32 GL_POINT_TOKEN = 0x0701;
        public const UInt32 GL_LINE_TOKEN = 0x0702;
        public const UInt32 GL_POLYGON_TOKEN = 0x0703;
        public const UInt32 GL_BITMAP_TOKEN = 0x0704;
        public const UInt32 GL_DRAW_PIXEL_TOKEN = 0x0705;
        public const UInt32 GL_COPY_PIXEL_TOKEN = 0x0706;
        public const UInt32 GL_LINE_RESET_TOKEN = 0x0707;
        public const UInt32 GL_EXP = 0x0800;
        public const UInt32 GL_EXP2 = 0x0801;
        public const UInt32 GL_CW = 0x0900;
        public const UInt32 GL_CCW = 0x0901;
        public const UInt32 GL_COEFF = 0x0A00;
        public const UInt32 GL_ORDER = 0x0A01;
        public const UInt32 GL_DOMAIN = 0x0A02;
        public const UInt32 GL_CURRENT_COLOR = 0x0B00;
        public const UInt32 GL_CURRENT_INDEX = 0x0B01;
        public const UInt32 GL_CURRENT_NORMAL = 0x0B02;
        public const UInt32 GL_CURRENT_TEXTURE_COORDS = 0x0B03;
        public const UInt32 GL_CURRENT_RASTER_COLOR = 0x0B04;
        public const UInt32 GL_CURRENT_RASTER_INDEX = 0x0B05;
        public const UInt32 GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06;
        public const UInt32 GL_CURRENT_RASTER_POSITION = 0x0B07;
        public const UInt32 GL_CURRENT_RASTER_POSITION_VALID = 0x0B08;
        public const UInt32 GL_CURRENT_RASTER_DISTANCE = 0x0B09;
        public const UInt32 GL_POINT_SMOOTH = 0x0B10;
        public const UInt32 GL_POINT_SIZE = 0x0B11;
        public const UInt32 GL_POINT_SIZE_RANGE = 0x0B12;
        public const UInt32 GL_POINT_SIZE_GRANULARITY = 0x0B13;
        public const UInt32 GL_LINE_SMOOTH = 0x0B20;
        public const UInt32 GL_LINE_WIDTH = 0x0B21;
        public const UInt32 GL_LINE_WIDTH_RANGE = 0x0B22;
        public const UInt32 GL_LINE_WIDTH_GRANULARITY = 0x0B23;
        public const UInt32 GL_LINE_STIPPLE = 0x0B24;
        public const UInt32 GL_LINE_STIPPLE_PATTERN = 0x0B25;
        public const UInt32 GL_LINE_STIPPLE_REPEAT = 0x0B26;
        public const UInt32 GL_LIST_MODE = 0x0B30;
        public const UInt32 GL_MAX_LIST_NESTING = 0x0B31;
        public const UInt32 GL_LIST_BASE = 0x0B32;
        public const UInt32 GL_LIST_INDEX = 0x0B33;
        public const UInt32 GL_POLYGON_MODE = 0x0B40;
        public const UInt32 GL_POLYGON_SMOOTH = 0x0B41;
        public const UInt32 GL_POLYGON_STIPPLE = 0x0B42;
        public const UInt32 GL_EDGE_FLAG = 0x0B43;
        public const UInt32 GL_CULL_FACE = 0x0B44;
        public const UInt32 GL_CULL_FACE_MODE = 0x0B45;
        public const UInt32 GL_FRONT_FACE = 0x0B46;
        public const UInt32 GL_LIGHTING = 0x0B50;
        public const UInt32 GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51;
        public const UInt32 GL_LIGHT_MODEL_TWO_SIDE = 0x0B52;
        public const UInt32 GL_LIGHT_MODEL_AMBIENT = 0x0B53;
        public const UInt32 GL_SHADE_MODEL = 0x0B54;
        public const UInt32 GL_COLOR_MATERIAL_FACE = 0x0B55;
        public const UInt32 GL_COLOR_MATERIAL_PARAMETER = 0x0B56;
        public const UInt32 GL_COLOR_MATERIAL = 0x0B57;
        public const UInt32 GL_FOG = 0x0B60;
        public const UInt32 GL_FOG_INDEX = 0x0B61;
        public const UInt32 GL_FOG_DENSITY = 0x0B62;
        public const UInt32 GL_FOG_START = 0x0B63;
        public const UInt32 GL_FOG_END = 0x0B64;
        public const UInt32 GL_FOG_MODE = 0x0B65;
        public const UInt32 GL_FOG_COLOR = 0x0B66;
        public const UInt32 GL_DEPTH_RANGE = 0x0B70;
        public const UInt32 GL_DEPTH_TEST = 0x0B71;
        public const UInt32 GL_DEPTH_WRITEMASK = 0x0B72;
        public const UInt32 GL_DEPTH_CLEAR_VALUE = 0x0B73;
        public const UInt32 GL_DEPTH_FUNC = 0x0B74;
        public const UInt32 GL_ACCUM_CLEAR_VALUE = 0x0B80;
        public const UInt32 GL_STENCIL_TEST = 0x0B90;
        public const UInt32 GL_STENCIL_CLEAR_VALUE = 0x0B91;
        public const UInt32 GL_STENCIL_FUNC = 0x0B92;
        public const UInt32 GL_STENCIL_VALUE_MASK = 0x0B93;
        public const UInt32 GL_STENCIL_FAIL = 0x0B94;
        public const UInt32 GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const UInt32 GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const UInt32 GL_STENCIL_REF = 0x0B97;
        public const UInt32 GL_STENCIL_WRITEMASK = 0x0B98;
        public const UInt32 GL_MATRIX_MODE = 0x0BA0;
        public const UInt32 GL_NORMALIZE = 0x0BA1;
        public const UInt32 GL_VIEWPORT = 0x0BA2;
        public const UInt32 GL_MODELVIEW_STACK_DEPTH = 0x0BA3;
        public const UInt32 GL_PROJECTION_STACK_DEPTH = 0x0BA4;
        public const UInt32 GL_TEXTURE_STACK_DEPTH = 0x0BA5;
        public const UInt32 GL_MODELVIEW_MATRIX = 0x0BA6;
        public const UInt32 GL_PROJECTION_MATRIX = 0x0BA7;
        public const UInt32 GL_TEXTURE_MATRIX = 0x0BA8;
        public const UInt32 GL_ATTRIB_STACK_DEPTH = 0x0BB0;
        public const UInt32 GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1;
        public const UInt32 GL_ALPHA_TEST = 0x0BC0;
        public const UInt32 GL_ALPHA_TEST_FUNC = 0x0BC1;
        public const UInt32 GL_ALPHA_TEST_REF = 0x0BC2;
        public const UInt32 GL_DITHER = 0x0BD0;
        public const UInt32 GL_BLEND_DST = 0x0BE0;
        public const UInt32 GL_BLEND_SRC = 0x0BE1;
        public const UInt32 GL_BLEND = 0x0BE2;
        public const UInt32 GL_LOGIC_OP_MODE = 0x0BF0;
        public const UInt32 GL_INDEX_LOGIC_OP = 0x0BF1;
        public const UInt32 GL_COLOR_LOGIC_OP = 0x0BF2;
        public const UInt32 GL_AUX_BUFFERS = 0x0C00;
        public const UInt32 GL_DRAW_BUFFER = 0x0C01;
        public const UInt32 GL_READ_BUFFER = 0x0C02;
        public const UInt32 GL_SCISSOR_BOX = 0x0C10;
        public const UInt32 GL_SCISSOR_TEST = 0x0C11;
        public const UInt32 GL_INDEX_CLEAR_VALUE = 0x0C20;
        public const UInt32 GL_INDEX_WRITEMASK = 0x0C21;
        public const UInt32 GL_COLOR_CLEAR_VALUE = 0x0C22;
        public const UInt32 GL_COLOR_WRITEMASK = 0x0C23;
        public const UInt32 GL_INDEX_MODE = 0x0C30;
        public const UInt32 GL_RGBA_MODE = 0x0C31;
        public const UInt32 GL_DOUBLEBUFFER = 0x0C32;
        public const UInt32 GL_STEREO = 0x0C33;
        public const UInt32 GL_RENDER_MODE = 0x0C40;
        public const UInt32 GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50;
        public const UInt32 GL_POINT_SMOOTH_HINT = 0x0C51;
        public const UInt32 GL_LINE_SMOOTH_HINT = 0x0C52;
        public const UInt32 GL_POLYGON_SMOOTH_HINT = 0x0C53;
        public const UInt32 GL_FOG_HINT = 0x0C54;
        public const UInt32 GL_TEXTURE_GEN_S = 0x0C60;
        public const UInt32 GL_TEXTURE_GEN_T = 0x0C61;
        public const UInt32 GL_TEXTURE_GEN_R = 0x0C62;
        public const UInt32 GL_TEXTURE_GEN_Q = 0x0C63;
        public const UInt32 GL_PIXEL_MAP_I_TO_I = 0x0C70;
        public const UInt32 GL_PIXEL_MAP_S_TO_S = 0x0C71;
        public const UInt32 GL_PIXEL_MAP_I_TO_R = 0x0C72;
        public const UInt32 GL_PIXEL_MAP_I_TO_G = 0x0C73;
        public const UInt32 GL_PIXEL_MAP_I_TO_B = 0x0C74;
        public const UInt32 GL_PIXEL_MAP_I_TO_A = 0x0C75;
        public const UInt32 GL_PIXEL_MAP_R_TO_R = 0x0C76;
        public const UInt32 GL_PIXEL_MAP_G_TO_G = 0x0C77;
        public const UInt32 GL_PIXEL_MAP_B_TO_B = 0x0C78;
        public const UInt32 GL_PIXEL_MAP_A_TO_A = 0x0C79;
        public const UInt32 GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0;
        public const UInt32 GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1;
        public const UInt32 GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2;
        public const UInt32 GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3;
        public const UInt32 GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4;
        public const UInt32 GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5;
        public const UInt32 GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6;
        public const UInt32 GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7;
        public const UInt32 GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8;
        public const UInt32 GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9;
        public const UInt32 GL_UNPACK_SWAP_BYTES = 0x0CF0;
        public const UInt32 GL_UNPACK_LSB_FIRST = 0x0CF1;
        public const UInt32 GL_UNPACK_ROW_LENGTH = 0x0CF2;
        public const UInt32 GL_UNPACK_SKIP_ROWS = 0x0CF3;
        public const UInt32 GL_UNPACK_SKIP_PIXELS = 0x0CF4;
        public const UInt32 GL_UNPACK_ALIGNMENT = 0x0CF5;
        public const UInt32 GL_PACK_SWAP_BYTES = 0x0D00;
        public const UInt32 GL_PACK_LSB_FIRST = 0x0D01;
        public const UInt32 GL_PACK_ROW_LENGTH = 0x0D02;
        public const UInt32 GL_PACK_SKIP_ROWS = 0x0D03;
        public const UInt32 GL_PACK_SKIP_PIXELS = 0x0D04;
        public const UInt32 GL_PACK_ALIGNMENT = 0x0D05;
        public const UInt32 GL_MAP_COLOR = 0x0D10;
        public const UInt32 GL_MAP_STENCIL = 0x0D11;
        public const UInt32 GL_INDEX_SHIFT = 0x0D12;
        public const UInt32 GL_INDEX_OFFSET = 0x0D13;
        public const UInt32 GL_RED_SCALE = 0x0D14;
        public const UInt32 GL_RED_BIAS = 0x0D15;
        public const UInt32 GL_ZOOM_X = 0x0D16;
        public const UInt32 GL_ZOOM_Y = 0x0D17;
        public const UInt32 GL_GREEN_SCALE = 0x0D18;
        public const UInt32 GL_GREEN_BIAS = 0x0D19;
        public const UInt32 GL_BLUE_SCALE = 0x0D1A;
        public const UInt32 GL_BLUE_BIAS = 0x0D1B;
        public const UInt32 GL_ALPHA_SCALE = 0x0D1C;
        public const UInt32 GL_ALPHA_BIAS = 0x0D1D;
        public const UInt32 GL_DEPTH_SCALE = 0x0D1E;
        public const UInt32 GL_DEPTH_BIAS = 0x0D1F;
        public const UInt32 GL_MAX_EVAL_ORDER = 0x0D30;
        public const UInt32 GL_MAX_LIGHTS = 0x0D31;
        public const UInt32 GL_MAX_CLIP_PLANES = 0x0D32;
        public const UInt32 GL_MAX_TEXTURE_SIZE = 0x0D33;
        public const UInt32 GL_MAX_PIXEL_MAP_TABLE = 0x0D34;
        public const UInt32 GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35;
        public const UInt32 GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36;
        public const UInt32 GL_MAX_NAME_STACK_DEPTH = 0x0D37;
        public const UInt32 GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38;
        public const UInt32 GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39;
        public const UInt32 GL_MAX_VIEWPORT_DIMS = 0x0D3A;
        public const UInt32 GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B;
        public const UInt32 GL_SUBPIXEL_BITS = 0x0D50;
        public const UInt32 GL_INDEX_BITS = 0x0D51;
        public const UInt32 GL_RED_BITS = 0x0D52;
        public const UInt32 GL_GREEN_BITS = 0x0D53;
        public const UInt32 GL_BLUE_BITS = 0x0D54;
        public const UInt32 GL_ALPHA_BITS = 0x0D55;
        public const UInt32 GL_DEPTH_BITS = 0x0D56;
        public const UInt32 GL_STENCIL_BITS = 0x0D57;
        public const UInt32 GL_ACCUM_RED_BITS = 0x0D58;
        public const UInt32 GL_ACCUM_GREEN_BITS = 0x0D59;
        public const UInt32 GL_ACCUM_BLUE_BITS = 0x0D5A;
        public const UInt32 GL_ACCUM_ALPHA_BITS = 0x0D5B;
        public const UInt32 GL_NAME_STACK_DEPTH = 0x0D70;
        public const UInt32 GL_AUTO_NORMAL = 0x0D80;
        public const UInt32 GL_MAP1_COLOR_4 = 0x0D90;
        public const UInt32 GL_MAP1_INDEX = 0x0D91;
        public const UInt32 GL_MAP1_NORMAL = 0x0D92;
        public const UInt32 GL_MAP1_TEXTURE_COORD_1 = 0x0D93;
        public const UInt32 GL_MAP1_TEXTURE_COORD_2 = 0x0D94;
        public const UInt32 GL_MAP1_TEXTURE_COORD_3 = 0x0D95;
        public const UInt32 GL_MAP1_TEXTURE_COORD_4 = 0x0D96;
        public const UInt32 GL_MAP1_VERTEX_3 = 0x0D97;
        public const UInt32 GL_MAP1_VERTEX_4 = 0x0D98;
        public const UInt32 GL_MAP2_COLOR_4 = 0x0DB0;
        public const UInt32 GL_MAP2_INDEX = 0x0DB1;
        public const UInt32 GL_MAP2_NORMAL = 0x0DB2;
        public const UInt32 GL_MAP2_TEXTURE_COORD_1 = 0x0DB3;
        public const UInt32 GL_MAP2_TEXTURE_COORD_2 = 0x0DB4;
        public const UInt32 GL_MAP2_TEXTURE_COORD_3 = 0x0DB5;
        public const UInt32 GL_MAP2_TEXTURE_COORD_4 = 0x0DB6;
        public const UInt32 GL_MAP2_VERTEX_3 = 0x0DB7;
        public const UInt32 GL_MAP2_VERTEX_4 = 0x0DB8;
        public const UInt32 GL_MAP1_GRID_DOMAIN = 0x0DD0;
        public const UInt32 GL_MAP1_GRID_SEGMENTS = 0x0DD1;
        public const UInt32 GL_MAP2_GRID_DOMAIN = 0x0DD2;
        public const UInt32 GL_MAP2_GRID_SEGMENTS = 0x0DD3;
        public const UInt32 GL_TEXTURE_1D = 0x0DE0;
        public const UInt32 GL_TEXTURE_2D = 0x0DE1;
        public const UInt32 GL_FEEDBACK_BUFFER_POINTER = 0x0DF0;
        public const UInt32 GL_FEEDBACK_BUFFER_SIZE = 0x0DF1;
        public const UInt32 GL_FEEDBACK_BUFFER_TYPE = 0x0DF2;
        public const UInt32 GL_SELECTION_BUFFER_POINTER = 0x0DF3;
        public const UInt32 GL_SELECTION_BUFFER_SIZE = 0x0DF4;
        public const UInt32 GL_TEXTURE_WIDTH = 0x1000;
        public const UInt32 GL_TEXTURE_HEIGHT = 0x1001;
        public const UInt32 GL_TEXTURE_INTERNAL_FORMAT = 0x1003;
        public const UInt32 GL_TEXTURE_BORDER_COLOR = 0x1004;
        public const UInt32 GL_TEXTURE_BORDER = 0x1005;
        public const UInt32 GL_DONT_CARE = 0x1100;
        public const UInt32 GL_FASTEST = 0x1101;
        public const UInt32 GL_NICEST = 0x1102;
        public const UInt32 GL_LIGHT0 = 0x4000;
        public const UInt32 GL_LIGHT1 = 0x4001;
        public const UInt32 GL_LIGHT2 = 0x4002;
        public const UInt32 GL_LIGHT3 = 0x4003;
        public const UInt32 GL_LIGHT4 = 0x4004;
        public const UInt32 GL_LIGHT5 = 0x4005;
        public const UInt32 GL_LIGHT6 = 0x4006;
        public const UInt32 GL_LIGHT7 = 0x4007;
        public const UInt32 GL_AMBIENT = 0x1200;
        public const UInt32 GL_DIFFUSE = 0x1201;
        public const UInt32 GL_SPECULAR = 0x1202;
        public const UInt32 GL_POSITION = 0x1203;
        public const UInt32 GL_SPOT_DIRECTION = 0x1204;
        public const UInt32 GL_SPOT_EXPONENT = 0x1205;
        public const UInt32 GL_SPOT_CUTOFF = 0x1206;
        public const UInt32 GL_CONSTANT_ATTENUATION = 0x1207;
        public const UInt32 GL_LINEAR_ATTENUATION = 0x1208;
        public const UInt32 GL_QUADRATIC_ATTENUATION = 0x1209;
        public const UInt32 GL_COMPILE = 0x1300;
        public const UInt32 GL_COMPILE_AND_EXECUTE = 0x1301;
        public const UInt32 GL_CLEAR = 0x1500;
        public const UInt32 GL_AND = 0x1501;
        public const UInt32 GL_AND_REVERSE = 0x1502;
        public const UInt32 GL_COPY = 0x1503;
        public const UInt32 GL_AND_INVERTED = 0x1504;
        public const UInt32 GL_NOOP = 0x1505;
        public const UInt32 GL_XOR = 0x1506;
        public const UInt32 GL_OR = 0x1507;
        public const UInt32 GL_NOR = 0x1508;
        public const UInt32 GL_EQUIV = 0x1509;
        public const UInt32 GL_INVERT = 0x150A;
        public const UInt32 GL_OR_REVERSE = 0x150B;
        public const UInt32 GL_COPY_INVERTED = 0x150C;
        public const UInt32 GL_OR_INVERTED = 0x150D;
        public const UInt32 GL_NAND = 0x150E;
        public const UInt32 GL_SET = 0x150F;
        public const UInt32 GL_EMISSION = 0x1600;
        public const UInt32 GL_SHININESS = 0x1601;
        public const UInt32 GL_AMBIENT_AND_DIFFUSE = 0x1602;
        public const UInt32 GL_COLOR_INDEXES = 0x1603;
        public const UInt32 GL_MODELVIEW = 0x1700;
        public const UInt32 GL_PROJECTION = 0x1701;
        public const UInt32 GL_TEXTURE = 0x1702;
        public const UInt32 GL_COLOR = 0x1800;
        public const UInt32 GL_DEPTH = 0x1801;
        public const UInt32 GL_STENCIL = 0x1802;
        public const UInt32 GL_COLOR_INDEX = 0x1900;
        public const UInt32 GL_STENCIL_INDEX = 0x1901;
        public const UInt32 GL_DEPTH_COMPONENT = 0x1902;
        public const UInt32 GL_RED = 0x1903;
        public const UInt32 GL_GREEN = 0x1904;
        public const UInt32 GL_BLUE = 0x1905;
        public const UInt32 GL_ALPHA = 0x1906;
        public const UInt32 GL_RGB = 0x1907;
        public const UInt32 GL_RGBA = 0x1908;
        public const UInt32 GL_LUMINANCE = 0x1909;
        public const UInt32 GL_LUMINANCE_ALPHA = 0x190A;
        public const UInt32 GL_BITMAP = 0x1A00;
        public const UInt32 GL_POINT = 0x1B00;
        public const UInt32 GL_LINE = 0x1B01;
        public const UInt32 GL_FILL = 0x1B02;
        public const UInt32 GL_RENDER = 0x1C00;
        public const UInt32 GL_FEEDBACK = 0x1C01;
        public const UInt32 GL_SELECT = 0x1C02;
        public const UInt32 GL_FLAT = 0x1D00;
        public const UInt32 GL_SMOOTH = 0x1D01;
        public const UInt32 GL_KEEP = 0x1E00;
        public const UInt32 GL_REPLACE = 0x1E01;
        public const UInt32 GL_INCR = 0x1E02;
        public const UInt32 GL_DECR = 0x1E03;
        public const UInt32 GL_VENDOR = 0x1F00;
        public const UInt32 GL_RENDERER = 0x1F01;
        public const UInt32 GL_VERSION = 0x1F02;
        public const UInt32 GL_EXTENSIONS = 0x1F03;
        public const UInt32 GL_S = 0x2000;
        public const UInt32 GL_T = 0x2001;
        public const UInt32 GL_R = 0x2002;
        public const UInt32 GL_Q = 0x2003;
        public const UInt32 GL_MODULATE = 0x2100;
        public const UInt32 GL_DECAL = 0x2101;
        public const UInt32 GL_TEXTURE_ENV_MODE = 0x2200;
        public const UInt32 GL_TEXTURE_ENV_COLOR = 0x2201;
        public const UInt32 GL_TEXTURE_ENV = 0x2300;
        public const UInt32 GL_EYE_LINEAR = 0x2400;
        public const UInt32 GL_OBJECT_LINEAR = 0x2401;
        public const UInt32 GL_SPHERE_MAP = 0x2402;
        public const UInt32 GL_TEXTURE_GEN_MODE = 0x2500;
        public const UInt32 GL_OBJECT_PLANE = 0x2501;
        public const UInt32 GL_EYE_PLANE = 0x2502;
        public const UInt32 GL_NEAREST = 0x2600;
        public const UInt32 GL_LINEAR = 0x2601;
        public const UInt32 GL_NEAREST_MIPMAP_NEAREST = 0x2700;
        public const UInt32 GL_LINEAR_MIPMAP_NEAREST = 0x2701;
        public const UInt32 GL_NEAREST_MIPMAP_LINEAR = 0x2702;
        public const UInt32 GL_LINEAR_MIPMAP_LINEAR = 0x2703;
        public const UInt32 GL_TEXTURE_MAG_FILTER = 0x2800;
        public const UInt32 GL_TEXTURE_MIN_FILTER = 0x2801;
        public const UInt32 GL_TEXTURE_WRAP_S = 0x2802;
        public const UInt32 GL_TEXTURE_WRAP_T = 0x2803;
        public const UInt32 GL_CLAMP = 0x2900;
        public const UInt32 GL_REPEAT = 0x2901;
        public const UInt32 GL_CLIENT_PIXEL_STORE_BIT = 0x00000001;
        public const UInt32 GL_CLIENT_VERTEX_ARRAY_BIT = 0x00000002;
        public const UInt32 GL_CLIENT_ALL_ATTRIB_BITS = 0xffffffff;
        public const UInt32 GL_POLYGON_OFFSET_FACTOR = 0x8038;
        public const UInt32 GL_POLYGON_OFFSET_UNITS = 0x2A00;
        public const UInt32 GL_POLYGON_OFFSET_POINT = 0x2A01;
        public const UInt32 GL_POLYGON_OFFSET_LINE = 0x2A02;
        public const UInt32 GL_POLYGON_OFFSET_FILL = 0x8037;
        public const UInt32 GL_ALPHA4 = 0x803B;
        public const UInt32 GL_ALPHA8 = 0x803C;
        public const UInt32 GL_ALPHA12 = 0x803D;
        public const UInt32 GL_ALPHA16 = 0x803E;
        public const UInt32 GL_LUMINANCE4 = 0x803F;
        public const UInt32 GL_LUMINANCE8 = 0x8040;
        public const UInt32 GL_LUMINANCE12 = 0x8041;
        public const UInt32 GL_LUMINANCE16 = 0x8042;
        public const UInt32 GL_LUMINANCE4_ALPHA4 = 0x8043;
        public const UInt32 GL_LUMINANCE6_ALPHA2 = 0x8044;
        public const UInt32 GL_LUMINANCE8_ALPHA8 = 0x8045;
        public const UInt32 GL_LUMINANCE12_ALPHA4 = 0x8046;
        public const UInt32 GL_LUMINANCE12_ALPHA12 = 0x8047;
        public const UInt32 GL_LUMINANCE16_ALPHA16 = 0x8048;
        public const UInt32 GL_INTENSITY = 0x8049;
        public const UInt32 GL_INTENSITY4 = 0x804A;
        public const UInt32 GL_INTENSITY8 = 0x804B;
        public const UInt32 GL_INTENSITY12 = 0x804C;
        public const UInt32 GL_INTENSITY16 = 0x804D;
        public const UInt32 GL_R3_G3_B2 = 0x2A10;
        public const UInt32 GL_RGB4 = 0x804F;
        public const UInt32 GL_RGB5 = 0x8050;
        public const UInt32 GL_RGB8 = 0x8051;
        public const UInt32 GL_RGB10 = 0x8052;
        public const UInt32 GL_RGB12 = 0x8053;
        public const UInt32 GL_RGB16 = 0x8054;
        public const UInt32 GL_RGBA2 = 0x8055;
        public const UInt32 GL_RGBA4 = 0x8056;
        public const UInt32 GL_RGB5_A1 = 0x8057;
        public const UInt32 GL_RGBA8 = 0x8058;
        public const UInt32 GL_RGB10_A2 = 0x8059;
        public const UInt32 GL_RGBA12 = 0x805A;
        public const UInt32 GL_RGBA16 = 0x805B;
        public const UInt32 GL_TEXTURE_RED_SIZE = 0x805C;
        public const UInt32 GL_TEXTURE_GREEN_SIZE = 0x805D;
        public const UInt32 GL_TEXTURE_BLUE_SIZE = 0x805E;
        public const UInt32 GL_TEXTURE_ALPHA_SIZE = 0x805F;
        public const UInt32 GL_TEXTURE_LUMINANCE_SIZE = 0x8060;
        public const UInt32 GL_TEXTURE_INTENSITY_SIZE = 0x8061;
        public const UInt32 GL_PROXY_TEXTURE_1D = 0x8063;
        public const UInt32 GL_PROXY_TEXTURE_2D = 0x8064;
        public const UInt32 GL_TEXTURE_PRIORITY = 0x8066;
        public const UInt32 GL_TEXTURE_RESIDENT = 0x8067;
        public const UInt32 GL_TEXTURE_BINDING_1D = 0x8068;
        public const UInt32 GL_TEXTURE_BINDING_2D = 0x8069;
        public const UInt32 GL_VERTEX_ARRAY = 0x8074;
        public const UInt32 GL_NORMAL_ARRAY = 0x8075;
        public const UInt32 GL_COLOR_ARRAY = 0x8076;
        public const UInt32 GL_INDEX_ARRAY = 0x8077;
        public const UInt32 GL_TEXTURE_COORD_ARRAY = 0x8078;
        public const UInt32 GL_EDGE_FLAG_ARRAY = 0x8079;
        public const UInt32 GL_VERTEX_ARRAY_SIZE = 0x807A;
        public const UInt32 GL_VERTEX_ARRAY_TYPE = 0x807B;
        public const UInt32 GL_VERTEX_ARRAY_STRIDE = 0x807C;
        public const UInt32 GL_NORMAL_ARRAY_TYPE = 0x807E;
        public const UInt32 GL_NORMAL_ARRAY_STRIDE = 0x807F;
        public const UInt32 GL_COLOR_ARRAY_SIZE = 0x8081;
        public const UInt32 GL_COLOR_ARRAY_TYPE = 0x8082;
        public const UInt32 GL_COLOR_ARRAY_STRIDE = 0x8083;
        public const UInt32 GL_INDEX_ARRAY_TYPE = 0x8085;
        public const UInt32 GL_INDEX_ARRAY_STRIDE = 0x8086;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A;
        public const UInt32 GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C;
        public const UInt32 GL_VERTEX_ARRAY_POINTER = 0x808E;
        public const UInt32 GL_NORMAL_ARRAY_POINTER = 0x808F;
        public const UInt32 GL_COLOR_ARRAY_POINTER = 0x8090;
        public const UInt32 GL_INDEX_ARRAY_POINTER = 0x8091;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092;
        public const UInt32 GL_EDGE_FLAG_ARRAY_POINTER = 0x8093;
        public const UInt32 GL_V2F = 0x2A20;
        public const UInt32 GL_V3F = 0x2A21;
        public const UInt32 GL_C4UB_V2F = 0x2A22;
        public const UInt32 GL_C4UB_V3F = 0x2A23;
        public const UInt32 GL_C3F_V3F = 0x2A24;
        public const UInt32 GL_N3F_V3F = 0x2A25;
        public const UInt32 GL_C4F_N3F_V3F = 0x2A26;
        public const UInt32 GL_T2F_V3F = 0x2A27;
        public const UInt32 GL_T4F_V4F = 0x2A28;
        public const UInt32 GL_T2F_C4UB_V3F = 0x2A29;
        public const UInt32 GL_T2F_C3F_V3F = 0x2A2A;
        public const UInt32 GL_T2F_N3F_V3F = 0x2A2B;
        public const UInt32 GL_T2F_C4F_N3F_V3F = 0x2A2C;
        public const UInt32 GL_T4F_C4F_N3F_V4F = 0x2A2D;
        public const UInt32 GL_EXT_vertex_array = 1;
        public const UInt32 GL_EXT_bgra = 1;
        public const UInt32 GL_EXT_paletted_texture = 1;
        public const UInt32 GL_WIN_swap_hint = 1;
        public const UInt32 GL_WIN_draw_range_elements = 1;
        public const UInt32 GL_VERTEX_ARRAY_EXT = 0x8074;
        public const UInt32 GL_NORMAL_ARRAY_EXT = 0x8075;
        public const UInt32 GL_COLOR_ARRAY_EXT = 0x8076;
        public const UInt32 GL_INDEX_ARRAY_EXT = 0x8077;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_EXT = 0x8078;
        public const UInt32 GL_EDGE_FLAG_ARRAY_EXT = 0x8079;
        public const UInt32 GL_VERTEX_ARRAY_SIZE_EXT = 0x807A;
        public const UInt32 GL_VERTEX_ARRAY_TYPE_EXT = 0x807B;
        public const UInt32 GL_VERTEX_ARRAY_STRIDE_EXT = 0x807C;
        public const UInt32 GL_VERTEX_ARRAY_COUNT_EXT = 0x807D;
        public const UInt32 GL_NORMAL_ARRAY_TYPE_EXT = 0x807E;
        public const UInt32 GL_NORMAL_ARRAY_STRIDE_EXT = 0x807F;
        public const UInt32 GL_NORMAL_ARRAY_COUNT_EXT = 0x8080;
        public const UInt32 GL_COLOR_ARRAY_SIZE_EXT = 0x8081;
        public const UInt32 GL_COLOR_ARRAY_TYPE_EXT = 0x8082;
        public const UInt32 GL_COLOR_ARRAY_STRIDE_EXT = 0x8083;
        public const UInt32 GL_COLOR_ARRAY_COUNT_EXT = 0x8084;
        public const UInt32 GL_INDEX_ARRAY_TYPE_EXT = 0x8085;
        public const UInt32 GL_INDEX_ARRAY_STRIDE_EXT = 0x8086;
        public const UInt32 GL_INDEX_ARRAY_COUNT_EXT = 0x8087;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_SIZE_EXT = 0x8088;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_TYPE_EXT = 0x8089;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B;
        public const UInt32 GL_EDGE_FLAG_ARRAY_STRIDE_EXT = 0x808C;
        public const UInt32 GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D;
        public const UInt32 GL_VERTEX_ARRAY_POINTER_EXT = 0x808E;
        public const UInt32 GL_NORMAL_ARRAY_POINTER_EXT = 0x808F;
        public const UInt32 GL_COLOR_ARRAY_POINTER_EXT = 0x8090;
        public const UInt32 GL_INDEX_ARRAY_POINTER_EXT = 0x8091;
        public const UInt32 GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092;
        public const UInt32 GL_EDGE_FLAG_ARRAY_POINTER_EXT = 0x8093;
        public const UInt32 GL_DOUBLE_EXT = 1;/*DOUBLE*/
        public const UInt32 GL_COLOR_TABLE_FORMAT_EXT = 0x80D8;
        public const UInt32 GL_COLOR_TABLE_WIDTH_EXT = 0x80D9;
        public const UInt32 GL_COLOR_TABLE_RED_SIZE_EXT = 0x80DA;
        public const UInt32 GL_COLOR_TABLE_GREEN_SIZE_EXT = 0x80DB;
        public const UInt32 GL_COLOR_TABLE_BLUE_SIZE_EXT = 0x80DC;
        public const UInt32 GL_COLOR_TABLE_ALPHA_SIZE_EXT = 0x80DD;
        public const UInt32 GL_COLOR_TABLE_LUMINANCE_SIZE_EXT = 0x80DE;
        public const UInt32 GL_COLOR_TABLE_INTENSITY_SIZE_EXT = 0x80DF;
        public const UInt32 GL_COLOR_INDEX1_EXT = 0x80E2;
        public const UInt32 GL_COLOR_INDEX2_EXT = 0x80E3;
        public const UInt32 GL_COLOR_INDEX4_EXT = 0x80E4;
        public const UInt32 GL_COLOR_INDEX8_EXT = 0x80E5;
        public const UInt32 GL_COLOR_INDEX12_EXT = 0x80E6;
        public const UInt32 GL_COLOR_INDEX16_EXT = 0x80E7;
        public const UInt32 GL_MAX_ELEMENTS_VERTICES_WIN = 0x80E8;
        public const UInt32 GL_MAX_ELEMENTS_INDICES_WIN = 0x80E9;
        public const UInt32 GL_PHONG_WIN = 0x80EA;
        public const UInt32 GL_PHONG_HINT_WIN = 0x80EB;
        public static UInt32 FOG_SPECULAR_TEXTURE_WIN = 0x80EC;
        public const UInt32 GLU_VERSION_1_1 = 1;
        public const UInt32 GLU_VERSION_1_2 = 1;
        public const UInt32 GLU_INVALID_ENUM = 100900;
        public const UInt32 GLU_INVALID_VALUE = 100901;
        public const UInt32 GLU_OUT_OF_MEMORY = 100902;
        public const UInt32 GLU_INCOMPATIBLE_GL_VERSION = 100903;
        public const UInt32 GLU_VERSION = 100800;
        public const UInt32 GLU_EXTENSIONS = 100801;
        public const UInt32 GLU_TRUE = 1;
        public const UInt32 GLU_FALSE = 0;
        public const UInt32 GLU_SMOOTH = 100000;
        public const UInt32 GLU_FLAT = 100001;
        public const UInt32 GLU_NONE = 100002;
        public const UInt32 GLU_POINT = 100010;
        public const UInt32 GLU_LINE = 100011;
        public const UInt32 GLU_FILL = 100012;
        public const UInt32 GLU_SILHOUETTE = 100013;
        public const UInt32 GLU_OUTSIDE = 100020;
        public const UInt32 GLU_INSIDE = 100021;
        public const Double GLU_TESS_MAX_COORD = 1.0e150;
        public const UInt32 GLU_TESS_WINDING_RULE = 100140;
        public const UInt32 GLU_TESS_BOUNDARY_ONLY = 100141;
        public const UInt32 GLU_TESS_TOLERANCE = 100142;
        public const UInt32 GLU_TESS_WINDING_ODD = 100130;
        public const UInt32 GLU_TESS_WINDING_NONZERO = 100131;
        public const UInt32 GLU_TESS_WINDING_POSITIVE = 100132;
        public const UInt32 GLU_TESS_WINDING_NEGATIVE = 100133;
        public const UInt32 GLU_TESS_WINDING_ABS_GEQ_TWO = 100134;
        public const UInt32 GLU_TESS_BEGIN = 100100;
        public const UInt32 GLU_TESS_VERTEX = 100101;
        public const UInt32 GLU_TESS_END = 100102;
        public const UInt32 GLU_TESS_ERROR = 100103;
        public const UInt32 GLU_TESS_EDGE_FLAG = 100104;
        public const UInt32 GLU_TESS_COMBINE = 100105;
        public const UInt32 GLU_TESS_BEGIN_DATA = 100106;
        public const UInt32 GLU_TESS_VERTEX_DATA = 100107;
        public const UInt32 GLU_TESS_END_DATA = 100108;
        public const UInt32 GLU_TESS_ERROR_DATA = 100109;
        public const UInt32 GLU_TESS_EDGE_FLAG_DATA = 100110;
        public const UInt32 GLU_TESS_COMBINE_DATA = 100111;
        public const UInt32 GLU_TESS_ERROR1 = 100151;
        public const UInt32 GLU_TESS_ERROR2 = 100152;
        public const UInt32 GLU_TESS_ERROR3 = 100153;
        public const UInt32 GLU_TESS_ERROR4 = 100154;
        public const UInt32 GLU_TESS_ERROR5 = 100155;
        public const UInt32 GLU_TESS_ERROR6 = 100156;
        public const UInt32 GLU_TESS_ERROR7 = 100157;
        public const UInt32 GLU_TESS_ERROR8 = 100158;
        public const UInt32 GLU_TESS_MISSING_BEGIN_POLYGON = 100151;
        public const UInt32 GLU_TESS_MISSING_BEGIN_CONTOUR = 100152;
        public const UInt32 GLU_TESS_MISSING_END_POLYGON = 100153;
        public const UInt32 GLU_TESS_MISSING_END_CONTOUR = 100154;
        public const UInt32 GLU_TESS_COORD_TOO_LARGE = 100155;
        public const UInt32 GLU_TESS_NEED_COMBINE_CALLBACK = 100156;
        public const UInt32 GLU_AUTO_LOAD_MATRIX = 100200;
        public const UInt32 GLU_CULLING = 100201;
        public const UInt32 GLU_SAMPLING_TOLERANCE = 100203;
        public const UInt32 GLU_DISPLAY_MODE = 100204;
        public const UInt32 GLU_PARAMETRIC_TOLERANCE = 100202;
        public const UInt32 GLU_SAMPLING_METHOD = 100205;
        public const UInt32 GLU_U_STEP = 100206;
        public const UInt32 GLU_V_STEP = 100207;
        public const UInt32 GLU_PATH_LENGTH = 100215;
        public const UInt32 GLU_PARAMETRIC_ERROR = 100216;
        public const UInt32 GLU_DOMAIN_DISTANCE = 100217;
        public const UInt32 GLU_MAP1_TRIM_2 = 100210;
        public const UInt32 GLU_MAP1_TRIM_3 = 100211;
        public const UInt32 GLU_OUTLINE_POLYGON = 100240;
        public const UInt32 GLU_OUTLINE_PATCH = 100241;
        public const UInt32 GLU_NURBS_ERROR1 = 100251;
        public const UInt32 GLU_NURBS_ERROR2 = 100252;
        public const UInt32 GLU_NURBS_ERROR3 = 100253;
        public const UInt32 GLU_NURBS_ERROR4 = 100254;
        public const UInt32 GLU_NURBS_ERROR5 = 100255;
        public const UInt32 GLU_NURBS_ERROR6 = 100256;
        public const UInt32 GLU_NURBS_ERROR7 = 100257;
        public const UInt32 GLU_NURBS_ERROR8 = 100258;
        public const UInt32 GLU_NURBS_ERROR9 = 100259;
        public const UInt32 GLU_NURBS_ERROR10 = 100260;
        public const UInt32 GLU_NURBS_ERROR11 = 100261;
        public const UInt32 GLU_NURBS_ERROR12 = 100262;
        public const UInt32 GLU_NURBS_ERROR13 = 100263;
        public const UInt32 GLU_NURBS_ERROR14 = 100264;
        public const UInt32 GLU_NURBS_ERROR15 = 100265;
        public const UInt32 GLU_NURBS_ERROR16 = 100266;
        public const UInt32 GLU_NURBS_ERROR17 = 100267;
        public const UInt32 GLU_NURBS_ERROR18 = 100268;
        public const UInt32 GLU_NURBS_ERROR19 = 100269;
        public const UInt32 GLU_NURBS_ERROR20 = 100270;
        public const UInt32 GLU_NURBS_ERROR21 = 100271;
        public const UInt32 GLU_NURBS_ERROR22 = 100272;
        public const UInt32 GLU_NURBS_ERROR23 = 100273;
        public const UInt32 GLU_NURBS_ERROR24 = 100274;
        public const UInt32 GLU_NURBS_ERROR25 = 100275;
        public const UInt32 GLU_NURBS_ERROR26 = 100276;
        public const UInt32 GLU_NURBS_ERROR27 = 100277;
        public const UInt32 GLU_NURBS_ERROR28 = 100278;
        public const UInt32 GLU_NURBS_ERROR29 = 100279;
        public const UInt32 GLU_NURBS_ERROR30 = 100280;
        public const UInt32 GLU_NURBS_ERROR31 = 100281;
        public const UInt32 GLU_NURBS_ERROR32 = 100282;
        public const UInt32 GLU_NURBS_ERROR33 = 100283;
        public const UInt32 GLU_NURBS_ERROR34 = 100284;
        public const UInt32 GLU_NURBS_ERROR35 = 100285;
        public const UInt32 GLU_NURBS_ERROR36 = 100286;
        public const UInt32 GLU_NURBS_ERROR37 = 100287;
        public const UInt32 GL_UNSIGNED_BYTE_3_3_2 = 0x8032;
        public const UInt32 GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const UInt32 GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const UInt32 GL_UNSIGNED_INT_8_8_8_8 = 0x8035;
        public const UInt32 GL_UNSIGNED_INT_10_10_10_2 = 0x8036;
        public const UInt32 GL_TEXTURE_BINDING_3D = 0x806A;
        public const UInt32 GL_PACK_SKIP_IMAGES = 0x806B;
        public const UInt32 GL_PACK_IMAGE_HEIGHT = 0x806C;
        public const UInt32 GL_UNPACK_SKIP_IMAGES = 0x806D;
        public const UInt32 GL_UNPACK_IMAGE_HEIGHT = 0x806E;
        public const UInt32 GL_TEXTURE_3D = 0x806F;
        public const UInt32 GL_PROXY_TEXTURE_3D = 0x8070;
        public const UInt32 GL_TEXTURE_DEPTH = 0x8071;
        public const UInt32 GL_TEXTURE_WRAP_R = 0x8072;
        public const UInt32 GL_MAX_3D_TEXTURE_SIZE = 0x8073;
        public const UInt32 GL_UNSIGNED_BYTE_2_3_3_REV = 0x8362;
        public const UInt32 GL_UNSIGNED_SHORT_5_6_5 = 0x8363;
        public const UInt32 GL_UNSIGNED_SHORT_5_6_5_REV = 0x8364;
        public const UInt32 GL_UNSIGNED_SHORT_4_4_4_4_REV = 0x8365;
        public const UInt32 GL_UNSIGNED_SHORT_1_5_5_5_REV = 0x8366;
        public const UInt32 GL_UNSIGNED_INT_8_8_8_8_REV = 0x8367;
        public const UInt32 GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368;
        public const UInt32 GL_BGR = 0x80E0;
        public const UInt32 GL_BGRA = 0x80E1;
        public const UInt32 GL_MAX_ELEMENTS_VERTICES = 0x80E8;
        public const UInt32 GL_MAX_ELEMENTS_INDICES = 0x80E9;
        public const UInt32 GL_CLAMP_TO_EDGE = 0x812F;
        public const UInt32 GL_TEXTURE_MIN_LOD = 0x813A;
        public const UInt32 GL_TEXTURE_MAX_LOD = 0x813B;
        public const UInt32 GL_TEXTURE_BASE_LEVEL = 0x813C;
        public const UInt32 GL_TEXTURE_MAX_LEVEL = 0x813D;
        public const UInt32 GL_SMOOTH_POINT_SIZE_RANGE = 0x0B12;
        public const UInt32 GL_SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13;
        public const UInt32 GL_SMOOTH_LINE_WIDTH_RANGE = 0x0B22;
        public const UInt32 GL_SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23;
        public const UInt32 GL_ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public const UInt32 GL_TEXTURE0 = 0x84C0;
        public const UInt32 GL_TEXTURE1 = 0x84C1;
        public const UInt32 GL_TEXTURE2 = 0x84C2;
        public const UInt32 GL_TEXTURE3 = 0x84C3;
        public const UInt32 GL_TEXTURE4 = 0x84C4;
        public const UInt32 GL_TEXTURE5 = 0x84C5;
        public const UInt32 GL_TEXTURE6 = 0x84C6;
        public const UInt32 GL_TEXTURE7 = 0x84C7;
        public const UInt32 GL_TEXTURE8 = 0x84C8;
        public const UInt32 GL_TEXTURE9 = 0x84C9;
        public const UInt32 GL_TEXTURE10 = 0x84CA;
        public const UInt32 GL_TEXTURE11 = 0x84CB;
        public const UInt32 GL_TEXTURE12 = 0x84CC;
        public const UInt32 GL_TEXTURE13 = 0x84CD;
        public const UInt32 GL_TEXTURE14 = 0x84CE;
        public const UInt32 GL_TEXTURE15 = 0x84CF;
        public const UInt32 GL_TEXTURE16 = 0x84D0;
        public const UInt32 GL_TEXTURE17 = 0x84D1;
        public const UInt32 GL_TEXTURE18 = 0x84D2;
        public const UInt32 GL_TEXTURE19 = 0x84D3;
        public const UInt32 GL_TEXTURE20 = 0x84D4;
        public const UInt32 GL_TEXTURE21 = 0x84D5;
        public const UInt32 GL_TEXTURE22 = 0x84D6;
        public const UInt32 GL_TEXTURE23 = 0x84D7;
        public const UInt32 GL_TEXTURE24 = 0x84D8;
        public const UInt32 GL_TEXTURE25 = 0x84D9;
        public const UInt32 GL_TEXTURE26 = 0x84DA;
        public const UInt32 GL_TEXTURE27 = 0x84DB;
        public const UInt32 GL_TEXTURE28 = 0x84DC;
        public const UInt32 GL_TEXTURE29 = 0x84DD;
        public const UInt32 GL_TEXTURE30 = 0x84DE;
        public const UInt32 GL_TEXTURE31 = 0x84DF;
        public const UInt32 GL_ACTIVE_TEXTURE = 0x84E0;
        public const UInt32 GL_MULTISAMPLE = 0x809D;
        public const UInt32 GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const UInt32 GL_SAMPLE_ALPHA_TO_ONE = 0x809F;
        public const UInt32 GL_SAMPLE_COVERAGE = 0x80A0;
        public const UInt32 GL_SAMPLE_BUFFERS = 0x80A8;
        public const UInt32 GL_SAMPLES = 0x80A9;
        public const UInt32 GL_SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const UInt32 GL_SAMPLE_COVERAGE_INVERT = 0x80AB;
        public const UInt32 GL_TEXTURE_CUBE_MAP = 0x8513;
        public const UInt32 GL_TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const UInt32 GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const UInt32 GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const UInt32 GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const UInt32 GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const UInt32 GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const UInt32 GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const UInt32 GL_PROXY_TEXTURE_CUBE_MAP = 0x851B;
        public const UInt32 GL_MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
        public const UInt32 GL_COMPRESSED_RGB = 0x84ED;
        public const UInt32 GL_COMPRESSED_RGBA = 0x84EE;
        public const UInt32 GL_TEXTURE_COMPRESSION_HINT = 0x84EF;
        public const UInt32 GL_TEXTURE_COMPRESSED_IMAGE_SIZE = 0x86A0;
        public const UInt32 GL_TEXTURE_COMPRESSED = 0x86A1;
        public const UInt32 GL_NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const UInt32 GL_COMPRESSED_TEXTURE_FORMATS = 0x86A3;
        public const UInt32 GL_CLAMP_TO_BORDER = 0x812D;
        public const UInt32 GL_BLEND_DST_RGB = 0x80C8;
        public const UInt32 GL_BLEND_SRC_RGB = 0x80C9;
        public const UInt32 GL_BLEND_DST_ALPHA = 0x80CA;
        public const UInt32 GL_BLEND_SRC_ALPHA = 0x80CB;
        public const UInt32 GL_POINT_FADE_THRESHOLD_SIZE = 0x8128;
        public const UInt32 GL_DEPTH_COMPONENT16 = 0x81A5;
        public const UInt32 GL_DEPTH_COMPONENT24 = 0x81A6;
        public const UInt32 GL_DEPTH_COMPONENT32 = 0x81A7;
        public const UInt32 GL_MIRRORED_REPEAT = 0x8370;
        public const UInt32 GL_MAX_TEXTURE_LOD_BIAS = 0x84FD;
        public const UInt32 GL_TEXTURE_LOD_BIAS = 0x8501;
        public const UInt32 GL_INCR_WRAP = 0x8507;
        public const UInt32 GL_DECR_WRAP = 0x8508;
        public const UInt32 GL_TEXTURE_DEPTH_SIZE = 0x884A;
        public const UInt32 GL_TEXTURE_COMPARE_MODE = 0x884C;
        public const UInt32 GL_TEXTURE_COMPARE_FUNC = 0x884D;
        public const UInt32 GL_BUFFER_SIZE = 0x8764;
        public const UInt32 GL_BUFFER_USAGE = 0x8765;
        public const UInt32 GL_QUERY_COUNTER_BITS = 0x8864;
        public const UInt32 GL_CURRENT_QUERY = 0x8865;
        public const UInt32 GL_QUERY_RESULT = 0x8866;
        public const UInt32 GL_QUERY_RESULT_AVAILABLE = 0x8867;
        public const UInt32 GL_ARRAY_BUFFER = 0x8892;
        public const UInt32 GL_ELEMENT_ARRAY_BUFFER = 0x8893;
        public const UInt32 GL_ARRAY_BUFFER_BINDING = 0x8894;
        public const UInt32 GL_ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;
        public const UInt32 GL_READ_ONLY = 0x88B8;
        public const UInt32 GL_WRITE_ONLY = 0x88B9;
        public const UInt32 GL_READ_WRITE = 0x88BA;
        public const UInt32 GL_BUFFER_ACCESS = 0x88BB;
        public const UInt32 GL_BUFFER_MAPPED = 0x88BC;
        public const UInt32 GL_BUFFER_MAP_POINTER = 0x88BD;
        public const UInt32 GL_STREAM_DRAW = 0x88E0;
        public const UInt32 GL_STREAM_READ = 0x88E1;
        public const UInt32 GL_STREAM_COPY = 0x88E2;
        public const UInt32 GL_STATIC_DRAW = 0x88E4;
        public const UInt32 GL_STATIC_READ = 0x88E5;
        public const UInt32 GL_STATIC_COPY = 0x88E6;
        public const UInt32 GL_DYNAMIC_DRAW = 0x88E8;
        public const UInt32 GL_DYNAMIC_READ = 0x88E9;
        public const UInt32 GL_DYNAMIC_COPY = 0x88EA;
        public const UInt32 GL_SAMPLES_PASSED = 0x8914;
        public const UInt32 GL_BLEND_EQUATION_RGB = 0x8009;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public const UInt32 GL_CURRENT_VERTEX_ATTRIB = 0x8626;
        public const UInt32 GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public const UInt32 GL_STENCIL_BACK_FUNC = 0x8800;
        public const UInt32 GL_STENCIL_BACK_FAIL = 0x8801;
        public const UInt32 GL_STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public const UInt32 GL_STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public const UInt32 GL_MAX_DRAW_BUFFERS = 0x8824;
        public const UInt32 GL_DRAW_BUFFER0 = 0x8825;
        public const UInt32 GL_DRAW_BUFFER1 = 0x8826;
        public const UInt32 GL_DRAW_BUFFER2 = 0x8827;
        public const UInt32 GL_DRAW_BUFFER3 = 0x8828;
        public const UInt32 GL_DRAW_BUFFER4 = 0x8829;
        public const UInt32 GL_DRAW_BUFFER5 = 0x882A;
        public const UInt32 GL_DRAW_BUFFER6 = 0x882B;
        public const UInt32 GL_DRAW_BUFFER7 = 0x882C;
        public const UInt32 GL_DRAW_BUFFER8 = 0x882D;
        public const UInt32 GL_DRAW_BUFFER9 = 0x882E;
        public const UInt32 GL_DRAW_BUFFER10 = 0x882F;
        public const UInt32 GL_DRAW_BUFFER11 = 0x8830;
        public const UInt32 GL_DRAW_BUFFER12 = 0x8831;
        public const UInt32 GL_DRAW_BUFFER13 = 0x8832;
        public const UInt32 GL_DRAW_BUFFER14 = 0x8833;
        public const UInt32 GL_DRAW_BUFFER15 = 0x8834;
        public const UInt32 GL_BLEND_EQUATION_ALPHA = 0x883D;
        public const UInt32 GL_MAX_VERTEX_ATTRIBS = 0x8869;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public const UInt32 GL_MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public const UInt32 GL_FRAGMENT_SHADER = 0x8B30;
        public const UInt32 GL_VERTEX_SHADER = 0x8B31;
        public const UInt32 GL_MAX_FRAGMENT_UNIFORM_COMPONENTS = 0x8B49;
        public const UInt32 GL_MAX_VERTEX_UNIFORM_COMPONENTS = 0x8B4A;
        public const UInt32 GL_MAX_VARYING_FLOATS = 0x8B4B;
        public const UInt32 GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public const UInt32 GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public const UInt32 GL_SHADER_TYPE = 0x8B4F;
        public const UInt32 GL_FLOAT_VEC2 = 0x8B50;
        public const UInt32 GL_FLOAT_Vertex = 0x8B51;
        public const UInt32 GL_FLOAT_VEC4 = 0x8B52;
        public const UInt32 GL_INT_VEC2 = 0x8B53;
        public const UInt32 GL_INT_Vertex = 0x8B54;
        public const UInt32 GL_INT_VEC4 = 0x8B55;
        public const UInt32 GL_BOOL = 0x8B56;
        public const UInt32 GL_BOOL_VEC2 = 0x8B57;
        public const UInt32 GL_BOOL_Vertex = 0x8B58;
        public const UInt32 GL_BOOL_VEC4 = 0x8B59;
        public const UInt32 GL_FLOAT_MAT2 = 0x8B5A;
        public const UInt32 GL_FLOAT_MAT3 = 0x8B5B;
        public const UInt32 GL_FLOAT_MAT4 = 0x8B5C;
        public const UInt32 GL_SAMPLER_1D = 0x8B5D;
        public const UInt32 GL_SAMPLER_2D = 0x8B5E;
        public const UInt32 GL_SAMPLER_3D = 0x8B5F;
        public const UInt32 GL_SAMPLER_CUBE = 0x8B60;
        public const UInt32 GL_SAMPLER_1D_SHADOW = 0x8B61;
        public const UInt32 GL_SAMPLER_2D_SHADOW = 0x8B62;
        public const UInt32 GL_DELETE_STATUS = 0x8B80;
        public const UInt32 GL_COMPILE_STATUS = 0x8B81;
        public const UInt32 GL_LINK_STATUS = 0x8B82;
        public const UInt32 GL_VALIDATE_STATUS = 0x8B83;
        public const UInt32 GL_INFO_LOG_LENGTH = 0x8B84;
        public const UInt32 GL_ATTACHED_SHADERS = 0x8B85;
        public const UInt32 GL_ACTIVE_UNIFORMS = 0x8B86;
        public const UInt32 GL_ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
        public const UInt32 GL_SHADER_SOURCE_LENGTH = 0x8B88;
        public const UInt32 GL_ACTIVE_ATTRIBUTES = 0x8B89;
        public const UInt32 GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
        public const UInt32 GL_FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B;
        public const UInt32 GL_SHADING_LANGUAGE_VERSION = 0x8B8C;
        public const UInt32 GL_CURRENT_PROGRAM = 0x8B8D;
        public const UInt32 GL_POINT_SPRITE_COORD_ORIGIN = 0x8CA0;
        public const UInt32 GL_LOWER_LEFT = 0x8CA1;
        public const UInt32 GL_UPPER_LEFT = 0x8CA2;
        public const UInt32 GL_STENCIL_BACK_REF = 0x8CA3;
        public const UInt32 GL_STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public const UInt32 GL_STENCIL_BACK_WRITEMASK = 0x8CA5;
        public const UInt32 GL_PIXEL_PACK_BUFFER = 0x88EB;
        public const UInt32 GL_PIXEL_UNPACK_BUFFER = 0x88EC;
        public const UInt32 GL_PIXEL_PACK_BUFFER_BINDING = 0x88ED;
        public const UInt32 GL_PIXEL_UNPACK_BUFFER_BINDING = 0x88EF;
        public const UInt32 GL_FLOAT_MAT2x3 = 0x8B65;
        public const UInt32 GL_FLOAT_MAT2x4 = 0x8B66;
        public const UInt32 GL_FLOAT_MAT3x2 = 0x8B67;
        public const UInt32 GL_FLOAT_MAT3x4 = 0x8B68;
        public const UInt32 GL_FLOAT_MAT4x2 = 0x8B69;
        public const UInt32 GL_FLOAT_MAT4x3 = 0x8B6A;
        public const UInt32 GL_SRGB = 0x8C40;
        public const UInt32 GL_SRGB8 = 0x8C41;
        public const UInt32 GL_SRGB_ALPHA = 0x8C42;
        public const UInt32 GL_SRGB8_ALPHA8 = 0x8C43;
        public const UInt32 GL_COMPRESSED_SRGB = 0x8C48;
        public const UInt32 GL_COMPRESSED_SRGB_ALPHA = 0x8C49;
        public const UInt32 GL_COMPARE_REF_TO_TEXTURE = 0x884E;
        public const UInt32 GL_CLIP_DISTANCE0 = 0x3000;
        public const UInt32 GL_CLIP_DISTANCE1 = 0x3001;
        public const UInt32 GL_CLIP_DISTANCE2 = 0x3002;
        public const UInt32 GL_CLIP_DISTANCE3 = 0x3003;
        public const UInt32 GL_CLIP_DISTANCE4 = 0x3004;
        public const UInt32 GL_CLIP_DISTANCE5 = 0x3005;
        public const UInt32 GL_CLIP_DISTANCE6 = 0x3006;
        public const UInt32 GL_CLIP_DISTANCE7 = 0x3007;
        public const UInt32 GL_MAX_CLIP_DISTANCES = 0x0D32;
        public const UInt32 GL_MAJOR_VERSION = 0x821B;
        public const UInt32 GL_MINOR_VERSION = 0x821C;
        public const UInt32 GL_NUM_EXTENSIONS = 0x821D;
        public const UInt32 GL_CONTEXT_FLAGS = 0x821E;
        public const UInt32 GL_DEPTH_BUFFER = 0x8223;
        public const UInt32 GL_STENCIL_BUFFER = 0x8224;
        public const UInt32 GL_COMPRESSED_RED = 0x8225;
        public const UInt32 GL_COMPRESSED_RG = 0x8226;
        public const UInt32 GL_CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT = 0x0001;
        public const UInt32 GL_RGBA32F = 0x8814;
        public const UInt32 GL_RGB32F = 0x8815;
        public const UInt32 GL_RGBA16F = 0x881A;
        public const UInt32 GL_RGB16F = 0x881B;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_INTEGER = 0x88FD;
        public const UInt32 GL_MAX_ARRAY_TEXTURE_LAYERS = 0x88FF;
        public const UInt32 GL_MIN_PROGRAM_TEXEL_OFFSET = 0x8904;
        public const UInt32 GL_MAX_PROGRAM_TEXEL_OFFSET = 0x8905;
        public const UInt32 GL_CLAMP_READ_COLOR = 0x891C;
        public const UInt32 GL_FIXED_ONLY = 0x891D;
        public const UInt32 GL_MAX_VARYING_COMPONENTS = 0x8B4B;
        public const UInt32 GL_TEXTURE_1D_ARRAY = 0x8C18;
        public const UInt32 GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19;
        public const UInt32 GL_TEXTURE_2D_ARRAY = 0x8C1A;
        public const UInt32 GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B;
        public const UInt32 GL_TEXTURE_BINDING_1D_ARRAY = 0x8C1C;
        public const UInt32 GL_TEXTURE_BINDING_2D_ARRAY = 0x8C1D;
        public const UInt32 GL_R11F_G11F_B10F = 0x8C3A;
        public const UInt32 GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B;
        public const UInt32 GL_RGB9_E5 = 0x8C3D;
        public const UInt32 GL_UNSIGNED_INT_5_9_9_9_REV = 0x8C3E;
        public const UInt32 GL_TEXTURE_SHARED_SIZE = 0x8C3F;
        public const UInt32 GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76;
        public const UInt32 GL_TRANSFORM_FEEDBACK_BUFFER_MODE = 0x8C7F;
        public const UInt32 GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 0x8C80;
        public const UInt32 GL_TRANSFORM_FEEDBACK_VARYINGS = 0x8C83;
        public const UInt32 GL_TRANSFORM_FEEDBACK_BUFFER_START = 0x8C84;
        public const UInt32 GL_TRANSFORM_FEEDBACK_BUFFER_SIZE = 0x8C85;
        public const UInt32 GL_PRIMITIVES_GENERATED = 0x8C87;
        public const UInt32 GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88;
        public const UInt32 GL_RASTERIZER_DISCARD = 0x8C89;
        public const UInt32 GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
        public const UInt32 GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 0x8C8B;
        public const UInt32 GL_INTERLEAVED_ATTRIBS = 0x8C8C;
        public const UInt32 GL_SEPARATE_ATTRIBS = 0x8C8D;
        public const UInt32 GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
        public const UInt32 GL_TRANSFORM_FEEDBACK_BUFFER_BINDING = 0x8C8F;
        public const UInt32 GL_RGBA32UI = 0x8D70;
        public const UInt32 GL_RGB32UI = 0x8D71;
        public const UInt32 GL_RGBA16UI = 0x8D76;
        public const UInt32 GL_RGB16UI = 0x8D77;
        public const UInt32 GL_RGBA8UI = 0x8D7C;
        public const UInt32 GL_RGB8UI = 0x8D7D;
        public const UInt32 GL_RGBA32I = 0x8D82;
        public const UInt32 GL_RGB32I = 0x8D83;
        public const UInt32 GL_RGBA16I = 0x8D88;
        public const UInt32 GL_RGB16I = 0x8D89;
        public const UInt32 GL_RGBA8I = 0x8D8E;
        public const UInt32 GL_RGB8I = 0x8D8F;
        public const UInt32 GL_RED_INTEGER = 0x8D94;
        public const UInt32 GL_GREEN_INTEGER = 0x8D95;
        public const UInt32 GL_BLUE_INTEGER = 0x8D96;
        public const UInt32 GL_RGB_INTEGER = 0x8D98;
        public const UInt32 GL_RGBA_INTEGER = 0x8D99;
        public const UInt32 GL_BGR_INTEGER = 0x8D9A;
        public const UInt32 GL_BGRA_INTEGER = 0x8D9B;
        public const UInt32 GL_SAMPLER_1D_ARRAY = 0x8DC0;
        public const UInt32 GL_SAMPLER_2D_ARRAY = 0x8DC1;
        public const UInt32 GL_SAMPLER_1D_ARRAY_SHADOW = 0x8DC3;
        public const UInt32 GL_SAMPLER_2D_ARRAY_SHADOW = 0x8DC4;
        public const UInt32 GL_SAMPLER_CUBE_SHADOW = 0x8DC5;
        public const UInt32 GL_UNSIGNED_INT_VEC2 = 0x8DC6;
        public const UInt32 GL_UNSIGNED_INT_Vertex = 0x8DC7;
        public const UInt32 GL_UNSIGNED_INT_VEC4 = 0x8DC8;
        public const UInt32 GL_INT_SAMPLER_1D = 0x8DC9;
        public const UInt32 GL_INT_SAMPLER_2D = 0x8DCA;
        public const UInt32 GL_INT_SAMPLER_3D = 0x8DCB;
        public const UInt32 GL_INT_SAMPLER_CUBE = 0x8DCC;
        public const UInt32 GL_INT_SAMPLER_1D_ARRAY = 0x8DCE;
        public const UInt32 GL_INT_SAMPLER_2D_ARRAY = 0x8DCF;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_1D = 0x8DD1;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_2D = 0x8DD2;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_3D = 0x8DD3;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_CUBE = 0x8DD4;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_1D_ARRAY = 0x8DD6;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_2D_ARRAY = 0x8DD7;
        public const UInt32 GL_QUERY_WAIT = 0x8E13;
        public const UInt32 GL_QUERY_NO_WAIT = 0x8E14;
        public const UInt32 GL_QUERY_BY_REGION_WAIT = 0x8E15;
        public const UInt32 GL_QUERY_BY_REGION_NO_WAIT = 0x8E16;
        public const UInt32 GL_BUFFER_ACCESS_FLAGS = 0x911F;
        public const UInt32 GL_BUFFER_MAP_LENGTH = 0x9120;
        public const UInt32 GL_BUFFER_MAP_OFFSET = 0x9121;
        public const UInt32 GL_R8 = 0x8229;
        public const UInt32 GL_R16 = 0x822A;
        public const UInt32 GL_RG8 = 0x822B;
        public const UInt32 GL_RG16 = 0x822C;
        public const UInt32 GL_R16F = 0x822D;
        public const UInt32 GL_R32F = 0x822E;
        public const UInt32 GL_RG16F = 0x822F;
        public const UInt32 GL_RG32F = 0x8230;
        public const UInt32 GL_R8I = 0x8231;
        public const UInt32 GL_R8UI = 0x8232;
        public const UInt32 GL_R16I = 0x8233;
        public const UInt32 GL_R16UI = 0x8234;
        public const UInt32 GL_R32I = 0x8235;
        public const UInt32 GL_R32UI = 0x8236;
        public const UInt32 GL_RG8I = 0x8237;
        public const UInt32 GL_RG8UI = 0x8238;
        public const UInt32 GL_RG16I = 0x8239;
        public const UInt32 GL_RG16UI = 0x823A;
        public const UInt32 GL_RG32I = 0x823B;
        public const UInt32 GL_RG32UI = 0x823C;
        public const UInt32 GL_RG = 0x8227;
        public const UInt32 GL_RG_INTEGER = 0x8228;
        public const UInt32 GL_SAMPLER_2D_RECT = 0x8B63;
        public const UInt32 GL_SAMPLER_2D_RECT_SHADOW = 0x8B64;
        public const UInt32 GL_SAMPLER_BUFFER = 0x8DC2;
        public const UInt32 GL_INT_SAMPLER_2D_RECT = 0x8DCD;
        public const UInt32 GL_INT_SAMPLER_BUFFER = 0x8DD0;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_2D_RECT = 0x8DD5;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_BUFFER = 0x8DD8;
        public const UInt32 GL_TEXTURE_BUFFER = 0x8C2A;
        public const UInt32 GL_MAX_TEXTURE_BUFFER_SIZE = 0x8C2B;
        public const UInt32 GL_TEXTURE_BINDING_BUFFER = 0x8C2C;
        public const UInt32 GL_TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D;
        public const UInt32 GL_TEXTURE_BUFFER_FORMAT = 0x8C2E;
        public const UInt32 GL_TEXTURE_RECTANGLE = 0x84F5;
        public const UInt32 GL_TEXTURE_BINDING_RECTANGLE = 0x84F6;
        public const UInt32 GL_PROXY_TEXTURE_RECTANGLE = 0x84F7;
        public const UInt32 GL_MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8;
        public const UInt32 GL_RED_SNORM = 0x8F90;
        public const UInt32 GL_RG_SNORM = 0x8F91;
        public const UInt32 GL_RGB_SNORM = 0x8F92;
        public const UInt32 GL_RGBA_SNORM = 0x8F93;
        public const UInt32 GL_R8_SNORM = 0x8F94;
        public const UInt32 GL_RG8_SNORM = 0x8F95;
        public const UInt32 GL_RGB8_SNORM = 0x8F96;
        public const UInt32 GL_RGBA8_SNORM = 0x8F97;
        public const UInt32 GL_R16_SNORM = 0x8F98;
        public const UInt32 GL_RG16_SNORM = 0x8F99;
        public const UInt32 GL_RGB16_SNORM = 0x8F9A;
        public const UInt32 GL_RGBA16_SNORM = 0x8F9B;
        public const UInt32 GL_SIGNED_NORMALIZED = 0x8F9C;
        public const UInt32 GL_PRIMITIVE_RESTART = 0x8F9D;
        public const UInt32 GL_PRIMITIVE_RESTART_INDEX = 0x8F9E;
        public const UInt32 GL_CONTEXT_CORE_PROFILE_BIT = 0x00000001;
        public const UInt32 GL_CONTEXT_COMPATIBILITY_PROFILE_BIT = 0x00000002;
        public const UInt32 GL_LINES_ADJACENCY = 0x000A;
        public const UInt32 GL_LINE_STRIP_ADJACENCY = 0x000B;
        public const UInt32 GL_TRIANGLES_ADJACENCY = 0x000C;
        public const UInt32 GL_TRIANGLE_STRIP_ADJACENCY = 0x000D;
        public const UInt32 GL_PROGRAM_POINT_SIZE = 0x8642;
        public const UInt32 GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 0x8C29;
        public const UInt32 GL_FRAMEBUFFER_ATTACHMENT_LAYERED = 0x8DA7;
        public const UInt32 GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS = 0x8DA8;
        public const UInt32 GL_GEOMETRY_SHADER = 0x8DD9;
        public const UInt32 GL_GEOMETRY_VERTICES_OUT = 0x8916;
        public const UInt32 GL_GEOMETRY_INPUT_TYPE = 0x8917;
        public const UInt32 GL_GEOMETRY_OUTPUT_TYPE = 0x8918;
        public const UInt32 GL_MAX_GEOMETRY_UNIFORM_COMPONENTS = 0x8DDF;
        public const UInt32 GL_MAX_GEOMETRY_OUTPUT_VERTICES = 0x8DE0;
        public const UInt32 GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 0x8DE1;
        public const UInt32 GL_MAX_VERTEX_OUTPUT_COMPONENTS = 0x9122;
        public const UInt32 GL_MAX_GEOMETRY_INPUT_COMPONENTS = 0x9123;
        public const UInt32 GL_MAX_GEOMETRY_OUTPUT_COMPONENTS = 0x9124;
        public const UInt32 GL_MAX_FRAGMENT_INPUT_COMPONENTS = 0x9125;
        public const UInt32 GL_CONTEXT_PROFILE_MASK = 0x9126;
        public const UInt32 GL_VERTEX_ATTRIB_ARRAY_DIVISOR = 0x88FE;
        public const UInt32 GL_SAMPLE_SHADING = 0x8C36;
        public const UInt32 GL_MIN_SAMPLE_SHADING_VALUE = 0x8C37;
        public const UInt32 GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5E;
        public const UInt32 GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5F;
        public const UInt32 GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009;
        public const UInt32 GL_TEXTURE_BINDING_CUBE_MAP_ARRAY = 0x900A;
        public const UInt32 GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B;
        public const UInt32 GL_SAMPLER_CUBE_MAP_ARRAY = 0x900C;
        public const UInt32 GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW = 0x900D;
        public const UInt32 GL_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900E;
        public const UInt32 GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900F;
        #endregion

        #region OpenGL DLL Functions
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glAccum(UInt32 op, Single value);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glAlphaFunc(UInt32 func, Single ref_notkeword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern Byte glAreTexturesResident(Int32 n, UInt32[] textures, Byte[] residences);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glArrayElement(Int32 i);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glBegin(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glBindTexture(UInt32 target, UInt32 texture);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glBitmap(Int32 width, Int32 height, Single xorig, Single yorig, Single xmove, Single ymove, Byte[] bitmap);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glBlendFunc(UInt32 sfactor, UInt32 dfactor);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCallList(UInt32 list);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCallLists(Int32 n, UInt32 type, IntPtr lists);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCallLists(Int32 n, UInt32 type, UInt32[] lists);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCallLists(Int32 n, UInt32 type, Byte[] lists);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClear(UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClearAccum(Single red, Single green, Single blue, Single alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClearColor(Single red, Single green, Single blue, Single alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClearDepth(Double depth);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClearIndex(Single c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClearStencil(Int32 s);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glClipPlane(UInt32 plane, Double[] equation);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3b(Byte red, Byte green, Byte blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3bv(Byte[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3d(Double red, Double green, Double blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3f(Single red, Single green, Single blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3i(Int32 red, Int32 green, Int32 blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3s(Int16 red, Int16 green, Int16 blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3ub(Byte red, Byte green, Byte blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3ubv(Byte[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3ui(UInt32 red, UInt32 green, UInt32 blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3uiv(UInt32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3us(UInt16 red, UInt16 green, UInt16 blue);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor3usv(UInt16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4b(Byte red, Byte green, Byte blue, Byte alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4bv(Byte[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4d(Double red, Double green, Double blue, Double alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4f(Single red, Single green, Single blue, Single alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4i(Int32 red, Int32 green, Int32 blue, Int32 alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4s(Int16 red, Int16 green, Int16 blue, Int16 alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4ub(Byte red, Byte green, Byte blue, Byte alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4ubv(Byte[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4ui(UInt32 red, UInt32 green, UInt32 blue, UInt32 alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4uiv(UInt32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4us(UInt16 red, UInt16 green, UInt16 blue, UInt16 alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColor4usv(UInt16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColorMask(Byte red, Byte green, Byte blue, Byte alpha);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColorMaterial(UInt32 face, UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glColorPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCopyPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 type);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCopyTexImage1D(UInt32 target, Int32 level, UInt32 internalFormat, Int32 x, Int32 y, Int32 width, Int32 border);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCopyTexImage2D(UInt32 target, Int32 level, UInt32 internalFormat, Int32 x, Int32 y, Int32 width, Int32 height, Int32 border);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCopyTexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 x, Int32 y, Int32 width);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCopyTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 x, Int32 y, Int32 width, Int32 height);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glCullFace(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDeleteLists(UInt32 list, Int32 range);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDeleteTextures(Int32 n, UInt32[] textures);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDepthFunc(UInt32 func);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDepthMask(Byte flag);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDepthRange(Double zNear, Double zFar);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDisable(UInt32 cap);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDisableClientState(UInt32 array);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawArrays(UInt32 mode, Int32 first, Int32 count);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawBuffer(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawElements(UInt32 mode, Int32 count, UInt32 type, IntPtr indices);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawElements(UInt32 mode, Int32 count, UInt32 type, UInt32[] indices);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, Single[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, UInt32[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, UInt16[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, Byte[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glDrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEdgeFlag(Byte flag);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEdgeFlagPointer(Int32 stride, Int32[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEdgeFlagv(Byte[] flag);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEnable(UInt32 cap);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEnableClientState(UInt32 array);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEnd();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEndList();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord1d(Double u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord1dv(Double[] u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord1f(Single u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord1fv(Single[] u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord2d(Double u, Double v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord2dv(Double[] u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord2f(Single u, Single v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalCoord2fv(Single[] u);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalMesh1(UInt32 mode, Int32 i1, Int32 i2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalMesh2(UInt32 mode, Int32 i1, Int32 i2, Int32 j1, Int32 j2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalPoint1(Int32 i);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glEvalPoint2(Int32 i, Int32 j);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFeedbackBuffer(Int32 size, UInt32 type, Single[] buffer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFinish();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFlush();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFogf(UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFogfv(UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFogi(UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFogiv(UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFrontFace(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glFrustum(Double left, Double right, Double bottom, Double top, Double zNear, Double zFar);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern UInt32 glGenLists(Int32 range);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGenTextures(Int32 n, UInt32[] textures);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetBooleanv(UInt32 pname, Byte[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetClipPlane(UInt32 plane, Double[] equation);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetDoublev(UInt32 pname, Double[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern UInt32 glGetError();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetFloatv(UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetIntegerv(UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetLightfv(UInt32 light, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetLightiv(UInt32 light, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetMapdv(UInt32 target, UInt32 query, Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetMapfv(UInt32 target, UInt32 query, Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetMapiv(UInt32 target, UInt32 query, Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetMaterialfv(UInt32 face, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetMaterialiv(UInt32 face, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetPixelMapfv(UInt32 map, Single[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetPixelMapuiv(UInt32 map, UInt32[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetPixelMapusv(UInt32 map, UInt16[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetPointerv(UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetPolygonStipple(Byte[] mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private unsafe static extern SByte* glGetString(UInt32 name);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexEnvfv(UInt32 target, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexEnviv(UInt32 target, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexGendv(UInt32 coord, UInt32 pname, Double[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexGenfv(UInt32 coord, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexGeniv(UInt32 coord, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexImage(UInt32 target, Int32 level, UInt32 format, UInt32 type, Int32[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexLevelParameterfv(UInt32 target, Int32 level, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexLevelParameteriv(UInt32 target, Int32 level, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexParameterfv(UInt32 target, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glGetTexParameteriv(UInt32 target, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glHint(UInt32 target, UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexMask(UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexPointer(UInt32 type, Int32 stride, Int32[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexd(Double c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexdv(Double[] c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexf(Single c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexfv(Single[] c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexi(Int32 c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexiv(Int32[] c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexs(Int16 c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexsv(Int16[] c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexub(Byte c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glIndexubv(Byte[] c);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glInitNames();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glInterleavedArrays(UInt32 format, Int32 stride, Int32[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern Byte glIsEnabled(UInt32 cap);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern Byte glIsList(UInt32 list);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern Byte glIsTexture(UInt32 texture);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightModelf(UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightModelfv(UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightModeli(UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightModeliv(UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightf(UInt32 light, UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightfv(UInt32 light, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLighti(UInt32 light, UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLightiv(UInt32 light, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLineStipple(Int32 factor, UInt16 pattern);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLineWidth(Single width);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glListBase(UInt32 base_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLoadIdentity();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLoadMatrixd(Double[] m);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLoadMatrixf(Single[] m);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLoadName(UInt32 name);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glLogicOp(UInt32 opcode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMap1d(UInt32 target, Double u1, Double u2, Int32 stride, Int32 order, Double[] points);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMap1f(UInt32 target, Single u1, Single u2, Int32 stride, Int32 order, Single[] points);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMap2d(UInt32 target, Double u1, Double u2, Int32 ustride, Int32 uorder, Double v1, Double v2, Int32 vstride, Int32 vorder, Double[] points);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMap2f(UInt32 target, Single u1, Single u2, Int32 ustride, Int32 uorder, Single v1, Single v2, Int32 vstride, Int32 vorder, Single[] points);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMapGrid1d(Int32 un, Double u1, Double u2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMapGrid1f(Int32 un, Single u1, Single u2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMapGrid2d(Int32 un, Double u1, Double u2, Int32 vn, Double v1, Double v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMapGrid2f(Int32 un, Single u1, Single u2, Int32 vn, Single v1, Single v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMaterialf(UInt32 face, UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMaterialfv(UInt32 face, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMateriali(UInt32 face, UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMaterialiv(UInt32 face, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMatrixMode(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMultMatrixd(Double[] m);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glMultMatrixf(Single[] m);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNewList(UInt32 list, UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3b(Byte nx, Byte ny, Byte nz);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3bv(Byte[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3d(Double nx, Double ny, Double nz);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3f(Single nx, Single ny, Single nz);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3i(Int32 nx, Int32 ny, Int32 nz);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3s(Int16 nx, Int16 ny, Int16 nz);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormal3sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormalPointer(UInt32 type, Int32 stride, IntPtr pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glNormalPointer(UInt32 type, Int32 stride, Single[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glOrtho(Double left, Double right, Double bottom, Double top, Double zNear, Double zFar);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPassThrough(Single token);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelMapfv(UInt32 map, Int32 mapsize, Single[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelMapuiv(UInt32 map, Int32 mapsize, UInt32[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelMapusv(UInt32 map, Int32 mapsize, UInt16[] values);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelStoref(UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelStorei(UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelTransferf(UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelTransferi(UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPixelZoom(Single xfactor, Single yfactor);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPointSize(Single size);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPolygonMode(UInt32 face, UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPolygonOffset(Single factor, Single units);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPolygonStipple(Byte[] mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPopAttrib();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPopClientAttrib();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPopMatrix();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPopName();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPrioritizeTextures(Int32 n, UInt32[] textures, Single[] priorities);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPushAttrib(UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPushClientAttrib(UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPushMatrix();
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glPushName(UInt32 name);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2d(Double x, Double y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2f(Single x, Single y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2i(Int32 x, Int32 y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2s(Int16 x, Int16 y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos2sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3d(Double x, Double y, Double z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3f(Single x, Single y, Single z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3i(Int32 x, Int32 y, Int32 z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3s(Int16 x, Int16 y, Int16 z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos3sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4d(Double x, Double y, Double z, Double w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4f(Single x, Single y, Single z, Single w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4i(Int32 x, Int32 y, Int32 z, Int32 w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4s(Int16 x, Int16 y, Int16 z, Int16 w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRasterPos4sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glReadBuffer(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glReadPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 format, UInt32 type, Byte[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glReadPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectd(Double x1, Double y1, Double x2, Double y2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectdv(Double[] v1, Double[] v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectf(Single x1, Single y1, Single x2, Single y2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectfv(Single[] v1, Single[] v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRecti(Int32 x1, Int32 y1, Int32 x2, Int32 y2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectiv(Int32[] v1, Int32[] v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRects(Int16 x1, Int16 y1, Int16 x2, Int16 y2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRectsv(Int16[] v1, Int16[] v2);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern Int32 glRenderMode(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRotated(Double angle, Double x, Double y, Double z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glRotatef(Single angle, Single x, Single y, Single z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glScaled(Double x, Double y, Double z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glScalef(Single x, Single y, Single z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glScissor(Int32 x, Int32 y, Int32 width, Int32 height);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glSelectBuffer(Int32 size, UInt32[] buffer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glShadeModel(UInt32 mode);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glStencilFunc(UInt32 func, Int32 ref_notkeword, UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glStencilMask(UInt32 mask);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glStencilOp(UInt32 fail, UInt32 zfail, UInt32 zpass);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1d(Double s);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1f(Single s);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1i(Int32 s);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1s(Int16 s);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord1sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2d(Double s, Double t);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2f(Single s, Single t);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2i(Int32 s, Int32 t);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2s(Int16 s, Int16 t);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord2sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3d(Double s, Double t, Double r);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3f(Single s, Single t, Single r);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3i(Int32 s, Int32 t, Int32 r);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3s(Int16 s, Int16 t, Int16 r);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord3sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4d(Double s, Double t, Double r, Double q);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4f(Single s, Single t, Single r, Single q);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4i(Int32 s, Int32 t, Int32 r, Int32 q);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4s(Int16 s, Int16 t, Int16 r, Int16 q);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoord4sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoordPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexCoordPointer(Int32 size, UInt32 type, Int32 stride, Single[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexEnvf(UInt32 target, UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexEnvfv(UInt32 target, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexEnvi(UInt32 target, UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexEnviv(UInt32 target, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGend(UInt32 coord, UInt32 pname, Double param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGendv(UInt32 coord, UInt32 pname, Double[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGenf(UInt32 coord, UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGenfv(UInt32 coord, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGeni(UInt32 coord, UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexGeniv(UInt32 coord, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexImage1D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 border, UInt32 format, UInt32 type, Byte[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, Byte[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, IntPtr pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexParameterf(UInt32 target, UInt32 pname, Single param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexParameterfv(UInt32 target, UInt32 pname, Single[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexParameteri(UInt32 target, UInt32 pname, Int32 param);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexParameteriv(UInt32 target, UInt32 pname, Int32[] params_notkeyword);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 width, UInt32 format, UInt32 type, Int32[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 width, Int32 height, UInt32 format, UInt32 type, Int32[] pixels);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTranslated(Double x, Double y, Double z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glTranslatef(Single x, Single y, Single z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2d(Double x, Double y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2f(Single x, Single y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2i(Int32 x, Int32 y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2s(Int16 x, Int16 y);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex2sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3d(Double x, Double y, Double z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3f(Single x, Single y, Single z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3i(Int32 x, Int32 y, Int32 z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3s(Int16 x, Int16 y, Int16 z);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex3sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4d(Double x, Double y, Double z, Double w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4dv(Double[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4f(Single x, Single y, Single z, Single w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4fv(Single[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4i(Int32 x, Int32 y, Int32 z, Int32 w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4iv(Int32[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4s(Int16 x, Int16 y, Int16 z, Int16 w);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertex4sv(Int16[] v);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertexPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertexPointer(Int32 size, UInt32 type, Int32 stride, Int16[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertexPointer(Int32 size, UInt32 type, Int32 stride, Int32[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertexPointer(Int32 size, UInt32 type, Int32 stride, Single[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glVertexPointer(Int32 size, UInt32 type, Int32 stride, Double[] pointer);
        [DllImport(LIBRARY_OPENGL, SetLastError = true)] private static extern void glViewport(Int32 x, Int32 y, Int32 width, Int32 height);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static unsafe extern SByte* gluErrorString(UInt32 errCode);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static unsafe extern SByte* gluGetString(Int32 name);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluOrtho2D(Double left, Double right, Double bottom, Double top);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPerspective(Double fovy, Double aspect, Double zNear, Double zFar);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPickMatrix(Double x, Double y, Double width, Double height, Int32[] viewport);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluLookAt(Double eyex, Double eyey, Double eyez, Double centerx, Double centery, Double centerz, Double upx, Double upy, Double upz);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluProject(Double objx, Double objy, Double objz, Double[] modelMatrix, Double[] projMatrix, Int32[] viewport, Double[] winx, Double[] winy, Double[] winz);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluUnProject(Double winx, Double winy, Double winz, Double[] modelMatrix, Double[] projMatrix, Int32[] viewport, ref Double objx, ref Double objy, ref Double objz);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluScaleImage(Int32 format, Int32 widthin, Int32 heightin, Int32 typein, Int32[] datain, Int32 widthout, Int32 heightout, Int32 typeout, Int32[] dataout);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBuild1DMipmaps(UInt32 target, UInt32 components, Int32 width, UInt32 format, UInt32 type, IntPtr data);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBuild2DMipmaps(UInt32 target, UInt32 components, Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr data);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewQuadric();
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDeleteQuadric(IntPtr state);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricNormals(IntPtr quadObject, UInt32 normals);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricTexture(IntPtr quadObject, Int32 textureCoords);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricOrientation(IntPtr quadObject, Int32 orientation);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluQuadricDrawStyle(IntPtr quadObject, UInt32 drawStyle);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluCylinder(IntPtr qobj, Double baseRadius, Double topRadius, Double height, Int32 slices, Int32 stacks);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDisk(IntPtr qobj, Double innerRadius, Double outerRadius, Int32 slices, Int32 loops);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPartialDisk(IntPtr qobj, Double innerRadius, Double outerRadius, Int32 slices, Int32 loops, Double startAngle, Double sweepAngle);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluSphere(IntPtr qobj, Double radius, Int32 slices, Int32 stacks);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewTess();
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDeleteTess(IntPtr tess);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessBeginPolygon(IntPtr tess, IntPtr polygonData);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessBeginContour(IntPtr tess);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessVertex(IntPtr tess, Double[] coords, Double[] data);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessEndContour(IntPtr tess);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessEndPolygon(IntPtr tess);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessProperty(IntPtr tess, Int32 which, Double value);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluTessNormal(IntPtr tess, Double x, Double y, Double z);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluGetTessProperty(IntPtr tess, Int32 which, Double value);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern IntPtr gluNewNurbsRenderer();
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluDeleteNurbsRenderer(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginSurface(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginCurve(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndCurve(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndSurface(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluBeginTrim(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluEndTrim(IntPtr nobj);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluPwlCurve(IntPtr nobj, Int32 count, Single array, Int32 stride, UInt32 type);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsCurve(IntPtr nobj, Int32 nknots, Single[] knot, Int32 stride, Single[] ctlarray, Int32 order, UInt32 type);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsSurface(IntPtr nobj, Int32 sknot_count, Single[] sknot, Int32 tknot_count, Single[] tknot, Int32 s_stride, Int32 t_stride, Single[] ctlarray, Int32 sorder, Int32 torder, UInt32 type);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluLoadSamplingMatrices(IntPtr nobj, Single[] modelMatrix, Single[] projMatrix, Int32[] viewport);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluNurbsProperty(IntPtr nobj, Int32 property, Single value);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void gluGetNurbsProperty(IntPtr nobj, Int32 property, Single value);
        [DllImport(LIBRARY_GLU, SetLastError = true)] private static extern void IntPtrCallback(IntPtr nobj, Int32 which, IntPtr Callback);
        #endregion

        #region delegates
        private delegate void glBlendColor(Single red, Single green, Single blue, Single alpha);
        private delegate void glBlendEquation(UInt32 mode);
        private delegate void glDrawRangeElements(UInt32 mode, UInt32 start, UInt32 end, Int32 count, UInt32 type, IntPtr indices);
        private delegate void glTexImage3D(UInt32 target, Int32 level, Int32 internalformat, Int32 width, Int32 height, Int32 depth, Int32 border, UInt32 format, UInt32 type, IntPtr pixels);
        private delegate void glTexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 width, Int32 height, Int32 depth, UInt32 format, UInt32 type, IntPtr pixels);
        private delegate void glCopyTexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 x, Int32 y, Int32 width, Int32 height);
        private delegate void glActiveTexture(UInt32 texture);
        private delegate void glSampleCoverage(Single value, Boolean invert);
        private delegate void glCompressedTexImage3D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 depth, Int32 border, Int32 imageSize, IntPtr data);
        private delegate void glCompressedTexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, Int32 imageSize, IntPtr data);
        private delegate void glCompressedTexImage1D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 border, Int32 imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 width, Int32 height, Int32 depth, UInt32 format, Int32 imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 width, Int32 height, UInt32 format, Int32 imageSize, IntPtr data);
        private delegate void glCompressedTexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 width, UInt32 format, Int32 imageSize, IntPtr data);
        private delegate void glGetCompressedTexImage(UInt32 target, Int32 level, IntPtr img);
        private delegate void glBlendFuncSeparate(UInt32 sfactorRGB, UInt32 dfactorRGB, UInt32 sfactorAlpha, UInt32 dfactorAlpha);
        private delegate void glMultiDrawArrays(UInt32 mode, Int32[] first, Int32[] count, Int32 primcount);
        private delegate void glMultiDrawElements(UInt32 mode, Int32[] count, UInt32 type, IntPtr indices, Int32 primcount);
        private delegate void glPointParameterf(UInt32 pname, Single parameter);
        private delegate void glPointParameterfv(UInt32 pname, Single[] parameters);
        private delegate void glPointParameteri(UInt32 pname, Int32 parameter);
        private delegate void glPointParameteriv(UInt32 pname, Int32[] parameters);
        private delegate void glGenQueries(Int32 n, UInt32[] ids);
        private delegate void glDeleteQueries(Int32 n, UInt32[] ids);
        private delegate Boolean glIsQuery(UInt32 id);
        private delegate void glBeginQuery(UInt32 target, UInt32 id);
        private delegate void glEndQuery(UInt32 target);
        private delegate void glGetQueryiv(UInt32 target, UInt32 pname, Int32[] parameters);
        private delegate void glGetQueryObjectiv(UInt32 id, UInt32 pname, Int32[] parameters);
        private delegate void glGetQueryObjectuiv(UInt32 id, UInt32 pname, UInt32[] parameters);
        private delegate void glBindBuffer(UInt32 target, UInt32 buffer);
        private delegate void glDeleteBuffers(Int32 n, UInt32[] buffers);
        private delegate void glGenBuffers(Int32 n, UInt32[] buffers);
        private delegate Boolean glIsBuffer(UInt32 buffer);
        private delegate void glBufferData(UInt32 target, Int32 size, IntPtr data, UInt32 usage);
        private delegate void glBufferSubData(UInt32 target, Int32 offset, Int32 size, IntPtr data);
        private delegate void glGetBufferSubData(UInt32 target, Int32 offset, Int32 size, IntPtr data);
        private delegate IntPtr glMapBuffer(UInt32 target, UInt32 access);
        private delegate Boolean glUnmapBuffer(UInt32 target);
        private delegate void glGetBufferParameteriv(UInt32 target, UInt32 pname, Int32[] parameters);
        private delegate void glGetBufferPointerv(UInt32 target, UInt32 pname, IntPtr[] parameters);
        private delegate void glBlendEquationSeparate(UInt32 modeRGB, UInt32 modeAlpha);
        private delegate void glDrawBuffers(Int32 n, UInt32[] bufs);
        private delegate void glStencilOpSeparate(UInt32 face, UInt32 sfail, UInt32 dpfail, UInt32 dppass);
        private delegate void glStencilFuncSeparate(UInt32 face, UInt32 func, Int32 reference, UInt32 mask);
        private delegate void glStencilMaskSeparate(UInt32 face, UInt32 mask);
        private delegate void glAttachShader(UInt32 program, UInt32 shader);
        private delegate void glBindAttribLocation(UInt32 program, UInt32 index, String name);
        private delegate void glCompileShader(UInt32 shader);
        private delegate UInt32 glCreateProgram();
        private delegate UInt32 glCreateShader(UInt32 type);
        private delegate void glDeleteProgram(UInt32 program);
        private delegate void glDeleteShader(UInt32 shader);
        private delegate void glDetachShader(UInt32 program, UInt32 shader);
        private delegate void glDisableVertexAttribArray(UInt32 index);
        private delegate void glEnableVertexAttribArray(UInt32 index);
        private delegate void glGetActiveAttrib(UInt32 program, UInt32 index, Int32 bufSize, out Int32 length, out Int32 size, out UInt32 type, StringBuilder name);
        private delegate void glGetActiveUniform(UInt32 program, UInt32 index, Int32 bufSize, out Int32 length, out Int32 size, out UInt32 type, StringBuilder name);
        private delegate void glGetAttachedShaders(UInt32 program, Int32 maxCount, Int32[] count, UInt32[] obj);
        private delegate Int32 glGetAttribLocation(UInt32 program, String name);
        private delegate void glGetProgramiv(UInt32 program, UInt32 pname, Int32[] parameters);
        private delegate void glGetProgramInfoLog(UInt32 program, Int32 bufSize, IntPtr length, StringBuilder infoLog);
        private delegate void glGetShaderiv(UInt32 shader, UInt32 pname, Int32[] parameters);
        private delegate void glGetShaderInfoLog(UInt32 shader, Int32 bufSize, IntPtr length, StringBuilder infoLog);
        private delegate void glGetShaderSource(UInt32 shader, Int32 bufSize, IntPtr length, StringBuilder source);
        private delegate Int32 glGetUniformLocation(UInt32 program, String name);
        private delegate void glGetUniformfv(UInt32 program, Int32 location, Single[] parameters);
        private delegate void glGetUniformiv(UInt32 program, Int32 location, Int32[] parameters);
        private delegate void glGetVertexAttribdv(UInt32 index, UInt32 pname, Double[] parameters);
        private delegate void glGetVertexAttribfv(UInt32 index, UInt32 pname, Single[] parameters);
        private delegate void glGetVertexAttribiv(UInt32 index, UInt32 pname, Int32[] parameters);
        private delegate void glGetVertexAttribPointerv(UInt32 index, UInt32 pname, IntPtr pointer);
        private delegate Boolean glIsProgram(UInt32 program);
        private delegate Boolean glIsShader(UInt32 shader);
        private delegate void glLinkProgram(UInt32 program);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, ThrowOnUnmappableChar = true)]
        private delegate void glShaderSource(UInt32 shader, Int32 count, String[] source, Int32[] length);
        private delegate void glUseProgram(UInt32 program);
        private delegate void glUniform1f(Int32 location, Single v0);
        private delegate void glUniform2f(Int32 location, Single v0, Single v1);
        private delegate void glUniform3f(Int32 location, Single v0, Single v1, Single v2);
        private delegate void glUniform4f(Int32 location, Single v0, Single v1, Single v2, Single v3);
        private delegate void glUniform1i(Int32 location, Int32 v0);
        private delegate void glUniform2i(Int32 location, Int32 v0, Int32 v1);
        private delegate void glUniform3i(Int32 location, Int32 v0, Int32 v1, Int32 v2);
        private delegate void glUniform4i(Int32 location, Int32 v0, Int32 v1, Int32 v2, Int32 v3);
        private delegate void glUniform1fv(Int32 location, Int32 count, Single[] value);
        private delegate void glUniform2fv(Int32 location, Int32 count, Single[] value);
        private delegate void glUniform3fv(Int32 location, Int32 count, Single[] value);
        private delegate void glUniform4fv(Int32 location, Int32 count, Single[] value);
        private delegate void glUniform1iv(Int32 location, Int32 count, Int32[] value);
        private delegate void glUniform2iv(Int32 location, Int32 count, Int32[] value);
        private delegate void glUniform3iv(Int32 location, Int32 count, Int32[] value);
        private delegate void glUniform4iv(Int32 location, Int32 count, Int32[] value);
        private delegate void glUniformMatrix2fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix3fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix4fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glValidateProgram(UInt32 program);
        private delegate void glVertexAttrib1d(UInt32 index, Double x);
        private delegate void glVertexAttrib1dv(UInt32 index, Double[] v);
        private delegate void glVertexAttrib1f(UInt32 index, Single x);
        private delegate void glVertexAttrib1fv(UInt32 index, Single[] v);
        private delegate void glVertexAttrib1s(UInt32 index, Int16 x);
        private delegate void glVertexAttrib1sv(UInt32 index, Int16[] v);
        private delegate void glVertexAttrib2d(UInt32 index, Double x, Double y);
        private delegate void glVertexAttrib2dv(UInt32 index, Double[] v);
        private delegate void glVertexAttrib2f(UInt32 index, Single x, Single y);
        private delegate void glVertexAttrib2fv(UInt32 index, Single[] v);
        private delegate void glVertexAttrib2s(UInt32 index, Int16 x, Int16 y);
        private delegate void glVertexAttrib2sv(UInt32 index, Int16[] v);
        private delegate void glVertexAttrib3d(UInt32 index, Double x, Double y, Double z);
        private delegate void glVertexAttrib3dv(UInt32 index, Double[] v);
        private delegate void glVertexAttrib3f(UInt32 index, Single x, Single y, Single z);
        private delegate void glVertexAttrib3fv(UInt32 index, Single[] v);
        private delegate void glVertexAttrib3s(UInt32 index, Int16 x, Int16 y, Int16 z);
        private delegate void glVertexAttrib3sv(UInt32 index, Int16[] v);
        private delegate void glVertexAttrib4Nbv(UInt32 index, SByte[] v);
        private delegate void glVertexAttrib4Niv(UInt32 index, Int32[] v);
        private delegate void glVertexAttrib4Nsv(UInt32 index, Int16[] v);
        private delegate void glVertexAttrib4Nub(UInt32 index, Byte x, Byte y, Byte z, Byte w);
        private delegate void glVertexAttrib4Nubv(UInt32 index, Byte[] v);
        private delegate void glVertexAttrib4Nuiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttrib4Nusv(UInt32 index, UInt16[] v);
        private delegate void glVertexAttrib4bv(UInt32 index, SByte[] v);
        private delegate void glVertexAttrib4d(UInt32 index, Double x, Double y, Double z, Double w);
        private delegate void glVertexAttrib4dv(UInt32 index, Double[] v);
        private delegate void glVertexAttrib4f(UInt32 index, Single x, Single y, Single z, Single w);
        private delegate void glVertexAttrib4fv(UInt32 index, Single[] v);
        private delegate void glVertexAttrib4iv(UInt32 index, Int32[] v);
        private delegate void glVertexAttrib4s(UInt32 index, Int16 x, Int16 y, Int16 z, Int16 w);
        private delegate void glVertexAttrib4sv(UInt32 index, Int16[] v);
        private delegate void glVertexAttrib4ubv(UInt32 index, Byte[] v);
        private delegate void glVertexAttrib4uiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttrib4usv(UInt32 index, UInt16[] v);
        private delegate void glVertexAttribPointer(UInt32 index, Int32 size, UInt32 type, Boolean normalized, Int32 stride, IntPtr pointer);
        private delegate void glUniformMatrix2x3fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix3x2fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix2x4fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix4x2fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix3x4fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glUniformMatrix4x3fv(Int32 location, Int32 count, Boolean transpose, Single[] value);
        private delegate void glColorMaski(UInt32 index, Boolean r, Boolean g, Boolean b, Boolean a);
        private delegate void glGetBooleani_v(UInt32 target, UInt32 index, Boolean[] data);
        private delegate void glGetIntegeri_v(UInt32 target, UInt32 index, Int32[] data);
        private delegate void glEnablei(UInt32 target, UInt32 index);
        private delegate void glDisablei(UInt32 target, UInt32 index);
        private delegate Boolean glIsEnabledi(UInt32 target, UInt32 index);
        private delegate void glBeginTransformFeedback(UInt32 primitiveMode);
        private delegate void glEndTransformFeedback();
        private delegate void glBindBufferRange(UInt32 target, UInt32 index, UInt32 buffer, Int32 offset, Int32 size);
        private delegate void glBindBufferBase(UInt32 target, UInt32 index, UInt32 buffer);
        private delegate void glTransformFeedbackVaryings(UInt32 program, Int32 count, String[] varyings, UInt32 bufferMode);
        private delegate void glGetTransformFeedbackVarying(UInt32 program, UInt32 index, Int32 bufSize, Int32[] length, Int32[] size, UInt32[] type, String name);
        private delegate void glClampColor(UInt32 target, UInt32 clamp);
        private delegate void glBeginConditionalRender(UInt32 id, UInt32 mode);
        private delegate void glEndConditionalRender();
        private delegate void glVertexAttribIPointer(UInt32 index, Int32 size, UInt32 type, Int32 stride, IntPtr pointer);
        private delegate void glGetVertexAttribIiv(UInt32 index, UInt32 pname, Int32[] parameters);
        private delegate void glGetVertexAttribIuiv(UInt32 index, UInt32 pname, UInt32[] parameters);
        private delegate void glVertexAttribI1i(UInt32 index, Int32 x);
        private delegate void glVertexAttribI2i(UInt32 index, Int32 x, Int32 y);
        private delegate void glVertexAttribI3i(UInt32 index, Int32 x, Int32 y, Int32 z);
        private delegate void glVertexAttribI4i(UInt32 index, Int32 x, Int32 y, Int32 z, Int32 w);
        private delegate void glVertexAttribI1ui(UInt32 index, UInt32 x);
        private delegate void glVertexAttribI2ui(UInt32 index, UInt32 x, UInt32 y);
        private delegate void glVertexAttribI3ui(UInt32 index, UInt32 x, UInt32 y, UInt32 z);
        private delegate void glVertexAttribI4ui(UInt32 index, UInt32 x, UInt32 y, UInt32 z, UInt32 w);
        private delegate void glVertexAttribI1iv(UInt32 index, Int32[] v);
        private delegate void glVertexAttribI2iv(UInt32 index, Int32[] v);
        private delegate void glVertexAttribI3iv(UInt32 index, Int32[] v);
        private delegate void glVertexAttribI4iv(UInt32 index, Int32[] v);
        private delegate void glVertexAttribI1uiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttribI2uiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttribI3uiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttribI4uiv(UInt32 index, UInt32[] v);
        private delegate void glVertexAttribI4bv(UInt32 index, SByte[] v);
        private delegate void glVertexAttribI4sv(UInt32 index, Int16[] v);
        private delegate void glVertexAttribI4ubv(UInt32 index, Byte[] v);
        private delegate void glVertexAttribI4usv(UInt32 index, UInt16[] v);
        private delegate void glGetUniformuiv(UInt32 program, Int32 location, UInt32[] parameters);
        private delegate void glBindFragDataLocation(UInt32 program, UInt32 color, String name);
        private delegate Int32 glGetFragDataLocation(UInt32 program, String name);
        private delegate void glUniform1ui(Int32 location, UInt32 v0);
        private delegate void glUniform2ui(Int32 location, UInt32 v0, UInt32 v1);
        private delegate void glUniform3ui(Int32 location, UInt32 v0, UInt32 v1, UInt32 v2);
        private delegate void glUniform4ui(Int32 location, UInt32 v0, UInt32 v1, UInt32 v2, UInt32 v3);
        private delegate void glUniform1uiv(Int32 location, Int32 count, UInt32[] value);
        private delegate void glUniform2uiv(Int32 location, Int32 count, UInt32[] value);
        private delegate void glUniform3uiv(Int32 location, Int32 count, UInt32[] value);
        private delegate void glUniform4uiv(Int32 location, Int32 count, UInt32[] value);
        private delegate void glTexParameterIiv(UInt32 target, UInt32 pname, Int32[] parameters);
        private delegate void glTexParameterIuiv(UInt32 target, UInt32 pname, UInt32[] parameters);
        private delegate void glGetTexParameterIiv(UInt32 target, UInt32 pname, Int32[] parameters);
        private delegate void glGetTexParameterIuiv(UInt32 target, UInt32 pname, UInt32[] parameters);
        private delegate void glClearBufferiv(UInt32 buffer, Int32 drawbuffer, Int32[] value);
        private delegate void glClearBufferuiv(UInt32 buffer, Int32 drawbuffer, UInt32[] value);
        private delegate void glClearBufferfv(UInt32 buffer, Int32 drawbuffer, Single[] value);
        private delegate void glClearBufferfi(UInt32 buffer, Int32 drawbuffer, Single depth, Int32 stencil);
        private delegate String glGetStringi(UInt32 name, UInt32 index);
        private delegate void glDrawArraysInstanced(UInt32 mode, Int32 first, Int32 count, Int32 primcount);
        private delegate void glDrawElementsInstanced(UInt32 mode, Int32 count, UInt32 type, IntPtr indices, Int32 primcount);
        private delegate void glTexBuffer(UInt32 target, UInt32 internalformat, UInt32 buffer);
        private delegate void glPrimitiveRestartIndex(UInt32 index);
        private delegate void glGetInteger64i_v(UInt32 target, UInt32 index, Int64[] data);
        private delegate void glGetBufferParameteri64v(UInt32 target, UInt32 pname, Int64[] parameters);
        private delegate void glFramebufferTexture(UInt32 target, UInt32 attachment, UInt32 texture, Int32 level);
        private delegate void glVertexAttribDivisor(UInt32 index, UInt32 divisor);
        private delegate void glMinSampleShading(Single value);
        private delegate void glBlendEquationi(UInt32 buf, UInt32 mode);
        private delegate void glBlendEquationSeparatei(UInt32 buf, UInt32 modeRGB, UInt32 modeAlpha);
        private delegate void glBlendFunci(UInt32 buf, UInt32 src, UInt32 dst);
        private delegate void glBlendFuncSeparatei(UInt32 buf, UInt32 srcRGB, UInt32 dstRGB, UInt32 srcAlpha, UInt32 dstAlpha);
        #endregion

        #region Wrapped OpenGL Functions
        public static void Accum(UInt32 op, Single value)
        {
            glAccum(op, value);
        }
        public static void AlphaFunc(UInt32 func, Single reference)
        {
            glAlphaFunc(func, reference);
        }
        public static Byte AreTexturesResident(Int32 n, UInt32[] textures, Byte[] residences)
        {
            return glAreTexturesResident(n, textures, residences);
        }
        public static void ArrayElement(Int32 i)
        {
            glArrayElement(i);
        }
        public static void Begin(UInt32 mode)
        {
            glBegin(mode);
        }
        public static void BeginCurve(IntPtr nurbsObject)
        {
            gluBeginCurve(nurbsObject);
        }
        public static void BeginSurface(IntPtr nurbsObject)
        {
            gluBeginSurface(nurbsObject);
        }
        public static void BindTexture(UInt32 target, UInt32 texture)
        {
            glBindTexture(target, texture);
        }
        public static void Bitmap(Int32 width, Int32 height, Single xorig, Single yorig, Single xmove, Single ymove, Byte[] bitmap)
        {
            glBitmap(width, height, xorig, yorig, xmove, ymove, bitmap);
        }
        public static void BlendFunc(UInt32 sfactor, UInt32 dfactor)
        {
            glBlendFunc(sfactor, dfactor);
        }
        public static void CallList(UInt32 list)
        {
            glCallList(list);
        }
        public static void CallLists(Int32 n, UInt32 type, IntPtr lists)
        {
            glCallLists(n, type, lists);
        }
        public static void CallLists(Int32 n, Byte[] lists)
        {
            glCallLists(n, GL_UNSIGNED_BYTE, lists);
        }
        public static void CallLists(Int32 n, UInt32[] lists)
        {
            glCallLists(n, GL_UNSIGNED_INT, lists);
        }
        public static void Clear(UInt32 mask)
        {
            glClear(mask);
        }
        public static void ClearAccum(Single red, Single green, Single blue, Single alpha)
        {
            glClearAccum(red, green, blue, alpha);
        }
        public static void ClearColor(Single red, Single green, Single blue, Single alpha)
        {
            glClearColor(red, green, blue, alpha);
        }

        public static void ClearColor(Color color)
        {
            ClearColor(color.R / 256f, color.G / 256f, color.B / 256f, color.A / 256f);
        }

        public static void ClearDepth(Double depth)
        {
            glClearDepth(depth);
        }
        public static void ClearIndex(Single c)
        {
            glClearIndex(c);
        }
        public static void ClearStencil(Int32 s)
        {
            glClearStencil(s);
        }
        public static void ClipPlane(UInt32 plane, Double[] equation)
        {
            glClipPlane(plane, equation);
        }
        public static void Color(Byte red, Byte green, Byte blue)
        {
            glColor3ub(red, green, blue);
        }
        public static void Color(Byte red, Byte green, Byte blue, Byte alpha)
        {
            glColor4ub(red, green, blue, alpha);
        }
        public static void Color(Double red, Double green, Double blue)
        {
            glColor3d(red, green, blue);
        }
        public static void Color(Double red, Double green, Double blue, Double alpha)
        {
            glColor4d(red, green, blue, alpha);
        }
        public static void Color(Single red, Single green, Single blue)
        {
            glColor3f(red, green, blue);
        }
        public static void Color(Single[] v)
        {
            if (v.Length == 3)
                glColor3fv(v);
            else if (v.Length == 4)
                glColor4fv(v);
        }
        public static void Color(Int32[] v)
        {
            if (v.Length == 3)
                glColor3iv(v);
            else if (v.Length == 4)
                glColor4iv(v);
        }
        public static void Color(Int16[] v)
        {
            if (v.Length == 3)
                glColor3sv(v);
            else if (v.Length == 4)
                glColor4sv(v);
        }
        public static void Color(Double[] v)
        {
            if (v.Length == 3)
                glColor3dv(v);
            else if (v.Length == 4)
                glColor4dv(v);
        }
        public static void Color(Byte[] v)
        {
            if (v.Length == 3)
                glColor3bv(v);
            else if (v.Length == 4)
                glColor4bv(v);
        }
        public static void Color(UInt32[] v)
        {
            if (v.Length == 3)
                glColor3uiv(v);
            else if (v.Length == 4)
                glColor4uiv(v);
        }
        public static void Color(UInt16[] v)
        {
            if (v.Length == 3)
                glColor3usv(v);
            else if (v.Length == 4)
                glColor4usv(v);
        }
        public static void Color(Int32 red, Int32 green, Int32 blue)
        {
            glColor3i(red, green, blue);
        }
        public static void Color(Int32 red, Int32 green, Int32 blue, Int32 alpha)
        {
            glColor4i(red, green, blue, alpha);
        }
        public static void Color(Int16 red, Int16 green, Int16 blue)
        {
            glColor3s(red, green, blue);
        }
        public static void Color(Int16 red, Int16 green, Int16 blue, Int16 alpha)
        {
            glColor4s(red, green, blue, alpha);
        }
        public static void Color(UInt32 red, UInt32 green, UInt32 blue)
        {
            glColor3ui(red, green, blue);
        }
        public static void Color(UInt32 red, UInt32 green, UInt32 blue, UInt32 alpha)
        {
            glColor4ui(red, green, blue, alpha);
        }
        public static void Color(UInt16 red, UInt16 green, UInt16 blue)
        {
            glColor3us(red, green, blue);
        }
        public static void Color(UInt16 red, UInt16 green, UInt16 blue, UInt16 alpha)
        {
            glColor4us(red, green, blue, alpha);
        }
        public static void Color(Single red, Single green, Single blue, Single alpha)
        {
            glColor4f(red, green, blue, alpha);
        }
        public static void Color(Color color)
        {
            glColor4f(color.R / 256f, color.G / 256f, color.B / 256f, color.A / 256f);
        }
        public static void ColorMask(Byte red, Byte green, Byte blue, Byte alpha)
        {
            glColorMask(red, green, blue, alpha);
        }
        public static void ColorMaterial(UInt32 face, UInt32 mode)
        {
            glColorMaterial(face, mode);
        }
        public static void ColorPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer)
        {
            glColorPointer(size, type, stride, pointer);
        }
        public static void CopyPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 type)
        {
            glCopyPixels(x, y, width, height, type);
        }
        public static void CopyTexImage1D(UInt32 target, Int32 level, UInt32 internalFormat, Int32 x, Int32 y, Int32 width, Int32 border)
        {
            glCopyTexImage1D(target, level, internalFormat, x, y, width, border);
        }
        public static void CopyTexImage2D(UInt32 target, Int32 level, UInt32 internalFormat, Int32 x, Int32 y, Int32 width, Int32 height, Int32 border)
        {
            glCopyTexImage2D(target, level, internalFormat, x, y, width, height, border);
        }
        public static void CopyTexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 x, Int32 y, Int32 width)
        {
            glCopyTexSubImage1D(target, level, xoffset, x, y, width);
        }
        public static void CopyTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            glCopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
        }
        public static void CullFace(UInt32 mode)
        {
            glCullFace(mode);
        }
        public static void Cylinder(IntPtr qobj, Double baseRadius, Double topRadius, Double height, Int32 slices, Int32 stacks)
        {
            gluCylinder(qobj, baseRadius, topRadius, height, slices, stacks);
        }
        public static void DeleteLists(UInt32 list, Int32 range)
        {
            glDeleteLists(list, range);
        }
        public static void DeleteNurbsRenderer(IntPtr nurbsObject)
        {
            gluDeleteNurbsRenderer(nurbsObject);
        }
        public static void DeleteTextures(Int32 n, UInt32[] textures)
        {
            glDeleteTextures(n, textures);
        }
        public static void DeleteQuadric(IntPtr quadric)
        {
            gluDeleteQuadric(quadric);
        }
        public static void DepthFunc(UInt32 func)
        {
            glDepthFunc(func);
        }
        public static void DepthMask(Byte flag)
        {
            glDepthMask(flag);
        }
        public static void DepthRange(Double zNear, Double zFar)
        {
            glDepthRange(zNear, zFar);
        }
        public static void Disable(UInt32 cap)
        {
            glDisable(cap);
        }
        public static void DisableClientState(UInt32 array)
        {
            glDisableClientState(array);
        }
        public static void DrawArrays(UInt32 mode, Int32 first, Int32 count)
        {
            glDrawArrays(mode, first, count);
        }
        public static void DrawBuffer(UInt32 mode)
        {
            glDrawBuffer(mode);
        }
        public static void DrawElements(UInt32 mode, Int32 count, UInt32[] indices)
        {
            glDrawElements(mode, count, GL_UNSIGNED_INT, indices);
        }
        public static void DrawElements(UInt32 mode, Int32 count, UInt32 type, IntPtr indices)
        {
            glDrawElements(mode, count, type, indices);
        }
        public static void DrawPixels(Int32 width, Int32 height, UInt32 format, Single[] pixels)
        {
            glDrawPixels(width, height, format, GL_FLOAT, pixels);
        }
        public static void DrawPixels(Int32 width, Int32 height, UInt32 format, UInt32[] pixels)
        {
            glDrawPixels(width, height, format, GL_UNSIGNED_INT, pixels);
        }
        public static void DrawPixels(Int32 width, Int32 height, UInt32 format, UInt16[] pixels)
        {
            glDrawPixels(width, height, format, GL_UNSIGNED_SHORT, pixels);
        }
        public static void DrawPixels(Int32 width, Int32 height, UInt32 format, Byte[] pixels)
        {
            glDrawPixels(width, height, format, GL_UNSIGNED_BYTE, pixels);
        }
        public static void DrawPixels(Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr pixels)
        {
            glDrawPixels(width, height, format, type, pixels);
        }
        public static void EdgeFlag(Byte flag)
        {
            glEdgeFlag(flag);
        }
        public static void EdgeFlagPointer(Int32 stride, Int32[] pointer)
        {
            glEdgeFlagPointer(stride, pointer);
        }
        public static void EdgeFlag(Byte[] flag)
        {
            glEdgeFlagv(flag);
        }
        public static void Enable(UInt32 cap)
        {
            glEnable(cap);
        }
        public static void EnableClientState(UInt32 array)
        {
            glEnableClientState(array);
        }
        public static void EnableIf(UInt32 cap, Boolean test)
        {
            if (test) Enable(cap);
            else Disable(cap);
        }
        public static void End()
        {
            glEnd();
        }
        public static void EndCurve(IntPtr nurbsObject)
        {
            gluEndCurve(nurbsObject);
        }
        public static void EndList()
        {
            glEndList();
        }
        public static void EndSurface(IntPtr nurbsObject)
        {
            gluEndSurface(nurbsObject);
        }
        public static void EvalCoord1(Double u)
        {
            glEvalCoord1d(u);
        }
        public static void EvalCoord1(Double[] u)
        {
            glEvalCoord1dv(u);
        }
        public static void EvalCoord1(Single u)
        {
            glEvalCoord1f(u);
        }
        public static void EvalCoord1(Single[] u)
        {
            glEvalCoord1fv(u);
        }
        public static void EvalCoord2(Double u, Double v)
        {
            glEvalCoord2d(u, v);
        }
        public static void EvalCoord2(Double[] u)
        {
            glEvalCoord2dv(u);
        }
        public static void EvalCoord2(Single u, Single v)
        {
            glEvalCoord2f(u, v);
        }
        public static void EvalCoord2(Single[] u)
        {
            glEvalCoord2fv(u);
        }
        public static void EvalMesh1(UInt32 mode, Int32 i1, Int32 i2)
        {
            glEvalMesh1(mode, i1, i2);
        }
        public static void EvalMesh2(UInt32 mode, Int32 i1, Int32 i2, Int32 j1, Int32 j2)
        {
            glEvalMesh2(mode, i1, i2, j1, j2);
        }
        public static void EvalPoint1(Int32 i)
        {
            glEvalPoint1(i);
        }
        public static void EvalPoint2(Int32 i, Int32 j)
        {
            glEvalPoint2(i, j);
        }
        public static void FeedbackBuffer(Int32 size, UInt32 type, Single[] buffer)
        {
            glFeedbackBuffer(size, type, buffer);
        }
        public static void Finish()
        {
            glFinish();
        }
        public static void Flush()
        {
            glFlush();
        }
        public static void Fog(UInt32 pname, Single param)
        {
            glFogf(pname, param);
        }
        public static void Fog(UInt32 pname, Single[] parameters)
        {
            glFogfv(pname, parameters);
        }
        public static void Fog(UInt32 pname, Int32 param)
        {
            glFogi(pname, param);
        }
        public static void Fog(UInt32 pname, Int32[] parameters)
        {
            glFogiv(pname, parameters);
        }
        public static void FrontFace(UInt32 mode)
        {
            glFrontFace(mode);
        }
        public static void Frustum(Double left, Double right, Double bottom,
            Double top, Double zNear, Double zFar)
        {
            glFrustum(left, right, bottom, top, zNear, zFar);
        }
        public static UInt32 GenLists(Int32 range)
        {
            var list = glGenLists(range);
            return list;
        }
        public static void GenTextures(Int32 n, UInt32[] textures)
        {
            glGenTextures(n, textures);
        }
        public static void GetBooleanv(UInt32 pname, Byte[] parameters)
        {
            glGetBooleanv(pname, parameters);
        }
        public static void GetClipPlane(UInt32 plane, Double[] equation)
        {
            glGetClipPlane(plane, equation);
        }
        public static void GetDouble(UInt32 pname, Double[] parameters)
        {
            glGetDoublev(pname, parameters);
        }
        public static UInt32 GetError()
        {
            return glGetError();
        }
        public static void GetFloat(UInt32 pname, Single[] parameters)
        {
            glGetFloatv(pname, parameters);
        }
        public static void GetInteger(UInt32 pname, Int32[] parameters)
        {
            glGetIntegerv(pname, parameters);
        }
        public static void GetLight(UInt32 light, UInt32 pname, Single[] parameters)
        {
            glGetLightfv(light, pname, parameters);
        }
        public static void GetLight(UInt32 light, UInt32 pname, Int32[] parameters)
        {
            glGetLightiv(light, pname, parameters);
        }
        public static void GetMap(UInt32 target, UInt32 query, Double[] v)
        {
            glGetMapdv(target, query, v);
        }
        public static void GetMap(UInt32 target, UInt32 query, Single[] v)
        {
            glGetMapfv(target, query, v);
        }
        public static void GetMap(UInt32 target, UInt32 query, Int32[] v)
        {
            glGetMapiv(target, query, v);
        }
        public static void GetMaterial(UInt32 face, UInt32 pname, Single[] parameters)
        {
            glGetMaterialfv(face, pname, parameters);
        }
        public static void GetMaterial(UInt32 face, UInt32 pname, Int32[] parameters)
        {
            glGetMaterialiv(face, pname, parameters);
        }
        public static void GetPixelMap(UInt32 map, Single[] values)
        {
            glGetPixelMapfv(map, values);
        }
        public static void GetPixelMap(UInt32 map, UInt32[] values)
        {
            glGetPixelMapuiv(map, values);
        }
        public static void GetPixelMap(UInt32 map, UInt16[] values)
        {
            glGetPixelMapusv(map, values);
        }
        public static void GetPointerv(UInt32 pname, Int32[] parameters)
        {
            glGetPointerv(pname, parameters);
        }
        public static void GetPolygonStipple(Byte[] mask)
        {
            glGetPolygonStipple(mask);
        }
        public static unsafe String GetString(UInt32 name)
        {
            var pStr = glGetString(name);
            var str = new String(pStr);
            return str;
        }
        public static void GetTexEnv(UInt32 target, UInt32 pname, Single[] parameters)
        {
            glGetTexEnvfv(target, pname, parameters);
        }
        public static void GetTexEnv(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            glGetTexEnviv(target, pname, parameters);
        }
        public static void GetTexGen(UInt32 coord, UInt32 pname, Double[] parameters)
        {
            glGetTexGendv(coord, pname, parameters);
        }
        public static void GetTexGen(UInt32 coord, UInt32 pname, Single[] parameters)
        {
            glGetTexGenfv(coord, pname, parameters);
        }
        public static void GetTexGen(UInt32 coord, UInt32 pname, Int32[] parameters)
        {
            glGetTexGeniv(coord, pname, parameters);
        }
        public static void GetTexImage(UInt32 target, Int32 level, UInt32 format, UInt32 type, Int32[] pixels)
        {
            glGetTexImage(target, level, format, type, pixels);
        }
        public static void GetTexLevelParameter(UInt32 target, Int32 level, UInt32 pname, Single[] parameters)
        {
            glGetTexLevelParameterfv(target, level, pname, parameters);
        }
        public static void GetTexLevelParameter(UInt32 target, Int32 level, UInt32 pname, Int32[] parameters)
        {
            glGetTexLevelParameteriv(target, level, pname, parameters);
        }
        public static void GetTexParameter(UInt32 target, UInt32 pname, Single[] parameters)
        {
            glGetTexParameterfv(target, pname, parameters);
        }
        public static void GetTexParameter(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            glGetTexParameteriv(target, pname, parameters);
        }
        public static void Hint(UInt32 target, UInt32 mode)
        {
            glHint(target, mode);
        }
        public static void IndexMask(UInt32 mask)
        {
            glIndexMask(mask);
        }
        public static void IndexPointer(UInt32 type, Int32 stride, Int32[] pointer)
        {
            glIndexPointer(type, stride, pointer);
        }
        public static void Index(Double c)
        {
            glIndexd(c);
        }
        public static void Index(Double[] c)
        {
            glIndexdv(c);
        }
        public static void Index(Single c)
        {
            glIndexf(c);
        }
        public static void Index(Single[] c)
        {
            glIndexfv(c);
        }
        public static void Index(Int32 c)
        {
            glIndexi(c);
        }
        public static void Index(Int32[] c)
        {
            glIndexiv(c);
        }
        public static void Index(Int16 c)
        {
            glIndexs(c);
        }
        public static void Index(Int16[] c)
        {
            glIndexsv(c);
        }
        public static void Index(Byte c)
        {
            glIndexub(c);
        }
        public static void Index(Byte[] c)
        {
            glIndexubv(c);
        }
        public static void InitNames()
        {
            glInitNames();
        }
        public static void InterleavedArrays(UInt32 format, Int32 stride, Int32[] pointer)
        {
            glInterleavedArrays(format, stride, pointer);
        }
        public static Boolean IsEnabled(UInt32 cap)
        {
            var e = glIsEnabled(cap);
            return e != 0;
        }
        public static Byte IsList(UInt32 list)
        {
            var islist = glIsList(list);
            return islist;
        }
        public static Byte IsTexture(UInt32 texture)
        {
            var returnValue = glIsTexture(texture);
            return returnValue;
        }
        public static void LightModel(UInt32 pname, Single param)
        {
            glLightModelf(pname, param);
        }
        public static void LightModel(UInt32 pname, Single[] parameters)
        {
            glLightModelfv(pname, parameters);
        }
        public static void LightModel(UInt32 pname, Int32 param)
        {
            glLightModeli(pname, param);
        }
        public static void LightModel(UInt32 pname, Int32[] parameters)
        {
            glLightModeliv(pname, parameters);
        }
        public static void Light(UInt32 light, UInt32 pname, Single param)
        {
            glLightf(light, pname, param);
        }
        public static void Light(UInt32 light, UInt32 pname, Single[] parameters)
        {
            glLightfv(light, pname, parameters);
        }
        public static void Light(UInt32 light, UInt32 pname, Color color)
        {
            glLightfv(light, pname, new[] { color.R / 256f, color.G / 256f, color.B / 256f, color.A / 256f });
        }
        public static void Light(UInt32 light, UInt32 pname, Vec3 position)
        {
            glLightfv(light, pname, new[] { (Single)position.X, (Single)position.Y, (Single)position.Z, 0 });
        }
        public static void Light(UInt32 light, UInt32 pname, Int32 param)
        {
            glLighti(light, pname, param);
        }
        public static void Light(UInt32 light, UInt32 pname, Int32[] parameters)
        {
            glLightiv(light, pname, parameters);
        }
        public static void LineStipple(Int32 factor, UInt16 pattern)
        {
            glLineStipple(factor, pattern);
        }
        public static void LineWidth(Single width)
        {
            glLineWidth(width);
        }
        public static void ListBase(UInt32 listbase)
        {
            glListBase(listbase);
        }
        public static void LoadIdentity()
        {
            glLoadIdentity();
        }
        public static void LoadMatrix(Double[] m)
        {
            glLoadMatrixd(m);
        }
        public static void LoadMatrix(Single[] m)
        {
            glLoadMatrixf(m);
        }
        public static void LoadName(UInt32 name)
        {
            glLoadName(name);
        }
        public static void LogicOp(UInt32 opcode)
        {
            glLogicOp(opcode);
        }
        public static void LookAt(Vec3 eye, Vec3 center, Vec3 up)
        {
            gluLookAt(eye.X, eye.Y, eye.Z, center.X, center.Y, center.Z, up.X, up.Y, up.Z);
        }
        public static void LookAt(Double eyex, Double eyey, Double eyez,
            Double centerx, Double centery, Double centerz,
            Double upx, Double upy, Double upz)
        {
            gluLookAt(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
        }
        public static void Map1(UInt32 target, Double u1, Double u2, Int32 stride, Int32 order, Double[] points)
        {
            glMap1d(target, u1, u2, stride, order, points);
        }
        public static void Map1(UInt32 target, Single u1, Single u2, Int32 stride, Int32 order, Single[] points)
        {
            glMap1f(target, u1, u2, stride, order, points);
        }
        public static void Map2(UInt32 target, Double u1, Double u2, Int32 ustride, Int32 uorder, Double v1, Double v2, Int32 vstride, Int32 vorder, Double[] points)
        {
            glMap2d(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }
        public static void Map2(UInt32 target, Single u1, Single u2, Int32 ustride, Int32 uorder, Single v1, Single v2, Int32 vstride, Int32 vorder, Single[] points)
        {
            glMap2f(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }
        public static void MapGrid1(Int32 un, Double u1, Double u2)
        {
            glMapGrid1d(un, u1, u2);
        }
        public static void MapGrid1(Int32 un, Single u1, Single u2)
        {
            glMapGrid1d(un, u1, u2);
        }
        public static void MapGrid2(Int32 un, Double u1, Double u2, Int32 vn, Double v1, Double v2)
        {
            glMapGrid2d(un, u1, u2, vn, v1, v2);
        }
        public static void MapGrid2(Int32 un, Single u1, Single u2, Int32 vn, Single v1, Single v2)
        {
            glMapGrid2f(un, u1, u2, vn, v1, v2);
        }
        public static void Material(UInt32 face, UInt32 pname, Single param)
        {
            glMaterialf(face, pname, param);
        }
        public static void Material(UInt32 face, UInt32 pname, Color color)
        {
            glMaterialfv(face, pname, new[] { color.R / 256f, color.G / 256f, color.B / 256f, color.A / 256f });
        }
        public static void Material(UInt32 face, UInt32 pname, Single[] parameters)
        {
            glMaterialfv(face, pname, parameters);
        }
        public static void Material(UInt32 face, UInt32 pname, Int32 param)
        {
            glMateriali(face, pname, param);
        }
        public static void Material(UInt32 face, UInt32 pname, Int32[] parameters)
        {
            glMaterialiv(face, pname, parameters);
        }
        public static void MatrixMode(UInt32 mode)
        {
            glMatrixMode(mode);
        }
        public static void MultMatrix(Double[] m)
        {
            glMultMatrixd(m);
        }
        public static void MultMatrix(Single[] m)
        {
            glMultMatrixf(m);
        }
        public static void NewList(UInt32 list, UInt32 mode)
        {
            glNewList(list, mode);
        }
        public static IntPtr NewNurbsRenderer()
        {
            var nurbs = gluNewNurbsRenderer();
            return nurbs;
        }
        public static IntPtr NewQuadric()
        {
            var quad = gluNewQuadric();
            return quad;
        }
        public static void Normal(Byte nx, Byte ny, Byte nz)
        {
            glNormal3b(nx, ny, nz);
        }
        public static void Normal(Byte[] v)
        {
            glNormal3bv(v);
        }
        public static void Normal(Double nx, Double ny, Double nz)
        {
            glNormal3d(nx, ny, nz);
        }
        public static void Normal(Double[] v)
        {
            glNormal3dv(v);
        }
        public static void Normal(Single nx, Single ny, Single nz)
        {
            glNormal3f(nx, ny, nz);
        }
        public static void Normal(Single[] v)
        {
            glNormal3fv(v);
        }
        public static void Normal3i(Int32 nx, Int32 ny, Int32 nz)
        {
            glNormal3i(nx, ny, nz);
        }
        public static void Normal(Int32[] v)
        {
            glNormal3iv(v);
        }
        public static void Normal(Int16 nx, Int16 ny, Int16 nz)
        {
            glNormal3s(nx, ny, nz);
        }
        public static void Normal(Int16[] v)
        {
            glNormal3sv(v);
        }
        public static void NormalPointer(UInt32 type, Int32 stride, IntPtr pointer)
        {
            glNormalPointer(type, stride, pointer);
        }
        public static void NormalPointer(UInt32 type, Int32 stride, Single[] pointer)
        {
            glNormalPointer(type, stride, pointer);
        }
        public static void NurbsCurve(IntPtr nurbsObject, Int32 knotsCount, Single[] knots,
            Int32 stride, Single[] controlPointsArray, Int32 order, UInt32 type)
        {
            gluNurbsCurve(nurbsObject, knotsCount, knots, stride, controlPointsArray,
                order, type);
        }
        public static void NurbsProperty(IntPtr nurbsObject, Int32 property, Single value)
        {
            gluNurbsProperty(nurbsObject, property, value);
        }
        public static void NurbsSurface(IntPtr nurbsObject, Int32 sknotsCount, Single[] sknots,
            Int32 tknotsCount, Single[] tknots, Int32 sStride, Int32 tStride,
            Single[] controlPointsArray, Int32 sOrder, Int32 tOrder, UInt32 type)
        {
            gluNurbsSurface(nurbsObject, sknotsCount, sknots, tknotsCount, tknots,
                sStride, tStride, controlPointsArray, sOrder, tOrder, type);
        }
        public static void Ortho(Double left, Double right, Double bottom,
            Double top, Double zNear, Double zFar)
        {
            glOrtho(left, right, bottom, top, zNear, zFar);
        }
        public static void Ortho2D(Double left, Double right, Double bottom, Double top)
        {
            gluOrtho2D(left, right, bottom, top);
        }
        public static void PartialDisk(IntPtr qobj, Double innerRadius, Double outerRadius, Int32 slices, Int32 loops, Double startAngle, Double sweepAngle)
        {
            gluPartialDisk(qobj, innerRadius, outerRadius, slices, loops, startAngle, sweepAngle);
        }
        public static void PassThrough(Single token)
        {
            glPassThrough(token);
        }
        public static void Perspective(Double fovy, Double aspect, Double zNear, Double zFar)
        {
            gluPerspective(fovy, aspect, zNear, zFar);
        }
        public static void PickMatrix(Double x, Double y, Double width, Double height, Int32[] viewport)
        {
            gluPickMatrix(x, y, width, height, viewport);
        }
        public static void PixelMap(UInt32 map, Int32 mapsize, Single[] values)
        {
            glPixelMapfv(map, mapsize, values);
        }
        public static void PixelMap(UInt32 map, Int32 mapsize, UInt32[] values)
        {
            glPixelMapuiv(map, mapsize, values);
        }
        public static void PixelMap(UInt32 map, Int32 mapsize, UInt16[] values)
        {
            glPixelMapusv(map, mapsize, values);
        }
        public static void PixelStore(UInt32 pname, Single param)
        {
            glPixelStoref(pname, param);
        }
        public static void PixelStore(UInt32 pname, Int32 param)
        {
            glPixelStorei(pname, param);
        }
        public static void PixelTransfer(UInt32 pname, Boolean param)
        {
            var p = param ? 1 : 0;
            glPixelTransferi(pname, p);
        }
        public static void PixelTransfer(UInt32 pname, Single param)
        {
            glPixelTransferf(pname, param);
        }
        public static void PixelTransfer(UInt32 pname, Int32 param)
        {
            glPixelTransferi(pname, param);
        }
        public static void PixelZoom(Single xfactor, Single yfactor)
        {
            glPixelZoom(xfactor, yfactor);
        }
        public static void PointSize(Single size)
        {
            glPointSize(size);
        }
        public static void PolygonMode(UInt32 face, UInt32 mode)
        {
            glPolygonMode(face, mode);
        }
        public static void PolygonOffset(Single factor, Single units)
        {
            glPolygonOffset(factor, units);
        }
        public static void PolygonStipple(Byte[] mask)
        {
            glPolygonStipple(mask);
        }
        public static void PopAttrib()
        {
            glPopAttrib();
        }
        public static void PopClientAttrib()
        {
            glPopClientAttrib();
        }
        public static void PopMatrix()
        {
            glPopMatrix();
        }
        public static void PopName()
        {
            glPopName();
        }
        public static void PrioritizeTextures(Int32 n, UInt32[] textures, Single[] priorities)
        {
            glPrioritizeTextures(n, textures, priorities);
        }
        public static void Project(Double objx, Double objy, Double objz, Double[] modelMatrix, Double[] projMatrix, Int32[] viewport, Double[] winx, Double[] winy, Double[] winz)
        {
            gluProject(objx, objy, objz, modelMatrix, projMatrix, viewport, winx, winy, winz);
        }
        public static Vec2 Project(Double objx, Double objy, Double objz)
        {
            var modelview = new Double[16];
            var projection = new Double[16];
            var viewport = new Int32[4];
            GetDouble(GL_MODELVIEW_MATRIX, modelview);
            GetDouble(GL_PROJECTION_MATRIX, projection);
            GetInteger(GL_VIEWPORT, viewport);
            var result = new[] { new Double[1], new Double[1], new Double[1] };

            gluProject(objx, objy, objz, modelview, projection, viewport, result[0], result[1], result[2]);

            return new Vec2(result[0][0], result[1][0]);
        }
        public static Vec2 Project(Vec3 v)
        {
            return Project(v.X, v.Y, v.Z);
        }
        public static void PushAttrib(UInt32 mask)
        {
            glPushAttrib(mask);
        }
        public static void PushClientAttrib(UInt32 mask)
        {
            glPushClientAttrib(mask);
        }
        public static void PushMatrix()
        {
            glPushMatrix();
        }
        public static void PushName(UInt32 name)
        {
            glPushName(name);
        }
        public static void QuadricNormals(IntPtr quadricObject, UInt32 normals)
        {
            gluQuadricNormals(quadricObject, normals);
        }
        public static void QuadricTexture(IntPtr quadricObject, Int32 textureCoords)
        {
            gluQuadricTexture(quadricObject, textureCoords);
        }
        public static void QuadricOrientation(IntPtr quadricObject, Int32 orientation)
        {
            gluQuadricOrientation(quadricObject, orientation);
        }
        public static void QuadricDrawStyle(IntPtr quadObject, UInt32 drawStyle)
        {
            gluQuadricDrawStyle(quadObject, drawStyle);
        }
        public static void RasterPos(Double x, Double y)
        {
            glRasterPos2d(x, y);
        }
        public static void RasterPos(Double[] v)
        {
            if (v.Length == 2)
                glRasterPos2dv(v);
            else if (v.Length == 3)
                glRasterPos3dv(v);
            else
                glRasterPos4dv(v);
        }
        public static void RasterPos(Single x, Single y)
        {
            glRasterPos2f(x, y);
        }
        public static void RasterPos(Single[] v)
        {
            if (v.Length == 2)
                glRasterPos2fv(v);
            else if (v.Length == 3)
                glRasterPos3fv(v);
            else
                glRasterPos4fv(v);
        }
        public static void RasterPos(Int32 x, Int32 y)
        {
            glRasterPos2i(x, y);
        }
        public static void RasterPos(Int32[] v)
        {
            if (v.Length == 2)
                glRasterPos2iv(v);
            else if (v.Length == 3)
                glRasterPos3iv(v);
            else
                glRasterPos4iv(v);
        }
        public static void RasterPos(Int16 x, Int16 y)
        {
            glRasterPos2s(x, y);
        }
        public static void RasterPos(Int16[] v)
        {
            if (v.Length == 2)
                glRasterPos2sv(v);
            else if (v.Length == 3)
                glRasterPos3sv(v);
            else
                glRasterPos4sv(v);
        }
        public static void RasterPos(Double x, Double y, Double z)
        {
            glRasterPos3d(x, y, z);
        }
        public static void RasterPos(Single x, Single y, Single z)
        {
            glRasterPos3f(x, y, z);
        }
        public static void RasterPos(Int32 x, Int32 y, Int32 z)
        {
            glRasterPos3i(x, y, z);
        }
        public static void RasterPos(Int16 x, Int16 y, Int16 z)
        {
            glRasterPos3s(x, y, z);
        }
        public static void RasterPos(Double x, Double y, Double z, Double w)
        {
            glRasterPos4d(x, y, z, w);
        }
        public static void RasterPos(Single x, Single y, Single z, Single w)
        {
            glRasterPos4f(x, y, z, w);
        }
        public static void RasterPos(Int32 x, Int32 y, Int32 z, Int32 w)
        {
            glRasterPos4i(x, y, z, w);
        }
        public static void RasterPos(Int16 x, Int16 y, Int16 z, Int16 w)
        {
            glRasterPos4s(x, y, z, w);
        }
        public static void ReadBuffer(UInt32 mode)
        {
            glReadBuffer(mode);
        }
        public static void ReadPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 format,
            UInt32 type, Byte[] pixels)
        {
            glReadPixels(x, y, width, height, format, type, pixels);
        }
        public static void ReadPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 format,
            UInt32 type, IntPtr pixels)
        {
            glReadPixels(x, y, width, height, format, type, pixels);
        }
        public static void Rect(Double x1, Double y1, Double x2, Double y2)
        {
            glRectd(x1, y1, x2, y2);
        }
        public static void Rect(Double[] v1, Double[] v2)
        {
            glRectdv(v1, v2);
        }
        public static void Rect(Single x1, Single y1, Single x2, Single y2)
        {
            glRectd(x1, y1, x2, y2);
        }
        public static void Rect(Single[] v1, Single[] v2)
        {
            glRectfv(v1, v2);
        }
        public static void Rect(Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            glRecti(x1, y1, x2, y2);
        }
        public static void Rect(Int32[] v1, Int32[] v2)
        {
            glRectiv(v1, v2);
        }
        public static void Rect(Int16 x1, Int16 y1, Int16 x2, Int16 y2)
        {
            glRects(x1, y1, x2, y2);
        }
        public static void Rect(Int16[] v1, Int16[] v2)
        {
            glRectsv(v1, v2);
        }
        public static Int32 RenderMode(UInt32 mode)
        {
            var hits = glRenderMode(mode);
            return hits;
        }
        public static void Rotate(Double angle, Double x, Double y, Double z)
        {
            glRotated(angle, x, y, z);
        }
        public static void Rotate(Single angle, Single x, Single y, Single z)
        {
            glRotatef(angle, x, y, z);
        }
        public static void Rotate(Single anglex, Single angley, Single anglez)
        {
            glRotatef(anglex, 1, 0, 0);
            glRotatef(angley, 0, 1, 0);
            glRotatef(anglez, 0, 0, 1);
        }
        public static void Scale(Double x, Double y, Double z)
        {
            glScaled(x, y, z);
        }
        public static void Scale(Single x, Single y, Single z)
        {
            glScalef(x, y, z);
        }
        public static void Scissor(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            glScissor(x, y, width, height);
        }
        public static void SelectBuffer(Int32 size, UInt32[] buffer)
        {
            glSelectBuffer(size, buffer);
        }
        public static void ShadeModel(UInt32 mode)
        {
            glShadeModel(mode);
        }
        public static void Sphere(IntPtr qobj, Double radius, Int32 slices, Int32 stacks)
        {
            gluSphere(qobj, radius, slices, stacks);
        }
        public static void StencilFunc(UInt32 func, Int32 reference, UInt32 mask)
        {
            glStencilFunc(func, reference, mask);
        }
        public static void StencilMask(UInt32 mask)
        {
            glStencilMask(mask);
        }
        public static void StencilOp(UInt32 fail, UInt32 zfail, UInt32 zpass)
        {
            glStencilOp(fail, zfail, zpass);
        }
        public static void TexCoord(Double s)
        {
            glTexCoord1d(s);
        }
        public static void TexCoord(Double[] v)
        {
            if (v.Length == 1)
                glTexCoord1dv(v);
            else if (v.Length == 2)
                glTexCoord2dv(v);
            else if (v.Length == 3)
                glTexCoord3dv(v);
            else if (v.Length == 4)
                glTexCoord4dv(v);
        }
        public static void TexCoord(Single s)
        {
            glTexCoord1f(s);
        }
        public static void TexCoord(Single[] v)
        {
            if (v.Length == 1)
                glTexCoord1fv(v);
            else if (v.Length == 2)
                glTexCoord2fv(v);
            else if (v.Length == 3)
                glTexCoord3fv(v);
            else if (v.Length == 4)
                glTexCoord4fv(v);
        }
        public static void TexCoord(Int32 s)
        {
            glTexCoord1i(s);
        }
        public static void TexCoord(Int32[] v)
        {
            if (v.Length == 1)
                glTexCoord1iv(v);
            else if (v.Length == 2)
                glTexCoord2iv(v);
            else if (v.Length == 3)
                glTexCoord3iv(v);
            else if (v.Length == 4)
                glTexCoord4iv(v);
        }
        public static void TexCoord(Int16 s)
        {
            glTexCoord1s(s);
        }
        public static void TexCoord(Int16[] v)
        {
            if (v.Length == 1)
                glTexCoord1sv(v);
            else if (v.Length == 2)
                glTexCoord2sv(v);
            else if (v.Length == 3)
                glTexCoord3sv(v);
            else if (v.Length == 4)
                glTexCoord4sv(v);
        }
        public static void TexCoord(Double s, Double t)
        {
            glTexCoord2d(s, t);
        }
        public static void TexCoord(Single s, Single t)
        {
            glTexCoord2f(s, t);
        }
        public static void TexCoord(Int32 s, Int32 t)
        {
            glTexCoord2i(s, t);
        }
        public static void TexCoord(Int16 s, Int16 t)
        {
            glTexCoord2s(s, t);
        }
        public static void TexCoord(Double s, Double t, Double r)
        {
            glTexCoord3d(s, t, r);
        }
        public static void TexCoord(Single s, Single t, Single r)
        {
            glTexCoord3f(s, t, r);
        }
        public static void TexCoord(Int32 s, Int32 t, Int32 r)
        {
            glTexCoord3i(s, t, r);
        }
        public static void TexCoord(Int16 s, Int16 t, Int16 r)
        {
            glTexCoord3s(s, t, r);
        }
        public static void TexCoord(Double s, Double t, Double r, Double q)
        {
            glTexCoord4d(s, t, r, q);
        }
        public static void TexCoord(Single s, Single t, Single r, Single q)
        {
            glTexCoord4f(s, t, r, q);
        }
        public static void TexCoord(Int32 s, Int32 t, Int32 r, Int32 q)
        {
            glTexCoord4i(s, t, r, q);
        }
        public static void TexCoord(Int16 s, Int16 t, Int16 r, Int16 q)
        {
            glTexCoord4s(s, t, r, q);
        }
        public static void TexCoordPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer)
        {
            glTexCoordPointer(size, type, stride, pointer);
        }
        public static void TexCoordPointer(Int32 size, UInt32 type, Int32 stride, Single[] pointer)
        {
            glTexCoordPointer(size, type, stride, pointer);
        }
        public static void TexEnv(UInt32 target, UInt32 pname, Single param)
        {
            glTexEnvf(target, pname, param);
        }
        public static void TexEnv(UInt32 target, UInt32 pname, Single[] parameters)
        {
            glTexEnvfv(target, pname, parameters);
        }
        public static void TexEnv(UInt32 target, UInt32 pname, Int32 param)
        {
            glTexEnvi(target, pname, param);
        }
        public static void TexEnv(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            glTexGeniv(target, pname, parameters);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Double param)
        {
            glTexGend(coord, pname, param);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Double[] parameters)
        {
            glTexGendv(coord, pname, parameters);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Single param)
        {
            glTexGenf(coord, pname, param);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Single[] parameters)
        {
            glTexGenfv(coord, pname, parameters);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Int32 param)
        {
            glTexGeni(coord, pname, param);
        }
        public static void TexGen(UInt32 coord, UInt32 pname, Int32[] parameters)
        {
            glTexGeniv(coord, pname, parameters);
        }
        public static void TexImage1D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 border, UInt32 format, UInt32 type, Byte[] pixels)
        {
            glTexImage1D(target, level, internalformat, width, border, format, type, pixels);
        }
        public static void TexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, Byte[] pixels)
        {
            glTexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
        }
        public static void TexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, IntPtr pixels)
        {
            glTexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
        }
        public static void TexParameter(UInt32 target, UInt32 pname, Single param)
        {
            glTexParameterf(target, pname, param);
        }
        public static void TexParameter(UInt32 target, UInt32 pname, Single[] parameters)
        {
            glTexParameterfv(target, pname, parameters);
        }
        public static void TexParameter(UInt32 target, UInt32 pname, Int32 param)
        {
            glTexParameteri(target, pname, param);
        }
        public static void TexParameter(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            glTexParameteriv(target, pname, parameters);
        }
        public static void TexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 width, UInt32 format, UInt32 type, Int32[] pixels)
        {
            glTexSubImage1D(target, level, xoffset, width, format, type, pixels);
        }
        public static void TexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 width, Int32 height, UInt32 format, UInt32 type, Int32[] pixels)
        {
            glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
        }
        public static void Translate(Double x, Double y, Double z)
        {
            glTranslated(x, y, z);
        }
        public static void Translate(Single x, Single y, Single z)
        {
            glTranslatef(x, y, z);
        }
        public static void UnProject(Double winx, Double winy, Double winz,
            Double[] modelMatrix, Double[] projMatrix, Int32[] viewport,
            ref Double objx, ref Double objy, ref Double objz)
        {
            gluUnProject(winx, winy, winz, modelMatrix, projMatrix, viewport,
                ref objx, ref objy, ref objz);
        }
        public static Vec3 UnProject(Double winx, Double winy, Double winz)
        {
            var modelview = new Double[16];
            var projection = new Double[16];
            var viewport = new Int32[4];
            GetDouble(GL_MODELVIEW_MATRIX, modelview);
            GetDouble(GL_PROJECTION_MATRIX, projection);
            GetInteger(GL_VIEWPORT, viewport);
            var result = new Double[3];
            gluUnProject(winx, winy, winz, modelview, projection, viewport, ref result[0], ref result[1], ref result[2]);
            return new Vec3(result[0], result[1], result[2]);
        }
        public static Vec3 UnProject(Vec2 v)
        {
            return UnProject(v.X, v.Y, 0);
        }
        public static void Vertex(Double x, Double y)
        {
            glVertex2d(x, y);
        }

        public static void Vertex(Vec3 v)
        {
            glVertex3dv(v.GetComponentsDouble());
        }

        public static void Vertex(Double[] v)
        {
            if (v.Length == 2)
                glVertex2dv(v);
            else if (v.Length == 3)
                glVertex3dv(v);
            else if (v.Length == 4)
                glVertex4dv(v);
        }
        public static void Vertex(Single x, Single y)
        {
            glVertex2f(x, y);
        }
        public static void Vertex(Int32 x, Int32 y)
        {
            glVertex2i(x, y);
        }
        public static void Vertex(Int32[] v)
        {
            if (v.Length == 2)
                glVertex2iv(v);
            else if (v.Length == 3)
                glVertex3iv(v);
            else if (v.Length == 4)
                glVertex4iv(v);
        }
        public static void Vertex(Int16 x, Int16 y)
        {
            glVertex2s(x, y);
        }
        public static void Vertex2sv(Int16[] v)
        {
            if (v.Length == 2)
                glVertex2sv(v);
            else if (v.Length == 3)
                glVertex3sv(v);
            else if (v.Length == 4)
                glVertex4sv(v);
        }
        public static void Vertex(Double x, Double y, Double z)
        {
            glVertex3d(x, y, z);
        }
        public static void Vertex(Single x, Single y, Single z)
        {
            glVertex3f(x, y, z);
        }
        public static void Vertex(Single[] v)
        {
            if (v.Length == 2)
                glVertex2fv(v);
            else if (v.Length == 3)
                glVertex3fv(v);
            else if (v.Length == 4)
                glVertex4fv(v);
        }
        public static void Vertex(Int32 x, Int32 y, Int32 z)
        {
            glVertex3i(x, y, z);
        }
        public static void Vertex(Int16 x, Int16 y, Int16 z)
        {
            glVertex3s(x, y, z);
        }
        public static void Vertex4d(Double x, Double y, Double z, Double w)
        {
            glVertex4d(x, y, z, w);
        }
        public static void Vertex4f(Single x, Single y, Single z, Single w)
        {
            glVertex4f(x, y, z, w);
        }
        public static void Vertex4i(Int32 x, Int32 y, Int32 z, Int32 w)
        {
            glVertex4i(x, y, z, w);
        }
        public static void Vertex4s(Int16 x, Int16 y, Int16 z, Int16 w)
        {
            glVertex4s(x, y, z, w);
        }
        public static void VertexPointer(Int32 size, UInt32 type, Int32 stride, IntPtr pointer)
        {
            glVertexPointer(size, type, stride, pointer);
        }
        public static void VertexPointer(Int32 size, Int32 stride, Int16[] pointer)
        {
            glVertexPointer(size, GL_SHORT, stride, pointer);
        }
        public static void VertexPointer(Int32 size, Int32 stride, Int32[] pointer)
        {
            glVertexPointer(size, GL_INT, stride, pointer);
        }
        public static void VertexPointer(Int32 size, Int32 stride, Single[] pointer)
        {
            glVertexPointer(size, GL_FLOAT, stride, pointer);
        }
        public static void VertexPointer(Int32 size, Int32 stride, Double[] pointer)
        {
            glVertexPointer(size, GL_DOUBLE, stride, pointer);
        }
        public static void Viewport(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            glViewport(x, y, width, height);
        }
        public static unsafe String ErrorString(UInt32 errCode)
        {
            var pStr = gluErrorString(errCode);
            var str = new String(pStr);
            return str;
        }
        public static unsafe String GetString(Int32 name)
        {
            var pStr = gluGetString(name);
            var str = new String(pStr);
            return str;
        }
        public static void ScaleImage(Int32 format, Int32 widthin, Int32 heightin, Int32 typein, Int32[] datain, Int32 widthout, Int32 heightout, Int32 typeout, Int32[] dataout)
        {
            gluScaleImage(format, widthin, heightin, typein, datain, widthout, heightout, typeout, dataout);
        }
        public static void Build1DMipmaps(UInt32 target, UInt32 components, Int32 width, UInt32 format, UInt32 type, IntPtr data)
        {
            gluBuild1DMipmaps(target, components, width, format, type, data);
        }
        public static void Build2DMipmaps(UInt32 target, UInt32 components, Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr data)
        {
            gluBuild2DMipmaps(target, components, width, height, format, type, data);
        }
        public static void Disk(IntPtr qobj, Double innerRadius, Double outerRadius, Int32 slices, Int32 loops)
        {
            gluDisk(qobj, innerRadius, outerRadius, slices, loops);
        }
        public static IntPtr NewTess()
        {
            var returnValue = gluNewTess();
            return returnValue;
        }
        public static void DeleteTess(IntPtr tess)
        {
            gluDeleteTess(tess);
        }
        public static void TessBeginPolygon(IntPtr tess, IntPtr polygonData)
        {
            gluTessBeginPolygon(tess, polygonData);
        }
        public static void TessBeginContour(IntPtr tess)
        {
            gluTessBeginContour(tess);
        }
        public static void TessVertex(IntPtr tess, Double[] coords, Double[] data)
        {
            gluTessVertex(tess, coords, data);
        }
        public static void TessEndContour(IntPtr tess)
        {
            gluTessEndContour(tess);
        }
        public static void TessEndPolygon(IntPtr tess)
        {
            gluTessEndPolygon(tess);
        }
        public static void TessProperty(IntPtr tess, Int32 which, Double value)
        {
            gluTessProperty(tess, which, value);
        }
        public static void TessNormal(IntPtr tess, Double x, Double y, Double z)
        {
            gluTessNormal(tess, x, y, z);
        }
        public static void GetTessProperty(IntPtr tess, Int32 which, Double value)
        {
            gluGetTessProperty(tess, which, value);
        }
        public static void BeginTrim(IntPtr nobj)
        {
            gluBeginTrim(nobj);
        }
        public static void EndTrim(IntPtr nobj)
        {
            gluEndTrim(nobj);
        }
        public static void PwlCurve(IntPtr nobj, Int32 count, Single array, Int32 stride, UInt32 type)
        {
            gluPwlCurve(nobj, count, array, stride, type);
        }
        public static void LoadSamplingMatrices(IntPtr nobj, Single[] modelMatrix, Single[] projMatrix, Int32[] viewport)
        {
            gluLoadSamplingMatrices(nobj, modelMatrix, projMatrix, viewport);
        }
        public static void GetNurbsProperty(IntPtr nobj, Int32 property, Single value)
        {
            gluGetNurbsProperty(nobj, property, value);
        }
        public static void BlendColor(Single red, Single green, Single blue, Single alpha)
        {
            GetDelegateFor<glBlendColor>()(red, green, blue, alpha);
        }
        public static void BlendEquation(UInt32 mode)
        {
            GetDelegateFor<glBlendEquation>()(mode);
        }
        public static void DrawRangeElements(UInt32 mode, UInt32 start, UInt32 end, Int32 count, UInt32 type, IntPtr indices)
        {
            GetDelegateFor<glDrawRangeElements>()(mode, start, end, count, type, indices);
        }
        public static void TexImage3D(UInt32 target, Int32 level, Int32 internalformat, Int32 width, Int32 height, Int32 depth, Int32 border, UInt32 format, UInt32 type, IntPtr pixels)
        {
            GetDelegateFor<glTexImage3D>()(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }
        public static void TexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 width, Int32 height, Int32 depth, UInt32 format, UInt32 type, IntPtr pixels)
        {
            GetDelegateFor<glTexSubImage3D>()(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }
        public static void CopyTexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            GetDelegateFor<glCopyTexSubImage3D>()(target, level, xoffset, yoffset, zoffset, x, y, width, height);
        }
        public static void ActiveTexture(UInt32 texture)
        {
            GetDelegateFor<glActiveTexture>()(texture);
        }
        public static void SampleCoverage(Single value, Boolean invert)
        {
            GetDelegateFor<glSampleCoverage>()(value, invert);
        }
        public static void CompressedTexImage3D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 depth, Int32 border, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexImage3D>()(target, level, internalformat, width, height, depth, border, imageSize, data);
        }
        public static void CompressedTexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexImage2D>()(target, level, internalformat, width, height, border, imageSize, data);
        }
        public static void CompressedTexImage1D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 border, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexImage1D>()(target, level, internalformat, width, border, imageSize, data);
        }
        public static void CompressedTexSubImage3D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 zoffset, Int32 width, Int32 height, Int32 depth, UInt32 format, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexSubImage3D>()(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
        }
        public static void CompressedTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 width, Int32 height, UInt32 format, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexSubImage2D>()(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }
        public static void CompressedTexSubImage1D(UInt32 target, Int32 level, Int32 xoffset, Int32 width, UInt32 format, Int32 imageSize, IntPtr data)
        {
            GetDelegateFor<glCompressedTexSubImage1D>()(target, level, xoffset, width, format, imageSize, data);
        }
        public static void GetCompressedTexImage(UInt32 target, Int32 level, IntPtr img)
        {
            GetDelegateFor<glGetCompressedTexImage>()(target, level, img);
        }
        public static void BlendFuncSeparate(UInt32 sfactorRGB, UInt32 dfactorRGB, UInt32 sfactorAlpha, UInt32 dfactorAlpha)
        {
            GetDelegateFor<glBlendFuncSeparate>()(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
        }
        public static void MultiDrawArrays(UInt32 mode, Int32[] first, Int32[] count, Int32 primcount)
        {
            GetDelegateFor<glMultiDrawArrays>()(mode, first, count, primcount);
        }
        public static void MultiDrawElements(UInt32 mode, Int32[] count, UInt32 type, IntPtr indices, Int32 primcount)
        {
            GetDelegateFor<glMultiDrawElements>()(mode, count, type, indices, primcount);
        }
        public static void PointParameter(UInt32 pname, Single parameter)
        {
            GetDelegateFor<glPointParameterf>()(pname, parameter);
        }
        public static void PointParameter(UInt32 pname, Single[] parameters)
        {
            GetDelegateFor<glPointParameterfv>()(pname, parameters);
        }
        public static void PointParameter(UInt32 pname, Int32 parameter)
        {
            GetDelegateFor<glPointParameteri>()(pname, parameter);
        }
        public static void PointParameter(UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glPointParameteriv>()(pname, parameters);
        }
        public static void GenQueries(Int32 n, UInt32[] ids)
        {
            GetDelegateFor<glGenQueries>()(n, ids);
        }
        public static void DeleteQueries(Int32 n, UInt32[] ids)
        {
            GetDelegateFor<glDeleteQueries>()(n, ids);
        }
        public static Boolean IsQuery(UInt32 id)
        {
            return GetDelegateFor<glIsQuery>()(id);
        }
        public static void BeginQuery(UInt32 target, UInt32 id)
        {
            GetDelegateFor<glBeginQuery>()(target, id);
        }
        public static void EndQuery(UInt32 target)
        {
            GetDelegateFor<glEndQuery>()(target);
        }
        public static void GetQuery(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetQueryiv>()(target, pname, parameters);
        }
        public static void GetQueryObject(UInt32 id, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetQueryObjectiv>()(id, pname, parameters);
        }
        public static void GetQueryObject(UInt32 id, UInt32 pname, UInt32[] parameters)
        {
            GetDelegateFor<glGetQueryObjectuiv>()(id, pname, parameters);
        }
        public static void BindBuffer(UInt32 target, UInt32 buffer)
        {
            GetDelegateFor<glBindBuffer>()(target, buffer);
        }
        public static void DeleteBuffers(Int32 n, UInt32[] buffers)
        {
            GetDelegateFor<glDeleteBuffers>()(n, buffers);
        }
        public static void GenBuffers(Int32 n, UInt32[] buffers)
        {
            GetDelegateFor<glGenBuffers>()(n, buffers);
        }
        public static Boolean IsBuffer(UInt32 buffer)
        {
            return GetDelegateFor<glIsBuffer>()(buffer);
        }
        public static void BufferData(UInt32 target, Int32 size, IntPtr data, UInt32 usage)
        {
            GetDelegateFor<glBufferData>()(target, size, data, usage);
        }
        public static void BufferData(UInt32 target, Single[] data, UInt32 usage)
        {
            var p = Marshal.AllocHGlobal(data.Length * sizeof(Single));
            Marshal.Copy(data, 0, p, data.Length);
            GetDelegateFor<glBufferData>()(target, data.Length * sizeof(Single), p, usage);
            Marshal.FreeHGlobal(p);
        }
        public static unsafe void BufferData(UInt32 target, Vec3[] data, UInt32 usage)
        {
            var p = Marshal.AllocHGlobal(data.Length * sizeof(Single) * 3);
            var pointer = (Single*)p.ToPointer();

            for (var i = 0; i < data.Length; i++)
            {
                *pointer++ = (Single)data[i].X;
                *pointer++ = (Single)data[i].Y;
                *pointer++ = (Single)data[i].Z;
            }

            GetDelegateFor<glBufferData>()(target, data.Length * sizeof(Single) * 3, p, usage);
            Marshal.FreeHGlobal(p);
        }

        public static unsafe void BufferData(UInt32 target, Vec2[] data, UInt32 usage)
        {
            var p = Marshal.AllocHGlobal(data.Length * sizeof(Single) * 2);
            var pointer = (Single*)p.ToPointer();

            for (var i = 0; i < data.Length; i++)
            {
                *pointer++ = (Single)data[i].X;
                *pointer++ = (Single)data[i].Y;
            }

            GetDelegateFor<glBufferData>()(target, data.Length * sizeof(Single) * 2, p, usage);
            Marshal.FreeHGlobal(p);
        }
        public static void BufferData(UInt32 target, Int32[] data, UInt32 usage)
        {
            var p = Marshal.AllocHGlobal(data.Length * sizeof(Int32));
            Marshal.Copy(data, 0, p, data.Length);
            GetDelegateFor<glBufferData>()(target, data.Length * sizeof(Int32), p, usage);
            Marshal.FreeHGlobal(p);
        }

        public static void BufferData(UInt32 target, UInt16[] data, UInt32 usage)
        {
            var dataSize = data.Length * sizeof(UInt16);
            var p = Marshal.AllocHGlobal(dataSize);
            var shortData = new Int16[data.Length];
            Buffer.BlockCopy(data, 0, shortData, 0, dataSize);
            Marshal.Copy(shortData, 0, p, data.Length);
            GetDelegateFor<glBufferData>()(target, dataSize, p, usage);
            Marshal.FreeHGlobal(p);
        }
        public static void BufferSubData(UInt32 target, Int32 offset, Int32 size, IntPtr data)
        {
            GetDelegateFor<glBufferSubData>()(target, offset, size, data);
        }
        public static void GetBufferSubData(UInt32 target, Int32 offset, Int32 size, IntPtr data)
        {
            GetDelegateFor<glGetBufferSubData>()(target, offset, size, data);
        }
        public static IntPtr MapBuffer(UInt32 target, UInt32 access)
        {
            return GetDelegateFor<glMapBuffer>()(target, access);
        }
        public static Boolean UnmapBuffer(UInt32 target)
        {
            return GetDelegateFor<glUnmapBuffer>()(target);
        }
        public static void GetBufferParameter(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetBufferParameteriv>()(target, pname, parameters);
        }
        public static void GetBufferPointer(UInt32 target, UInt32 pname, IntPtr[] parameters)
        {
            GetDelegateFor<glGetBufferPointerv>()(target, pname, parameters);
        }

        public static void BlendEquationSeparate(UInt32 modeRGB, UInt32 modeAlpha)
        {
            GetDelegateFor<glBlendEquationSeparate>()(modeRGB, modeAlpha);
        }
        public static void DrawBuffers(Int32 n, UInt32[] bufs)
        {
            GetDelegateFor<glDrawBuffers>()(n, bufs);
        }
        public static void StencilOpSeparate(UInt32 face, UInt32 sfail, UInt32 dpfail, UInt32 dppass)
        {
            GetDelegateFor<glStencilOpSeparate>()(face, sfail, dpfail, dppass);
        }
        public static void StencilFuncSeparate(UInt32 face, UInt32 func, Int32 reference, UInt32 mask)
        {
            GetDelegateFor<glStencilFuncSeparate>()(face, func, reference, mask);
        }
        public static void StencilMaskSeparate(UInt32 face, UInt32 mask)
        {
            GetDelegateFor<glStencilMaskSeparate>()(face, mask);
        }
        public static void AttachShader(UInt32 program, UInt32 shader)
        {
            GetDelegateFor<glAttachShader>()(program, shader);
        }
        public static void BindAttribLocation(UInt32 program, UInt32 index, String name)
        {
            GetDelegateFor<glBindAttribLocation>()(program, index, name);
        }
        public static void CompileShader(UInt32 shader)
        {
            GetDelegateFor<glCompileShader>()(shader);
        }
        public static UInt32 CreateProgram()
        {
            return GetDelegateFor<glCreateProgram>()();
        }
        public static UInt32 CreateShader(UInt32 type)
        {
            return GetDelegateFor<glCreateShader>()(type);
        }
        public static void DeleteProgram(UInt32 program)
        {
            GetDelegateFor<glDeleteProgram>()(program);
        }
        public static void DeleteShader(UInt32 shader)
        {
            GetDelegateFor<glDeleteShader>()(shader);
        }
        public static void DetachShader(UInt32 program, UInt32 shader)
        {
            GetDelegateFor<glDetachShader>()(program, shader);
        }
        public static void DisableVertexAttribArray(UInt32 index)
        {
            GetDelegateFor<glDisableVertexAttribArray>()(index);
        }
        public static void EnableVertexAttribArray(UInt32 index)
        {
            GetDelegateFor<glEnableVertexAttribArray>()(index);
        }
        public static void GetActiveAttrib(UInt32 program, UInt32 index, Int32 bufSize, out Int32 length, out Int32 size, out UInt32 type, out String name)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetActiveAttrib>()(program, index, bufSize, out length, out size, out type, builder);
            name = builder.ToString();
        }
        public static void GetActiveUniform(UInt32 program, UInt32 index, Int32 bufSize, out Int32 length, out Int32 size, out UInt32 type, out String name)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetActiveUniform>()(program, index, bufSize, out length, out size, out type, builder);
            name = builder.ToString();
        }
        public static void GetAttachedShaders(UInt32 program, Int32 maxCount, Int32[] count, UInt32[] obj)
        {
            GetDelegateFor<glGetAttachedShaders>()(program, maxCount, count, obj);
        }
        public static Int32 GetAttribLocation(UInt32 program, String name)
        {
            return GetDelegateFor<glGetAttribLocation>()(program, name);
        }
        public static void GetProgram(UInt32 program, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetProgramiv>()(program, pname, parameters);
        }
        public static void GetProgramInfoLog(UInt32 program, Int32 bufSize, IntPtr length, StringBuilder infoLog)
        {
            GetDelegateFor<glGetProgramInfoLog>()(program, bufSize, length, infoLog);
        }
        public static void GetShader(UInt32 shader, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetShaderiv>()(shader, pname, parameters);
        }
        public static void GetShaderInfoLog(UInt32 shader, Int32 bufSize, IntPtr length, StringBuilder infoLog)
        {
            GetDelegateFor<glGetShaderInfoLog>()(shader, bufSize, length, infoLog);
        }
        public static void GetShaderSource(UInt32 shader, Int32 bufSize, IntPtr length, StringBuilder source)
        {
            GetDelegateFor<glGetShaderSource>()(shader, bufSize, length, source);
        }
        public static Int32 GetUniformLocation(UInt32 program, String name)
        {
            return GetDelegateFor<glGetUniformLocation>()(program, name);
        }
        public static void GetUniform(UInt32 program, Int32 location, Single[] parameters)
        {
            GetDelegateFor<glGetUniformfv>()(program, location, parameters);
        }
        public static void GetUniform(UInt32 program, Int32 location, Int32[] parameters)
        {
            GetDelegateFor<glGetUniformiv>()(program, location, parameters);
        }
        public static void GetVertexAttrib(UInt32 index, UInt32 pname, Double[] parameters)
        {
            GetDelegateFor<glGetVertexAttribdv>()(index, pname, parameters);
        }
        public static void GetVertexAttrib(UInt32 index, UInt32 pname, Single[] parameters)
        {
            GetDelegateFor<glGetVertexAttribfv>()(index, pname, parameters);
        }
        public static void GetVertexAttrib(UInt32 index, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetVertexAttribiv>()(index, pname, parameters);
        }
        public static void GetVertexAttribPointer(UInt32 index, UInt32 pname, IntPtr pointer)
        {
            GetDelegateFor<glGetVertexAttribPointerv>()(index, pname, pointer);
        }
        public static Boolean IsProgram(UInt32 program)
        {
            return GetDelegateFor<glIsProgram>()(program);
        }
        public static Boolean IsShader(UInt32 shader)
        {
            return GetDelegateFor<glIsShader>()(shader);
        }
        public static void LinkProgram(UInt32 program)
        {
            GetDelegateFor<glLinkProgram>()(program);
        }
        public static void ShaderSource(UInt32 shader, String source)
        {
            GetDelegateFor<glShaderSource>()(shader, 1, new[] { source }, new[] { source.Length });
        }
        public static IntPtr StringToPtrAnsi(String str)
        {
            if (String.IsNullOrEmpty(str))
                return IntPtr.Zero;
            var bytes = Encoding.ASCII.GetBytes(str + '\0');
            var strPtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, strPtr, bytes.Length);
            return strPtr;
        }
        public static void UseProgram(UInt32 program)
        {
            GetDelegateFor<glUseProgram>()(program);
        }
        public static void Uniform1(Int32 location, Single v0)
        {
            GetDelegateFor<glUniform1f>()(location, v0);
        }
        public static void Uniform2(Int32 location, Single v0, Single v1)
        {
            GetDelegateFor<glUniform2f>()(location, v0, v1);
        }
        public static void Uniform3(Int32 location, Single v0, Single v1, Single v2)
        {
            GetDelegateFor<glUniform3f>()(location, v0, v1, v2);
        }
        public static void Uniform4(Int32 location, Single v0, Single v1, Single v2, Single v3)
        {
            GetDelegateFor<glUniform4f>()(location, v0, v1, v2, v3);
        }
        public static void Uniform1(Int32 location, Int32 v0)
        {
            GetDelegateFor<glUniform1i>()(location, v0);
        }
        public static void Uniform2(Int32 location, Int32 v0, Int32 v1)
        {
            GetDelegateFor<glUniform2i>()(location, v0, v1);
        }
        public static void Uniform3(Int32 location, Int32 v0, Int32 v1, Int32 v2)
        {
            GetDelegateFor<glUniform3i>()(location, v0, v1, v2);
        }
        public static void Uniform(Int32 location, Int32 v0, Int32 v1, Int32 v2, Int32 v3)
        {
            GetDelegateFor<glUniform4i>()(location, v0, v1, v2, v3);
        }
        public static void Uniform1(Int32 location, Int32 count, Single[] value)
        {
            GetDelegateFor<glUniform1fv>()(location, count, value);
        }
        public static void Uniform2(Int32 location, Int32 count, Single[] value)
        {
            GetDelegateFor<glUniform2fv>()(location, count, value);
        }
        public static void Uniform3(Int32 location, Int32 count, Single[] value)
        {
            GetDelegateFor<glUniform3fv>()(location, count, value);
        }
        public static void Uniform4(Int32 location, Int32 count, Single[] value)
        {
            GetDelegateFor<glUniform4fv>()(location, count, value);
        }
        public static void Uniform1(Int32 location, Int32 count, Int32[] value)
        {
            GetDelegateFor<glUniform1iv>()(location, count, value);
        }
        public static void Uniform2(Int32 location, Int32 count, Int32[] value)
        {
            GetDelegateFor<glUniform2iv>()(location, count, value);
        }
        public static void Uniform3(Int32 location, Int32 count, Int32[] value)
        {
            GetDelegateFor<glUniform3iv>()(location, count, value);
        }
        public static void Uniform4(Int32 location, Int32 count, Int32[] value)
        {
            GetDelegateFor<glUniform4iv>()(location, count, value);
        }
        public static void UniformMatrix2(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix2fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix3(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix3fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix4(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix4fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix4(Int32 location, Int32 count, Boolean transpose, Double[] value)
        {
            var data = new Single[value.Length];
            for (var i = 0; i < value.Length; i++)
                data[i] = (Single)value[i];
            GetDelegateFor<glUniformMatrix4fv>()(location, count, transpose, data);
        }
        public static void ValidateProgram(UInt32 program)
        {
            GetDelegateFor<glValidateProgram>()(program);
        }
        public static void VertexAttrib1(UInt32 index, Double x)
        {
            GetDelegateFor<glVertexAttrib1d>()(index, x);
        }
        public static void VertexAttrib1(UInt32 index, Double[] v)
        {
            GetDelegateFor<glVertexAttrib1dv>()(index, v);
        }
        public static void VertexAttrib(UInt32 index, Single x)
        {
            GetDelegateFor<glVertexAttrib1f>()(index, x);
        }
        public static void VertexAttrib1(UInt32 index, Single[] v)
        {
            GetDelegateFor<glVertexAttrib1fv>()(index, v);
        }
        public static void VertexAttrib(UInt32 index, Int16 x)
        {
            GetDelegateFor<glVertexAttrib1s>()(index, x);
        }
        public static void VertexAttrib1(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttrib1sv>()(index, v);
        }
        public static void VertexAttrib2(UInt32 index, Double x, Double y)
        {
            GetDelegateFor<glVertexAttrib2d>()(index, x, y);
        }
        public static void VertexAttrib2(UInt32 index, Double[] v)
        {
            GetDelegateFor<glVertexAttrib2dv>()(index, v);
        }
        public static void VertexAttrib2(UInt32 index, Single x, Single y)
        {
            GetDelegateFor<glVertexAttrib2f>()(index, x, y);
        }
        public static void VertexAttrib2(UInt32 index, Single[] v)
        {
            GetDelegateFor<glVertexAttrib2fv>()(index, v);
        }
        public static void VertexAttrib2(UInt32 index, Int16 x, Int16 y)
        {
            GetDelegateFor<glVertexAttrib2s>()(index, x, y);
        }
        public static void VertexAttrib2(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttrib2sv>()(index, v);
        }
        public static void VertexAttrib3(UInt32 index, Double x, Double y, Double z)
        {
            GetDelegateFor<glVertexAttrib3d>()(index, x, y, z);
        }
        public static void VertexAttrib3(UInt32 index, Double[] v)
        {
            GetDelegateFor<glVertexAttrib3dv>()(index, v);
        }
        public static void VertexAttrib3(UInt32 index, Single x, Single y, Single z)
        {
            GetDelegateFor<glVertexAttrib3f>()(index, x, y, z);
        }
        public static void VertexAttrib3(UInt32 index, Single[] v)
        {
            GetDelegateFor<glVertexAttrib3fv>()(index, v);
        }
        public static void VertexAttrib3(UInt32 index, Int16 x, Int16 y, Int16 z)
        {
            GetDelegateFor<glVertexAttrib3s>()(index, x, y, z);
        }
        public static void VertexAttrib3(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttrib3sv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, SByte[] v)
        {
            GetDelegateFor<glVertexAttrib4Nbv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttrib4Niv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttrib4Nsv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, Byte x, Byte y, Byte z, Byte w)
        {
            GetDelegateFor<glVertexAttrib4Nub>()(index, x, y, z, w);
        }
        public static void VertexAttrib4N(UInt32 index, Byte[] v)
        {
            GetDelegateFor<glVertexAttrib4Nubv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttrib4Nuiv>()(index, v);
        }
        public static void VertexAttrib4N(UInt32 index, UInt16[] v)
        {
            GetDelegateFor<glVertexAttrib4Nusv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, SByte[] v)
        {
            GetDelegateFor<glVertexAttrib4bv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, Double x, Double y, Double z, Double w)
        {
            GetDelegateFor<glVertexAttrib4d>()(index, x, y, z, w);
        }
        public static void VertexAttrib4(UInt32 index, Double[] v)
        {
            GetDelegateFor<glVertexAttrib4dv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, Single x, Single y, Single z, Single w)
        {
            GetDelegateFor<glVertexAttrib4f>()(index, x, y, z, w);
        }
        public static void VertexAttrib4(UInt32 index, Single[] v)
        {
            GetDelegateFor<glVertexAttrib4fv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttrib4iv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, Int16 x, Int16 y, Int16 z, Int16 w)
        {
            GetDelegateFor<glVertexAttrib4s>()(index, x, y, z, w);
        }
        public static void VertexAttrib4(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttrib4sv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, Byte[] v)
        {
            GetDelegateFor<glVertexAttrib4ubv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttrib4uiv>()(index, v);
        }
        public static void VertexAttrib4(UInt32 index, UInt16[] v)
        {
            GetDelegateFor<glVertexAttrib4usv>()(index, v);
        }
        public static void VertexAttribPointer(UInt32 index, Int32 size, UInt32 type, Boolean normalized, Int32 stride, IntPtr pointer)
        {
            GetDelegateFor<glVertexAttribPointer>()(index, size, type, normalized, stride, pointer);
        }

        public static void UniformMatrix2x3(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix2x3fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix3x2(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix3x2fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix2x4(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix2x4fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix4x2(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix4x2fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix3x4(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix3x4fv>()(location, count, transpose, value);
        }
        public static void UniformMatrix4x3(Int32 location, Int32 count, Boolean transpose, Single[] value)
        {
            GetDelegateFor<glUniformMatrix4x3fv>()(location, count, transpose, value);
        }
        public static void ColorMask(UInt32 index, Boolean r, Boolean g, Boolean b, Boolean a)
        {
            GetDelegateFor<glColorMaski>()(index, r, g, b, a);
        }
        public static void GetBoolean(UInt32 target, UInt32 index, Boolean[] data)
        {
            GetDelegateFor<glGetBooleani_v>()(target, index, data);
        }
        public static void GetInteger(UInt32 target, UInt32 index, Int32[] data)
        {
            GetDelegateFor<glGetIntegeri_v>()(target, index, data);
        }
        public static void Enable(UInt32 target, UInt32 index)
        {
            GetDelegateFor<glEnablei>()(target, index);
        }
        public static void Disable(UInt32 target, UInt32 index)
        {
            GetDelegateFor<glDisablei>()(target, index);
        }
        public static Boolean IsEnabled(UInt32 target, UInt32 index)
        {
            return GetDelegateFor<glIsEnabledi>()(target, index);
        }
        public static void BeginTransformFeedback(UInt32 primitiveMode)
        {
            GetDelegateFor<glBeginTransformFeedback>()(primitiveMode);
        }
        public static void EndTransformFeedback()
        {
            GetDelegateFor<glEndTransformFeedback>()();
        }
        public static void BindBufferRange(UInt32 target, UInt32 index, UInt32 buffer, Int32 offset, Int32 size)
        {
            GetDelegateFor<glBindBufferRange>()(target, index, buffer, offset, size);
        }
        public static void BindBufferBase(UInt32 target, UInt32 index, UInt32 buffer)
        {
            GetDelegateFor<glBindBufferBase>()(target, index, buffer);
        }
        public static void TransformFeedbackVaryings(UInt32 program, Int32 count, String[] varyings, UInt32 bufferMode)
        {
            GetDelegateFor<glTransformFeedbackVaryings>()(program, count, varyings, bufferMode);
        }
        public static void GetTransformFeedbackVarying(UInt32 program, UInt32 index, Int32 bufSize, Int32[] length, Int32[] size, UInt32[] type, String name)
        {
            GetDelegateFor<glGetTransformFeedbackVarying>()(program, index, bufSize, length, size, type, name);
        }
        public static void ClampColor(UInt32 target, UInt32 clamp)
        {
            GetDelegateFor<glClampColor>()(target, clamp);
        }
        public static void BeginConditionalRender(UInt32 id, UInt32 mode)
        {
            GetDelegateFor<glBeginConditionalRender>()(id, mode);
        }
        public static void EndConditionalRender()
        {
            GetDelegateFor<glEndConditionalRender>()();
        }
        public static void VertexAttribIPointer(UInt32 index, Int32 size, UInt32 type, Int32 stride, IntPtr pointer)
        {
            GetDelegateFor<glVertexAttribIPointer>()(index, size, type, stride, pointer);
        }
        public static void GetVertexAttribI(UInt32 index, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetVertexAttribIiv>()(index, pname, parameters);
        }
        public static void GetVertexAttribI(UInt32 index, UInt32 pname, UInt32[] parameters)
        {
            GetDelegateFor<glGetVertexAttribIuiv>()(index, pname, parameters);
        }
        public static void VertexAttribI1(UInt32 index, Int32 x)
        {
            GetDelegateFor<glVertexAttribI1i>()(index, x);
        }
        public static void VertexAttribI2(UInt32 index, Int32 x, Int32 y)
        {
            GetDelegateFor<glVertexAttribI2i>()(index, x, y);
        }
        public static void VertexAttribI3(UInt32 index, Int32 x, Int32 y, Int32 z)
        {
            GetDelegateFor<glVertexAttribI3i>()(index, x, y, z);
        }
        public static void VertexAttribI4(UInt32 index, Int32 x, Int32 y, Int32 z, Int32 w)
        {
            GetDelegateFor<glVertexAttribI4i>()(index, x, y, z, w);
        }
        public static void VertexAttribI1(UInt32 index, UInt32 x)
        {
            GetDelegateFor<glVertexAttribI1ui>()(index, x);
        }
        public static void VertexAttribI2(UInt32 index, UInt32 x, UInt32 y)
        {
            GetDelegateFor<glVertexAttribI2ui>()(index, x, y);
        }
        public static void VertexAttribI3(UInt32 index, UInt32 x, UInt32 y, UInt32 z)
        {
            GetDelegateFor<glVertexAttribI3ui>()(index, x, y, z);
        }
        public static void VertexAttribI4(UInt32 index, UInt32 x, UInt32 y, UInt32 z, UInt32 w)
        {
            GetDelegateFor<glVertexAttribI4ui>()(index, x, y, z, w);
        }
        public static void VertexAttribI1(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttribI1iv>()(index, v);
        }
        public static void VertexAttribI2(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttribI2iv>()(index, v);
        }
        public static void VertexAttribI3(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttribI3iv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, Int32[] v)
        {
            GetDelegateFor<glVertexAttribI4iv>()(index, v);
        }
        public static void VertexAttribI1(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttribI1uiv>()(index, v);
        }
        public static void VertexAttribI2(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttribI2uiv>()(index, v);
        }
        public static void VertexAttribI3(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttribI3uiv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, UInt32[] v)
        {
            GetDelegateFor<glVertexAttribI4uiv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, SByte[] v)
        {
            GetDelegateFor<glVertexAttribI4bv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, Int16[] v)
        {
            GetDelegateFor<glVertexAttribI4sv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, Byte[] v)
        {
            GetDelegateFor<glVertexAttribI4ubv>()(index, v);
        }
        public static void VertexAttribI4(UInt32 index, UInt16[] v)
        {
            GetDelegateFor<glVertexAttribI4usv>()(index, v);
        }
        public static void GetUniform(UInt32 program, Int32 location, UInt32[] parameters)
        {
            GetDelegateFor<glGetUniformuiv>()(program, location, parameters);
        }
        public static void BindFragDataLocation(UInt32 program, UInt32 color, String name)
        {
            GetDelegateFor<glBindFragDataLocation>()(program, color, name);
        }
        public static Int32 GetFragDataLocation(UInt32 program, String name)
        {
            return GetDelegateFor<glGetFragDataLocation>()(program, name);
        }
        public static void Uniform1(Int32 location, UInt32 v0)
        {
            GetDelegateFor<glUniform1ui>()(location, v0);
        }
        public static void Uniform2(Int32 location, UInt32 v0, UInt32 v1)
        {
            GetDelegateFor<glUniform2ui>()(location, v0, v1);
        }
        public static void Uniform3(Int32 location, UInt32 v0, UInt32 v1, UInt32 v2)
        {
            GetDelegateFor<glUniform3ui>()(location, v0, v1, v2);
        }
        public static void Uniform4(Int32 location, UInt32 v0, UInt32 v1, UInt32 v2, UInt32 v3)
        {
            GetDelegateFor<glUniform4ui>()(location, v0, v1, v2, v3);
        }
        public static void Uniform1(Int32 location, Int32 count, UInt32[] value)
        {
            GetDelegateFor<glUniform1uiv>()(location, count, value);
        }
        public static void Uniform2(Int32 location, Int32 count, UInt32[] value)
        {
            GetDelegateFor<glUniform2uiv>()(location, count, value);
        }
        public static void Uniform3(Int32 location, Int32 count, UInt32[] value)
        {
            GetDelegateFor<glUniform3uiv>()(location, count, value);
        }
        public static void Uniform4(Int32 location, Int32 count, UInt32[] value)
        {
            GetDelegateFor<glUniform4uiv>()(location, count, value);
        }
        public static void TexParameterI(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glTexParameterIiv>()(target, pname, parameters);
        }
        public static void TexParameterI(UInt32 target, UInt32 pname, UInt32[] parameters)
        {
            GetDelegateFor<glTexParameterIuiv>()(target, pname, parameters);
        }
        public static void GetTexParameterI(UInt32 target, UInt32 pname, Int32[] parameters)
        {
            GetDelegateFor<glGetTexParameterIiv>()(target, pname, parameters);
        }
        public static void GetTexParameterI(UInt32 target, UInt32 pname, UInt32[] parameters)
        {
            GetDelegateFor<glGetTexParameterIuiv>()(target, pname, parameters);
        }
        public static void ClearBuffer(UInt32 buffer, Int32 drawbuffer, Int32[] value)
        {
            GetDelegateFor<glClearBufferiv>()(buffer, drawbuffer, value);
        }
        public static void ClearBuffer(UInt32 buffer, Int32 drawbuffer, UInt32[] value)
        {
            GetDelegateFor<glClearBufferuiv>()(buffer, drawbuffer, value);
        }
        public static void ClearBuffer(UInt32 buffer, Int32 drawbuffer, Single[] value)
        {
            GetDelegateFor<glClearBufferfv>()(buffer, drawbuffer, value);
        }
        public static void ClearBuffer(UInt32 buffer, Int32 drawbuffer, Single depth, Int32 stencil)
        {
            GetDelegateFor<glClearBufferfi>()(buffer, drawbuffer, depth, stencil);
        }
        public static String GetString(UInt32 name, UInt32 index)
        {
            return GetDelegateFor<glGetStringi>()(name, index);
        }
        public static void DrawArraysInstanced(UInt32 mode, Int32 first, Int32 count, Int32 primcount)
        {
            GetDelegateFor<glDrawArraysInstanced>()(mode, first, count, primcount);
        }
        public static void DrawElementsInstanced(UInt32 mode, Int32 count, UInt32 type, IntPtr indices, Int32 primcount)
        {
            GetDelegateFor<glDrawElementsInstanced>()(mode, count, type, indices, primcount);
        }
        public static void TexBuffer(UInt32 target, UInt32 internalformat, UInt32 buffer)
        {
            GetDelegateFor<glTexBuffer>()(target, internalformat, buffer);
        }
        public static void PrimitiveRestartIndex(UInt32 index)
        {
            GetDelegateFor<glPrimitiveRestartIndex>()(index);
        }
        public static void GetInteger64(UInt32 target, UInt32 index, Int64[] data)
        {
            GetDelegateFor<glGetInteger64i_v>()(target, index, data);
        }
        public static void GetBufferParameteri64(UInt32 target, UInt32 pname, Int64[] parameters)
        {
            GetDelegateFor<glGetBufferParameteri64v>()(target, pname, parameters);
        }
        public static void FramebufferTexture(UInt32 target, UInt32 attachment, UInt32 texture, Int32 level)
        {
            GetDelegateFor<glFramebufferTexture>()(target, attachment, texture, level);
        }
        public static void VertexAttribDivisor(UInt32 index, UInt32 divisor)
        {
            GetDelegateFor<glVertexAttribDivisor>()(index, divisor);
        }
        public static void MinSampleShading(Single value)
        {
            GetDelegateFor<glMinSampleShading>()(value);
        }
        public static void BlendEquation(UInt32 buf, UInt32 mode)
        {
            GetDelegateFor<glBlendEquationi>()(buf, mode);
        }
        public static void BlendEquationSeparate(UInt32 buf, UInt32 modeRGB, UInt32 modeAlpha)
        {
            GetDelegateFor<glBlendEquationSeparatei>()(buf, modeRGB, modeAlpha);
        }
        public static void BlendFunc(UInt32 buf, UInt32 src, UInt32 dst)
        {
            GetDelegateFor<glBlendFunci>()(buf, src, dst);
        }
        public static void BlendFuncSeparate(UInt32 buf, UInt32 srcRGB, UInt32 dstRGB, UInt32 srcAlpha, UInt32 dstAlpha)
        {
            GetDelegateFor<glBlendFuncSeparatei>()(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }
        #endregion
    }
}
