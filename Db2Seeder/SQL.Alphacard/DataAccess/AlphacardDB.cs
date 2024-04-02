using Db2Seeder.NIS.SQL.Webportal;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.SQL.Alphacard.DataAccess
{
    public class AlphacardDB
    {
        private readonly AlphacardContext _context = new AlphacardContext();
        public async Task InsertNixportAsync(NiXport nixport)
        {
            try
            {
                _context.NiXport.Add(nixport);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertXportrecord(Document_Employee Document_Employee, string nisNum)
        {
            try
            {
                NiXport xport = new NiXport();

                while (nisNum.Length < 9)
                {
                    nisNum = "0" + nisNum;
                }
                xport.Nisnum = nisNum;
                xport.Fname = Document_Employee.firstName;
                xport.Lname = Document_Employee.lastName;
                xport.Dob = Document_Employee.dateOfBirth.Value.ToString("dd/MM/yyyy");
                xport.Natl = Document_Employee.nationality;
                xport.Plob = Document_Employee.birthPlace;
                xport.Cardprint = 0;
                xport.Lastmod = Document_Employee.CompletedBy;
                xport.Photo = string.Empty;
                xport.Signature = string.Empty;
                xport.PicUpdated = false;
                xport.SigUpdated = false;

                await InsertNixportAsync(xport);
                return true;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
