namespace JiJiBotApp_Backend.DTOs.Model.Base
{
    public class BaseCLS
    {
   
        public int CreatedBy { get; set; }  // User who created record

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;  // Created date

        public bool IsActive { get; set; } = true;  // Active flag

        public int UpdatedBy { get; set; }  // Last updated by user

        public DateTime UpdatedDate { get; set; }  // Last updated date
    }
}