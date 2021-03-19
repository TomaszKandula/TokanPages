import * as React from "react";
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
import { 
    getNavigationContent,
    getArticleFeatContent, 
    getContactFormContent, 
    getCookiesPromptContent, 
    getFeaturedContent, 
    getFeaturesContent, 
    getNewsletterContent, 
    getTestimonialsContent,
    getHeaderContent
} from "../Api/Services/";
import { 
    navigationDefault,
    headerDefault,
    footerDefault,
    articleFeatDefault, 
    contactFormDefault, 
    cookiesPromptDefault, 
    featuredDefault, 
    featuresDefault, 
    newsletterDefault, 
    testimonialsDefault
} from "../Api/Defaults";

import AOS from "aos";
import { getFooterContent } from "Api/Services/components";

export default function Index() 
{
    const mountedRef = React.useRef(true);

    const [navigation, setNavigationContent] = React.useState(navigationDefault);
    const [header, setHeaderContent] = React.useState(headerDefault);
    const [footer, setFooterContent] = React.useState(footerDefault);
    const [articles, setArticlesContent] = React.useState(articleFeatDefault);
    const [features, setFeaturesContent] = React.useState(featuresDefault);
    const [featured, setFeaturedContent] = React.useState(featuredDefault);
    const [testimonials, setTestimonialsContent] = React.useState(testimonialsDefault);
    const [contactForm, setContactFormContent] = React.useState(contactFormDefault);
    const [cookiesPrompt, setCookiesPromptContent] = React.useState(cookiesPromptDefault);
    const [newsletter, setNewsletterContent] = React.useState(newsletterDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent(await getNavigationContent());
        setHeaderContent(await getHeaderContent());
        setFooterContent(await getFooterContent());
        setArticlesContent(await getArticleFeatContent());
        setFeaturesContent(await getFeaturesContent());
        setFeaturedContent(await getFeaturedContent());
        setTestimonialsContent(await getTestimonialsContent());
        setContactFormContent(await getContactFormContent());
        setCookiesPromptContent(await getCookiesPromptContent());
        setNewsletterContent(await getNewsletterContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    React.useEffect(() => 
    {
        AOS.refresh();
    });

    return (
        <>
            <Navigation content={navigation.content} />
            <Header content={header.content} />
            <Features content={features.content} />
            <ArticleFeat content={articles.content} />
            <Featured content={featured.content} />
            <Testimonials content={testimonials.content} />
            <Newsletter content={newsletter.content} />
            <ContactForm content={contactForm.content} />
            <Cookies content={cookiesPrompt.content} />
            <Footer footer={footer} backgroundColor="#FAFAFA" />
        </>
    );
}
