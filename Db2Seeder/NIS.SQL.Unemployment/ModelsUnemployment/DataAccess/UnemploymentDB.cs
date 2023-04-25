using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> InsertRequestClaimMapping(long requestId, long claimNumber)
        {
            try
            {
                var newMapping = new RequestClaimMapping
                {
                    RequestId = requestId,
                    ClaimNumber = claimNumber
                };

                _contextUnemployment.RequestClaimMapping.Add(newMapping);
                await _contextUnemployment.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RequestClaimMapping> GetClaimRequestMappingByRequestId(long requestId)
        {
            try
            {
                return await _contextUnemployment.RequestClaimMapping
                                .FirstOrDefaultAsync(x => x.RequestId == requestId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //update linked claim, linked by and linkedtime
        public async Task<bool> UpdateTerminationCertificateLinkedClaim(long SupportrequestId, long ClaimSupportRequestId, long claimNumber, string user)
        {
            try
            {
                var result = await _contextUnemployment.TerminationCertificate
                    .FirstOrDefaultAsync(x => x.SupportrequestId == SupportrequestId);
                if (result != null)
                {
                    result.ClaimNumber = claimNumber;
                    result.ClaimSupportRequestId= ClaimSupportRequestId;
                    result.LinkedBy = user;
                    result.LinkedTime = DateTime.Now;
                    _contextUnemployment.Entry(result).State = EntityState.Modified;
                    await _contextUnemployment.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
