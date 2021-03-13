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
    getArticleFeatText, 
    getContactFormText, 
    getCookiesPromptText, 
    getFeaturedText, 
    getFeaturesText, 
    getNewsletterText, 
    getTestimonialsText
} from "../Api/Services/";
import { 
    articleFeatInitValues, 
    contactFormInitValues, 
    cookiesPromptInitValues, 
    featuredInitValues, 
    featuresInitValues, 
    newsletterInitValues, 
    testimonialsInitValues
} from "../Api/Defaults";

import AOS from "aos";

export default function Index() 
{
    const mountedRef = React.useRef(true);

    const [articles, setArticles] = React.useState(articleFeatInitValues);
    const [features, setFeatures] = React.useState(featuresInitValues);
    const [featured, setFeatured] = React.useState(featuredInitValues);
    const [testimonials, setTestimonials] = React.useState(testimonialsInitValues);
    const [contactForm, setContactForm] = React.useState(contactFormInitValues);
    const [cookiesPrompt, setCookiesPrompt] = React.useState(cookiesPromptInitValues);
    const [newsletter, setNewsletter] = React.useState(newsletterInitValues);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
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
            <Navigation content={null} />
            <Header />
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
