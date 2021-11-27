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

        public async Task InsertLog( Log log)
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
    }
}
