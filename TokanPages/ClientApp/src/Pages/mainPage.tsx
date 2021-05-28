import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Navigation from "../Components/Layout/navigation";
import Header from "../Components/Layout/header";
import Footer from "../Components/Layout/footer";
import Features from "../Components/Features/features";
import Featured from "../Components/Featured/featured";
import Testimonials from "../Components/Testimonials/testimonials";
import Newsletter from "../Components/Newsletter/newsletter";
import ContactForm from "../Components/Contact/contactForm";
import Cookies from "../Components/Cookies/cookies";
import ArticleFeat from "../Components/Articles/articleFeat";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as HeaderContent } from "../Redux/Actions/getHeaderContentAction";
import { ActionCreators as FeaturesContent } from "../Redux/Actions/getFeaturesContentAction";
import { ActionCreators as ArticlesContent } from "../Redux/Actions/getArticleFeatContentAction";
import { ActionCreators as FeaturedContent } from "../Redux/Actions/getFeaturedContentAction";
import { ActionCreators as TestimonialsContent } from "../Redux/Actions/getTestimonialsContentAction";
import { ActionCreators as NewsletterContent } from "../Redux/Actions/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "../Redux/Actions/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "../Redux/Actions/getCookiesPromptContentAction";
import AOS from "aos";

export default function MainPage() 
{
    const dispatch = useDispatch();

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const header = useSelector((state: IApplicationState) => state.getHeaderContent);
    const features = useSelector((state: IApplicationState) => state.getFeaturesContent);
    const articles = useSelector((state: IApplicationState) => state.getArticleFeatContent);
    const featured = useSelector((state: IApplicationState) => state.getFeaturedContent);
    const testimonials = useSelector((state: IApplicationState) => state.getTestimonialsContent);
    const newsletter = useSelector((state: IApplicationState) => state.getNewsletterContent);
    const contactForm = useSelector((state: IApplicationState) => state.getContactFormContent);
    const cookiesPrompt = useSelector((state: IApplicationState) => state.getCookiesPromptContent);
    
    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(HeaderContent.getHeaderContent());
        dispatch(FeaturesContent.getFeaturesContent());
        dispatch(ArticlesContent.getArticleFeaturesContent());
        dispatch(FeaturedContent.getFeaturedContent());
        dispatch(TestimonialsContent.getTestimonialsContent());
        dispatch(NewsletterContent.getNewsletterContent());
        dispatch(ContactFormContent.getContactFormContent());
        dispatch(CookiesContent.getCookiesPromptContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    React.useEffect(() => AOS.refresh());

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Header content={header?.content} isLoading={header?.isLoading} />
            <Features content={features?.content} isLoading={features?.isLoading} />
            <ArticleFeat content={articles?.content} isLoading={articles?.isLoading} />
            <Featured content={featured?.content} isLoading={featured?.isLoading} />
            <Testimonials content={testimonials?.content} isLoading={testimonials?.isLoading} />
            <Newsletter content={newsletter?.content} isLoading={newsletter?.isLoading} />
            <ContactForm content={contactForm?.content} isLoading={contactForm?.isLoading} />
            <Cookies content={cookiesPrompt?.content} isLoading={cookiesPrompt?.isLoading} />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
