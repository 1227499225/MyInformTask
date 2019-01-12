using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 普通便签
    /// </summary>   
    [Serializable]
    [Description("普通便签")]
    [_Mo(TableName  = "PlainNote")]
    public class PlainNoteModel : BaseZhModel
    {
        /// <summary>
        /// 明细
        /// </summary>
        [Filed(IsIncrement = false)]
        public List<PNDetailedModel> _dets = new List<PNDetailedModel>();
    }

    /// <summary>
    /// 普通明细
    /// </summary>   
    [Serializable]
    [Description("普通明细")]
    [_Mo(TableName = "PNDetailed")]
    public class PNDetailedModel : BaseDetailModel
    {
    }
}
