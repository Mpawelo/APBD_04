namespace APBD_04.Models;

public class VisitDTO
{
    public DateTime VisitDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
