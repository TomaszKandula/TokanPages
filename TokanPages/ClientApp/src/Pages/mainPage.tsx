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
    getNavigationText,
    getArticleFeatText, 
    getContactFormText, 
    getCookiesPromptText, 
    getFeaturedText, 
    getFeaturesText, 
    getNewsletterText, 
    getTestimonialsText,
    getHeaderText
} from "../Api/Services/";
import { 
    navigationDefault,
    headerDefault,
    articleFeatDefault, 
    contactFormDefault, 
    cookiesPromptDefault, 
    featuredDefault, 
    featuresDefault, 
    newsletterDefault, 
    testimonialsDefault
} from "../Api/Defaults";

import AOS from "aos";

export default function Index() 
{
    const mountedRef = React.useRef(true);

    const [navigation, setNavigation] = React.useState(navigationDefault);
    const [header, setHeader] = React.useState(headerDefault);
    const [articles, setArticles] = React.useState(articleFeatDefault);
    const [features, setFeatures] = React.useState(featuresDefault);
    const [featured, setFeatured] = React.useState(featuredDefault);
    const [testimonials, setTestimonials] = React.useState(testimonialsDefault);
    const [contactForm, setContactForm] = React.useState(contactFormDefault);
    const [cookiesPrompt, setCookiesPrompt] = React.useState(cookiesPromptDefault);
    const [newsletter, setNewsletter] = React.useState(newsletterDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigation(await getNavigationText());
        setHeader(await getHeaderText());
        setArticles(await getArticleFeatText());
        setFeatures(await getFeaturesText());
        setFeatured(await getFeaturedText());
        setTestimonials(await getTestimonialsText());
        setContactForm(await getContactFormText());
        setCookiesPrompt(await getCookiesPromptText());
        setNewsletter(await getNewsletterText());
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
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
