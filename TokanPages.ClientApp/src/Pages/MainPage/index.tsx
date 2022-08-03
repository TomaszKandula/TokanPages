import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Redux/applicationState";
import { Navigation } from "../../Components/Layout";
import { Header } from "../../Components/Layout";
import { Clients } from "../../Components/Clients";
import { Footer } from "../../Components/Layout";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Newsletter } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { Cookies } from "../../Components/Cookies";
import { Features } from "../../Components/Features";

import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as HeaderContent } from "../../Redux/Actions/Content/getHeaderContentAction";
import { ActionCreators as ActivateAccountContent } from "../../Redux/Actions/Content/getActivateAccountContentAction";
import { ActionCreators as ArticleFeaturesContent } from "../../Redux/Actions/Content/getArticleFeaturesContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as ClientsContent } from "../../Redux/Actions/Content/getClientsContentAction";
import { ActionCreators as FeaturedContent } from "../../Redux/Actions/Content/getFeaturedContentAction";
import { ActionCreators as FeaturesContent } from "../../Redux/Actions/Content/getFeaturesContentAction";
import { ActionCreators as NewsletterContent } from "../../Redux/Actions/Content/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "../../Redux/Actions/Content/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "../../Redux/Actions/Content/getCookiesPromptContentAction";
import { ActionCreators as TestimonialsContent } from "../../Redux/Actions/Content/getTestimonialsContentAction";

export const MainPage = (): JSX.Element => 
{
    const dispatch = useDispatch();

    const language = useSelector((state: IApplicationState) => state.userLanguage);

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const header = useSelector((state: IApplicationState) => state.getHeaderContent);
    const clients = useSelector((state: IApplicationState) => state.getClientsContent);
    const features = useSelector((state: IApplicationState) => state.getFeaturesContent);
    const articles = useSelector((state: IApplicationState) => state.getArticleFeaturesContent);
    const featured = useSelector((state: IApplicationState) => state.getFeaturedContent);
    const testimonials = useSelector((state: IApplicationState) => state.getTestimonialsContent);
    const newsletter = useSelector((state: IApplicationState) => state.getNewsletterContent);
    const contactForm = useSelector((state: IApplicationState) => state.getContactFormContent);
    const cookiesPrompt = useSelector((state: IApplicationState) => state.getCookiesPromptContent);
    
    React.useEffect(() => 
    { 
        dispatch(NavigationContent.getNavigationContent());
        dispatch(HeaderContent.getHeaderContent());
        dispatch(ClientsContent.getClientsContent());
        dispatch(ActivateAccountContent.getActivateAccountContent());
        dispatch(FeaturesContent.getFeaturesContent());
        dispatch(ArticleFeaturesContent.getArticleFeaturesContent());
        dispatch(FeaturedContent.getFeaturedContent());
        dispatch(TestimonialsContent.getTestimonialsContent());
        dispatch(NewsletterContent.getNewsletterContent());
        dispatch(ContactFormContent.getContactFormContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(CookiesContent.getCookiesPromptContent());    
    }, 
    [ dispatch, language.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Header content={header?.content} isLoading={header?.isLoading} />
            <Clients content={clients?.content} isLoading={clients?.isLoading} />
            <Technologies content={features?.content} isLoading={features?.isLoading} />
            <Features content={articles?.content} isLoading={articles?.isLoading} />
            <Featured content={featured?.content} isLoading={featured?.isLoading} />
            <Testimonials content={testimonials?.content} isLoading={testimonials?.isLoading} />
            <Newsletter content={newsletter?.content} isLoading={newsletter?.isLoading} />
            <ContactForm content={contactForm?.content} isLoading={contactForm?.isLoading} />
            <Cookies content={cookiesPrompt?.content} isLoading={cookiesPrompt?.isLoading} />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
