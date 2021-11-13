﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class Document_Employer
    {
        public int employerRegistrationFormId { get; set; }
        public int firmBusinessCompany { get; set; }
        public string firmName { get; set; }
        public string employerName { get; set; }
        public string businessAddress { get; set; }
        public string businessTown { get; set; }
        public string businessParish { get; set; }
        public string mailingAddress { get; set; }
        public string mobile { get; set; }
        public string secondMobile { get; set; }
        public string businessPhone { get; set; }
        public string fax { get; set; }
        public string emailAddress { get; set; }
        public string businessNature { get; set; }
        public DateTime businessCommencedDate { get; set; }
        public string bank { get; set; }
        public string accountNo { get; set; }
        public int accountType { get; set; }
        public int maleEmployee { get; set; }
        public int femaleEmployee { get; set; }
        public int totalEmployee { get; set; }
        public string employeeInfoForms { get; set; }
        public int documentId { get; set; }
        public Guid documentGuid { get; set; }
        public int documentTypeId { get; set; }
        public string documentCode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public object checkInById { get; set; }
        public DateTime checkInAtTime { get; set; }
        public string checkInMessage { get; set; }
        public object checkOutById { get; set; }
        public object checkOutTime { get; set; }
        public object checkOutMessage { get; set; }
        public object checkOutExpiration { get; set; }
        public int version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime? updatedOn { get; set; }
    }
}
