using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IReportRepository
    {
        void Upsert(Report report);
        Report Get(int ReportId);
        List<Report> GetAll();

    }
    
}
