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
import { combinedDefaults } from "../Redux/combinedDefaults";
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

export default function Index() 
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

    const fetchNavigationContent = React.useCallback(() => dispatch(NavigationContent.getNavigationContent()), [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => dispatch(FooterContent.getFooterContent()), [ dispatch ]);
    const fetchHeaderContent = React.useCallback(() => dispatch(HeaderContent.getHeaderContent()), [ dispatch ]);
    const fetchFeaturesContent = React.useCallback(() => dispatch(FeaturesContent.getFeaturesContent()), [ dispatch ]);
    const fetchArticlesContent = React.useCallback(() => dispatch(ArticlesContent.getArticleFeaturesContent()), [ dispatch ]);
    const fetchFeaturedContent = React.useCallback(() => dispatch(FeaturedContent.getFeaturedContent()), [ dispatch ]);
    const fetchTestimonialsContent = React.useCallback(() => dispatch(TestimonialsContent.getTestimonialsContent()), [ dispatch ]);
    const fetchNewsletterContent = React.useCallback(() => dispatch(NewsletterContent.getNewsletterContent()), [ dispatch ]);
    const fetchContactFormContent = React.useCallback(() => dispatch(ContactFormContent.getContactFormContent()), [ dispatch ]);
    const fetchCookiesContent = React.useCallback(() => dispatch(CookiesContent.getCookiesPromptContent()), [ dispatch ]);
    
    React.useEffect(() => 
    { 
        if (navigation?.content === combinedDefaults.getNavigationContent.content) 
            fetchNavigationContent(); 
    }, [ fetchNavigationContent, navigation ]);
    
    React.useEffect(() => 
    { 
        if (footer?.content === combinedDefaults.getFooterContent.content) 
            fetchFooterContent(); 
    }, [ fetchFooterContent, footer ]);
    
    React.useEffect(() => 
    { 
        if (header?.content === combinedDefaults.getHeaderContent.content) 
            fetchHeaderContent(); 
    }, [ fetchHeaderContent, header ]);
    
    React.useEffect(() => 
    { 
        if (features?.content === combinedDefaults.getFeaturesContent.content) 
            fetchFeaturesContent(); 
    }, [ fetchFeaturesContent, features ]);
    
    React.useEffect(() => 
    { 
        if (articles?.content === combinedDefaults.getArticleFeatContent.content) 
            fetchArticlesContent(); 
    }, [ fetchArticlesContent, articles ]);
    
    React.useEffect(() => 
    { 
        if (featured?.content === combinedDefaults.getFeaturedContent.content) 
            fetchFeaturedContent(); 
    }, [ fetchFeaturedContent, featured ]);
    
    React.useEffect(() => 
    { 
        if (testimonials?.content === combinedDefaults.getTestimonialsContent.content) 
            fetchTestimonialsContent(); 
    }, [ fetchTestimonialsContent, testimonials ]);
    
    React.useEffect(() => 
    { 
        if (newsletter?.content === combinedDefaults.getNewsletterContent.content) 
            fetchNewsletterContent(); 
    }, [ fetchNewsletterContent, newsletter ]);
    
    React.useEffect(() => 
    { 
        if (contactForm?.content === combinedDefaults.getContactFormContent.content) 
            fetchContactFormContent(); 
    }, [ fetchContactFormContent, contactForm ]);
    
    React.useEffect(() => 
    { 
        if (cookiesPrompt?.content === combinedDefaults.getCookiesPromptContent.content) 
            fetchCookiesContent(); 
    }, [ fetchCookiesContent, cookiesPrompt ]);
    
    React.useEffect(() => AOS.refresh());

    return (
        <>
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Header header={header} isLoading={header?.isLoading} />
            <Features features={features} isLoading={features?.isLoading} />
            <ArticleFeat articles={articles} isLoading={articles?.isLoading} />
            <Featured featured={featured} isLoading={featured?.isLoading} />
            <Testimonials testimonials={testimonials} isLoading={testimonials?.isLoading} />
            <Newsletter newsletter={newsletter} isLoading={newsletter?.isLoading} />
            <ContactForm contactForm={contactForm} isLoading={contactForm?.isLoading} />
            <Cookies cookiesPrompt={cookiesPrompt} isLoading={cookiesPrompt?.isLoading} />
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
