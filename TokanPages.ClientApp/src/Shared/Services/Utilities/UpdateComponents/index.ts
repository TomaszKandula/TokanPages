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
        result.account = item.contentName === "account" ? (item as AccountContentDto) : result.account;
        result.activateAccount =
            item.contentName === "activateAccount" ? (item as ActivateAccountContentDto) : result.activateAccount;
        result.article = item.contentName === "article" ? (item as ArticleContentDto) : result.article;
        result.articleFeatures =
            item.contentName === "articleFeatures" ? (item as ArticleFeaturesContentDto) : result.articleFeatures;
        result.businessForm =
            item.contentName === "businessForm" ? (item as BusinessFormContentDto) : result.businessForm;
        result.clients = item.contentName === "clients" ? (item as ClientsContentDto) : result.clients;
        result.contactForm = item.contentName === "contactForm" ? (item as ContactFormContentDto) : result.contactForm;
        result.cookiesPrompt =
            item.contentName === "cookiesPrompt" ? (item as CookiesPromptContentDto) : result.cookiesPrompt;
        result.document = item.contentName === "document" ? (item as DocumentContentDto) : result.document;
        result.featured = item.contentName === "featured" ? (item as FeaturedContentDto) : result.featured;
        result.footer = item.contentName === "footer" ? (item as FooterContentDto) : result.footer;
        result.header = item.contentName === "header" ? (item as HeaderContentDto) : result.header;
        result.navigation = item.contentName === "navigation" ? (item as NavigationContentDto) : result.navigation;
        result.newsletter = item.contentName === "newsletter" ? (item as NewsletterContentDto) : result.newsletter;
        result.newsletterRemove =
            item.contentName === "newsletterRemove" ? (item as NewsletterRemoveContentDto) : result.newsletterRemove;
        result.newsletterUpdate =
            item.contentName === "newsletterUpdate" ? (item as NewsletterUpdateContentDto) : result.newsletterUpdate;
        result.resetPassword =
            item.contentName === "resetPassword" ? (item as ResetPasswordContentDto) : result.resetPassword;
        result.technologies =
            item.contentName === "technologies" ? (item as TechnologiesContentDto) : result.technologies;
        result.templates = item.contentName === "templates" ? (item as TemplatesContent) : result.templates;
        result.testimonials =
            item.contentName === "testimonials" ? (item as TestimonialsContentDto) : result.testimonials;
        result.updatePassword =
            item.contentName === "updatePassword" ? (item as UpdatePasswordContentDto) : result.updatePassword;
        result.userSignin = item.contentName === "userSignin" ? (item as UserSigninContentDto) : result.userSignin;
        result.userSignout = item.contentName === "userSignout" ? (item as UserSignoutContentDto) : result.userSignout;
        result.userSignup = item.contentName === "userSignup" ? (item as UserSignupContentDto) : result.userSignup;
    });

    return result;
};
