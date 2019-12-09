namespace Pelo.Web.Commons
{
    public class ApiUrl
    {
        public const string BASE_API_URL = "http://localhost:49577";

        #region Account

        public const string LOG_ON = BASE_API_URL+ "/api/account/logon";

        #endregion

        #region User

        public const string USER_GET_BY_PAGING = BASE_API_URL +
                                                 "/api/user?username={0}&display_name={1}&full_name={2}&phone_number={3}&branch_id={4}&role_id={5}&page={6}&page_size={7}&column_order={8}&sort_dir={9}";

        #endregion
    }
}