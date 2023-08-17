using System;

namespace TUTOR.Biz.SeedWork
{
    public interface ISendStatus
    {
        int Status { get; set; }

        DateTime BeginDate { get; set; }

        DateTime EndDate { get; set; }
    }
}