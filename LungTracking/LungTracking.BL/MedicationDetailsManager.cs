using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class MedicationDetailsManager
    {
        public static List<MedicationDetails> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<MedicationDetails> medDetails = new List<MedicationDetails>();

                dc.tblMedicationDetails
                    .ToList()
                    .ForEach(u => medDetails.Add(new MedicationDetails
                    {
                        Id = u.Id,
                        MedicationName = u.MedicationName,
                        MedicationDosageTotal = u.MedicationDosageTotal,
                        MedicationDosagePerPill = u.MedicationDosagePerPill,
                        MedicationInstructions = u.MedicationInstructions,
                        NumberOfPills = u.NumberOfPills,
                        DateFilled = u.DateFilled,
                        QuantityOfFill = u.QuantityOfFill,
                        RefillDate = u.RefillDate,
                        PatientId = u.PatientId
                    }));
                return medDetails;
            }
        }
        public static int Insert(string medName, string medDosageTotal, string medDosagePerPill, string medInstructions, int numberOfPills, DateTime dateFilled, int quantityOfFill, DateTime refillDate, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationDetail newMedDetail = new tblMedicationDetail
                    {
                        Id = Guid.NewGuid(),
                        MedicationName = medName,
                        MedicationDosageTotal = medDosageTotal,
                        MedicationDosagePerPill = medDosagePerPill,
                        MedicationInstructions = medInstructions,
                        NumberOfPills = numberOfPills,
                        DateFilled = dateFilled,
                        QuantityOfFill = quantityOfFill,
                        RefillDate = refillDate,
                        PatientId = patientId
                    };
                    dc.tblMedicationDetails.Add(newMedDetail);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(MedicationDetails medDetail)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationDetail newMedDetail = new tblMedicationDetail
                    {
                        Id = Guid.NewGuid(),
                        MedicationName = medDetail.MedicationName,
                        MedicationDosageTotal = medDetail.MedicationDosageTotal,
                        MedicationDosagePerPill = medDetail.MedicationDosagePerPill,
                        MedicationInstructions = medDetail.MedicationInstructions,
                        NumberOfPills = medDetail.NumberOfPills,
                        DateFilled = medDetail.DateFilled,
                        QuantityOfFill = medDetail.QuantityOfFill,
                        RefillDate = medDetail.RefillDate,
                        PatientId = medDetail.PatientId
                    };
                    dc.tblMedicationDetails.Add(newMedDetail);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string medName, string medDosageTotal, string medDosagePerPill, string medInstructions, int numberOfPills, DateTime dateFilled, int quantityOfFill, DateTime refillDate, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationDetail updaterow = (from dt in dc.tblMedicationDetails where dt.Id == id select dt).FirstOrDefault();
                    updaterow.MedicationName = medName;
                    updaterow.MedicationDosageTotal = medDosageTotal;
                    updaterow.MedicationDosagePerPill = medDosagePerPill;
                    updaterow.MedicationInstructions = medInstructions;
                    updaterow.NumberOfPills = numberOfPills;
                    updaterow.DateFilled = dateFilled;
                    updaterow.QuantityOfFill = quantityOfFill;
                    updaterow.RefillDate = refillDate;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(MedicationDetails medDetail)
        {
            return Update(medDetail.Id, medDetail.MedicationName, medDetail.MedicationDosageTotal, medDetail.MedicationDosagePerPill, medDetail.MedicationInstructions, medDetail.NumberOfPills, medDetail.DateFilled, medDetail.QuantityOfFill, medDetail.RefillDate, medDetail.PatientId);
        }

        public static List<MedicationDetails> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<MedicationDetails> medDetails = new List<MedicationDetails>();

                    var results = (from mdt in dc.tblMedicationDetails where mdt.PatientId == patientId
                                   select new
                                   {
                                       mdt.Id,
                                       mdt.MedicationName,
                                       mdt.MedicationDosageTotal,
                                       mdt.MedicationDosagePerPill,
                                       mdt.MedicationInstructions,
                                       mdt.NumberOfPills,
                                       mdt.DateFilled,
                                       mdt.QuantityOfFill,
                                       mdt.RefillDate,
                                       mdt.PatientId
                                   }).ToList();

                    results.ForEach(r => medDetails.Add(new MedicationDetails
                    {
                        Id = r.Id,
                        MedicationName = r.MedicationName,
                        MedicationDosageTotal = r.MedicationDosageTotal,
                        MedicationDosagePerPill = r.MedicationDosagePerPill,
                        MedicationInstructions = r.MedicationInstructions,
                        NumberOfPills = r.NumberOfPills,
                        DateFilled = r.DateFilled,
                        QuantityOfFill = r.QuantityOfFill,
                        RefillDate = r.RefillDate,
                        PatientId = r.PatientId
                    }));

                    return medDetails;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationDetail deleterow = (from dt in dc.tblMedicationDetails where dt.Id == id select dt).FirstOrDefault();
                    dc.tblMedicationDetails.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
