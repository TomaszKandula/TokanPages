import {
    AccountSettingsContentDto,
    AccountActivateContentDto,
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
    ResetPasswordContentDto,
    TechnologiesContentDto,
    TemplatesContent,
    TestimonialsContentDto,
    UpdatePasswordContentDto,
    UserSigninContentDto,
    UserSignoutContentDto,
    UserSignupContentDto,
} from "../../../Models";

export interface ComponentsDto {
    accountSettings: AccountSettingsContentDto;
    accountActivate: AccountActivateContentDto;
    accountUserSignin: UserSigninContentDto;
    accountUserSignout: UserSignoutContentDto;
    accountUserSignup: UserSignupContentDto;
    article: ArticleContentDto;
    articleFeatures: ArticleFeaturesContentDto;
    businessForm: BusinessFormContentDto;
    clients: ClientsContentDto;
    contactForm: ContactFormContentDto;
    cookiesPrompt: CookiesPromptContentDto;
    featured: FeaturedContentDto;
    footer: FooterContentDto;
    header: HeaderContentDto;
    navigation: NavigationContentDto;
    newsletter: NewsletterContentDto;
    newsletterRemove: NewsletterRemoveContentDto;
    newsletterUpdate: NewsletterUpdateContentDto;
    passwordReset: ResetPasswordContentDto;
    passwordUpdate: UpdatePasswordContentDto;
    technologies: TechnologiesContentDto;
    templates: TemplatesContent; //TODO: rename
    testimonials: TestimonialsContentDto;
    policy: DocumentContentDto;
    terms: DocumentContentDto;
    about: DocumentContentDto;
    story: DocumentContentDto;
    showcase: DocumentContentDto;
    bicycle: DocumentContentDto;
    electronics: DocumentContentDto;
    football: DocumentContentDto;
    guitar: DocumentContentDto;
    photography: DocumentContentDto;
}
