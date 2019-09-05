using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace AppSetting.Domain.Models
{
    /// <summary>
    /// 用户反馈
    /// </summary>
    public class UserFadeback
    {
        /// <summary>
        /// 类别, 如果类别则为空
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 玩家提的问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 是否是快捷问题
        /// </summary>
        public bool IsQuick { get; set; }
    }
    public class FadeBackAnswer
    {
        public string Answer { get; set; }
        public List<string> AnswerUrls { get; set; }
    }
    public class FadebackInfo
    {



        public FadebackInfo()
        {

        }

        [BsonId]
        public Guid gid { get; set; }
        public long Id { get; set; }
        public string Category {get; set;}
        public string Question { get; set; }
        public bool IsQuick { get; set; }
        public long FadebackTime { get; set; }
        public List<string> QuestionUrls { get; set; }
        public List<FadeBackAnswer> Reply { get; set; }
        public long ReplyTime { get; set; }

    }

    /// <summary>
    /// 获取反馈回复的调试
    /// </summary>
    public class FadebackReplyNum
    {
        /// <summary>
        /// 回复数量
        /// </summary>
        public int ReplyNum { get; set; }
    }
}
