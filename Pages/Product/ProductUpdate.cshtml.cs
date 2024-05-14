using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KTLT_23880008.Pages.Product
{
    public class ProductUpdateModel : PageModel
    {
        //[BindProperty]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public Nullable<System.DateOnly> ManufacturedDate { get; set; }

        public void OnGet()
        {
            Console.WriteLine("onGet");
        }

        public void OnPost() {
            Console.WriteLine("onPost");
        }

        public void FindProductById()
        {
            Console.WriteLine("FindProductArrayById");
        }
    }
}
