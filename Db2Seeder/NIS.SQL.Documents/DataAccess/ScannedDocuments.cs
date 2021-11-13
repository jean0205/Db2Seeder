using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using System;
using System.Threading.Tasks;

namespace Db2Seeder.NIS.SQL.Documents.DataAccess
{
    public class ScannedDocuments
    {
        private readonly scanned_documents_testContext _context;
        public ScannedDocuments(scanned_documents_testContext context)
        {
            _context = context;
        }
        public async Task<bool> InsertDocumentforEmployeeRegistration(Models_ScannedDocuments.Documents document)
        {
            try
            {
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
