using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Common.Configuration
{
    #region Base

    public enum LanguageType : byte
    {
        Vietnamese = 0,
        English = 1
    }

    public enum CurrencyType : byte
    {
        VND = 0,
        USD = 1,

        Undefined = 254,
    }

    public enum HostIndex : byte
    {
        CurrentHost = 0,
        HostIndex1 = 1,
        HostIndex2 = 2,

        Undefined = 254
    }

    public enum Status : byte
    {
        Activated = 0,
        Displayed = 1,

        Locked = 100,


        Disabled = 250,

        Undefined = 254
    }

    public enum DefaultLocation : byte
    {
        Country = 1,

        Province = 2,

        District = 201
    }

    public enum ObjectType : byte
    {
        Product = 0,
        ShopBlog = 1,
        Brand = 2,
        Category = 3,
        PrdFavorite = 4,
        PrdReview = 5,
        PrdQuestion = 6,

        Undefined = 254
    }

    public enum MediaType : byte
    {
        Image = 0,
        Video = 1,

        Undefined = 254
    }

    public enum BannerLevel : byte
    {
        Main = 0,

        Undefined = 254
    }

    public enum SMSType : byte
    {
        Register = 0,
        WithDrawal = 1,
        ForgotPwd = 2,


        Undefined = 254
    }

    public enum NotifyType : byte
    {
        Promotion = 0,
        News = 1,

        // Sellers
        SellerPromotion = 20,
        SellerActivity = 21,
        SellerTransaction = 22,
        SellerOrder = 23,


        Undefined = 254
    }

    public enum BankingType : byte
    {
        Bank = 0,

        Undefined = 254
    }

    public enum RedeemType : byte
    {
        Card = 0,

        Undefined = 254
    }

    public enum TransactionType : byte
    {
        Profit = 0,
        Referral = 1,
        Withdrawal = 2,

        Undefined = 254
    }
    public enum TransactionStatus : byte
    {
        Pending = 0,
        Processed = 1,
        Approved = 2,
        Disapprove = 3,
        Canceled = 4,

        Undefined = 254
    }

    public enum WithdrawalMethod : byte
    {
        Bank = 0,
        Card = 1,
        Coupon = 2,
        Transfer = 3,

        Undefined = 254
    }

    #endregion

    #region Account
    public enum Gender : byte
    {
        Female = 0,
        Male = 1,
        Other = 2,
        Undefined = 254
    }

    public enum AccountType : byte
    {
        Application = 0,
        Facebook = 1,
        Google = 2,
        Zalo = 3,
        Naver = 4,

        Undefined = 254
    }

    public enum AccountRole : byte
    {
        Member = 0,
        Seller = 1,



        Admin = 200,

        Undefined = 254
    }

    public enum AccountStatus : byte
    {
        Active = 0,
        Locked = 1,

        Disabled = 250,

        Undefined = 254
    }
    #endregion

    #region Seller
    public enum SellerLevel : byte
    {
        New = 0,
        Stone = 1,
        Bronze = 2,
        Silver = 3,
        Gold = 4,
        Platinum = 5,

        Undefined = 254
    }

    #endregion

    #region Product

    public enum CollectionType : byte
    {
        NewProduct = 0,

        BestProduct = 200,

        Undefined = 254
    }

    public enum CategoryType : byte
    {
        Mega = 1,
        Normal = 2,
        Sub = 3,
        Undefined = 254
    }

    public enum ProductStatus : byte
    {
        Active = 1,

        Disabled = 250,

        Undefined = 254
    }

    public enum ProductMediaType : byte
    {
        Image = 0,
        Video = 1,

        Undefined = 254
    }

    public enum ReviewStatus : byte
    {
        New = 0,
        Active = 1,
        Disabled = 250,
        Undefined = 254
    }

    public enum QandAStatus : byte
    {
        New = 0,
        Active = 1,
        Disabled = 250,
        Undefined = 254
    }
    #endregion

    #region SEO

    public enum SEOType : byte
    {
        Page = 0,
        Product = 1,
        Brand = 2,
        Collections = 3,

        Undefined = 254
    }

    #endregion

    public enum TagType : byte
    {
        Product = 0,
        Blog = 1,

        Undefined = 254
    }

    public enum BranchType : byte
    {
        DisplayOnWeb = 0,// gomi.com.vn
        DisplayOnSeller = 1,// gomisellers.com.vn
        DisplayOnPost = 2,

        Store = 10,

        Disabled = 250,
        Undefined = 254
    }

    #region Message

    public enum ResultMessage : int
    {
        // SYSTEM
        DataError = 0,
        UpdateComplete = 1,
        UpdateFailed = 2,

        // ACCOUNT 10 ~ 29
        InvalidPhoneNumber = 10,
        InvalidEmail = 11,
        PhoneNumberNotExits = 12,
        EmailNotExits = 13,
        AccountLocked = 14,
        SignInFailed = 15,
        SignInByGoogle = 16,
        SignInByFacebook = 17,
        SignInByZalo = 18,
        OldPasswordWrong = 19,
        ChangePwdSuccess = 20,
        CodeUpTo = 21,
        InvalidCode = 22,

        // Seller 30 ~ 
        ReferralYourself = 30,
        InvalidReferralCode = 31,
        AlreadyReferral = 32,
        EnterReferralSuccess = 33,
        InvalidURL = 34,
        PostBlogSuccess = 35,
        LinkBankSuccess = 36,
        LinkBankFailed = 37,
        InvalidComment = 38,
        WriteCommentSuccess = 39,
        AddVideoSuccess = 40,

        AddProductSuccess = 41,
        AddProductFailed = 42,
        RemoveProductSuccess = 43,
        RemoveProductFailed = 44,

        // Title
        HighlyProfit = 200,

        // Notifications
        NotifyReferral = 210,
        NotifyTitleWithdrawalBank = 211,
        NotifyContentWithdrawalBank = 212,

        NotifyTitleWithdrawalCard = 213,
        NotifyContentWithdrawalCard = 214,
    }
    #endregion

    #region
    public enum OrderStt : byte
    {
        New = 0,
        Cancel = 1,
    }

    public enum ConfirmStt : byte
    {
        New = 0,
        Confirmed = 1
    }

    public enum DeliveryStt : byte
    {
        New = 0,
        Delivered = 1
    }

    public enum TransactionStt : byte
    {
        New = 0,
        Paid = 1
    }

    public enum CODStt : byte
    {
        New = 0,
        Received = 1
    }

    #endregion

    public enum NotifyStt : byte
    {
        New = 0,
        Read = 1
    }
}
