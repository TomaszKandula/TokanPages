import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../Redux/applicationState";
import Navigation from "../Components/Layout/navigation";
import HeaderView from "../Components/Layout/headerView";
import ClientsView from "../Components/Clients/clientsView";
import Footer from "../Components/Layout/footer";
import FeaturesView from "../Components/Features/featuresView";
import FeaturedView from "../Components/Featured/featuredView";
import TestimonialsView from "../Components/Testimonials/testimonialsView";
import Newsletter from "../Components/Newsletter/newsletter";
import ContactForm from "../Components/Contact/contactForm";
import Cookies from "../Components/Cookies/cookies";
import ArticleFeaturesView from "../Components/Articles/articleFeaturesView";
import { GetMainPageContent } from "./Services";

const MainPage = (): JSX.Element => 
{
    const dispatch = useDispatch();

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
    
    React.useEffect(() => { GetMainPageContent(dispatch) }, [ dispatch ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <HeaderView content={header?.content} isLoading={header?.isLoading} />
            <ClientsView content={clients?.content} isLoading={clients?.isLoading} />
            <FeaturesView content={features?.content} isLoading={features?.isLoading} />
            <ArticleFeaturesView content={articles?.content} isLoading={articles?.isLoading} />
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
