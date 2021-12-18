using System.Collections.Generic;

namespace OnlineStore.WebUI.Models.ViewModels;

public class CartViewModel
{
    
    public IEnumerable<Product> Products { get; set; }
    
    public IEnumerable<Category> Categories { get; set; }
    
    public IEnumerable<FileModel> Files { get; set; }

    public double AllPrice { get; set; }
}