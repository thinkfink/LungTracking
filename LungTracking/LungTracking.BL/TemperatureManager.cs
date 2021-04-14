using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class TemperatureManager
    {
        public static List<Temperature> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Temperature> temps = new List<Temperature>();

                dc.tblTemperatures
                    .ToList()
                    .ForEach(u => temps.Add(new Temperature
                    {
                        Id = u.Id,
                        TempNumber = u.TempNumber,
                        BeginningEnd = u.BeginningEnd,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return temps;
            }
        }
        public static int Insert(int tempNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblTemperature newTemp = new tblTemperature
                    {
                        Id = Guid.NewGuid(),
                        TempNumber = tempNumber,
                        BeginningEnd = beginningEnd,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblTemperatures.Add(newTemp);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Temperature temp)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblTemperature newTemp = new tblTemperature
                    {
                        Id = Guid.NewGuid(),
                        TempNumber = temp.TempNumber,
                        BeginningEnd = temp.BeginningEnd,
                        TimeOfDay = temp.TimeOfDay,
                        PatientId = temp.PatientId
                    };
                    dc.tblTemperatures.Add(newTemp);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, decimal tempNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblTemperature updaterow = (from dt in dc.tblTemperatures where dt.Id == id select dt).FirstOrDefault();
                    updaterow.TempNumber = tempNumber;
                    updaterow.BeginningEnd = beginningEnd;
                    updaterow.TimeOfDay = timeOfDay;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Temperature temp)
        {
            return Update(temp.Id, temp.TempNumber, temp.BeginningEnd, temp.TimeOfDay, temp.PatientId); ;
        }

        public static List<Temperature> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<Temperature> temps = new List<Temperature>();

                    var results = (from temp in dc.tblTemperatures
                                   where temp.PatientId == patientId
                                   select new
                                   {
                                       temp.Id,
                                       temp.TempNumber,
                                       temp.BeginningEnd,
                                       temp.TimeOfDay,
                                       temp.PatientId
                                   }).ToList();

                    results.ForEach(r => temps.Add(new Temperature
                    {
                        Id = r.Id,
                        TempNumber = r.TempNumber,
                        BeginningEnd = r.BeginningEnd,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return temps;
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
                    tblTemperature deleterow = (from dt in dc.tblTemperatures where dt.Id == id select dt).FirstOrDefault();
                    dc.tblTemperatures.Remove(deleterow);
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
