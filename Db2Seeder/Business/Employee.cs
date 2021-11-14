﻿using As400DataAccess;
using Db2Seeder.API.Helpers;
using Db2Seeder.API.Models;
using Db2Seeder.API.Request;
using Db2Seeder.NIS.SQL.Documents.DataAccess;
using Db2Seeder.NIS.SQL.Documents.Models_ScannedDocuments;
using Microsoft.AppCenter.Crashes;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.Business
{
    public class Employee
    {
        //AS400DATACCESS
        EmployeeDB2 as400Empe;

        List<SupportRequest> RequestList;
        Document_Employee Document_Employee;

        //SupportRequest/GetByState/Type/3/State/8
        public async Task GetEmployeesCompleted()
        {
            try
            {               
                RequestList = new List<SupportRequest>();
                RequestList = await ApiRequest.GetSupportRequestTypeByState(3, 8);
                if (RequestList.Any())
                {
                    foreach (var request in RequestList)
                    {
                        DocumentGuid guid = new DocumentGuid();
                        guid = await ApiRequest.GetRequestGUID(request.supportRequestId);

                        if (guid.message != Guid.Empty)
                        {
                            Document_Employee = new Document_Employee();
                            Document_Employee = await GetRequestDetailsEmployee(guid);
                            //TODO
                            //pasar el objeto a as400 para que se registre el employee y recibir el NIS
                            as400Empe = new EmployeeDB2();
                            Document_Employee.dateOfBirth = DateTime.Today;
                            Document_Employee.dateOfMarriage = DateTime.Today;
                            Document_Employee.middleName = String.Empty;

                            Document_Employee.nisNo = await as400Empe.InsertEmployees(Document_Employee);
                            if (Document_Employee.nisNo == 0)
                            {
                                return;
                            }
                            //el nis recibido despues de insertar


                            //TODO
                            //si se registra satisfactoriamente(recibo un nis de palacio), leer la lista de attachement para insertarla en sql server

                            List<DocumentGuid> attachmentsGuid = await ApiRequest.GetAttachmentsGuid(request.supportRequestId);
                            if (attachmentsGuid.Any())
                            {
                                List<Document_MetaData> document_MetaDataList = new List<Document_MetaData>();
                                document_MetaDataList = await ApiRequest.GetDocument_MetaData(attachmentsGuid);
                                if (document_MetaDataList.Any())
                                {
                                    //crear elimport log 
                                    //TODO
                                    //ahi que agregar  usuario necesito cogerlo del webportal
                                    ImportLog importLog = new ImportLog();
                                    importLog.ImportedBy = "Webportal";
                                    importLog.ImportDatetime = DateTime.Now;
                                    ScannedDocumentsDB scannedDocumentsDB = new ScannedDocumentsDB();
                                    int importId = 0;
                                    importId = await scannedDocumentsDB.InsertImportLog(importLog);
                                    //buscar el pdf Data
                                    foreach (var item in document_MetaDataList)
                                    {
                                        //TODO                                       
                                        //crear el documento que voy a insertar
                                        Documents documents = new Documents();
                                        documents.ActiveCode = "A";
                                        documents.RegistrantTypeId = Document_Employee.registrationType == 1 ? 1 :3;
                                        documents.DocTypeId = "skc";
                                        documents.ImportId = importId;
                                        documents.NisNumber = (int)Document_Employee.nisNo;
                                        documents.PdfData = await ApiRequest.GetDocument_Data(item.documentImageGuid);
                                        documents.ScannedBy = "Webportal";
                                        documents.ScanDatetime = DateTime.Now;
                                        documents.ModifiedDatetime = DateTime.Now;
                                        await scannedDocumentsDB.InsertDocumentforRegistration(documents);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Document/Get?id=b5a1b323-3adf-4917-b77a-c6fda5f1be5f
        public static async Task<Document_Employee> GetRequestDetailsEmployee(DocumentGuid guid)
        {
            try
            {
                Response response = await ApiServices.FindAsyncByGuid<Document_Employee>("Document/Get?id", guid.message);

                if (!response.IsSuccess)
                {

                    return null;
                }
                return (Document_Employee)response.Result;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return null;
        }
    }
}