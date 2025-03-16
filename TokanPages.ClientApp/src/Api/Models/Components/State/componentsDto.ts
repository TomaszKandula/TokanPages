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
    SocialsContentDto,
    FeatureShowcaseContentDto,
} from "../../../Models";

export interface ComponentsDto {
    accountActivate: AccountActivateContentDto;
    accountSettings: AccountSettingsContentDto;
    accountUserNotes: AccountUserNotesContentDto;
    accountUserSignin: AccountUserSigninContentDto;
    accountUserSignout: AccountUserSignoutContentDto;
    accountUserSignup: AccountUserSignupContentDto;
    layoutFooter: FooterContentDto;
    layoutHeader: HeaderContentDto;
    layoutNavigation: NavigationContentDto;
    legalPolicy: DocumentContentDto;
    legalTerms: DocumentContentDto;
    leisureBicycle: DocumentContentDto;
    leisureElectronics: DocumentContentDto;
    leisureFootball: DocumentContentDto;
    leisureGuitar: DocumentContentDto;
    leisurePhotography: DocumentContentDto;
    pageAbout: DocumentContentDto;
    pageArticle: ArticleContentDto;
    pageShowcase: DocumentContentDto;
    pageStory: DocumentContentDto;
    pageBusinessForm: BusinessFormContentDto;
    pageNewsletterRemove: NewsletterRemoveContentDto;
    pageNewsletterUpdate: NewsletterUpdateContentDto;
    pagePasswordReset: PasswordResetContentDto;
    pagePasswordUpdate: PasswordUpdateContentDto;
    sectionArticle: ArticleFeaturesContentDto;
    sectionClients: ClientsContentDto;
    sectionContactForm: ContactFormContentDto;
    sectionCookiesPrompt: CookiesPromptContentDto;
    sectionFeatured: FeaturedContentDto;
    sectionNewsletter: NewsletterContentDto;
    sectionShowcase: FeatureShowcaseContentDto;
    sectionSocials: SocialsContentDto;
    sectionTechnologies: TechnologiesContentDto;
    sectionTestimonials: TestimonialsContentDto;
    templates: TemplatesContent;
}
