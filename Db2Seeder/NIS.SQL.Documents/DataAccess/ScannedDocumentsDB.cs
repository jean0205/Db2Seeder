using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.NIS.SQL.Documents.DataAccess
{
    public class ScannedDocumentsDB
    {
        private readonly scanned_documents_testContext _context = new scanned_documents_testContext();
       
        public async Task<bool> InsertDocumentforRegistration(Models_ScannedDocuments.Documents document)
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
        public async Task<int> InsertImportLog(ImportLog importLog)
        {
            try
            {
                _context.ImportLog.Add(importLog);
                await _context.SaveChangesAsync();
                return _context.ImportLog.ToList().Last().ImportId;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
