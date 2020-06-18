using System.Configuration;

namespace GomiShop.Common.Configuration
{
    public class AppSettings
    {

        public static char Delimiter
        {
            get { return '|'; }
        }

        public static string SecretKey
        {
            get { return "JBSWY3DPEHPK3PXP"; }
        }

        public static string Resources
        {
            get { return "GomiShop.Image"; }
        }

        public static string ImageHosting
        {
            get { return ConfigurationManager.AppSettings["ImageHosting"]; }
        }

        public static string SellerUrl
        {
            get { return ConfigurationManager.AppSettings["SellerUrl"]; }
        }

        public static string DateFormat
        {
            get { return "dd/MM/yyyy"; }

        }
        public static string DateFormatLongTime
        {
            get { return "dd/MM/yyyy - HH:mm"; }

        }

        public static string CurrencyFormat
        {
            get { return "{0:n0}"; }
        }

        public static int ItemPerPage
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ItemPerPage"]); }
        }

        public static string SessionCKEditor
        {
            get { return "UserLogin"; }
        }

        #region =====-- Template Path
        public static string ForgotPasswordPath
        {
            get { return "Template/ForgotPassword.html"; }
        }

        public static string SendCodePath
        {
            get { return "Template/SendCode.html"; }
        }

        #endregion


        #region =====-- Resource Path

        public static string Common
        {
            get { return "Common/"; }
        }

        public static string BrandPath
        {
            get { return "Brand/"; }
        }

        public static string BannerPath
        {
            get { return "Banner/"; }
        }

        public static string RedeemPath
        {
            get { return "Redeem/"; }
        }
        public static string RedeemCoverPath
        {
            get { return "Redeem/Cover/"; }
        }
        public static string CategoryIconPath
        {
            get { return "Category/Icon/"; }
        }

        public static string CategoryBannerPath
        {
            get { return "Category/Banner/"; }
        }

        public static string CollectionsPath
        {
            get { return "Collections/"; }
        }

        public static string CountryFlagPath
        {
            get { return "CountryFlag/"; }
        }

        public static string ProductPath
        {
            get { return "Product/"; }
        }

        public static string ProductReviewPath
        {
            get { return "Product/Review/"; }
        }

        public static string ProductVideoReviewPath
        {
            get { return "Product/VideoReview/"; }
        }

        public static string ProductVideoReviewThumbnailPath
        {
            get { return "Product/VideoReview/Thumbnail/"; }
        }

        public static string BankingPath
        {
            get { return "Banking/"; }
        }

        public static string TempPath
        {
            get { return "Resources/Temp/"; }
        }


        #endregion


        #region =========-- CKFinder
        public static string ContentPath
        {
            get { return "Content/"; }

        }

        public static string ContentGomiPath
        {
            get { return "Content/Gomi/"; }

        }

        public static string ContentSEOPath
        {
            get { return "Content/SEO/"; }

        }
        #endregion


        #region ======-- Seller - Shop Path
        public static string SellerAvatarPath
        {
            get { return "Seller/Avatar/"; }
        }

        public static string ShopCoverPath
        {
            get { return "Seller/Shop/Cover/"; }
        }

        public static string ShopAvatarPath
        {
            get { return "Seller/Shop/Avatar/"; }
        }

        public static string ShopBlogPath
        {
            get { return "Seller/Shop/Blog/"; }
        }
        #endregion


        #region ======-- Email Config

        public static string FromEmailAddress
        {
            get { return ConfigurationManager.AppSettings["FromEmailAddress"]; }
        }

        public static string SMTPPassword
        {
            get { return ConfigurationManager.AppSettings["SMTPPassword"]; }
        }

        public static string SMTPUsername
        {
            get { return ConfigurationManager.AppSettings["SMTPUsername"]; }
        }

        public static string SMTPHost
        {
            get { return ConfigurationManager.AppSettings["SMTPHost"]; }
        }

        public static int SMTPPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SMTPPort"]); }
        }

        public static bool EnabledSSL
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"]); }
        }

        #endregion Email Config


        #region =======-- Facebook Key
        public static string FacebookAppId
        {
            get { return "542422132944042"; }
        }

        public static string FacebookAppSecret
        {
            get { return "6bb3f493c0202a100180708320092584"; }
        }

        #endregion

        #region =======-- Google Key
        public static string GoogleRecaptchaSecret
        {
            get { return "6LcxI3wUAAAAAOQDVlJf_H9eX89EMdiZCBrQ8Qwv"; }
        }

        public static string GoogleRecaptchaKeySite
        {
            get { return "6LcxI3wUAAAAAM1KZF2y_0LqLQS79t8_GHDtiQ0p"; }
        }

        public static string GoogleAPIID
        {
            get { return "132335799244-6dvachutdo9h2a8mbqt7sc9fia40uu5c.apps.googleusercontent.com"; }
        }

        public static string GoogleAPISecret
        {
            get { return "uPOK0Nwqg_ps2jD9vMlTDg1a"; }
        }

        #endregion

        #region =======-- Zalo Key
        public static string ZaloAppId
        {
            get { return "4220523664723612038"; }
        }

        public static string ZaloAppSecret
        {
            get { return "Gy23WE6TPaKXUW8QN22x"; }
        }
        #endregion


        #region ======-- GHTK Config
        public static string GHTK_BaseUrl
        {
            get { return ConfigurationManager.AppSettings["GHTK_BaseUrl"]; }

        }
        public static string GHTK_Token
        {
            get { return ConfigurationManager.AppSettings["GHTK_Token"]; }

        }

        public static string GHTK_Id
        {
            get { return ConfigurationManager.AppSettings["GHTK_Id"]; }

        }

        #endregion

        #region ======-- FPT Config
        public static string FPT_BrandName
        {
            get { return ConfigurationManager.AppSettings["FPT_BrandName"]; }

        }
        public static string FPT_BaseUrl
        {
            get { return ConfigurationManager.AppSettings["FPT_BaseUrl"]; }

        }
        public static string FPT_ClientId
        {
            get { return ConfigurationManager.AppSettings["FPT_ClientId"]; }

        }

        public static string FPT_SecretId
        {
            get { return ConfigurationManager.AppSettings["FPT_SecretId"]; }

        }

        #endregion

    }
}