using System.Collections.Generic;

namespace I18N
{
    public class Language : Singleton<Language>
    {
        private string _code = "en";

        public void SetCode(string value)
        {
            _code = value;
        }

        public Dictionary<string, string> GetLanguage()
        {
            if (_code.Equals("en"))
            {
                return en;
            }
            
            if (_code.Equals("zh"))
            {
                return zh;
            }
            
            return en;
        }
        
        public Dictionary<string, string> kLanguageCodes = new Dictionary<string, string>
        {
            {"af_NA", ""},
            {"af_ZA", ""},
            {"af", ""},
            {"ak_GH", ""},
            {"ak", ""},
            {"sq_AL", ""},
            {"sq", ""},
            {"am_ET", ""},
            {"am", ""},
            {"ar_DZ", ""},
            {"ar_BH", ""},
            {"ar_EG", ""},
            {"ar_IQ", ""},
            {"ar_JO", ""},
            {"ar_KW", ""},
            {"ar_LB", ""},
            {"ar_LY", ""},
            {"ar_MA", ""},
            {"ar_OM", ""},
            {"ar_QA", ""},
            {"ar_SA", ""},
            {"ar_SD", ""},
            {"ar_SY", ""},
            {"ar_TN", ""},
            {"ar_AE", ""},
            {"ar_YE", ""},
            {"ar", ""},
            {"hy_AM", ""},
            {"hy", ""},
            {"as_IN", ""},
            {"as", ""},
            {"asa_TZ", ""},
            {"asa", ""},
            {"az_Cyrl", ""},
            {"az_Cyrl_AZ", ""},
            {"az_Latn", ""},
            {"az_Latn_AZ", ""},
            {"az", ""},
            {"bm_ML", ""},
            {"bm", ""},
            {"eu_ES", ""},
            {"eu", ""},
            {"be_BY", ""},
            {"be", ""},
            {"bem_ZM", ""},
            {"bem", ""},
            {"bez_TZ", ""},
            {"bez", ""},
            {"bn_BD", ""},
            {"bn_IN", ""},
            {"bn", ""},
            {"bs_BA", ""},
            {"bs", ""},
            {"bg_BG", ""},
            {"bg", ""},
            {"my_MM", ""},
            {"my", ""},
            {"ca_ES", ""},
            {"ca", ""},
            {"tzm_Latn", ""},
            {"tzm_Latn_MA", ""},
            {"tzm", ""},
            {"chr_US", ""},
            {"chr", ""},
            {"cgg_UG", ""},
            {"cgg", ""},
            {"zh_Hans", "zh_CN"},
            {"zh_Hans_CN", "zh_CN"},
            {"zh_Hans_HK", "zh_CN"},
            {"zh_Hans_MO", "zh_CN"},
            {"zh_Hans_SG", "zh_CN"},
            {"zh_Hant", "zh_CN"},
            {"zh_Hant_HK", "zh_CN"},
            {"zh_Hant_MO", "zh_CN"},
            {"zh_Hant_TW", "zh_CN"},
            {"zh_CN", "zh_CN"},
            {"zh_HK", "zh_CN"},
            {"zh_TW", "zh_CN"},
            {"zh", "zh_CN"},
            {"kw_GB", ""},
            {"kw", ""},
            {"hr_HR", ""},
            {"hr", ""},
            {"cs_CZ", ""},
            {"cs", ""},
            {"da_DK", ""},
            {"da", ""},
            {"nl_BE", ""},
            {"nl_NL", ""},
            {"nl", ""},
            {"ebu_KE", ""},
            {"ebu", ""},
            {"en_AS", "en_US"},
            {"en_AU", "en_US"},
            {"en_BE", "en_US"},
            {"en_BZ", "en_US"},
            {"en_BW", "en_US"},
            {"en_CA", "en_US"},
            {"en_GU", "en_US"},
            {"en_HK", "en_US"},
            {"en_IN", "en_US"},
            {"en_IE", "en_US"},
            {"en_JM", "en_US"},
            {"en_MT", "en_US"},
            {"en_MH", "en_US"},
            {"en_MU", "en_US"},
            {"en_NA", "en_US"},
            {"en_NZ", "en_US"},
            {"en_MP", "en_US"},
            {"en_PK", "en_US"},
            {"en_PH", "en_US"},
            {"en_SG", "en_US"},
            {"en_ZA", "en_US"},
            {"en_TT", "en_US"},
            {"en_UM", "en_US"},
            {"en_VI", "en_US"},
            {"en_GB", "en_US"},
            {"en_US", "en_US"},
            {"en_ZW", "en_US"},
            {"en", "en_US"},
            {"eo", ""},
            {"et_EE", ""},
            {"et", ""},
            {"ee_GH", ""},
            {"ee_TG", ""},
            {"ee", ""},
            {"fo_FO", ""},
            {"fo", ""},
            {"fil_PH", ""},
            {"fil", ""},
            {"fi_FI", ""},
            {"fi", ""},
            {"fr_BE", ""},
            {"fr_BJ", ""},
            {"fr_BF", ""},
            {"fr_BI", ""},
            {"fr_CM", ""},
            {"fr_CA", ""},
            {"fr_CF", ""},
            {"fr_TD", ""},
            {"fr_KM", ""},
            {"fr_CG", ""},
            {"fr_CD", ""},
            {"fr_CI", ""},
            {"fr_DJ", ""},
            {"fr_GQ", ""},
            {"fr_FR", ""},
            {"fr_GA", ""},
            {"fr_GP", ""},
            {"fr_GN", ""},
            {"fr_LU", ""},
            {"fr_MG", ""},
            {"fr_ML", ""},
            {"fr_MQ", ""},
            {"fr_MC", ""},
            {"fr_NE", ""},
            {"fr_RW", ""},
            {"fr_RE", ""},
            {"fr_BL", ""},
            {"fr_MF", ""},
            {"fr_SN", ""},
            {"fr_CH", ""},
            {"fr_TG", ""},
            {"fr", ""},
            {"ff_SN", ""},
            {"ff", ""},
            {"gl_ES", ""},
            {"gl", ""},
            {"lg_UG", ""},
            {"lg", ""},
            {"ka_GE", ""},
            {"ka", ""},
            {"de_AT", ""},
            {"de_BE", ""},
            {"de_DE", ""},
            {"de_LI", ""},
            {"de_LU", ""},
            {"de_CH", ""},
            {"de", ""},
            {"el_CY", ""},
            {"el_GR", ""},
            {"el", ""},
            {"gu_IN", ""},
            {"gu", ""},
            {"guz_KE", ""},
            {"guz", ""},
            {"ha_Latn", ""},
            {"ha_Latn_GH", ""},
            {"ha_Latn_NE", ""},
            {"ha_Latn_NG", ""},
            {"ha", ""},
            {"haw_US", ""},
            {"haw", ""},
            {"he_IL", ""},
            {"he", ""},
            {"hi_IN", ""},
            {"hi", ""},
            {"hu_HU", ""},
            {"hu", ""},
            {"is_IS", ""},
            {"is", ""},
            {"ig_NG", ""},
            {"ig", ""},
            {"id_ID", ""},
            {"id", ""},
            {"ga_IE", ""},
            {"ga", ""},
            {"it_IT", ""},
            {"it_CH", ""},
            {"it", ""},
            {"ja_JP", ""},
            {"ja", ""},
            {"kea_CV", ""},
            {"kea", ""},
            {"kab_DZ", ""},
            {"kab", ""},
            {"kl_GL", ""},
            {"kl", ""},
            {"kln_KE", ""},
            {"kln", ""},
            {"kam_KE", ""},
            {"kam", ""},
            {"kn_IN", ""},
            {"kn", ""},
            {"kk_Cyrl", ""},
            {"kk_Cyrl_KZ", ""},
            {"kk", ""},
            {"km_KH", ""},
            {"km", ""},
            {"ki_KE", ""},
            {"ki", ""},
            {"rw_RW", ""},
            {"rw", ""},
            {"kok_IN", ""},
            {"kok", ""},
            {"ko_KR", ""},
            {"ko", ""},
            {"khq_ML", ""},
            {"khq", ""},
            {"ses_ML", ""},
            {"ses", ""},
            {"lag_TZ", ""},
            {"lag", ""},
            {"lv_LV", ""},
            {"lv", ""},
            {"lt_LT", ""},
            {"lt", ""},
            {"luo_KE", ""},
            {"luo", ""},
            {"luy_KE", ""},
            {"luy", ""},
            {"mk_MK", ""},
            {"mk", ""},
            {"jmc_TZ", ""},
            {"jmc", ""},
            {"kde_TZ", ""},
            {"kde", ""},
            {"mg_MG", ""},
            {"mg", ""},
            {"ms_BN", ""},
            {"ms_MY", ""},
            {"ms", ""},
            {"ml_IN", ""},
            {"ml", ""},
            {"mt_MT", ""},
            {"mt", ""},
            {"gv_GB", ""},
            {"gv", ""},
            {"mr_IN", ""},
            {"mr", ""},
            {"mas_KE", ""},
            {"mas_TZ", ""},
            {"mas", ""},
            {"mer_KE", ""},
            {"mer", ""},
            {"mfe_MU", ""},
            {"mfe", ""},
            {"naq_NA", ""},
            {"naq", ""},
            {"ne_IN", ""},
            {"ne_NP", ""},
            {"ne", ""},
            {"nd_ZW", ""},
            {"nd", ""},
            {"nb_NO", ""},
            {"nb", ""},
            {"nn_NO", ""},
            {"nn", ""},
            {"nyn_UG", ""},
            {"nyn", ""},
            {"or_IN", ""},
            {"or", ""},
            {"om_ET", ""},
            {"m_KE", ""},
            {"om", ""},
            {"ps_AF", ""},
            {"ps", ""},
            {"fa_AF", ""},
            {"fa_IR", ""},
            {"fa", ""},
            {"pl_PL", ""},
            {"pl", ""},
            {"pt_BR", ""},
            {"pt_GW", ""},
            {"pt_MZ", ""},
            {"pt_PT", ""},
            {"pt", ""},
            {"pa_Arab", ""},
            {"pa_Arab_PK", ""},
            {"pa_Guru", ""},
            {"pa_Guru_IN", ""},
            {"pa", ""},
            {"ro_MD", ""},
            {"ro_RO", ""},
            {"ro", ""},
            {"rm_CH", ""},
            {"rm", ""},
            {"rof_TZ", ""},
            {"rof", ""},
            {"ru_MD", ""},
            {"ru_RU", ""},
            {"ru_UA", ""},
            {"ru", ""},
            {"rwk_TZ", ""},
            {"rwk", ""},
            {"saq_KE", ""},
            {"saq", ""},
            {"sg_CF", ""},
            {"sg", ""},
            {"seh_MZ", ""},
            {"seh", ""},
            {"sr_Cyrl", ""},
            {"sr_Cyrl_BA", ""},
            {"sr_Cyrl_ME", ""},
            {"sr_Cyrl_RS", ""},
            {"sr_Latn", ""},
            {"sr_Latn_BA", ""},
            {"sr_Latn_ME", ""},
            {"sr_Latn_RS", ""},
            {"sr", ""},
            {"sn_ZW", ""},
            {"sn", ""},
            {"ii_CN", ""},
            {"ii", ""},
            {"si_LK", ""},
            {"si", ""},
            {"sk_SK", ""},
            {"sk", ""},
            {"sl_SI", ""},
            {"sl", ""},
            {"xog_UG", ""},
            {"xog", ""},
            {"so_DJ", ""},
            {"so_ET", ""},
            {"so_KE", ""},
            {"so_SO", ""},
            {"so", ""},
            {"es_AR", ""},
            {"es_BO", ""},
            {"es_CL", ""},
            {"es_CO", ""},
            {"es_CR", ""},
            {"es_DO", ""},
            {"es_EC", ""},
            {"es_SV", ""},
            {"es_GQ", ""},
            {"es_GT", ""},
            {"es_HN", ""},
            {"es_419", ""},
            {"es_MX", ""},
            {"es_NI", ""},
            {"es_PA", ""},
            {"es_PY", ""},
            {"es_PE", ""},
            {"es_PR", ""},
            {"es_ES", ""},
            {"es_US", ""},
            {"es_UY", ""},
            {"es_VE", ""},
            {"es", ""},
            {"sw_KE", ""},
            {"sw_TZ", ""},
            {"sw", ""},
            {"sv_FI", ""},
            {"sv_SE", ""},
            {"sv", ""},
            {"gsw_CH", ""},
            {"gsw", ""},
            {"shi_Latn", ""},
            {"shi_Latn_MA", ""},
            {"shi_Tfng", ""},
            {"shi_Tfng_MA", ""},
            {"shi", ""},
            {"dav_KE", ""},
            {"dav", ""},
            {"ta_IN", ""},
            {"ta_LK", ""},
            {"ta", ""},
            {"te_IN", ""},
            {"te", ""},
            {"teo_KE", ""},
            {"teo_UG", ""},
            {"teo", ""},
            {"th_TH", ""},
            {"th", ""},
            {"bo_CN", ""},
            {"bo_IN", ""},
            {"bo", ""},
            {"ti_ER", ""},
            {"ti_ET", ""},
            {"ti", ""},
            {"to_TO", ""},
            {"to", ""},
            {"tr_TR", ""},
            {"tr", ""},
            {"uk_UA", ""},
            {"uk", ""},
            {"ur_IN", ""},
            {"ur_PK", ""},
            {"ur", ""},
            {"uz_Arab", ""},
            {"uz_Arab_AF", ""},
            {"uz_Cyrl", ""},
            {"uz_Cyrl_UZ", ""},
            {"uz_Latn", ""},
            {"uz_Latn_UZ", ""},
            {"uz", ""},
            {"vi_VN", ""},
            {"vi", ""},
            {"vun_TZ", ""},
            {"vun", ""},
            {"cy_GB", ""},
            {"cy", ""},
            {"yo_NG", ""},
            {"yo", ""},
            {"zu_ZA", ""},
            {"zu", ""}
        };

        public Dictionary<string, string> en = new Dictionary<string, string>
        {
            {"KEY_PRIVACY", "Privacy"},
            {"KEY_USER_AGREEMENT", "User Agreement"},
            {"KEY_RESTART", "RESTART"},
        };
        
        public Dictionary<string, string> zh = new Dictionary<string, string>
        {
            {"KEY_PRIVACY", "Privacy"},
            {"KEY_USER_AGREEMENT", "User Agreement"},
            {"KEY_RESTART", "RESTART"},
        };
    }
}