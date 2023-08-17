using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUTOR.Biz.Models;

namespace TUTOR.Biz.Models
{
    public class CommonResult
    {
        /// <summary>
        /// 執行操作的使用者
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// 來源ID
        /// </summary>
        public int RefId { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 錯誤訊息集合
        /// </summary>
        public List<ResultError> Errors { get; set; } = new List<ResultError>();

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (Errors.Any())
                {
                    return string.Join(",", Errors);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Success
        {
            get
            {
                return !Errors.Any();
            }
        }

        public void AddError(IEnumerable<string> errorCodes)
        {
            Errors.AddRange(errorCodes.Select(x => ResultError.Get(x)));
        }

        public void AddError(IEnumerable<ResultError> commonErrors)
        {
            Errors.AddRange(commonErrors);
        }

        public void AddError(string errorCode)
        {
            Errors.Add(ResultError.Get(errorCode));
        }

        public void AddError(ResultError commonError)
        {
            Errors.Add(commonError);
        }
    }

    public class CommonResult<T> : CommonResult
    {
        /// <summary>
        /// 資料
        /// </summary>
        public T Data { get; set; }
    }
}