using Core.Text.Formatter;

namespace Core.Extensions.TextRelated
{
    public static class SiFormatterExt
    {
        /// <summary>
        /// Convenient method to activate auto scaling of si-prefix. Same as: ForcedDegree = null
        /// </summary>
        public static void AutoScale(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = null;
        }
        
        /// <summary>
        /// Convenient method to force si-prefix to kilo (e3)
        /// </summary>
        public static void ForceKilo(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 1;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Mega (e6)
        /// </summary>
        public static void ForceMega(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 2;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Giga (e9)
        /// </summary>
        public static void ForceGiga(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 3;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Tera (e12)
        /// </summary>
        public static void ForceTera(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = 4;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Milli (e-3)
        /// </summary>
        public static void ForceMilli(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -1;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Micro (e-6)
        /// </summary>
        public static void ForceMicro(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -2;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Nano (e-9)
        /// </summary>
        public static void ForceNano(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -3;
        }

        /// <summary>
        /// Convenient method to force si-prefix to Pico (e-12)
        /// </summary>
        public static void ForcePico(this ISiFormatter formatter)
        {
            formatter.ForcedDegree = -4;
        }
    }
}