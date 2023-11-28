namespace sahand.Models.Models
{
    public class Vm_Company
    {
        public int Vm_Id { get; set; }
        public string Vm_image { get; set; }
        public string  Vm_Title { get; set; }
        public IFormFile Vm_img { get; set; }
        public bool Vm_status { get; set; }
    }
}