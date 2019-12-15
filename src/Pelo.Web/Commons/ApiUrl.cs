﻿namespace Pelo.Web.Commons
{
    public class ApiUrl
    {
        public const string BASE_API_URL = "http://localhost:49577";

        #region Account

        public const string LOG_ON = BASE_API_URL + "/api/account/logon";

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

        #region User

        public const string USER_GET_BY_PAGING = BASE_API_URL + "/api/user?code={0}&full_name={1}&phone_number={2}&branch_id={3}&department_id={4}&role_id={5}&status={6}&page={7}&page_size={8}&column_order={9}&sort_dir={10}";

        public const string USER_DELETE = BASE_API_URL + "/api/user/{0}";

        public const string USER_INSERT = BASE_API_URL + "/api/user";

        public const string USER_GET_BY_ID = BASE_API_URL + "/api/user/{0}";

        public const string USER_UPDATE = BASE_API_URL + "/api/user";

        #endregion

        #region AppConfig

        public const string APP_CONFIG_GET_BY_PAGING = BASE_API_URL + "/api/app_config?name={0}&description={1}&page={2}&page_size={3}&column_order={4}&sort_dir={5}";

        public const string APP_CONFIG_DELETE = BASE_API_URL + "/api/app_config/{0}";

        public const string APP_CONFIG_INSERT = BASE_API_URL + "/api/app_config";

        public const string APP_CONFIG_GET_BY_ID = BASE_API_URL + "/api/app_config/{0}";

        public const string APP_CONFIG_UPDATE = BASE_API_URL + "/api/app_config";

        #endregion
    }
}