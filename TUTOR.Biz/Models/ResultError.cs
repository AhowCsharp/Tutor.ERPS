using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TUTOR.Biz.Models
{
    public class ResultError
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public ResultError(string code, string type, string title, string message)
        {
            Code = code;
            Type = type;
            Title = title;
            Message = message;
        }

        public static ResultError Get(string code)
        {
            return GetAll().FirstOrDefault(m => m.Code == code) as ResultError;
        }

        public static IEnumerable<ResultError> GetAll()
        {
            return typeof(ResultError).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select(f => f.GetValue(null)).Cast<ResultError>();
        }

        public static readonly ResultError ERR_TOKEN = new(nameof(ERR_TOKEN), "系統管理", "系統錯誤", "查無Token資料");

        public static readonly ResultError ERR_STUDENT = new(nameof(ERR_STUDENT), "學生管理", "資料錯誤", "資料格式錯誤");
    }
}