using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdModel
{
    public partial class AdBanner
    {
        private int _BannerSequence;
        public int BannerSequence
        {
            get
            {
                return this._BannerSequence;
            }
            set
            {
                this._BannerSequence = value;
            }
        }
    }
}
