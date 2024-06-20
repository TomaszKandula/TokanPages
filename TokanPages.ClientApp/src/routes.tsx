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
    path: string;
    page: JSX.Element;
    exact?: boolean;
}

const pages: PageProps[] = [
    { path: "/", page: <MainPage /> },
    { path: "/about", page: <AboutPage /> },
    { path: "/story", page: <StoryPage /> },
    { path: "/articles", page: <ArticlesPage /> },
    { path: "/showcase", page: <ShowcasePage /> },
    { path: "/bicycle", page: <BicyclePage /> },
    { path: "/electronics", page: <ElectronicsPage /> },
    { path: "/football", page: <FootballPage /> },
    { path: "/guitar", page: <GuitarPage /> },
    { path: "/photography", page: <PhotographyPage /> },
    { path: "/terms", page: <TermsPage /> },
    { path: "/policy", page: <PolicyPage /> },
    { path: "/contact", page: <ContactPage /> },
    { path: "/signin", page: <SigninPage /> },
    { path: "/signup", page: <SignupPage /> },
    { path: "/signout", page: <SignoutPage /> },
    { path: "/account", page: <AccountPage /> },
    { path: "/accountactivation", page: <ActivationPage /> },
    { path: "/updatepassword", page: <PasswordUpdatePage /> },
    { path: "/resetpassword", page: <PasswordResetPage /> },
    { path: "/update-newsletter", page: <NewsletterUpdatePage /> },
    { path: "/remove-newsletter", page: <NewsletterRemovePage /> },
];

export const Routes = (): JSX.Element => {
    const renderRoute = (props: PageProps) => {
        return (
            <Route exact={props.exact ?? true} path={props.path} key={uuidv4()}>
                {props.page}
            </Route>
        );
    };

    let buffer: JSX.Element[] = [];
    pages.forEach(item => {
        buffer.push(renderRoute({ path: item.path, page: item.page }));
    });

    return buffer.length > 0 ? <>{buffer}</> : <></>;
};
