using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Model
{
    public class EnumBase
    {
        /// <summary>
        /// 付款进度
        /// </summary>
        public enum PaymentScheduleNode
        {
            /// <summary>
            /// 首付款
            /// </summary>
            [Description("首付款")]
            One = 1,
            /// <summary>
            /// 二期款
            /// </summary>
            [Description("二期款")]
            Two = 2,
            /// <summary>
            /// 发货款
            /// </summary>
            [Description("发货款")]
            Three = 3,
            /// <summary>
            /// 到货款
            /// </summary>
            [Description("到货款")]
            Four = 4,
            /// <summary>
            /// 验收款
            /// </summary>
            [Description("验收款")]
            Five = 5,
            /// <summary>
            /// 质保金
            /// </summary>
            [Description("质保金")]
            Six = 6
        }

        /// <summary>
        /// 邮箱模板
        /// </summary>
        public enum EmailTemplateEn
        {
            /// <summary>
            /// 付款
            /// </summary>
            [Description("PaEmailTemplate.html")]
            PaEmailTemplate = 1,
            /// <summary>
            /// 验收提交
            /// </summary>
            [Description("GrEmailTemplate.html")]
            GrEmailTemplate = 2,
        }
    }
    /// <summary>
    /// 报错级别
    /// </summary>
    public enum MessageLevel
    {
        LogNormal = 0,
        LogAppend = 1,
        LogError = 2,
        LogWarning = 3,
        LogMessage = 4,
        LogCustom = 5,
    }
    /// <summary>
    /// 状态等级
    /// </summary>
    public enum StateLevel
    {
        LvZero = 0,//正常
        LvOne = 1,//删除
        LvTwo = 2,//取消展示
        LvThree = 3,//正常-邮件提醒
        LvFour = 4,//正常-短信提醒
        LvFive = 5,//正常-双向提醒
        LvSix = 6//正常-已被修改
    }
    /// <summary>
    /// 任务展示模式
    /// </summary>
    public enum ShowType
    {
        [Description("平铺")]
        Tile = 0,//平铺
        [Description("列表")]
        List = 1//列表
    }
    /// <summary>
    /// 便签类型
    /// </summary>
    public enum NoteType
    {
        [Description("普通便签")]
        GeneralNote = 0,//普通便签
        [Description("任务便签")]
        TaskNote = 1//任务便签
    }

    /// <summary>
    /// 语种类别
    /// </summary>
    public enum LanguageEnum
    {
        [Description("中文")]
        LanguageCN,
        [Description("英文")]
        LanguageEN,
    }
    /// <summary>
    /// be in common use  通用
    /// </summary>
    public enum Types {
        _null,
        _a,
        _b,
        _c,
        _d
    }
    /*
    * ============================================================
    * 函数名：ModularizationSql
    * 作者：木子杨
    * 版本：1.0
    * 日期：
    * 描述：Sql模块儿化
    * ============================================================
    */
    public enum ModularizationSql
    {
        [Description("任务相关")]
        _INSTTASK = 001,
        [Description("客户端用户相关")]
        _CLIENTINFO = 001,
        [Description("日志相关")]
        _LOGSINFO = 002
    }

    /*
    * ============================================================
    * 函数名：DateBaseType
    * 作者：木子杨
    * 版本：1.0
    * 日期：
    * 描述：数据存储类型
    * ============================================================
    */
    public enum DateBaseLocation
    {
        _LOCAL,
        _SERVER,
        _LOCAL_AND_SERVER
    }

}