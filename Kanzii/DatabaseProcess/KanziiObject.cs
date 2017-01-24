using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProcess
{
    public class KanziiObject
    {
        public int id { get; set; }
        public String mazii_id { get; set; }
        public String word { get; set; }
        public String lesson { get; set; }
        public String vi_mean { get; set; }
        public String uvi_mean { get; set; }
        public String cn_mean { get; set; }
        public String ucn_mean { get; set; }
        public String image { get; set; }
        public String remember { get; set; }
        public String write { get; set; }
        public String onjomi { get; set; }
        public String ronjomi { get; set; }
        public String kunjomi { get; set; }
        public String rkunjomi { get; set; }
        public String numstroke { get; set; }
        public String favorite { get; set; }
        public String note { get; set; }
        public String tag { get; set; }
    
        public KanziiObject()
        {
            this.id = -1;
            this.mazii_id = null;
            this.word = null;
            this.lesson = null;
            this.vi_mean = null;
            this.uvi_mean = null;
            this.cn_mean = null;
            this.ucn_mean = null;
            this.image = null;
            this.remember = null;
            this.write = null;
            this.onjomi = null;
            this.ronjomi = null;
            this.kunjomi = null;
            this.rkunjomi = null;
            this.numstroke = null;
            this.favorite = null;
            this.note = null;
            this.tag = null;
        }

        public KanziiObject(int id, String mazii_id, String word, String lesson, String vi_mean, String uvi_mean, 
            String cn_mean, String ucn_mean, String image, String remember, String write, String onjomi,
            String ronjomi, String kunjomi, String rkunjomi, String numstroke, String favorite, String note,
            String tag)
        {
            this.id = id;
            this.mazii_id = mazii_id;
            this.word = word;
            this.lesson = lesson;
            this.vi_mean = vi_mean;
            this.uvi_mean = uvi_mean;
            this.cn_mean = cn_mean;
            this.ucn_mean = ucn_mean;
            this.image = image;
            this.remember = remember;
            this.write = write;
            this.onjomi = onjomi;
            this.ronjomi = ronjomi;
            this.kunjomi = kunjomi;
            this.rkunjomi = rkunjomi;
            this.numstroke = numstroke;
            this.favorite = favorite;
            this.note = note;
            this.tag = tag;
        }
    }
}
