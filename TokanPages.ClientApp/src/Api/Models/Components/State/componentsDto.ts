import {
    AccountSettingsContentDto,
    AccountActivateContentDto,
    AccountUserSigninContentDto,
    AccountUserSignoutContentDto,
    AccountUserSignupContentDto,
    ArticleContentDto,
    ArticleFeaturesContentDto,
    BusinessFormContentDto,
    ClientsContentDto,
    ContactFormContentDto,
    CookiesPromptContentDto,
    DocumentContentDto,
    FeaturedContentDto,
    FooterContentDto,
    HeaderContentDto,
    NavigationContentDto,
    NewsletterContentDto,
    NewsletterRemoveContentDto,
    NewsletterUpdateContentDto,
    PasswordResetContentDto,
    PasswordUpdateContentDto,
    TechnologiesContentDto,
    TemplatesContent,
    TestimonialsContentDto,
    AccountUserNotesContentDto,
} from "../../../Models";

export interface ComponentsDto {
    about: DocumentContentDto;
    accountActivate: AccountActivateContentDto;
    accountSettings: AccountSettingsContentDto;
    accountUserNotes: AccountUserNotesContentDto;
    accountUserSignin: AccountUserSigninContentDto;
    accountUserSignout: AccountUserSignoutContentDto;
    accountUserSignup: AccountUserSignupContentDto;
    article: ArticleContentDto;
    articleFeatures: ArticleFeaturesContentDto;
    bicycle: DocumentContentDto;
    businessForm: BusinessFormContentDto;
    clients: ClientsContentDto;
    contactForm: ContactFormContentDto;
    cookiesPrompt: CookiesPromptContentDto;
    electronics: DocumentContentDto;
    featured: FeaturedContentDto;
    football: DocumentContentDto;
    footer: FooterContentDto;
    guitar: DocumentContentDto;
    header: HeaderContentDto;
    navigation: NavigationContentDto;
    newsletter: NewsletterContentDto;
    newsletterRemove: NewsletterRemoveContentDto;
    newsletterUpdate: NewsletterUpdateContentDto;
    passwordReset: PasswordResetContentDto;
    passwordUpdate: PasswordUpdateContentDto;
    photography: DocumentContentDto;
    policy: DocumentContentDto;
    showcase: DocumentContentDto;
    story: DocumentContentDto;
    technologies: TechnologiesContentDto;
    templates: TemplatesContent; //TODO: rename
    terms: DocumentContentDto;
    testimonials: TestimonialsContentDto;
}
