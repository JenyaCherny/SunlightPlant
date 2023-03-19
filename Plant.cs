using System.ComponentModel.DataAnnotations;
public class Plant
{
    public int Id { get; set; }
    public string? PlantName { get; set; }
    public string? PlantDescription { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateDelivered { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}