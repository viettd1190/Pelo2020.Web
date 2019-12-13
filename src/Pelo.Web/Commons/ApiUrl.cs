namespace Pelo.Web.Commons
{
    public class ApiUrl
    {
        public const string BASE_API_URL = "http://localhost:49577";

        #region Account

        public const string LOG_ON = BASE_API_URL + "/api/account/logon";

        #endregion

        #region User

        public const string USER_GET_BY_PAGING = BASE_API_URL + "/api/user?code={0}&full_name={1}&phone_number={2}&branch_id={3}&department_id={4}&role_id={5}&status={6}&page={7}&page_size={8}&column_order={9}&sort_dir={10}";

        public const string USER_DELETE = BASE_API_URL + "/api/user/{0}";

        #endregion

        #region Role

        public const string ROLE_GET_ALL = BASE_API_URL + "/api/role/all";

        #endregion

        #region Branch

        public const string BRANCH_GET_ALL = BASE_API_URL + "/api/branch/all";

        #endregion

        #region Department

        public const string DEPARTMENT_GET_ALL = BASE_API_URL + "/api/department/all";

        #endregion
    }
}
