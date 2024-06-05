import * as React from "react";
import { Route } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";
import {
    MainPage,
    AboutPage,
    StoryPage,
    ArticlesPage,
    TermsPage,
    PolicyPage,
    ContactPage,
    ShowcasePage,
    SigninPage,
    SignupPage,
    SignoutPage,
    AccountPage,
    PasswordResetPage,
    PasswordUpdatePage,
    NewsletterRemovePage,
    NewsletterUpdatePage,
    ActivationPage,
    BicyclePage,
    ElectronicsPage,
    FootballPage,
    GuitarPage,
    PhotographyPage,
} from "./Pages";

interface PageProps {
    componentPath: string;
    componentPage: JSX.Element;
}

const pages: PageProps[] = [
    { componentPath: "/", componentPage: <MainPage /> },
    { componentPath: "/about", componentPage: <AboutPage /> },
    { componentPath: "/story", componentPage: <StoryPage /> },
    { componentPath: "/articles", componentPage: <ArticlesPage /> },
    { componentPath: "/showcase", componentPage: <ShowcasePage /> },
    { componentPath: "/bicycle", componentPage: <BicyclePage /> },
    { componentPath: "/electronics", componentPage: <ElectronicsPage /> },
    { componentPath: "/football", componentPage: <FootballPage /> },
    { componentPath: "/guitar", componentPage: <GuitarPage /> },
    { componentPath: "/photography", componentPage: <PhotographyPage /> },
    { componentPath: "/terms", componentPage: <TermsPage /> },
    { componentPath: "/policy", componentPage: <PolicyPage /> },
    { componentPath: "/contact", componentPage: <ContactPage /> },
    { componentPath: "/signin", componentPage: <SigninPage /> },
    { componentPath: "/signup", componentPage: <SignupPage /> },
    { componentPath: "/signout", componentPage: <SignoutPage /> },
    { componentPath: "/account", componentPage: <AccountPage /> },
    { componentPath: "/accountactivation", componentPage: <ActivationPage /> },
    { componentPath: "/updatepassword", componentPage: <PasswordUpdatePage /> },
    { componentPath: "/resetpassword", componentPage: <PasswordResetPage /> },
    { componentPath: "/update-newsletter", componentPage: <NewsletterUpdatePage /> },
    { componentPath: "/remove-newsletter", componentPage: <NewsletterRemovePage /> },
];

export const Routes = (): JSX.Element => {
    const renderRoute = (props: PageProps) => {
        return (
            <Route exact path={props.componentPath} key={uuidv4()}>
                {props.componentPage}
            </Route>
        )
    };

    let buffer: JSX.Element[] = [];
    pages.forEach(item => {
        buffer.push(renderRoute({ componentPath: item.componentPath, componentPage: item.componentPage }));
    });

    return buffer.length > 0 ? <>{buffer}</> : <></>;
};
