using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.NIS.SQL.Unemployment.ModelsUnemployment.DataAccess
{
    public class UnemploymentDB
    {
        private readonly UnemploymentContext _contextUnemployment = new UnemploymentContext();
        public async Task<bool> InsertTerminationCertificate(TerminationCertificate document)
        {
            try
            {
                _contextUnemployment.TerminationCertificate.Add(document);
                await _contextUnemployment.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> InsertUnemploymentDeclaration(UnempDeclaration document)
        {
            try
            {
                _contextUnemployment.UnempDeclaration.Add(document);
                await _contextUnemployment.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
