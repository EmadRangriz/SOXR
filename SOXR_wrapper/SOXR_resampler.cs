using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SOXR_wrapper
{
    public struct ComplexF : IComparable, ICloneable
    {

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// The real component of the complex number
        /// </summary>
        public float Re;

        /// <summary>
        /// The imaginary component of the complex number
        /// </summary>
        public float Im;

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Create a complex number from a real and an imaginary component
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public ComplexF(float real, float imaginary)
        {
            this.Re = (float)real;
            this.Im = (float)imaginary;
        }

        /// <summary>
        /// Create a complex number based on an existing complex number
        /// </summary>
        /// <param name="c"></param>
        public ComplexF(ComplexF c)
        {
            this.Re = c.Re;
            this.Im = c.Im;
        }

        /// <summary>
        /// Create a complex number from a real and an imaginary component
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        /// <returns></returns>
        static public ComplexF FromRealImaginary(float real, float imaginary)
        {
            ComplexF c;
            c.Re = (float)real;
            c.Im = (float)imaginary;
            return c;
        }

        /// <summary>
        /// Create a complex number from a modulus (length) and an argument (radian)
        /// </summary>
        /// <param name="modulus"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        static public ComplexF FromModulusArgument(float modulus, float argument)
        {
            ComplexF c;
            c.Re = (float)(modulus * System.Math.Cos(argument));
            c.Im = (float)(modulus * System.Math.Sin(argument));
            return c;
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        object ICloneable.Clone()
        {
            return new ComplexF(this);
        }
        /// <summary>
        /// Clone the complex number
        /// </summary>
        /// <returns></returns>
        public ComplexF Clone()
        {
            return new ComplexF(this);
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// The modulus (length) of the complex number
        /// </summary>
        /// <returns></returns>
        public float GetModulus()
        {
            float x = this.Re;
            float y = this.Im;
            return (float)Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// The squared modulus (length^2) of the complex number
        /// </summary>
        /// <returns></returns>
        public float GetModulusSquared()
        {
            float x = this.Re;
            float y = this.Im;
            return (float)x * x + y * y;
        }

        /// <summary>
        /// The argument (radians) of the complex number
        /// </summary>
        /// <returns></returns>
        public float GetArgument()
        {
            return (float)Math.Atan2(this.Im, this.Re);
        }

        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Get the conjugate of the complex number
        /// </summary>
        /// <returns></returns>
        public ComplexF GetConjugate()
        {
            return FromRealImaginary(this.Re, -this.Im);
        }

        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Scale the complex number to 1.
        /// </summary>
        public void Normalize()
        {
            double modulus = this.GetModulus();
            if (modulus == 0)
            {
                throw new DivideByZeroException("Can not normalize a complex number that is zero.");
            }
            this.Re = (float)(this.Re / modulus);
            this.Im = (float)(this.Im / modulus);
        }


        /// <summary>
        /// Convert from a single precision real number to a complex number
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static explicit operator ComplexF(float f)
        {
            ComplexF c;
            c.Re = (float)f;
            c.Im = (float)0;
            return c;
        }

        /// <summary>
        /// Convert from a single precision complex to a real number
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static explicit operator float(ComplexF c)
        {
            return (float)c.Re;
        }
        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Are these two complex numbers equivalent?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ComplexF a, ComplexF b)
        {
            return (a.Re == b.Re) && (a.Im == b.Im);
        }

        /// <summary>
        /// Are these two complex numbers different?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ComplexF a, ComplexF b)
        {
            return (a.Re != b.Re) || (a.Im != b.Im);
        }

        /// <summary>
        /// Get the hash code of the complex number
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (this.Re.GetHashCode() ^ this.Im.GetHashCode());
        }

        /// <summary>
        /// Is this complex number equivalent to another object?
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o is ComplexF)
            {
                ComplexF c = (ComplexF)o;
                return (this == c);
            }
            return false;
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Compare to other complex numbers or real numbers
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;  // null sorts before current
            }
            if (o is ComplexF)
            {
                return this.GetModulus().CompareTo(((ComplexF)o).GetModulus());
            }
            if (o is float)
            {
                return this.GetModulus().CompareTo((float)o);
            }
            //    if (o is Complex)
            //    {
            //      return this.GetModulus().CompareTo(((Complex)o).GetModulus());
            //   }
            if (o is double)
            {
                return this.GetModulus().CompareTo((double)o);
            }
            throw new ArgumentException();
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// This operator doesn't do much. :-)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ComplexF operator +(ComplexF a)
        {
            return a;
        }

        /// <summary>
        /// Negate the complex number
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ComplexF operator -(ComplexF a)
        {
            a.Re = -a.Re;
            a.Im = -a.Im;
            return a;
        }

        /// <summary>
        /// Add a complex number to a real
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static ComplexF operator +(ComplexF a, float f)
        {
            a.Re = (float)(a.Re + f);
            return a;
        }

        /// <summary>
        /// Add a real to a complex number
        /// </summary>
        /// <param name="f"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ComplexF operator +(float f, ComplexF a)
        {
            a.Re = (float)(a.Re + f);
            return a;
        }

        /// <summary>
        /// Add to complex numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplexF operator +(ComplexF a, ComplexF b)
        {
            a.Re = a.Re + b.Re;
            a.Im = a.Im + b.Im;
            return a;
        }

        /// <summary>
        /// Subtract a real from a complex number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static ComplexF operator -(ComplexF a, float f)
        {
            a.Re = (float)(a.Re - f);
            return a;
        }

        /// <summary>
        /// Subtract a complex number from a real
        /// </summary>
        /// <param name="f"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ComplexF operator -(float f, ComplexF a)
        {
            a.Re = (float)(f - a.Re);
            a.Im = (float)(0 - a.Im);
            return a;
        }

        /// <summary>
        /// Subtract two complex numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplexF operator -(ComplexF a, ComplexF b)
        {
            a.Re = a.Re - b.Re;
            a.Im = a.Im - b.Im;
            return a;
        }

        /// <summary>
        /// Multiply a complex number by a real
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static ComplexF operator *(ComplexF a, float f)
        {
            a.Re = (float)(a.Re * f);
            a.Im = (float)(a.Im * f);
            return a;
        }

        /// <summary>
        /// Multiply a real by a complex number
        /// </summary>
        /// <param name="f"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ComplexF operator *(float f, ComplexF a)
        {
            a.Re = (float)(a.Re * f);
            a.Im = (float)(a.Im * f);
            return a;
        }

        /// <summary>
        /// Multiply two complex numbers together
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplexF operator *(ComplexF a, ComplexF b)
        {
            // (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
            double x = a.Re, y = a.Im;
            double u = b.Re, v = b.Im;
            a.Re = (float)(x * u - y * v);
            a.Im = (float)(x * v + y * u);
            return a;
        }

        /// <summary>
        /// Divide a complex number by a real number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static ComplexF operator /(ComplexF a, float f)
        {
            if (f == 0)
            {
                throw new DivideByZeroException();
            }
            a.Re = (float)(a.Re / f);
            a.Im = (float)(a.Im / f);
            return a;
        }

        /// <summary>
        /// Divide a complex number by a complex number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplexF operator /(ComplexF a, ComplexF b)
        {
            double x = a.Re, y = a.Im;
            double u = b.Re, v = b.Im;
            double denom = u * u + v * v;

            if (denom == 0)
            {
                throw new DivideByZeroException();
            }
            a.Re = (float)((x * u + y * v) / denom);
            a.Im = (float)((y * u - x * v) / denom);
            return a;
        }

        /// <summary>
        /// Parse a complex representation in this fashion: "( %f, %f )"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public ComplexF Parse(string s)
        {
            throw new NotImplementedException("ComplexF ComplexF.Parse( string s ) is not implemented.");
        }

        /// <summary>
        /// Get the string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("( {0}, {1}i )", this.Re, this.Im);
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Determine whether two complex numbers are almost (i.e. within the tolerance) equivalent.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        static public bool IsEqual(ComplexF a, ComplexF b, float tolerance)
        {
            return
                (Math.Abs(a.Re - b.Re) < tolerance) &&
                (Math.Abs(a.Im - b.Im) < tolerance);

        }

        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        /// <summary>
        /// Represents zero
        /// </summary>
        static public ComplexF Zero
        {
            get { return new ComplexF(0, 0); }
        }

        /// <summary>
        /// Represents the result of sqrt( -1 )
        /// </summary>
        static public ComplexF I
        {
            get { return new ComplexF(0, 1); }
        }

        /// <summary>
        /// Represents the largest possible value of ComplexF.
        /// </summary>
        static public ComplexF MaxValue
        {
            get { return new ComplexF(float.MaxValue, float.MaxValue); }
        }

        /// <summary>
        /// Represents the smallest possible value of ComplexF.
        /// </summary>
        static public ComplexF MinValue
        {
            get { return new ComplexF(float.MinValue, float.MinValue); }
        }


        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------
    }
    public struct ComplexS : IComparable, ICloneable
    {
        /// <summary>
        /// The real component of the complex number
        /// </summary>
        public short Re;

        /// <summary>
        /// The imaginary component of the complex number
        /// </summary>
        public short Im;

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Create a complex number from a real and an imaginary component
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public ComplexS(short real, short imaginary)
        {
            this.Re = (short)real;
            this.Im = (short)imaginary;
        }

        /// <summary>
        /// Create a complex number based on an existing complex number
        /// </summary>
        /// <param name="c"></param>
        public ComplexS(ComplexS c)
        {
            this.Re = c.Re;
            this.Im = c.Im;
        }

        /// <summary>
        /// Create a complex number from a real and an imaginary component
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        /// <returns></returns>
        static public ComplexS FromRealImaginary(short real, short imaginary)
        {
            ComplexS c;
            c.Re = (short)real;
            c.Im = (short)imaginary;
            return c;
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        object ICloneable.Clone()
        {
            return new ComplexS(this);
        }
        /// <summary>
        /// Clone the complex number
        /// </summary>
        /// <returns></returns>
        public ComplexS Clone()
        {
            return new ComplexS(this);
        }

        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// The modulus (length) of the complex number
        /// </summary>
        /// <returns></returns>
        public float GetModulus()
        {
            short x = this.Re;
            short y = this.Im;
            return (float)Math.Sqrt(x * x + y * y);
        }


        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Compare to other complex numbers or real numbers
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;  // null sorts before current
            }
            if (o is ComplexS)
            {
                return this.GetModulus().CompareTo(((ComplexS)o).GetModulus());
            }
            if (o is float)
            {
                return this.GetModulus().CompareTo((float)o);
            }
            //    if (o is Complex)
            //    {
            //      return this.GetModulus().CompareTo(((Complex)o).GetModulus());
            //   }
            if (o is double)
            {
                return this.GetModulus().CompareTo((double)o);
            }
            throw new ArgumentException();
        }
    }
    public class SOXR_resampler
    {
        public unsafe class dll_import
        {
            const string libname = "libsoxr.dll";
            public struct soxr
            {
                uint num_channels;
                double io_ratio;
                void* error;
                soxr_quality_spec_struct q_spec;
                soxr_io_spec_struct io_spec;
                soxr_runtime_spec_struct runtime_spec;

                void* input_fn_state;
                void* input_fn;
                uint max_ilen;

                void* shared;
                void* resamplers;
                void* control_block;
                void* deinterleave;
                void* interleave;

                void** channel_ptrs;
                uint clips;
                ulong seed;
                int flushing;
            };
            public struct soxr_quality_spec_struct
            {                                       /* Typically */
                double precision;         /* Conversion precision (in bits).           20   */
                double phase_response;    /* 0=minimum, ... 50=linear, ... 100=maximum 50   */
                double passband_end;      /* 0dB pt. bandwidth to preserve; nyquist=1  0.913*/
                double stopband_begin;    /* Aliasing/imaging control; > passband_end   1   */
                void* e;                 /* Reserved for internal use.                 0   */
                ulong flags;      /* Per the following #defines.                0   */
            };
            public enum precision_in_bits_enum : uint
            {
                /* The 5 standard qualities found in SoX: */
                SOXR_QQ = 0,   /* 'Quick' cubic interpolation. */
                SOXR_LQ = 1,   /* 'Low' 16-bit with larger rolloff. */
                SOXR_MQ = 2,   /* 'Medium' 16-bit with medium rolloff. */
                SOXR_HQ = SOXR_20_BITQ,/* 'High quality'. */
                SOXR_VHQ = SOXR_28_BITQ,/* 'Very high quality'. */

                SOXR_16_BITQ = 3,
                SOXR_20_BITQ = 4,
                SOXR_24_BITQ = 5,
                SOXR_28_BITQ = 6,
                SOXR_32_BITQ = 7
            }
            public enum phase_mode_enum : uint
            {
                SOXR_LINEAR_PHASE = 0x00,
                SOXR_INTERMEDIATE_PHASE = 0x10,
                SOXR_MINIMUM_PHASE = 0x30,

                SOXR_STEEP_FILTER = 0x40
            }
            public enum rolloff_mode_enum : uint
            {
                SOXR_ROLLOFF_SMALL = 0u,   /* <= 0.01 dB */
                SOXR_ROLLOFF_MEDIUM = 1u,   /* <= 0.35 dB */
                SOXR_ROLLOFF_NONE = 2u,   /* For Chebyshev bandwidth. */
            }
            public enum resample_mode_enum : uint
            {
                SOXR_none = 0,
                SOXR_HI_PREC_CLOCK = 8u,  /* Increase `irrational' ratio accuracy. */
                SOXR_DOUBLE_PRECISION = 16u,  /* Use D.P. calcs even if precision <= 20. */
                SOXR_VR = 32u  /* Variable-rate resampling. */
            }
            public struct soxr_io_spec_struct
            {                                            /* Typically */
                public soxr_datatype_enum itype;     /* Input datatype.                SOXR_FLOAT32_I */
                public soxr_datatype_enum otype;     /* Output datatype.               SOXR_FLOAT32_I */
                public double scale;              /* Linear gain to apply during resampling.  1    */
                public void* e;                  /* Reserved for internal use                0    */
                public ulong flags;       /* Per the following #defines.              0    */
            };
            //public enum soxr_datatype_enum
            //{          /* Datatypes supported for I/O to/from the resampler: */
            //           /* Internal; do not use: */
            //    SOXR_FLOAT32, SOXR_FLOAT64, SOXR_INT32, SOXR_INT16, SOXR_SPLIT = 4,

            //    /* Use for interleaved channels: */
            //    SOXR_FLOAT32_I = SOXR_FLOAT32, SOXR_FLOAT64_I, SOXR_INT32_I, SOXR_INT16_I,

            //    /* Use for split channels: */
            //    SOXR_FLOAT32_S = SOXR_SPLIT, SOXR_FLOAT64_S, SOXR_INT32_S, SOXR_INT16_S

            //}
            public enum soxr_datatype_enum
            {
                SOXR_FLOAT = 0,
                SOXR_DOUBLE = 1,
                SOXR_INT = 2,
                SOXR_INT16 = 3,

                /* Use for split channels: */
                SOXR_FLOAT32_split_channels = 4,
                SOXR_FLOAT64_split_channels = 5,
                SOXR_INT32_split_channels = 6,
                SOXR_INT16_split_channels = 7
            }
            public struct soxr_runtime_spec_struct
            {                                       /* Typically */
                uint log2_min_dft_size;   /* For DFT efficiency. [8,15]           10    */
                uint log2_large_dft_size; /* For DFT efficiency. [8,20]           17    */
                uint coef_size_kbytes;    /* For SOXR_COEF_INTERP_AUTO (below).   400   */
                uint num_threads;         /* 0: per OMP_NUM_THREADS; 1: 1 thread.  1    */
                void* e;                     /* Reserved for internal use.            0    */
                ulong flags;          /* Per the following #defines.           0    */
            };
            public enum runtime_spec_enum : uint
            {
                /* For `irrational' ratios only: */
                SOXR_COEF_INTERP_AUTO = 0,    /* Auto select coef. interpolation. */
                SOXR_COEF_INTERP_LOW = 2,   /* Man. select: less CPU, more memory. */
                SOXR_COEF_INTERP_HIGH = 3    /* Man. select: more CPU, less memory. */
            }

            [DllImport(libname)]
            public static extern
            IntPtr soxr_create(double input_rate, double output_rate,
               uint num_channels,
               IntPtr* error0,
               IntPtr io_spec,
               IntPtr q_spec,
               IntPtr runtime_spec);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_process(IntPtr soxr, IntPtr input, ulong ilen0, ulong* idone0,
                 IntPtr output, ulong olen, ulong* odone0);

            [DllImport(libname)]
            public static extern
            double soxr_delay(IntPtr soxr);

            [DllImport(libname)]
            public static extern
            ulong soxr_output(IntPtr soxr, float* output, ulong len0);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_runtime_spec(uint num_threads, runtime_spec_enum rSpec);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_quality_spec(ulong recipe, ulong flags);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_io_spec(soxr_datatype_enum itype, soxr_datatype_enum otype);
            [DllImport(libname)]
            public static extern
            void soxr_delete0(IntPtr soxr);
            [DllImport(libname)]
            public static extern
            void soxr_delete(IntPtr psoxr);
            [DllImport(libname)]
            public static extern
            void soxr_delete_complete(IntPtr psoxr, IntPtr soxr_quality_spec, IntPtr soxr_io_spec, IntPtr soxr_runtime_spec);
            [DllImport(libname)]
            public static extern
            IntPtr initialise(IntPtr soxr);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_set_num_channels(IntPtr soxr, uint num_channels);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_set_io_ratio(IntPtr soxr, double io_ratio, ulong slew_len);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_clear(IntPtr soxr);
            [DllImport(libname)]
            public static extern
            IntPtr soxr_oneshot(double irate, double orate, uint num_channels,
                                float* input, ulong ilen, ulong* idone,
                                float* output, ulong olen, ulong* odone,
                                IntPtr io_spec,
                                IntPtr q_spec,
                                IntPtr runtime_spec);
        }

        private IntPtr Ptr_soxr, Ptr_error, Ptr_q_spec, Ptr_io_spec, Ptr_runtime_spec;
        public dll_import.soxr_quality_spec_struct qSp;
        public dll_import.soxr_io_spec_struct iSp;
        public dll_import.soxr_runtime_spec_struct rSp;
        public dll_import.soxr ps;

        public double inRate = -1, outRate = -1;
        public bool is_complex;
        string Status;
        public dll_import.precision_in_bits_enum precision_in_bits = dll_import.precision_in_bits_enum.SOXR_QQ;
        public dll_import.phase_mode_enum phase_mode = dll_import.phase_mode_enum.SOXR_LINEAR_PHASE;
        public dll_import.resample_mode_enum resample_mode = dll_import.resample_mode_enum.SOXR_none;
        public dll_import.rolloff_mode_enum rolloff_mode = dll_import.rolloff_mode_enum.SOXR_ROLLOFF_SMALL;
        public dll_import.runtime_spec_enum runtime_spec = dll_import.runtime_spec_enum.SOXR_COEF_INTERP_LOW;
        public dll_import.soxr_datatype_enum soxr_input_datatype = dll_import.soxr_datatype_enum.SOXR_FLOAT;
        public dll_import.soxr_datatype_enum soxr_output_datatype = dll_import.soxr_datatype_enum.SOXR_FLOAT;
        public uint numThread = 1;

        public ulong indone, outdone;
        ~SOXR_resampler()
        {
            dll_import.soxr_delete_complete(Ptr_soxr, Ptr_q_spec, Ptr_io_spec, Ptr_runtime_spec);
            Ptr_soxr = IntPtr.Zero;
            Ptr_error = IntPtr.Zero;
            Ptr_q_spec = IntPtr.Zero;
            Ptr_io_spec = IntPtr.Zero;
            Ptr_runtime_spec = IntPtr.Zero;
        }
        public string Init()
        {
            Ptr_q_spec = dll_import.soxr_quality_spec(((ulong)precision_in_bits | (ulong)phase_mode), ((ulong)rolloff_mode | (ulong)resample_mode));
            qSp = (dll_import.soxr_quality_spec_struct)Marshal.PtrToStructure(Ptr_q_spec, typeof(dll_import.soxr_quality_spec_struct));

            Ptr_io_spec = dll_import.soxr_io_spec(soxr_input_datatype, soxr_output_datatype);
            iSp = (dll_import.soxr_io_spec_struct)Marshal.PtrToStructure(Ptr_io_spec, typeof(dll_import.soxr_io_spec_struct));

            Ptr_runtime_spec = dll_import.soxr_runtime_spec(numThread, runtime_spec);
            rSp = (dll_import.soxr_runtime_spec_struct)Marshal.PtrToStructure(Ptr_runtime_spec, typeof(dll_import.soxr_runtime_spec_struct));
            unsafe
            {
                fixed (IntPtr* df = &Ptr_error) 
                Ptr_soxr = (dll_import.soxr_create(inRate, outRate,
                    is_complex ? (uint)2 : (uint)1, df, Ptr_io_spec, Ptr_q_spec, Ptr_runtime_spec));
            }
            ps = (dll_import.soxr)Marshal.PtrToStructure(Ptr_soxr, typeof(dll_import.soxr));

            Status = Marshal.PtrToStringAnsi(Ptr_error);
            return Status;
        }
        public bool do_resample(ref float[] input, ref float[] output)
        {
            unsafe
            {
                fixed (float* pinput = input, poutput = output)
                fixed (ulong* pindone = &indone, poutdone = &outdone)
                {
                    Ptr_error = dll_import.soxr_process(Ptr_soxr, (IntPtr)(pinput), (ulong)input.Length,
                        pindone, (IntPtr)poutput, (ulong)Math.Ceiling(outRate / inRate * input.Length), poutdone);
                }
            }
            Status = Marshal.PtrToStringAnsi(Ptr_error);
            return Status != null;
        }
        public bool do_resample(ref float[] input, int from_in, int len, ref float[] output, int from_out)
        {
            unsafe
            {
                fixed (float* pinput = input, poutput = output)
                fixed (ulong* pindone = &indone, poutdone = &outdone)
                {
                    Ptr_error = dll_import.soxr_process(Ptr_soxr, (IntPtr)(pinput + from_in)
                        , (ulong)len, pindone, (IntPtr)(poutput + from_out),
                        (ulong)Math.Ceiling(outRate / inRate * len), poutdone);
                }
            }
            Status = Marshal.PtrToStringAnsi(Ptr_error);
            return Status != null;
        }
        public bool do_resample(ref ComplexF[] input, ref ComplexF[] output)
        {
            unsafe
            {
                fixed (ComplexF* pinput = input, poutput = output)
                fixed (ulong* pindone = &indone, poutdone = &outdone)
                {
                    Ptr_error = dll_import.soxr_process(Ptr_soxr, (IntPtr)(pinput), (ulong)input.Length,
                        pindone, (IntPtr)poutput, (ulong)Math.Ceiling(outRate / inRate * input.Length), poutdone);
                }
            }
            Status = Marshal.PtrToStringAnsi(Ptr_error);
            return Status != null;
        }
        public bool do_resample(ref ComplexF[] input, int from_in, int len, ref ComplexF[] output, int from_out)
        {
            unsafe
            {
                fixed (ComplexF* pinput = input, poutput = output)
                fixed (ulong* pindone = &indone, poutdone = &outdone)
                {
                    Ptr_error = dll_import.soxr_process(Ptr_soxr, (IntPtr)(pinput + from_in)
                        , (ulong)len, pindone, (IntPtr)(poutput + from_out),
                        (ulong)Math.Ceiling(outRate / inRate * len), poutdone);
                }
            }
            Status = Marshal.PtrToStringAnsi(Ptr_error);
            return Status != null;
        }
    }
}
