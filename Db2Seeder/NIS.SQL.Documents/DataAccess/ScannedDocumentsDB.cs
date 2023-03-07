using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Db2Seeder.NIS.SQL.DocumentsTest;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.NIS.SQL.Documents.DataAccess
{
    public class ScannedDocumentsDB
    {
        private readonly scanned_documents_Context _context = new scanned_documents_Context();
        private readonly scanned_documents_testContext _contextTest = new scanned_documents_testContext();

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
        public async Task<int> InsertImportLog(Models_ScannedDocuments.ImportLog importLog)
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




        public async Task<bool> InsertDocumentforRegistrationTest(DocumentsTest.Documents document)
        {
            try
            {
                _contextTest.Documents.Add(document);
                await _contextTest.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> InsertImportLogTest(DocumentsTest.ImportLog importLog)
        {
            try
            {
                _contextTest.ImportLog.Add(importLog);
                await _contextTest.SaveChangesAsync();
                return _contextTest.ImportLog.ToList().Last().ImportId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
