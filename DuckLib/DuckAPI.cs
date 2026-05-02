using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace DuckLib
{
    public class DuckAPI
    {
        public static Medication GetMedication(int Id)
        {
            DuckLib.Models.Medication entity = dx.Medication; // fetch from database using Id
            return Medication.FromEntity(entity);
            //return new Medication();
        }
    }
}
