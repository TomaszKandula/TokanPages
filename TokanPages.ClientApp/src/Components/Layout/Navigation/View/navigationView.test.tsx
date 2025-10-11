import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { NavigationView } from "../../Navigation/View/navigationView";
import { ItemDto } from "../../../../Api/Models";
import { ApplicationLanguageState } from "../../../../Store/States";

describe("test component: NavigationView", () => {
    it("should render correctly '<NavigationView />' when content is loaded.", () => {
        const items: ItemDto = {
            id: "79a6c65d-08b8-479b-9507-97feb05e30c2",
            type: "item",
            value: "Home",
            link: "/",
            icon: "Home",
            enabled: true,
        };

        const languages: ApplicationLanguageState = {
            id: "en",
            flagImageDir: "flags",
            flagImageType: "webp",
            languages: [
                {
                    id: "en",
                    iso: "en-GB",
                    name: "English",
                    isDefault: true,
                },
                {
                    id: "pl",
                    iso: "pl-PL",
                    name: "Polski",
                    isDefault: false,
                },
            ],
            warnings: [],
            pages: [
                {
                    language: "en",
                    pages: [
                        {
                            title: "software developer",
                            page: "MainPage",
                            description: "software development",
                        },
                    ],
                },
            ],
            meta: [
                {
                    language: "en",
                    facebook: {
                        title: "",
                        description: "",
                        imageAlt: "",
                    },
                    twitter: {
                        title: "",
                        description: "",
                    },
                },
            ],
            errorBoundary: [
                {
                    language: "en",
                    title: "Critical Error",
                    subtitle: "Something went wrong...",
                    text: "Contact the site's administrator or support for assistance.",
                    linkHref: "mailto:admin@tomkandula.com",
                    linkText: "IT support",
                    footer: "tomkandula.com",
                },
            ],
        };

        const html = render(
            <NavigationView
                isLoading={false}
                isAnonymous={false}
                isMenuOpen={false}
                isBottomSheetOpen={false}
                media={{
                    hasPortrait: true,
                    hasLandscape: false,
                    isDesktop: true,
                    isMobile: false,
                    isTablet: false,
                    width: 430,
                    height: 932,
                }}
                triggerSideMenu={jest.fn()}
                triggerBottomSheet={jest.fn()}
                infoHandler={jest.fn()}
                avatarName=""
                avatarSource=""
                aliasName=""
                navigation={{
                    language: "en",
                    logo: "logo.webp",
                    languageMenu: {
                        caption: "Select language",
                    },
                    signup: {
                        caption: "Create a new account",
                        link: "/en/signup",
                    },
                    userInfo: {
                        textUserAlias: "tokan",
                        textRegistered: "2025-07-07",
                        textRoles: "",
                        textPermissions: "",
                        textButton: "OK",
                    },
                    menu: {
                        image: "background.webp",
                        items: [items],
                    },
                }}
                languages={languages}
                languageId="en"
                languageFlagDir="flags"
                languageFlagType="webp"
                languagePickHandler={jest.fn()}
                languageMenuHandler={jest.fn()}
                isLanguageMenuOpen={false}
                backPathHandler={jest.fn()}
            />,
            { wrapper: BrowserRouter }
        );

        expect(html).toMatchSnapshot();
    });
});
