import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Navigation from "../Components/Layout/navigation";
import HeaderView from "../Components/Layout/headerView";
import Footer from "../Components/Layout/footer";
import FeaturesView from "../Components/Features/featuresView";
import FeaturedView from "../Components/Featured/featuredView";
import TestimonialsView from "../Components/Testimonials/testimonialsView";
import Newsletter from "../Components/Newsletter/newsletter";
import ContactForm from "../Components/Contact/contactForm";
import Cookies from "../Components/Cookies/cookies";
import ArticleFeatView from "../Components/Articles/articleFeatView";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as HeaderContent } from "../Redux/Actions/Content/getHeaderContentAction";
import { ActionCreators as FeaturesContent } from "../Redux/Actions/Content/getFeaturesContentAction";
import { ActionCreators as ArticlesContent } from "../Redux/Actions/Content/getArticleFeatContentAction";
import { ActionCreators as FeaturedContent } from "../Redux/Actions/Content/getFeaturedContentAction";
import { ActionCreators as TestimonialsContent } from "../Redux/Actions/Content/getTestimonialsContentAction";
import { ActionCreators as NewsletterContent } from "../Redux/Actions/Content/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "../Redux/Actions/Content/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "../Redux/Actions/Content/getCookiesPromptContentAction";
import AOS from "aos";

const MainPage = (): JSX.Element => 
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
            <HeaderView content={header?.content} isLoading={header?.isLoading} />
            <FeaturesView content={features?.content} isLoading={features?.isLoading} />
            <ArticleFeatView content={articles?.content} isLoading={articles?.isLoading} />
            <FeaturedView content={featured?.content} isLoading={featured?.isLoading} />
            <TestimonialsView content={testimonials?.content} isLoading={testimonials?.isLoading} />
            <Newsletter content={newsletter?.content} isLoading={newsletter?.isLoading} />
            <ContactForm content={contactForm?.content} isLoading={contactForm?.isLoading} />
            <Cookies content={cookiesPrompt?.content} isLoading={cookiesPrompt?.isLoading} />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default MainPage;
