import {
    UserActivateState,
    NewsletterAddState,
    ArticleSelectionState,
    ArticleListingState,
    ApplicationDialogState,
    ApplicationErrorState,
    UserReAuthenticateState,
    UserRemoveState,
    NewsletterRemoveState,
    UserPasswordResetState,
    ApplicationEmailState,
    UserSigninState,
    UserSignupState,
    UserDataStoreState,
    UserUpdateState,
    NewsletterUpdateState,
    ArticleUpdateState,
    UserPasswordUpdateState,
    ApplicationLanguageState,
    UserMediaUploadState,
    UserSignoutState,
    UserNotificationState,
    UserEmailVerificationState,
    ApplicationNavbarState,
    ContentPageDataState,
    ArticleInfoState,
    UserNoteReadState,
    UserNotesReadState,
    UserNoteCreateState,
    UserNoteUpdateState,
    UserNoteDeleteState,
} from "../States";

export interface ApplicationState {
    applicationError: ApplicationErrorState;
    applicationEmail: ApplicationEmailState;
    applicationDialog: ApplicationDialogState;
    applicationLanguage: ApplicationLanguageState;
    applicationNavbar: ApplicationNavbarState;
    articleInfo: ArticleInfoState;
    articleListing: ArticleListingState;
    articleSelection: ArticleSelectionState;
    articleUpdate: ArticleUpdateState;
    contentPageData: ContentPageDataState;
    newsletterAdd: NewsletterAddState;
    newsletterRemove: NewsletterRemoveState;
    newsletterUpdate: NewsletterUpdateState;
    userActivate: UserActivateState;
    userEmailVerification: UserEmailVerificationState;
    userDataStore: UserDataStoreState;
    userMediaUpload: UserMediaUploadState;
    userNotification: UserNotificationState;
    userNoteRead: UserNoteReadState;
    userNotesRead: UserNotesReadState;
    userNoteCreate: UserNoteCreateState;
    userNoteUpdate: UserNoteUpdateState;
    userNoteDelete: UserNoteDeleteState;
    userPasswordReset: UserPasswordResetState;
    userPasswordUpdate: UserPasswordUpdateState;
    userReAuthenticate: UserReAuthenticateState;
    userRemove: UserRemoveState;
    userSignin: UserSigninState;
    userSignout: UserSignoutState;
    userSignup: UserSignupState;
    userUpdate: UserUpdateState;
}
