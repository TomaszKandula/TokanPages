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
    document: DocumentContentDto;
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
}

export interface ContentDocumentDto {
    contentPolicy?: DocumentContentDto;
    contentTerms?: DocumentContentDto;
    contentAbout?: DocumentContentDto;
    contentStory?: DocumentContentDto;
    contentShowcase?: DocumentContentDto;
    contentBicycle?: DocumentContentDto;
    contentElectronics?: DocumentContentDto;
    contentFootball?: DocumentContentDto;
    contentGuitar?: DocumentContentDto;
    contentPhotography?: DocumentContentDto;
}
