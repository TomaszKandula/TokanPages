import * as React from "react";
import { useSelector } from "react-redux";
import { Route } from "react-router-dom";
import {
    LINK_HREF_ATTRIBUTE,
    LINK_HREFLANG_ATTRIBUTE,
    LINK_QUERY_SELECTOR,
    LINK_REL_ATTRIBUTE,
    PRERENDER_PATH_PREFIX,
} from "./Shared/constants";
import { ApplicationState } from "./Store/Configuration";
import { LanguageItemDto } from "./Api/Models";
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
    { path: "/newsletter/update", page: <NewsletterUpdatePage /> },
    { path: "/newsletter/remove", page: <NewsletterRemovePage /> },
    { path: "/account/signin", page: <SigninPage /> },
    { path: "/account/signup", page: <SignupPage /> },
    { path: "/account/signout", page: <SignoutPage /> },
    { path: "/account/settings", page: <SettingsPage /> },
    { path: "/account/user-notes", page: <UserNotesPage /> },
    { path: "/account/activation", page: <ActivationPage /> },
    { path: "/account/password-update", page: <PasswordUpdatePage /> },
    { path: "/account/password-reset", page: <PasswordResetPage /> },
];

const renderRoute = (props: PageProps) => {
    return (
        <Route exact={props.exact ?? true} path={props.path} key={uuidv4()}>
            {props.page}
        </Route>
    );
};

const createCanonicalLink = (): void => {
    const link = document.querySelector(LINK_QUERY_SELECTOR);
    if (link === null) {
        let newlink = document.createElement("link");
        newlink.setAttribute(LINK_REL_ATTRIBUTE, "canonical");
        newlink.setAttribute(LINK_HREF_ATTRIBUTE, window.location.href);
        document.head.appendChild(newlink);
    } else {
        link.setAttribute(LINK_HREF_ATTRIBUTE, window.location.href);
    }
};

const createAlternateLink = (href: string, hreflang: string): void => {
    const link = document.querySelector(`link[hreflang="${hreflang}"]`);
    if (link === null) {
        let element = document.createElement("link");
        element.setAttribute(LINK_REL_ATTRIBUTE, "alternate");
        element.setAttribute(LINK_HREFLANG_ATTRIBUTE, hreflang);
        element.setAttribute(LINK_HREF_ATTRIBUTE, href);
        document.head.appendChild(element);
    } else {
        link.setAttribute(LINK_HREFLANG_ATTRIBUTE, hreflang);
        link.setAttribute(LINK_HREF_ATTRIBUTE, href);
    }
};

export const Routes = (props: RoutesProps): React.ReactElement => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    /* MAP COMPONENTS TO ROUTES */
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

    /* UPDATE CANONICAL & ALTERNATE URL ON PAGE CHANGE */
    React.useEffect(() => {
        createCanonicalLink();

        const languages = language?.languages;
        languages?.forEach(item => {
            const url = window.location.href.replace(`/${language.id}`, `/${item.id}`);
            createAlternateLink(url, item.id);
            if (item.isDefault) {
                createAlternateLink(url, "x-default");
            }
        });
    }, [language?.id, language?.languages, window.location.href]);

    return buffer.length > 0 ? <>{buffer}</> : <></>;
};
