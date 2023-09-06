using System;

namespace side.DataSet
{
    public class DataSet_WidthdrawItem
    {
        public string CaseId { get; set; }
        public int MemberId { get; set; }
        public int ToMemberId { get; set; }
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal FeeRatio { get; set; }
        public decimal Fee1 { get; set; }
        public decimal Fee2 { get; set; }
        public decimal Available { get; set; }
        public string State { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
