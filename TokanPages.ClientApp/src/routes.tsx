import * as React from "react";
import { Route } from "react-router-dom";
import { PRERENDER_PATH_PREFIX } from "./Shared/constants";
import { LanguageItemDto } from "./Api/Models";
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
    canPrerender?: boolean;
}

interface RoutesProps {
    languages: LanguageItemDto[] | undefined;
}

const pages: PageProps[] = [
    { path: "/", page: <MainPage />, canPrerender: true },
    { path: "/showcase", page: <ShowcasePage />, canPrerender: true },
    { path: "/articles", page: <ArticlesPage /> },
    { path: "/business", page: <BusinessPage />, canPrerender: true },
    { path: "/leisure/bicycle", page: <BicyclePage />, canPrerender: true },
    { path: "/leisure/electronics", page: <ElectronicsPage />, canPrerender: true },
    { path: "/leisure/football", page: <FootballPage />, canPrerender: true },
    { path: "/leisure/guitar", page: <GuitarPage />, canPrerender: true },
    { path: "/leisure/photography", page: <PhotographyPage />, canPrerender: true },
    { path: "/contact", page: <ContactPage />, canPrerender: true },
    { path: "/about/info", page: <InfoPage />, canPrerender: true },
    { path: "/about/story", page: <StoryPage />, canPrerender: true },
    { path: "/terms", page: <TermsPage />, canPrerender: true },
    { path: "/policy", page: <PolicyPage />, canPrerender: true },
    { path: "/document", page: <PdfViewerPage /> },
    { path: "/signin", page: <SigninPage /> },
    { path: "/signup", page: <SignupPage /> },
    { path: "/signout", page: <SignoutPage /> },
    { path: "/account", page: <AccountPage /> },
    { path: "/accountactivation", page: <ActivationPage /> },//TODO: rename to 'account-activation'
    { path: "/updatepassword", page: <PasswordUpdatePage /> },//TODO: rename to 'update-password'
    { path: "/resetpassword", page: <PasswordResetPage /> },//TODO: rename to 'reset-password'
    { path: "/update-newsletter", page: <NewsletterUpdatePage /> },
    { path: "/remove-newsletter", page: <NewsletterRemovePage /> },
];

export const Routes = (props: RoutesProps): React.ReactElement => {
    const renderRoute = (props: PageProps) => {
        return (
            <Route exact={props.exact ?? true} path={props.path} key={uuidv4()}>
                {props.page}
            </Route>
        );
    };

    let buffer: React.ReactElement[] = [];
    pages.forEach(item => {
        if (props.languages && props.languages.length > 0) {
            props.languages.forEach(language => {
                const registerPath = `/${language.id}${item.path}`;
                buffer.push(renderRoute({ path: registerPath, page: item.page }));

                if (item.canPrerender) {
                    const snapshotPath = `${PRERENDER_PATH_PREFIX}${registerPath}`;
                    buffer.push(renderRoute({ path: snapshotPath, page: item.page }));
                }
            });
        }
    });

    return buffer.length > 0 ? <>{buffer}</> : <></>;
};
