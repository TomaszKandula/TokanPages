import * as React from "react";
import { Route } from "react-router-dom";
import { LanguageItemDto } from "./Api/Models";
import { PRERENDER_PATH_PREFIX } from "./Shared/constants";
import { v4 as uuidv4 } from "uuid";
import {
    MainPage,
    ArticlesPage,
    ContactPage,
    SigninPage,
    SignupPage,
    SignoutPage,
    SettingsPage,
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
    UserNotesPage,
    ResumePage,
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
    { path: "/resume", page: <ResumePage />, canPrerender: true },
    { path: "/showcase", page: <ShowcasePage />, canPrerender: true },
    { path: "/articles", page: <ArticlesPage />, canPrerender: true },
    { path: "/business", page: <BusinessPage />, canPrerender: true },
    { path: "/leisure/bicycle", page: <BicyclePage />, canPrerender: true },
    { path: "/leisure/electronics", page: <ElectronicsPage />, canPrerender: true },
    { path: "/leisure/football", page: <FootballPage />, canPrerender: true },
    { path: "/leisure/guitar", page: <GuitarPage />, canPrerender: true },
    { path: "/leisure/photography", page: <PhotographyPage />, canPrerender: true },
    { path: "/contact", page: <ContactPage />, canPrerender: true },
    { path: "/about/info", page: <InfoPage />, canPrerender: true },
    { path: "/about/story", page: <StoryPage />, canPrerender: true },
    { path: "/legal/terms", page: <TermsPage />, canPrerender: true },
    { path: "/legal/policy", page: <PolicyPage />, canPrerender: true },
    { path: "/document", page: <PdfViewerPage /> },
    { path: "/newsletter/update", page: <NewsletterUpdatePage /> },
    { path: "/newsletter/remove", page: <NewsletterRemovePage /> },
    { path: "/account/signin", page: <SigninPage />, canPrerender: true },
    { path: "/account/signup", page: <SignupPage />, canPrerender: true },
    { path: "/account/signout", page: <SignoutPage /> },
    { path: "/account/password/update", page: <PasswordUpdatePage /> },
    { path: "/account/password/reset", page: <PasswordResetPage /> },
    { path: "/account/settings", page: <SettingsPage /> },
    { path: "/account/user-notes", page: <UserNotesPage /> },
    { path: "/account/activation", page: <ActivationPage /> },
];

const renderRoute = (props: PageProps) => {
    return (
        <Route exact={props.exact ?? true} path={props.path} key={uuidv4()}>
            {props.page}
        </Route>
    );
};

export const MapComponentsToRoutes = (props: RoutesProps): React.ReactElement => {
    const buffer: React.ReactElement[] = [];

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
