using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public enum ErrorType
    {
        OPEN,
        SHORT,

        OPEN_VPH ,
        OPEN_GND,
        OPEN_DorN,
        OPEN_UP,
        OPEN_PWR,
        OPEN_PWR_VPH,
        OPEN_GND_VPH,
        OPEN_UP_PWR,
        OPEN_GND_DorN,

        SHORT_GNG_UP,
        SHORT_GNG_DorN,
        SHORT_PWR_VPH,
        SHORT_UP_DorN,
        SHORT_PWR_DorN,
        SHORT_VPH_DorN,
        SHORT_VPH_UP,
        SHORT_GND_PWR,
        SHORT_GND_VPH,
        SHORT_UP_PWR
    }
}
