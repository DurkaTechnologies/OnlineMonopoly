namespace BLL.DTO
{
	public class BranchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public virtual BranchTypeDTO BranchType { get; set; }

        public string Image { get; set; }

        public int Pledge { get; set; }

        public int Buyout { get; set; }

        public int Price { get; set; }

        public int Upgrade { get; set; }

        public virtual RentSettingDTO RentSetting { get; set; }
    }
}
