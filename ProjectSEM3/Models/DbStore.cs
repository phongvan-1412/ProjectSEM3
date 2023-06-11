namespace ProjectSEM3.Models
{
    public class DbStore
    {
        public static string InsertType = "Sp_InsertType";
        public static string UpdateType = "Sp_UpdateType";
        public static string GetTypeById = "Sp_GetTypeById";
        public static string GetTypes = "Sp_GetTypes";
        public static string GetAllTypes = "Sp_GetAllTypes";

        public static string InsertLevel = "Sp_InsertLevel";
        public static string UpdateLevel = "Sp_UpdateLevel";
        public static string GetLevelById = "Sp_GetLevelById";
        public static string GetLevels = "Sp_GetLevels";
        public static string GetAllLevels = "Sp_GetAllLevels";

        public static string InsertContestant = "Sp_InsertContestant";
        public static string UpdateContestant = "Sp_UpdateContestant";
        public static string GetContestantById = "Sp_GetContestantById";
        public static string GetContestantByEmail = "Sp_GetContestantByEmail";
        public static string GetContestantByEmailPass = "Sp_GetContestantByEmailPass";
        public static string GetContestants = "Sp_GetContestants";

        public static string InsertHr = "Sp_InsertHr";
        public static string UpdateHr = "Sp_UpdateHr";
        public static string ChangeHrStatus = "Sp_ChangeHrStatus";
        public static string GetHrById = "Sp_GetHrById";
        public static string GetHrByEmailPass = "Sp_GetHrByEmailPass";
        public static string GetHrByEmail = "Sp_GetHrByEmail";
        public static string GetHrs = "Sp_GetHrs";

        public static string InsertQuestion = "Sp_InsertQuestion";
        public static string UpdateQuestion = "Sp_UpdateQuestion";
        public static string ChangeQuestionStatus = "Sp_ChangeQuestionStatus";
        public static string GetQuestionById = "Sp_GetQuestionById";
        public static string GetQuestionsByLevel = "Sp_GetQuestionsByLevel";
        public static string GetQuestions = "Sp_GetQuestions";

        public static string GetAllQuestionTypes = "Sp_GetAllQuestionTypes";

        public static string InsertExam = "Sp_InsertExam";
        public static string UpdateExam = "Sp_UpdateExam";
        public static string ChangeExamStatus = "Sp_ChangeExamStatus";
        public static string GetExamnById = "Sp_GetExamById";
        public static string GetExamns = "Sp_GetExams";

        public static string InsertCV = "Sp_InsertCV";
        public static string GetCVs = "Sp_GetCVs";
        public static string ChangeCvStatus = "Sp_ChangeCvStatus";


        public static string IsEmailIsExsists = "Sp_IsEmailIsExsists";
    }
}