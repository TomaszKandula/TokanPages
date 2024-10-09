import {
    AccountContentDto,
    ActivateAccountContentDto,
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
    account: AccountContentDto;
    activateAccount: ActivateAccountContentDto;
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
    resetPassword: ResetPasswordContentDto;
    technologies: TechnologiesContentDto;
    templates: TemplatesContent; //TODO: rename
    testimonials: TestimonialsContentDto;
    updatePassword: UpdatePasswordContentDto;
    userSignin: UserSigninContentDto;
    userSignout: UserSignoutContentDto;
    userSignup: UserSignupContentDto;
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
