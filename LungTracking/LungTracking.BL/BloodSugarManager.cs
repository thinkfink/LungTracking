using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class BloodSugarManager
    {
        public static List<BloodSugar> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<BloodSugar> bloodSugar = new List<BloodSugar>();

                dc.tblBloodSugars
                    .ToList()
                    .ForEach(u => bloodSugar.Add(new BloodSugar
                    {
                        Id = u.Id,
                        BloodSugarNumber = u.BloodSugarNumber,
                        TimeOfDay = u.TimeOfDay,
                        UnitsOfInsulinGiven = u.UnitsOfInsulinGiven,
                        TypeOfInsulinGiven = u.TypeOfInsulinGiven,
                        Notes = u.Notes,
                        PatientId = u.PatientId
                    }));
                return bloodSugar;
            }
        }
        public static int Insert(int bloodSugarNumber, DateTime timeOfDay, int unitsOfInsulin, string typeOfInsulin, string notes, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodSugar newBloodSugar = new tblBloodSugar
                    {
                        Id = Guid.NewGuid(),
                        BloodSugarNumber = bloodSugarNumber,
                        TimeOfDay = timeOfDay,
                        UnitsOfInsulinGiven = unitsOfInsulin,
                        TypeOfInsulinGiven = typeOfInsulin,
                        Notes = notes,
                        PatientId = patientId
                    };
                    dc.tblBloodSugars.Add(newBloodSugar);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(BloodSugar bloodSugar)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodSugar newBloodSugar = new tblBloodSugar
                    {
                        Id = Guid.NewGuid(),
                        BloodSugarNumber = bloodSugar.BloodSugarNumber,
                        TimeOfDay = bloodSugar.TimeOfDay,
                        UnitsOfInsulinGiven = bloodSugar.UnitsOfInsulinGiven,
                        TypeOfInsulinGiven = bloodSugar.TypeOfInsulinGiven,
                        Notes = bloodSugar.Notes,
                        PatientId = bloodSugar.PatientId
                    };
                    dc.tblBloodSugars.Add(newBloodSugar);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, int bloodSugarNumber, DateTime timeOfDay, int unitsOfInsulin, string typeOfInsulin, string notes, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodSugar updaterow = (from dt in dc.tblBloodSugars where dt.Id == id select dt).FirstOrDefault();
                    updaterow.BloodSugarNumber = bloodSugarNumber;
                    updaterow.TimeOfDay = timeOfDay;
                    updaterow.UnitsOfInsulinGiven = unitsOfInsulin;
                    updaterow.TypeOfInsulinGiven = typeOfInsulin;
                    updaterow.Notes = notes;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(BloodSugar bloodSugar)
        {
            return Update(bloodSugar.Id, bloodSugar.BloodSugarNumber, bloodSugar.TimeOfDay, bloodSugar.UnitsOfInsulinGiven, bloodSugar.TypeOfInsulinGiven, bloodSugar.Notes, bloodSugar.PatientId);
        }

        public static List<BloodSugar> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<BloodSugar> bloodSugar = new List<BloodSugar>();

                    var results = (from bs in dc.tblBloodSugars
                                   where bs.PatientId == patientId
                                   select new
                                   {
                                       bs.Id,
                                       bs.BloodSugarNumber,
                                       bs.TimeOfDay,
                                       bs.UnitsOfInsulinGiven,
                                       bs.TypeOfInsulinGiven,
                                       bs.Notes,
                                       bs.PatientId
                                   }).ToList();

                    results.ForEach(r => bloodSugar.Add(new BloodSugar
                    {
                        Id = r.Id,
                        BloodSugarNumber = r.BloodSugarNumber,
                        TimeOfDay = r.TimeOfDay,
                        UnitsOfInsulinGiven = r.UnitsOfInsulinGiven,
                        TypeOfInsulinGiven = r.TypeOfInsulinGiven,
                        Notes = r.Notes,
                        PatientId = r.PatientId
                    }));

                    return bloodSugar;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodSugar deleterow = (from dt in dc.tblBloodSugars where dt.Id == id select dt).FirstOrDefault();
                    dc.tblBloodSugars.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
