namespace FinalProject.Models.Entities;
public interface IReport
{
    int ReportNumber { get; }
    DateTime CreationDate { get; set; }
    string? Description { get; set; }
    string Create();
}
