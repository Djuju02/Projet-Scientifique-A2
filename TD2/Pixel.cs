using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_2
{
    class Pixel
    {
        byte r;
        byte g;
        byte b;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rouge_1"></param>
        /// <param name="vert_1"></param>
        /// <param name="bleu_1"></param>
        public Pixel(byte rouge_1, byte vert_1, byte bleu_1)
        {
            if (r >= 0 && r <= 255) this.r = rouge_1;
            if (g >= 0 && g <= 255) this.g = vert_1;
            if (b >= 0 && b <= 255) this.b = bleu_1;
        }
        /// <summary>
        /// 
        /// </summary>
        public byte GetRouge
        {
            get { return this.r; }
            set { this.r = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte GetVert
        {
            get { return this.g; }
            set { this.g = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte GetBleu
        {
            get { return this.b; }
            set { this.b = value; }
        }

    }
}
