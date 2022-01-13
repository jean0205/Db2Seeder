using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.SQL.Logs.DataAccess
{
    public  class LogsDB
    {
        private readonly DB2SeederDBContext _context = new DB2SeederDBContext();

        public async Task InsertErrorLog( Log log)
        {
            try
            {
                _context.Log.Add(log);
                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task InsertComplianceCertificateLog(ComplianceCertRequestLog log)
        {
            try
            {
                _context.ComplianceCertRequestLog.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task InsertEmployeeRequestLog(EmployeeRequestLog log)
        {
            try
            {
                _context.EmployeeRequestLog.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task InsertEmployerRequestLog(EmployerRequestLog log)
        {
            try
            {
                _context.EmployerRequestLog.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task InsertRemittanceLog(RemittanceLog log)
        {
            try
            {
                _context.RemittanceLog.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Log>>GetErrorLogsListAsync()
        {           
            return await _context.Log.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeRequestLog>> GetEmployeeLogsListAsync()
        {
            return await _context.EmployeeRequestLog.ToListAsync();
        }
        public async Task<IEnumerable<EmployerRequestLog>> GetEmployerLogsListAsync()
        {
            return await _context.EmployerRequestLog.ToListAsync();
        }
        public async Task<IEnumerable<ComplianceCertRequestLog>> GetComplianceLogsListAsync()
        {
            return await _context.ComplianceCertRequestLog.ToListAsync();
        }
        public async Task<IEnumerable<RemittanceLog>> GetRemittanceLogsListAsync()
        {
            return await _context.RemittanceLog.ToListAsync();
        }
    }
}
