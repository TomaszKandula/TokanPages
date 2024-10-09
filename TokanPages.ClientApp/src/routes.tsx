import * as React from "react";
import { Route } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";
import {
    MainPage,
    ArticlesPage,
    ContactPage,
    SigninPage,
    SignupPage,
    SignoutPage,
    AccountPage,
    PasswordResetPage,
    PasswordUpdatePage,
    NewsletterRemovePage,
    NewsletterUpdatePage,
    ActivationPage,
    InfoPage,
    StoryPage,
    TermsPage,
    PolicyPage,
    ShowcasePage,
    BicyclePage,
    ElectronicsPage,
    FootballPage,
    GuitarPage,
    PhotographyPage,
    PdfViewerPage,
    BusinessPage,
} from "./Pages";

interface PageProps {
    path: string;
    page: React.ReactElement;
    exact?: boolean;
}

const pages: PageProps[] = [
    { path: "/", page: <MainPage /> },
    { path: "/about/info", page: <InfoPage /> },
    { path: "/about/story", page: <StoryPage /> },
    { path: "/articles", page: <ArticlesPage /> },
    { path: "/showcase", page: <ShowcasePage /> },
    { path: "/document", page: <PdfViewerPage /> },
    { path: "/business", page: <BusinessPage /> },
    { path: "/leisure/bicycle", page: <BicyclePage /> },
    { path: "/leisure/electronics", page: <ElectronicsPage /> },
    { path: "/leisure/football", page: <FootballPage /> },
    { path: "/leisure/guitar", page: <GuitarPage /> },
    { path: "/leisure/photography", page: <PhotographyPage /> },
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

export const Routes = (): React.ReactElement => {
    const renderRoute = (props: PageProps) => {
        return (
            <Route exact={props.exact ?? true} path={props.path} key={uuidv4()}>
                {props.page}
            </Route>
        );
    };

    let buffer: React.ReactElement[] = [];
    pages.forEach(item => {
        buffer.push(renderRoute({ path: item.path, page: item.page }));
    });

    return buffer.length > 0 ? <>{buffer}</> : <></>;
};
