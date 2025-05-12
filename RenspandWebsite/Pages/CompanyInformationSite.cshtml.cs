using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace RenspandWebsite.Pages
{
    public class CompanyInformationSiteModel : PageModel
    {
        public string CompanyName { get; set; } 
        public string MobileNumber { get; set; }
        public string OfficeNumber { get; set; } 
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; } 
        public string ImageUrl { get; set; } 
        public string Description { get; set; }


        public void OnGet()
        {
            CompanyName = "Renspand";
            MobileNumber = "+45 31626924";
            OfficeNumber = "+45 81815758";
            CompanyEmail = "anderse@renspand.dk";
            CompanyAddress = "Renspand , Køge, Danmark";
            ImageUrl = "/Assets/RenSpandLogo.png";
            Description = "Renspand er et firma der kører rundt i køge og renser skraldespande hjemme hos folk";
        }
    }
}
