﻿using Newtonsoft.Json;
using ShareModels.Helpers;
using System;
using System.Collections.Generic;

namespace ShareModels.Models.Benefit_Claims
{
    public class Document_Disablemet
    {
        [JsonProperty("disablementBenefitFormId")]
        public int DisablementBenefitFormId { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }
        
        [JsonProperty("nisNo")]
        public int NisNo { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("maidenName")]
        public string MaidenName { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("maritalStatus")]
        public int? MaritalStatus { get; set; }

        [JsonProperty("tAddress")]
        public string TAddress { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("dateAccident")]
        public DateTime? DateAccident { get; set; }

        [JsonProperty("whatWayDisabled")]
        public string WhatWayDisabled { get; set; }

        [JsonProperty("fitTravel")]
        public int? FitTravel { get; set; }

        [JsonProperty("attendedHospital")]
        public int? AttendedHospital { get; set; }

        [JsonProperty("hospitalList")]
        public string HospitalList { get; set; }

        [JsonProperty("xrayTaken")]
        public int? XrayTaken { get; set; }

        [JsonProperty("otherRemarks")]
        public string OtherRemarks { get; set; }

        [JsonProperty("medicalDesc")]
        public string MedicalDesc { get; set; }

        [JsonProperty("medicalPracticeId")]
        public string MedicalPracticeId { get; set; }

        [JsonProperty("doctorId")]
        public object DoctorId { get; set; }
        public string medicalPractitionerName { get; set; }
        public string medicalRegistrationNo { get; set; }

        public string bank { get; set; }
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public int? accountType { get; set; }
        public string lateReason { get; set; }
        public string percentage { get; set; }
        public int? mentalPhysical { get; set; }
        public int? injuryOccupational { get; set; }
        public int? facultyLoss { get; set; }
        public int? provisionalFinal { get; set; }

        [JsonProperty("hospitalListEntity")]
        public List<HospitalListEntity> HospitalListEntity { get; set; }

        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        [JsonProperty("documentTypeId")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("documentCode")]
        public string DocumentCode { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("createdBy")]
        public int? CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public int? UpdatedBy { get; set; }

        [JsonProperty("checkInById")]
        public int? CheckInById { get; set; }

        [JsonProperty("checkInAtTime")]
        public DateTime? CheckInAtTime { get; set; }

        [JsonProperty("checkInMessage")]
        public object CheckInMessage { get; set; }

        [JsonProperty("checkOutById")]
        public int? CheckOutById { get; set; }

        [JsonProperty("checkOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonProperty("checkOutMessage")]
        public string CheckOutMessage { get; set; }

        [JsonProperty("checkOutExpiration")]
        public DateTime? CheckOutExpiration { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime? UpdatedOn { get; set; }

        public string CompletedBy { get; set; }
        public DateTime? CompletedTime { get; set; }
        public int SupportRequestId { get; set; }
        public string WebPortalLink => Settings.GetPortalUrl() + SupportRequestId;
        public int? ClaimNumber { get; set; }

    }
    public class HospitalListEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("patienttype")]
        public string Patienttype { get; set; }

        [JsonProperty("admissionNo")]
        public string AdmissionNo { get; set; }

        [JsonProperty("periodTreatment")]
        public string PeriodTreatment { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
