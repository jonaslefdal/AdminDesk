using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminDesk.Repositories
{
    public class EfReportRepository : IReportRepository
    {
        private readonly DataContext _dataContext;

        public EfReportRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public Report Get(int ServiceOrderId)
        {
            return _dataContext.Report
            
            .FirstOrDefault(x => x.ServiceOrderId == ServiceOrderId);

        }

        public List<Report> GetAll()
        {
            return _dataContext.Report.ToList();

        }

        public void Upsert(Report report)
        {
            var existing = Get(report.ReportId);
            if(existing != null)
            {
                existing.ReportId = report.ReportId;
                _dataContext.SaveChanges();
                return;
            }
            report.ReportId = 0;
            _dataContext.Add(report);
            _dataContext.SaveChanges();
        }
    }
}
