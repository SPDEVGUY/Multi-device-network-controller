using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TETCSharpClient.Data;

namespace NetworkController.Plugin.EyeTribe
{
    public enum CalibrationRatingEnum
    {
        Perfect,
        Good,
        Moderate,
        Poor,
        Redo
    }

    public static class CalibrationResultExtensions
    {
        public const double PerfectRange = 0.5;
        public const double GoodRange = 0.7;
        public const double ModerateRange = 1;
        public const double PoorRange = 1.5;

        public static CalibrationRatingEnum GetRating(this CalibrationResult result)
        {
            if (result == null) return CalibrationRatingEnum.Redo;
        
            var accuracy = result.AverageErrorDegree;
            if (accuracy < PerfectRange) return CalibrationRatingEnum.Perfect;
            if (accuracy < GoodRange) return CalibrationRatingEnum.Good;
            if (accuracy < ModerateRange) return CalibrationRatingEnum.Moderate;
            if (accuracy < PoorRange) return CalibrationRatingEnum.Poor;
            return CalibrationRatingEnum.Redo;
            
        }
    }
}
