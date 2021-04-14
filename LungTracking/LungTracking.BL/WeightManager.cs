using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class WeightManager
    {
        public static List<Weight> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Weight> weights = new List<Weight>();

                dc.tblWeights
                    .ToList()
                    .ForEach(u => weights.Add(new Weight
                    {
                        Id = u.Id,
                        WeightNumberInPounds = u.WeightNumberInPounds,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return weights;
            }
        }
        public static int Insert(int weightNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblWeight newWeight = new tblWeight
                    {
                        Id = Guid.NewGuid(),
                        WeightNumberInPounds = weightNumber,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblWeights.Add(newWeight);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Weight weight)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblWeight newWeight = new tblWeight
                    {
                        Id = Guid.NewGuid(),
                        WeightNumberInPounds = weight.WeightNumberInPounds,
                        TimeOfDay = weight.TimeOfDay,
                        PatientId = weight.PatientId
                    };
                    dc.tblWeights.Add(newWeight);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, int weightNumber, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblWeight updaterow = (from dt in dc.tblWeights where dt.Id == id select dt).FirstOrDefault();
                    updaterow.WeightNumberInPounds = weightNumber;
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

        public static int Update(Weight weight)
        {
            return Update(weight.Id, weight.WeightNumberInPounds, weight.TimeOfDay, weight.PatientId);
        }

        public static List<Weight> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<Weight> weights = new List<Weight>();

                    var results = (from weight in dc.tblWeights
                                   where weight.PatientId == patientId
                                   select new
                                   {
                                       weight.Id,
                                       weight.WeightNumberInPounds,
                                       weight.TimeOfDay,
                                       weight.PatientId
                                   }).ToList();

                    results.ForEach(r => weights.Add(new Weight
                    {
                        Id = r.Id,
                        WeightNumberInPounds = r.WeightNumberInPounds,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return weights;
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
                    tblWeight deleterow = (from dt in dc.tblWeights where dt.Id == id select dt).FirstOrDefault();
                    dc.tblWeights.Remove(deleterow);
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
