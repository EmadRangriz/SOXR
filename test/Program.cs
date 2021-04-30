using System;
using SOXR_wrapper;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            #region comparing piecewise approach and one step for float inputs
            SOXR_resampler SR1 = new SOXR_resampler();
            SOXR_resampler SR2 = new SOXR_resampler();
            //set resampler's variabes
            //SR1.precision_in_bits = SOXR_resampler.dll_import.precision_in_bits_enum.SOXR_LQ;
            SR1.phase_mode = SOXR_resampler.dll_import.phase_mode_enum.SOXR_INTERMEDIATE_PHASE;
            SR1.resample_mode = SOXR_resampler.dll_import.resample_mode_enum.SOXR_HI_PREC_CLOCK;
            SR1.rolloff_mode = SOXR_resampler.dll_import.rolloff_mode_enum.SOXR_ROLLOFF_MEDIUM;
            SR1.runtime_spec = SOXR_resampler.dll_import.runtime_spec_enum.SOXR_COEF_INTERP_AUTO;
            SR1.inRate = 44100; SR1.outRate = 8000;
            SR2.phase_mode = SOXR_resampler.dll_import.phase_mode_enum.SOXR_MINIMUM_PHASE;
            SR2.resample_mode = SOXR_resampler.dll_import.resample_mode_enum.SOXR_DOUBLE_PRECISION;
            SR2.rolloff_mode = SOXR_resampler.dll_import.rolloff_mode_enum.SOXR_ROLLOFF_SMALL;
            SR2.runtime_spec = SOXR_resampler.dll_import.runtime_spec_enum.SOXR_COEF_INTERP_HIGH;
            SR2.inRate = 44100; SR2.outRate = 8000;
            //initiation
            SR1.Init();
            SR2.Init();

            int signal_length = 1000;
            float[] input_signal = new float[signal_length];
            float[] output_signal = new float[signal_length];
            float[] output_signal_pieceWise = new float[signal_length];
            for (int i = 0; i < signal_length; i++)
                input_signal[i] = i + 1;// rnd.Next(0, 1023);
            SR1.do_resample(ref input_signal, 0, signal_length, ref output_signal, 0);

            ulong outdone = 0;
            int num_division = 50;
            for (int i = 0; i < num_division; i++)
            {
                SR2.indone = 0;
                SR2.do_resample(ref input_signal, i * signal_length / num_division, signal_length / num_division
                    , ref output_signal_pieceWise, (int)outdone);
                outdone += SR2.outdone;
            }

            float[] outputs_diff = new float[outdone];
            for (int i = 0; i < (int)outdone; i++)
            {
                outputs_diff[i] = output_signal[i] - output_signal_pieceWise[i];
            }
            #endregion
            #region comparing piecewise approach and one step for complex inputs
            SOXR_resampler ScR1 = new SOXR_resampler();
            SOXR_resampler ScR2 = new SOXR_resampler();
            //set resampler's variabes
            ScR1.phase_mode = SOXR_resampler.dll_import.phase_mode_enum.SOXR_STEEP_FILTER;
            ScR1.resample_mode = SOXR_resampler.dll_import.resample_mode_enum.SOXR_none;
            ScR1.rolloff_mode = SOXR_resampler.dll_import.rolloff_mode_enum.SOXR_ROLLOFF_NONE;
            ScR1.runtime_spec = SOXR_resampler.dll_import.runtime_spec_enum.SOXR_COEF_INTERP_LOW;
            ScR1.inRate = 44100; ScR1.outRate = 8000;
            ScR2.phase_mode = SOXR_resampler.dll_import.phase_mode_enum.SOXR_MINIMUM_PHASE;
            ScR2.resample_mode = SOXR_resampler.dll_import.resample_mode_enum.SOXR_DOUBLE_PRECISION;
            ScR2.rolloff_mode = SOXR_resampler.dll_import.rolloff_mode_enum.SOXR_ROLLOFF_SMALL;
            ScR2.runtime_spec = SOXR_resampler.dll_import.runtime_spec_enum.SOXR_COEF_INTERP_HIGH;
            ScR2.inRate = 44100; ScR2.outRate = 8000;
            ScR1.is_complex = true; ScR2.is_complex = true;  // dont forget to change "is_complex" to true
            //initiation
            ScR1.Init();
            ScR2.Init();

            signal_length = 1000;
            ComplexF[] inputc_signal = new ComplexF[signal_length];
            ComplexF[] outputc_signal = new ComplexF[signal_length];
            ComplexF[] outputc_signal_pieceWise = new ComplexF[signal_length];
            for (int i = 0; i < signal_length; i++)
            {
                inputc_signal[i].Re = i + rnd.Next(0, 10);
                inputc_signal[i].Im = i + rnd.Next(0, 10);
            }
            ScR1.do_resample(ref inputc_signal, 0, signal_length, ref outputc_signal, 0);

            outdone = 0;
            num_division = 50;
            for (int i = 0; i < num_division; i++)
            {
                ScR2.indone = 0;
                ScR2.do_resample(ref inputc_signal, i * signal_length / num_division, signal_length / num_division
                    , ref outputc_signal_pieceWise, (int)outdone);
                outdone += ScR2.outdone;
            }

            ComplexF[] outputsc_diff = new ComplexF[outdone];
            for (int i = 0; i < (int)outdone; i++)
            {
                outputsc_diff[i] = outputc_signal[i] - outputc_signal_pieceWise[i];
            }
            #endregion
        }
    }
}
