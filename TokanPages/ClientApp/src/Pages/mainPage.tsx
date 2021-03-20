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
    getHeaderContent,
    getFooterContent,
    getArticleFeatContent,
    getFeaturesContent,
    getFeaturedContent,
    getTestimonialsContent,
    getNewsletterContent,
    getContactFormContent,
    getCookiesPromptContent
} from "../Api/Services/";
import { 
    navigationDefault,
    headerDefault,
    footerDefault,
    articleFeatDefault,
    featuresDefault, 
    featuredDefault,
    testimonialsDefault,
    newsletterDefault,
    contactFormDefault,
    cookiesPromptDefault
} from "../Api/Defaults";

import AOS from "aos";

export default function Index() 
{
    const mountedRef = React.useRef(true);

    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [header, setHeaderContent] = React.useState({ data: headerDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });
    const [articles, setArticlesContent] = React.useState({ data: articleFeatDefault, isLoading: true });
    const [features, setFeaturesContent] = React.useState({ data: featuresDefault, isLoading: true });
    const [featured, setFeaturedContent] = React.useState({ data: featuredDefault, isLoading: true });
    const [testimonials, setTestimonialsContent] = React.useState({ data: testimonialsDefault, isLoading: true });
    const [newsletter, setNewsletterContent] = React.useState({ data: newsletterDefault, isLoading: true });
    const [contactForm, setContactFormContent] = React.useState({ data: contactFormDefault, isLoading: true });
    const [cookiesPrompt, setCookiesPromptContent] = React.useState({ data: cookiesPromptDefault, isLoading: true });
 
    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setHeaderContent({ data: await getHeaderContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
        setArticlesContent({ data: await getArticleFeatContent(), isLoading: false });
        setFeaturesContent({ data: await getFeaturesContent(), isLoading: false });
        setFeaturedContent({ data: await getFeaturedContent(), isLoading: false });
        setTestimonialsContent({ data: await getTestimonialsContent(), isLoading: false });
        setNewsletterContent({ data: await getNewsletterContent(), isLoading: false });
        setContactFormContent({ data: await getContactFormContent(), isLoading: false });
        setCookiesPromptContent({ data: await getCookiesPromptContent(), isLoading: false });
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
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Header header={header.data} isLoading={header.isLoading} />
            <Features features={features.data} isLoading={features.isLoading} />
            <ArticleFeat articles={articles.data} isLoading={articles.isLoading} />
            <Featured featured={featured.data} isLoading={featured.isLoading} />
            <Testimonials testimonials={testimonials.data} isLoading={testimonials.isLoading} />
            <Newsletter newsletter={newsletter.data} isLoading={newsletter.isLoading} />
            <ContactForm contactForm={contactForm.data} isLoading={contactForm.isLoading} />
            <Cookies cookiesPrompt={cookiesPrompt.data} isLoading={cookiesPrompt.isLoading} />
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
