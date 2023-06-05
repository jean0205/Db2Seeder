using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db2Seeder.NIS.SQL.Webportal.DataAccess
{
    public class WebPortalSQLDB
    {
        private readonly WebPortalContext _contextWebPortal = new WebPortalContext();

        //is selfEmployed Person, when [SECT02] in NI.EMPR  =3
        public async Task<bool> IsSelfEmployedPerson(string niNumber)
        {
            try
            {
                var selfEmployedPerson = await _contextWebPortal.NiEmpr.FirstOrDefaultAsync(x => x.Rreg02.ToString() == niNumber && x.Rrsf02==0 && x.Sect02 == 3 && x.Tcen02==0 && x.Tdat02==0 );

                if (selfEmployedPerson != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //return a list with all SelfEmployed Persons
        public async Task<List<NiEmpr>> GetAllSelfEmployedPersons()
        {
            try
            {
                var selfEmployedPersons = await _contextWebPortal.NiEmpr.Where(x => x.Sect02 == 3 && x.Tcen02 == 0 && x.Tdat02 == 0).ToListAsync();
                return selfEmployedPersons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
