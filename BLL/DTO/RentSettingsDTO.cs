namespace BLL.DTO
{
	public class RentSettingsDTO
    {
        public int Id { get; set; }
        public int StartCost { get; set; }
        public float FirstCoef { get; set; }
        public int SecondCoef { get; set; }
        public bool BybranchQuantity { get; set; }
    }
}
