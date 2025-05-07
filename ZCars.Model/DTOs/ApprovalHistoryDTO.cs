namespace ZhooCars.Model.DTOs
{
    public class ApprovalHistoryDTO
    {
        #region Properties

        public DateTime ChangedAt { get; set; }

        public int ChangedBy { get; set; }

        public int EntityId { get; set; }

        public string EntityType { get; set; }

        public int Id { get; set; }

        public int NewStatusId { get; set; }

        public int PreviousStatusId { get; set; }

        public string? RejectionReason { get; set; }

        #endregion
    }

}
