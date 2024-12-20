import { ApplicationState } from "./applicationState";
import {
    UserActivate,
    NewsletterAdd,
    ArticleListing,
    ApplicationDialog,
    ApplicationError,
    UserReAuthenticate,
    UserRemove,
    NewsletterRemove,
    UserPasswordReset,
    ArticleSelection,
    ApplicationEmail,
    UserSignin,
    UserSignup,
    UserDataStore,
    ArticleUpdate,
    NewsletterUpdate,
    UserUpdate,
    UserPasswordUpdate,
    ApplicationLanguage,
    UserMediaUpload,
    UserSignout,
    UserNotification,
    UserEmailVerification,
    ApplicationNavbar,
    ContentPageData,
    ArticleInfo,
} from "../Defaults";

export const ApplicationDefault: ApplicationState = {
    applicationError: ApplicationError,
    applicationEmail: ApplicationEmail,
    applicationDialog: ApplicationDialog,
    applicationLanguage: ApplicationLanguage,
    applicationNavbar: ApplicationNavbar,
    articleInfo: ArticleInfo,
    articleListing: ArticleListing,
    articleSelection: ArticleSelection,
    articleUpdate: ArticleUpdate,
    contentPageData: ContentPageData,
    newsletterAdd: NewsletterAdd,
    newsletterRemove: NewsletterRemove,
    newsletterUpdate: NewsletterUpdate,
    userActivate: UserActivate,
    userEmailVerification: UserEmailVerification,
    userDataStore: UserDataStore,
    userMediaUpload: UserMediaUpload,
    userNotification: UserNotification,
    userPasswordReset: UserPasswordReset,
    userPasswordUpdate: UserPasswordUpdate,
    userReAuthenticate: UserReAuthenticate,
    userRemove: UserRemove,
    userSignin: UserSignin,
    userSignout: UserSignout,
    userSignup: UserSignup,
    userUpdate: UserUpdate,
};
