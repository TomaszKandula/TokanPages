import { ContentPageDataState } from "../../../../Store/States";
import {
    AccountContentDto,
    ActivateAccountContentDto,
    ArticleContentDto,
    ArticleFeaturesContentDto,
    BusinessFormContentDto,
    ClientsContentDto,
    ComponentsDto,
    ContactFormContentDto,
    ContentModelDto,
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
} from "../../../../Api/Models";

export const UpdateComponents = (state: ContentPageDataState, source: ContentModelDto[]): ComponentsDto => {
    let result = { ...state.components };

    source.forEach(item => {
        result.account = item.contentName === "account" ? (item.content as AccountContentDto) : result.account;
        result.activateAccount = item.contentName === "activateAccount" ? (item.content as ActivateAccountContentDto) : result.activateAccount;
        result.article = item.contentName === "article" ? (item.content as ArticleContentDto) : result.article;
        result.articleFeatures = item.contentName === "articleFeatures" ? (item.content as ArticleFeaturesContentDto) : result.articleFeatures;
        result.businessForm = item.contentName === "businessForm" ? (item.content as BusinessFormContentDto) : result.businessForm;
        result.clients = item.contentName === "clients" ? (item.content as ClientsContentDto) : result.clients;
        result.contactForm = item.contentName === "contactForm" ? (item.content as ContactFormContentDto) : result.contactForm;
        result.cookiesPrompt = item.contentName === "cookiesPrompt" ? (item.content as CookiesPromptContentDto) : result.cookiesPrompt;
        result.document = item.contentName === "document" ? (item.content as DocumentContentDto) : result.document;
        result.featured = item.contentName === "featured" ? (item.content as FeaturedContentDto) : result.featured;
        result.footer = item.contentName === "footer" ? (item.content as FooterContentDto) : result.footer;
        result.header = item.contentName === "header" ? (item.content as HeaderContentDto) : result.header;
        result.navigation = item.contentName === "navigation" ? (item.content as NavigationContentDto) : result.navigation;
        result.newsletter = item.contentName === "newsletter" ? (item.content as NewsletterContentDto) : result.newsletter;
        result.newsletterRemove = item.contentName === "newsletterRemove" ? (item.content as NewsletterRemoveContentDto) : result.newsletterRemove;
        result.newsletterUpdate = item.contentName === "newsletterUpdate" ? (item.content as NewsletterUpdateContentDto) : result.newsletterUpdate;
        result.resetPassword = item.contentName === "resetPassword" ? (item.content as ResetPasswordContentDto) : result.resetPassword;
        result.technologies = item.contentName === "technologies" ? (item.content as TechnologiesContentDto) : result.technologies;
        result.templates = item.contentName === "templates" ? (item.content as TemplatesContent) : result.templates;
        result.testimonials = item.contentName === "testimonials" ? (item.content as TestimonialsContentDto) : result.testimonials;
        result.updatePassword = item.contentName === "updatePassword" ? (item.content as UpdatePasswordContentDto) : result.updatePassword;
        result.userSignin = item.contentName === "userSignin" ? (item.content as UserSigninContentDto) : result.userSignin;
        result.userSignout = item.contentName === "userSignout" ? (item.content as UserSignoutContentDto) : result.userSignout;
        result.userSignup = item.contentName === "userSignup" ? (item.content as UserSignupContentDto) : result.userSignup;
    });

    return result;
};
